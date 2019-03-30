using System;
using System.IO;
using IntLearnShared.Core;
using IntLearnShared.Core.LearningTasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OwcUnitTest
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void TestSerialize()
        {
            Category root = GetTestTree();
            string xml = Serializer.Serialize(root);

            Category deserialized = Serializer.Deserialize(xml);

            Assert.IsTrue(AreTreesEqual(root, deserialized));
        }

        private bool AreTreesEqual(Category root1, Category root2)
        {
            // Comparing child base elemets

            if (root1.Count != root2.Count)
                return false;
            

            for(int i = 0; i < root1.Count; i++)
            {
                if (root1[i].Name != root2[i].Name)
                    return false;
                if (root1[i].Description != root2[i].Description)
                    return false;
                
                if(root1[i].GetType() != root2[i].GetType())                
                    return false;
                
                if(root1[i] is Category cat1)
                {
                    bool equal = AreTreesEqual(cat1, (Category)root2[i]);
                    if (!equal)
                        return false;
                }

                if(root1[i] is LearningTask task1)
                {
                    if (task1.TaskText != ((LearningTask)root2[i]).TaskText)
                        return false;
                }               
            }

            return true;
        }

        private static Category GetTestTree()
        {
            // 1) Создаём объект класса категория, заполняем его поля name и description
            // Placeholder
            Category rootCategory = new Category();
            rootCategory.Name = "ROOT";
            rootCategory.Description = "Root category should not be displayed to user";

            // 2) Создаем еще одну категорию (будущую подкатегорию), аналогично
            // cat1 CATEGORY
            Category cat1 = new Category();
            cat1.Name = "Cat 1";
            cat1.Description = "Cat1 desc";

            // 3) Создаём задачу - объект класса learningTask. Заполняем его поля, создаем
            // в нем нужные методы. Каждая задача должна быть описана таким уникальным классом.
            // task1 TASK
            LearningTask task1 = new LearningTask();
            task1.Name = "Task 1";
            task1.Description = "task1 na opisaniye";
            task1.TaskText = "Какой-то текст задачи с двумя звёздочками";
            task1.Picture = null;

            // Связываем задачу (объект learningTask) с категорией. В категории лежит данная задача.
            cat1.Add(task1);

            // Связываем категории. cat1 лежит В rootCategory.
            rootCategory.Add(cat1);

            return rootCategory;
        }
    }
}
