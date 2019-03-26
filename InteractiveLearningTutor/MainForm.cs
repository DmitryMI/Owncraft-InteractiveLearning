using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntLearnShared.Core;
using IntLearnShared.Networking;

namespace InteractiveLearningTutor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //NetworkHelper.GetInstance().NetworkEvent += ProcessNetworkCommand;
            NetworkHelper.GetInstance().StartListener();
        }

        private void NetworkReadTimer_Tick(object sender, EventArgs e)
        {
            NetworkHelper net = NetworkHelper.GetInstance();

            NetPackage package = net.PeekPackage();
            ProcessNetworkCommand(package.NetCommand, package.Sender);
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
                Category root = PrebuiltTaskCreator.GetDebugTree();
                string serialized = Serializer.Serialize(root);

                NetCommand response = new NetCommand(NetCommand.TaskListResponseHeader, serialized);
                NetworkHelper.GetInstance().SendCommand(response, sender);

                Debug.WriteLine("Task list response to " + sender.ToString());
            }
        }
    }
}
