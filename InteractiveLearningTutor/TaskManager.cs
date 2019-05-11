using IntLearnShared.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveLearningTutor
{
    class TaskManager
    {
        public const string DataFileName = "catalogue.xml";

        private Category _root;
        private Category _current;

        public Category Current { get => _current; set => _current = value; }

        public void Init()
        {
            if (!File.Exists(DataFileName))
            {
                LoadPrebuiltTasks();
                SaveToFile();
            }
            else
            {
                LoadFromFile();
            }

            _current = _root;
        }

        public void Refresh()
        {
            LoadFromFile();
        }

        public void SaveToFile()
        {
            string ser = Serializer.Serialize(_root);
            File.WriteAllBytes(DataFileName, Encoding.Unicode.GetBytes(ser));
        }

        private void LoadFromFile()
        {
            byte[] fileData = File.ReadAllBytes(DataFileName);
            string data = Encoding.Unicode.GetString(fileData);
            _root = Serializer.Deserialize(data);
        }

        private void LoadPrebuiltTasks()
        {                       
            Category root = PrebuiltTaskCreator.GetPrebuiltTasksAux();
            Category merge = Category.MergeTrees(PrebuiltTaskCreator.GetPrebuiltTasks(), root);

            _root = merge;
        }
    }
}
