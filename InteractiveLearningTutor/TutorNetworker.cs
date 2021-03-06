﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntLearnShared.Core;
using IntLearnShared.Networking;

namespace InteractiveLearningTutor
{
    class TutorNetworker
    {
        private Timer _timer;
        private TaskManager _taskManager;

        public TutorNetworker(TaskManager manager)
        {
            _taskManager = manager;

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += NetworkReadTimer_Tick;
            _timer.Start();
        }

        public void StartListening()
        {
            NetworkHelper.GetInstance().StartListener();
        }

        public void StopListening()
        {
            NetworkHelper.GetInstance().StopListener();
        }

        private void NetworkReadTimer_Tick(object sender, EventArgs e)
        {          

            NetworkHelper net = NetworkHelper.GetInstance();

            if (net.PackageQueueCount() > 0)
            {
                Debug.WriteLine($"Checking net queue. {net.PackageQueueCount()} commands received");
                NetPackage package = net.PeekPackage();
                ProcessNetworkCommand(package.NetCommand, package.Sender);
            }
            else
            {
                Debug.WriteLine("Checking net queue. No commands received");
            }
        }

        private void ProcessNetworkCommand(NetCommand cmd, IPAddress sender)
        {
            if (cmd.CmdType == NetCommand.CommandType.SeekServer)
            {
                NetworkHelper.GetInstance().PopPackage();
                NetworkHelper.GetInstance().SendCommand(NetCommand.SeekWhoIsPreset, sender);

                Debug.WriteLine("Seek server response to " + sender.ToString());
            }

            if (cmd.CmdType == NetCommand.CommandType.TaskListRequest)
            {
                NetworkHelper.GetInstance().PopPackage();
                //Category root = PrebuiltTaskCreator.GetDebugTree();

                string serialized = Serializer.Serialize(_taskManager.Root);

                NetCommand response = new NetCommand(NetCommand.TaskListResponseHeader, serialized);
                NetworkHelper.GetInstance().SendCommand(response, sender);

                Debug.WriteLine("Task list response to " + sender.ToString());
            }
        }
    }
}
