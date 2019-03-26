using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace IntLearnShared.Core
{
    public class BaseElement
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Image Thumbnail { get; set; }

        public static BaseElement Deserialize(XmlElement xml)
        {
            string metadataType = xml.GetAttribute("Metadata-type");

            Type parentType = typeof(BaseElement);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();

            IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(parentType));

            foreach (Type type in subclasses)
            {
                if (type.Name == metadataType)
                {
                    BaseElement instance = (BaseElement)Activator.CreateInstance(type);
                    instance.DeserializeFromXml(xml);
                    return instance;
                }
            }

            return null;
        }

        protected virtual void DeserializeFromXml(XmlElement xml)
        {
            Name = xml.GetAttribute("Name");
            Description = xml.GetAttribute("Description");
        }


        public virtual XmlElement SerializeToXml(XmlDocument xmlDocument)
        {
            XmlElement baseElement = xmlDocument.CreateElement(String.Empty, "Element", String.Empty);
            //xmlDocument.AppendChild(baseElement);
            baseElement.SetAttribute("Name", Name);
            baseElement.SetAttribute("Description", Description);
            baseElement.SetAttribute("Metadata-type", GetType().Name);

            return baseElement;
        }
    }
}
