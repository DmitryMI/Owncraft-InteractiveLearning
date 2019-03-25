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
            NetworkHelper.GetInstance().NetworkCallbackEvent += NetCallback;
            NetworkHelper.GetInstance().StartListener();

        }

        private void NetCallback(NetCommand cmd, IPAddress sender)
        {
            Debug.WriteLine("Package received in MainForm! Thread: " + Thread.CurrentThread.ManagedThreadId);

            MessageBox.Show("Network!");
        }
    }
}
