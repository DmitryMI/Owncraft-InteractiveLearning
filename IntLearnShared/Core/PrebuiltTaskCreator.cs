using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntLearnShared.Core.LearningTasks;

namespace IntLearnShared.Core
{
    public class PrebuiltTaskCreator
    {
        public static Category GetPrebuiltTasksAux()
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

        public static Category GetPrebuiltTasks()
        {
            Category rootCategory = new Category();
            rootCategory.Name = "ROOT";
            rootCategory.Description = "Root category should not be displayed to user";

            Category ariphm = new Category();
            ariphm.Name = "Арифметика";
            ariphm.Description = "Задачи на проведение простых арифметических операций";

            SimpleSummEquation summEquation = new SimpleSummEquation
            {
                Name = "Сложение двух чисел от 0 до 20",
                Description = "В этой задаче будет предлажено сложить два некоторых небольших числа."
            };
            ariphm.Add(summEquation);

            rootCategory.Add(ariphm);

            return rootCategory;
        }

        public static Category GetDebugTree()
        {

            Category rootCategory = new Category
            {
                Name = "ROOT", Description = "Root category should not be displayed to user"
            };

            Category cat1 = new Category {Name = "cat1", Description = "cat1 opisanie"};

            Category cat12 = new Category {Name = "cat12", Description = "cat1 opisanie 2"};

            LearningTask task1 = new LearningTask
            {
                Name = "task1",
                Description = "task1 na opisaniye",
                TaskText = "Какой-то текст задачи с двумя звёздочками",
                Picture = null
            };

            LearningTask task12 = new LearningTask
            {
                Name = "task12",
                Description = "task1 na opisaniye 2",
                TaskText = "Какой-то текст задачи с двумя звёздочками 2",
                Picture = null
            };

            LearningTask task13 = new LearningTask
            {
                Name = "task13",
                Description = "task1 na opisaniye 3",
                TaskText = "Какой-то текст задачи с двумя звёздочками 3",
                Picture = null
            };

            TaskWithAnswer task2 = new TaskWithAnswer();
            task2.Name = "Токарь и ученик";
            task2.Description = "Задача на работу";
            task2.TaskText = "Токарь и его ученик вместе за смену выточили {C:D, V:130, деталей}. "
                + "Сколько деталей ({Q:T, D - D/(Ts*Tk+1)}) выточил токарь и сколько деталей ({Q:S, D/(Ts*Tk+1)}) выточил ученик "
                + ", если часть деталей, которую выточил токарь, уменьшенная в {C:Tk, V:3, раза}"
                + ", была равна деталям, которые выточил ученик, увеличенным в {C:Ts, V:4, раза}?";
            task2.Picture = null;

            TaskWithAnswer task3 = new TaskWithAnswer();
            task3.Name = "Умножение";
            task3.Description = "Задача на умножение";
            task3.TaskText = "Пусть {C:A, R:10:100} и {C:B, R:0:30}. Найти {Q:произведение, A*B} A и B.";
            task3.Picture = null;

            cat1.Add(task1);
            cat1.Add(task2);
            cat1.Add(task3);

            cat12.Add(task12);
            cat12.Add(task13);

            rootCategory.Add(cat1);
            rootCategory.Add(cat12);

            return rootCategory;
        }
    }
}
