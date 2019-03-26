﻿using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using IntLearnShared.Networking;

namespace IntLearnShared.Core
{
    public class LearningTask : BaseElement
    {
        public delegate bool CheckFunction(object answer);

        public string TaskText { get; set; }
        public Image Picture { get; set; }
        

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
