using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IntLearnShared.Core.LearningTasks
{
    public class TaskWithAnswer : LearningTask
    {
        public TaskWithAnswer()
        {
            IsRandomizable = false;
        }

        public string CorrectAnswer { get; set; }

        public override bool CheckAnswer(string answer)
        {
            return CorrectAnswer.Equals(answer);
        }

        public override XmlElement SerializeToXml(XmlDocument xmlDocument)
        {
            XmlElement taskElement = xmlDocument.CreateElement(string.Empty, "Element", string.Empty);
            //xmlDocument.AppendChild(taskElement);
            taskElement.SetAttribute("Name", Name);
            taskElement.SetAttribute("Description", Description);
            taskElement.SetAttribute("Metadata-type", GetType().Name);

            XmlElement taskTextElement = xmlDocument.CreateElement(string.Empty, "TaskText", string.Empty);
            taskElement.AppendChild(taskTextElement);
            taskTextElement.InnerText = TaskText;

            XmlElement taskAnswerElement = xmlDocument.CreateElement(string.Empty, "TaskAnswer", string.Empty);
            taskElement.AppendChild(taskAnswerElement);
            taskAnswerElement.InnerText = CorrectAnswer;

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

        protected override void DeserializeFromXml(XmlElement xmlData)
        {
            base.DeserializeFromXml(xmlData);
            XmlElement taskText = (XmlElement)xmlData.GetElementsByTagName("TaskText")[0];

            foreach (XmlElement child in taskText)
            {
                if (child.Name == "TaskText")
                {
                    TaskText = child.InnerText;
                }

                if (child.Name == "TaskAnswer")
                {
                    CorrectAnswer = child.InnerText;
                }
            }
            // TODO Picture!
        }
    }
}
