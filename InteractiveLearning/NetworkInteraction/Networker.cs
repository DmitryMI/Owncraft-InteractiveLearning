using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using InteractiveLearning.Core;
using NetworkShared;

namespace InteractiveLearning.NetworkInteraction
{
    class Networker
    {

        #region Singleton
        // Singleton --------------------------
        private static Networker _instance;

        public static Networker GetInstance()
        {
            if(_instance == null)
                _instance = new Networker();
            return _instance;
        }

        //-------------------------------------
        #endregion


        public delegate void TaskListReadingCallback(Category root);

        private TaskListReadingCallback _readingCallback;

        private Networker()
        {
            
        }

        // TODO Listen for direct packages

        private void FindServer()
        {
            UdpClient udpClient = new UdpClient();

            IPAddress multicastIp = IPAddress.Parse("239.0.0.222");
            udpClient.JoinMulticastGroup(multicastIp);
            IPEndPoint remoteEndPoint = new IPEndPoint(multicastIp, NetCommand.ClientPort);

            NetworkShared.NetCommand cmd = new NetCommand("seek-server", "TEST");

            byte[] data = cmd.GetBytes();

            udpClient.Send(data, data.Length, remoteEndPoint);
        }

        // Пример создания задачи
        public void RequestDataFromServer(TaskListReadingCallback callback)
        {
            FindServer();

            _readingCallback = callback;
            // TODO Retreiving task list from tutor's server

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

            // DO NOT REMOVE CALLBACK INVOKATION!
            _readingCallback(rootCategory);
        }
        
    }
}
