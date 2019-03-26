using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntLearnShared.Core
{
    public class PrebuiltTaskCreator
    {
        public static Category GetPrebuiltTasks()
        {
            // 1) Создаём объект класса категория, заполняем его поля name и description
            // Placeholder
            Category rootCategory = new Category();
            rootCategory.Name = "ROOT";
            rootCategory.Description = "Root category should not be displayed to user";

            // 2) Создаем еще одну категорию (будущую подкатегорию), аналогично
            // NAHUY CATEGORY
            Category nahuy = new Category();
            nahuy.Name = "Nahuy";
            nahuy.Description = "Nahuy opisanie";

            // 3) Создаём задачу - объект класса learningTask. Заполняем его поля, создаем
            // в нем нужные методы. Каждая задача должна быть описана таким уникальным классом.
            // POHUY TASK
            LearningTask pohuy = new LearningTask();
            pohuy.Name = "Pohuy";
            pohuy.Description = "Pohuy na opisaniye";
            pohuy.TaskText = "Какой-то текст задачи с двумя звёздочками";
            pohuy.Picture = null;

            // Связываем задачу (объект learningTask) с категорией. В категории лежит данная задача.
            nahuy.Add(pohuy);

            // Связываем категории. Nahuy лежит В rootCategory.
            rootCategory.Add(nahuy);

            return rootCategory;
        }

        public static Category GetDebugTree()
        {

            Category rootCategory = new Category
            {
                Name = "ROOT", Description = "Root category should not be displayed to user"
            };

            Category nahuy = new Category {Name = "Nahuy", Description = "Nahuy opisanie"};

            Category nahuy2 = new Category {Name = "Nahuy2", Description = "Nahuy opisanie 2"};

            LearningTask pohuy = new LearningTask
            {
                Name = "Pohuy",
                Description = "Pohuy na opisaniye",
                TaskText = "Какой-то текст задачи с двумя звёздочками",
                Picture = null
            };

            LearningTask pohuy2 = new LearningTask
            {
                Name = "Pohuy2",
                Description = "Pohuy na opisaniye 2",
                TaskText = "Какой-то текст задачи с двумя звёздочками 2",
                Picture = null
            };

            LearningTask pohuy3 = new LearningTask
            {
                Name = "Pohuy3",
                Description = "Pohuy na opisaniye 3",
                TaskText = "Какой-то текст задачи с двумя звёздочками 3",
                Picture = null
            };

            nahuy.Add(pohuy);
            nahuy2.Add(pohuy2);
            nahuy2.Add(pohuy3);

            rootCategory.Add(nahuy);
            rootCategory.Add(nahuy2);

            return rootCategory;
        }
    }
}
