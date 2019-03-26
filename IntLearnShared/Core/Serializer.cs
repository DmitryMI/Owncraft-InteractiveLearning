// ReSharper disable CommentTypo

/* Монахов Д.И.
 * Справка по сериализации каталога задач
 *
 * Сериализацией называется процесс преобразования структур данных программы в последовательность битов.
 *
 * Каталог задач представляет из себя дерево с корневым каталогом в вершине,
 * подкаталогами в качестве ветвей и задачами в качестве листьев.
 *
 * Класс Serializer является статическим и имеет два метода: Serialize и Deserialize
 * Первый получает на вход корневой каталог задач и возвращает Xml представление каталога задач.
 * Второй получает на вход Xml-строку и возвращает корневой каталог задач как вершину дерева
 * какталого (т.е. выполняет обратную задачу)
 *
 * Для работы сериализатора необходимо выполнение нескольких условий касательно структуры классов типа
 * BaseElement.
 * К классом этого типа относится BaseElement и его наследники: Category, LearningTask и т.д.
 *
 * Подразумевается, что наследник BaseElement любого уровня должен корректно работать с сериализатором.
 * Сериализатор использует два виртуальных метода класса BaseElement: SerializeToXml и DeserializeFromXml.
 * Изменять уровни доступности этих двух методов запрещено (DeserializeFromXml должен быть Protected).
 *
 * Метод SerializeToXml получает на вход XmlDocument и на его основе создает XmlElement, который отвечает
 * за сериализованное представление данного экземпляра BaseElement.
 * Имя XmlElement обязательно должно быть "Element"
 * XmlElement обязательно должен содержать три атрибута:
 * 1) Name
 * 2) Description
 * 3) Metadata-type = точное имя типа.
 * Для заполнения этих полей можно использовать метод SerializeToXml базового типа BaseElement.
 *
 * Метод DeserializeFromXml может содержать любой код на усмотрение пользователя.
 * Задача метода - заполнить необходимые поля данного экземпляра BaseElement
 *
 * Всем наследникам BaseElement запрещено использование конструкторов с параметрами.
 */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using IntLearnShared.Utils;

namespace IntLearnShared.Core
{
    public static class Serializer
    {
        public static string Serialize(Category rootCategory)
        {
            AssociativeTable<BaseElement, List<BaseElement>> linkTable = new AssociativeTable<BaseElement, List<BaseElement>>();

            FillLinkTable(linkTable, rootCategory);

            XmlDocument xmlDocument = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = xmlDocument.DocumentElement;
            xmlDocument.InsertBefore(xmlDeclaration, root);

            XmlElement rootElement = xmlDocument.CreateElement(string.Empty, "Owc-interactive-learning", string.Empty);
            xmlDocument.AppendChild(rootElement);

            int i = 0;

            foreach (var element in linkTable)
            {
                Debug.WriteLine("Processing: " + element.Val1.Name);
                XmlElement node = xmlDocument.CreateElement("node");
                node.SetAttribute("index", i.ToString());
                rootElement.AppendChild(node);

                XmlElement serialized = element.Val1.SerializeToXml(xmlDocument);
                node.AppendChild(serialized);

                foreach (var relation in element.Val2)
                {
                    XmlElement childIndexElement = xmlDocument.CreateElement("relation");
                    BaseElement child = relation;
                    int index = linkTable.GetKeyIndex(child);
                    childIndexElement.SetAttribute("index", index.ToString());
                    node.AppendChild(childIndexElement);
                }

                i++;
            }

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlDocument.PreserveWhitespace = true;
            
            xmlDocument.WriteTo(xmlTextWriter);

            return stringWriter.ToString();
        }

        private static void FillLinkTable(AssociativeTable<BaseElement, List<BaseElement>> linkTable, BaseElement current)
        {
            if (!linkTable.IsKeyInTable(current))
            {
                linkTable.Add(current, new List<BaseElement>());
            }

            if (current is Category category)
            {
                foreach (var child in category)
                {
                    linkTable[current].Add(child);
                    FillLinkTable(linkTable, child);
                }
            }

        }

        public static Category Deserialize(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            XmlElement rootElement = xmlDocument.DocumentElement;

            if(rootElement == null)
                throw new ArgumentException("Xml structure is corrupted or incorrect");

            var nodes = rootElement.ChildNodes;

            AssociativeTable<BaseElement, List<int>> table = new AssociativeTable<BaseElement, List<int>>();

            foreach (XmlElement node in nodes)
            {
                XmlElement element = (XmlElement)node.GetElementsByTagName("Element")[0];
                BaseElement baseElement = BaseElement.Deserialize(element);
                List<int> childIndexList = new List<int>();
                table.Add(baseElement, childIndexList);

                XmlNodeList relationXmlList = node.GetElementsByTagName("relation");

                foreach (XmlElement relationNode in relationXmlList)
                {
                    string attrVal = relationNode.GetAttribute("index");
                    int relativeIndex = Int32.Parse(attrVal);
                    childIndexList.Add(relativeIndex);
                }
            }

            // Restoring relation tree
            foreach (var pair in table)
            {
                BaseElement baseElement = pair.Val1;
                List<int> relations = pair.Val2;

                if (baseElement is Category category)
                {
                    foreach (var relationIndex in relations)
                    {
                        var relationPair = table[relationIndex];
                        category.Add(relationPair.Val1);
                    }
                }
            }

            return table[0].Val1 as Category;
        }
    }
}
