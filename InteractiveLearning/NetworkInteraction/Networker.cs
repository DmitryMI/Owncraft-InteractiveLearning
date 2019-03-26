using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using IntLearnShared.Core;
using IntLearnShared.Networking;
using Timer = System.Windows.Forms.Timer;

// ReSharper disable CommentTypo

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

        private const int TimerInterval = 100;
        public delegate void TaskListReadingCallback(Category root);

        public delegate void ErrorCallback(string message);

        private TaskListReadingCallback _readingCallback;
        private ErrorCallback _readingErrorCallback;
        private IPAddress _serverIp;
        private Queue<PlannedAction> _timerPlannedActions = new Queue<PlannedAction>();
        Timer timer = new Timer();

        private Networker()
        {
            timer.Tick += TimerTickHandler;
            timer.Interval = TimerInterval;
            timer.Start();
            NetworkHelper.GetInstance().StartListener();
        }

        private void NetworkPackageReceived(NetCommand cmd, IPAddress sender)
        {
            Debug.WriteLine("Package received in Networker! Thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        // TODO Listen for direct packages

        private void FindServer()
        {
            _timerPlannedActions.Enqueue(new PlannedAction(FindServerWaiter, 100));
            NetworkHelper.GetInstance().SendCommandMulticast(NetCommand.SeekServerPreset);
        }

        private void NetworkError(string message)
        {
            _readingErrorCallback(message);
            //MessageBox.Show(message);
        }

        private void FindServerWaiter(object data)
        {
            int repeats = (int) data;

            Debug.WriteLine("Repeats: " + repeats);

            NetworkHelper net = NetworkHelper.GetInstance();

            if (net.PackageQueueCount() != 0 && net.PeekPackage().NetCommand.CmdType != NetCommand.CommandType.ServerWhois)
            {
                var package = net.PopPackage();
                _serverIp = package.Sender;

                MessageBox.Show("Server IP: " + _serverIp.ToString());

                ReturnData();
            }
            else
            {
                repeats--;
                if (repeats == 0)
                    NetworkError("Timeout");
                else
                {
                    _timerPlannedActions.Enqueue(new PlannedAction(FindServerWaiter, repeats));
                }
            }
        }

        private void TimerTickHandler(object sender, EventArgs ea)
        {
            int additionalActions = 0;
            while (_timerPlannedActions.Count > additionalActions)
            {
                var plannedAction = _timerPlannedActions.Dequeue();
                int actionsLeft = _timerPlannedActions.Count;
                plannedAction.Invoke();
                Debug.WriteLine("Timer invoke!");
                additionalActions += _timerPlannedActions.Count - actionsLeft;
            }
        }

        private void ReturnData()
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

            // DO NOT REMOVE CALLBACK INVOKATION!
            _readingCallback(rootCategory);
        }

        // Пример создания задачи
        public void RequestDataFromServer(TaskListReadingCallback callback, ErrorCallback errorCallback)
        {
            _readingCallback = callback;
            _readingErrorCallback = errorCallback;

            FindServer();
        }
        
    }
}
