using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace IntLearnShared.Core.LearningTasks
{
    public class LearningTask : BaseElement
    {
        public string TaskText { get; set; }
        public Image Picture { get; set; }

        /// <summary>
        /// If true, task's parameters or any other data is volatile can be randomized.
        /// </summary>
        public bool IsRandomizable { get; set; }

        /// <summary>
        /// Should be called to check user's answer
        /// Must be overriden. Debug value is always true
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public virtual bool CheckAnswer(string answer)
        {
            return true;
        }

        /// <summary>
        /// Randomizes task's data. If IsRandomizable is false, should not be called
        /// </summary>
        public virtual void Randomize()
        {

        }
        
        /// <summary>
        /// Tip: can be used in child classes
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
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
            TaskText = taskText.LastChild.InnerText;

            // TODO Picture!
        }

    }
}
