using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Data;

namespace IntLearnShared.Core.LearningTasks
{
    public enum VariableType
    {
        WITH_VALUE,
        RANGED
    }

    public struct VariableDescription
    {
        public VariableType Type { get; set; }

        public double Value { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public string Description { get; set; }
    }

    public class TaskWithAnswer : LearningTask
    {
        public TaskWithAnswer()
        {
            IsRandomizable = false;
        }

        public Dictionary<string, string> Answer { get; set; }
        public Dictionary<string, VariableDescription> Variables { get; set; }

        public override bool CheckAnswer(string answer)
        {
            var AnswerDict = new Dictionary<string, double>();

            string[] Vars = answer.Split(',');
            foreach (var Var in Vars)
            {
                string[] VarAnswer = Var.Split('=');

                AnswerDict.Add(VarAnswer[0].Trim().ToLowerInvariant(), double.Parse(VarAnswer[1].Trim()));
            }

            var CurrentAnswer = GetCurrentAnswer();

            return CurrentAnswer.Count == AnswerDict.Count && !CurrentAnswer.Except(AnswerDict).Any();
        }

        public override string RenderTaskDescription()
        {
            if (Variables == null || Answer == null)
            {
                ParseTaskText();
                if (IsRandomizable)
                    Randomize();
            }

            string Result = TaskText;
            string Text = TaskText;
            int BeginIndex = Text.IndexOf('{');
            int EndIndex = Text.IndexOf('}');

            while (BeginIndex != -1 && EndIndex != -1 && Text.Length != 0)
            {
                string Variable = Text.Substring(BeginIndex + 1, EndIndex - BeginIndex - 1);
                string[] Infos = Variable.Split(',');

                string[] TypeInfo = Infos[0].Split(':');
                string Type = TypeInfo[0].Trim();
                string Key = TypeInfo[1].Trim();

                string VariableReplace = Key;
                if (Type == "C")
                {
                    VariableReplace = Key + " = " + Variables[Key].Value;

                    if (Infos.Length == 3)
                        VariableReplace += " " + Infos[2].Trim();
                }

                Result = Result.Replace("{" + Variable + "}", VariableReplace);

                Text = Text.Substring(EndIndex + 1);
                BeginIndex = Text.IndexOf('{');
                EndIndex = Text.IndexOf('}');
            }

            return Result;
        }

        public override XmlElement SerializeToXml(XmlDocument xmlDocument)
        {
            ParseTaskText();

            XmlElement taskElement = xmlDocument.CreateElement(string.Empty, "Element", string.Empty);
            //xmlDocument.AppendChild(taskElement);
            taskElement.SetAttribute("Name", Name);
            taskElement.SetAttribute("Description", Description);
            taskElement.SetAttribute("Metadata-type", GetType().Name);
            taskElement.SetAttribute("IsRandomizable", IsRandomizable.ToString());


            XmlElement taskTextElement = xmlDocument.CreateElement(string.Empty, "TaskText", string.Empty);
            taskElement.AppendChild(taskTextElement);
            taskTextElement.InnerText = TaskText;

            XmlElement taskVariablesElement = xmlDocument.CreateElement(string.Empty, "TaskVariables", string.Empty);
            foreach(var Var in Variables)
            {
                XmlElement taskVarElement = xmlDocument.CreateElement(string.Empty, "Var", string.Empty);
                taskVarElement.SetAttribute("Name", Var.Key);
                taskVarElement.SetAttribute("Description", Var.Value.Description);

                if(Var.Value.Type == VariableType.WITH_VALUE)
                {
                    taskVarElement.SetAttribute("Value", Var.Value.Value.ToString());
                }
                else if(Var.Value.Type == VariableType.RANGED)
                {
                    taskVarElement.SetAttribute("MinValue", Var.Value.MinValue.ToString());
                    taskVarElement.SetAttribute("MaxValue", Var.Value.MaxValue.ToString());
                }

                taskVariablesElement.AppendChild(taskVarElement);
            }

            taskElement.AppendChild(taskVariablesElement);

            XmlElement taskAnswerElement = xmlDocument.CreateElement(string.Empty, "TaskAnswer", string.Empty);

            foreach (var Var in Answer)
            {
                XmlElement taskAnswerPart = xmlDocument.CreateElement(string.Empty, "Var", string.Empty);
                taskAnswerPart.SetAttribute("Name", Var.Key);
                taskAnswerPart.InnerText = Var.Value;

                taskAnswerElement.AppendChild(taskAnswerPart);
            }

            //taskAnswerElement.InnerText = CorrectAnswer;

            taskElement.AppendChild(taskAnswerElement);

            // Image to Xml
            if (Picture != null)
            {
                MemoryStream ms = new MemoryStream();
                Picture.Save(ms, Picture.RawFormat);
                byte[] rawData = ms.ToArray();

                XmlCDataSection cdata = xmlDocument.CreateCDataSection(Encoding.ASCII.GetString(rawData));

                taskElement.AppendChild(cdata);
            }

            return taskElement;
        }

        public override void Randomize()
        {
            List<string> Keys = new List<string>(Variables.Keys);
            foreach(var Key in Keys)
            {
                VariableDescription Desc = Variables[Key];
                if (Desc.Type == VariableType.RANGED)
                {
                    Desc.Value = (int)new Random().Next((int)Desc.MinValue, (int)Desc.MaxValue);
                }

                Variables[Key] = Desc;
            }
        }

        protected override void DeserializeFromXml(XmlElement xmlData)
        {
            base.DeserializeFromXml(xmlData);
            ParseTaskText();

            //XmlElement AnswerXml = (XmlElement)xmlData.GetElementsByTagName("Answer")[0];

            //Answer = new Dictionary<string, string>();
            //foreach(XmlElement Var in AnswerXml)
            //{
            //    if(Var.Name == "Var")
            //    {
            //        Answer.Add(Var.GetAttribute("Name"), Var.InnerText);
            //    }
            //}

            //XmlElement taskText = (XmlElement)xmlData.GetElementsByTagName("TaskText")[0];

            //foreach (var child in taskText)
            //{
            //    XmlElement element = child as XmlElement;
            //    if(element == null)
            //        continue;

            //    if (element.Name == "TaskText")
            //    {
            //        TaskText = element.InnerText;
            //    }

            //    if (element.Name == "TaskAnswer")
            //    {
            //        CorrectAnswer = element.InnerText;
            //    }
            //}
            // TODO Picture!
        }

        private void ParseTaskText()
        {
            Variables = new Dictionary<string, VariableDescription>();
            Answer = new Dictionary<string, string>();

            string Text = TaskText;
            int BeginIndex = Text.IndexOf('{');
            int EndIndex = Text.IndexOf('}');

            while (BeginIndex != -1 && EndIndex != -1 && Text.Length != 0)
            {
                string Variable = Text.Substring(BeginIndex + 1, EndIndex - BeginIndex - 1);
                string[] Infos = Variable.Split(',');

                string[] TypeInfo = Infos[0].Split(':');
                string Type = TypeInfo[0].Trim();
                string Key = TypeInfo[1].Trim();
                if (Type == "C")
                {
                    var Desc = new VariableDescription();

                    TypeInfo = Infos[1].Split(':');
                    Type = TypeInfo[0].Trim();
                    if (Type == "V")
                    {
                        Desc.Type = VariableType.WITH_VALUE;
                        Desc.Value = double.Parse(TypeInfo[1].Trim());
                    }
                    else if(Type == "R")
                    {
                        IsRandomizable = true;
                        Desc.Type = VariableType.RANGED;
                        Desc.MinValue = double.Parse(TypeInfo[1].Trim());
                        Desc.MaxValue = double.Parse(TypeInfo[2].Trim());
                    }

                    if (Infos.Length == 3)
                    {
                        Desc.Description = Infos[2].Trim();
                    }

                    Variables.Add(Key, Desc);
                }
                else if(Type == "Q")
                {
                    Answer.Add(Key.ToLowerInvariant(), Infos[1].Trim());
                }


                Text = Text.Substring(EndIndex + 1);
                BeginIndex = Text.IndexOf('{');
                EndIndex = Text.IndexOf('}');
            }
        }

        public Dictionary<string, double> GetCurrentAnswer()
        {
            var CurrentAnswer = new Dictionary<string, double>();
            DataTable table = new DataTable();

            foreach (var AnsVar in Answer)
            {
                string formula = AnsVar.Value;
                foreach (var Var in Variables)
                {
                    formula = formula.Replace(Var.Key, Var.Value.Value.ToString());
                }

                CurrentAnswer.Add(AnsVar.Key, (double)table.Compute("Convert(" + formula + ", System.Double)", null));
            }

            return CurrentAnswer;
        }
    }
}
