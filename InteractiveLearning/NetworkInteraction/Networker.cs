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

            //Debug.WriteLine("Repeats: " + repeats);

            NetworkHelper net = NetworkHelper.GetInstance();

            if (net.PackageQueueCount() != 0 && net.PeekPackage().NetCommand.CmdType == NetCommand.CommandType.ServerWhois)
            {
                var package = net.PopPackage();
                _serverIp = package.Sender;

                //MessageBox.Show("Server IP: " + _serverIp.ToString());
            }
            else
            {
                repeats--;
                if (repeats == 0)
                    NetworkError("Server ip seeking timeout");
                else
                {
                    _timerPlannedActions.Enqueue(new PlannedAction(FindServerWaiter, repeats));
                }
            }
        }

        private void SendRefreshRequestWaiter(object data)
        {
            int repeats = (int)data;

            //Debug.WriteLine("Repeats: " + repeats);

            NetworkHelper net = NetworkHelper.GetInstance();

            if (_serverIp != null)
            {
                net.SendCommand(NetCommand.TaskListRequestPreset, _serverIp);

                _timerPlannedActions.Enqueue(new PlannedAction(ReadTaskListWaiter, 100));
            }
            else
            {
                repeats--;
                if (repeats == 0)
                    NetworkError("Waiting for server ip failed. I don't know who is server.");
                else
                {
                    _timerPlannedActions.Enqueue(new PlannedAction(SendRefreshRequestWaiter, repeats));
                }
            }
        }

        private void ReadTaskListWaiter(object data)
        {
            int repeats = (int)data;

            //Debug.WriteLine("Repeats: " + repeats);

            NetworkHelper net = NetworkHelper.GetInstance();

            if (net.PackageQueueCount() != 0 && net.PeekPackage().NetCommand.CmdType == NetCommand.CommandType.TaskListResponse)
            {
                var package = net.PopPackage();

                //MessageBox.Show("Task list delivered!");

                ReturnData(package.NetCommand);
            }
            else
            {
                repeats--;
                if (repeats == 0)
                    NetworkError("Reading task lis timeout");
                else
                {
                    _timerPlannedActions.Enqueue(new PlannedAction(ReadTaskListWaiter, repeats));
                }
            }
        }

        private void TimerTickHandler(object sender, EventArgs ea)
        {
            int additionalActions = 0;
            while (_timerPlannedActions.Count > additionalActions)
            {
                if(_timerPlannedActions.Count == 0)
                    break;
                
                var plannedAction = _timerPlannedActions.Dequeue();
                int actionsLeft = _timerPlannedActions.Count;
                plannedAction.Invoke();
                //Debug.WriteLine("Timer invoke!");
                additionalActions += _timerPlannedActions.Count - actionsLeft;
            }
        }

        private void ReturnData(NetCommand cmd)
        {
            string data = cmd.GetCommandData();
            Category root = Serializer.Deserialize(data);

            _readingCallback(root);
        }

        // Пример создания задачи
        public void RequestDataFromServer(TaskListReadingCallback callback, ErrorCallback errorCallback)
        {
            _readingCallback = callback;
            _readingErrorCallback = errorCallback;

            FindServer();
            _timerPlannedActions.Enqueue(new PlannedAction(SendRefreshRequestWaiter, 100));
        }
        
    }
}
