using IntLearnShared.Networking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OwcUnitTest
{
    [TestClass]
    public class NetworkHelperTest
    {
        bool _receivedFlag = false;
        bool _okFlag = false;

        [TestMethod]
        public void TestNetworkHelperSend()
        {
            Thread t = new Thread(SendPackage);
            t.Start();

            Thread tReceiver = new Thread(ReceiveThread);
            tReceiver.Start();

            tReceiver.Join();

            Assert.IsTrue(_receivedFlag);
            Assert.IsTrue(_okFlag);
        }

        private void ReceiveThread()
        {
            NetworkHelper net = NetworkHelper.GetInstance();

            Debug.WriteLine("Listener started!");

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(100);

                Debug.WriteLine("Check!");

                if (net.PackageQueueCount() > 0)
                {
                    NetPackage received = net.PopPackage();

                    NetCommand cmd = received.NetCommand;

                    _receivedFlag = true;

                    if (cmd.GetCommandHeader() == "test" && cmd.GetCommandData() == "test_data")
                        _okFlag = true; ;
                }
            }

            Debug.WriteLine("Timeout fail!");
        }

        private void SendPackage()
        {
            Thread.Sleep(100);
            NetworkHelper net = NetworkHelper.GetInstance();
            NetCommand cmd = new NetCommand("test", "test_data");
            net.SendCommand(cmd, NetworkHelper.GetSelfIp());

            Debug.WriteLine("Test command sent!");
        }

        
    }
}
