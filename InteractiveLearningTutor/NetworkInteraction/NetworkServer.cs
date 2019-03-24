using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InteractiveLearningTutor.NetworkInteraction
{
    class NetworkServer
    {
        #region Singlton

        private static NetworkServer _instance;

        public static NetworkServer GetInstance()
        {
            if(_instance == null)
                _instance = new NetworkServer();
            return _instance;
        }

        #endregion
        
        private Thread _mclListenerThread;
        private bool _mclListenerThreadShouldStop;

        private void McListenerCallback(byte[] data)
        {
        }
        
        private NetworkServer()
        {
        }

        public void StartListening()
        {
            _mclListenerThread = new Thread(MulticastListener);
            _mclListenerThread.Start();
        }

        public void StopListening()
        {
            _mclListenerThread.Abort();
            _mclListenerThread = null;
        }

        private void MulticastListener()
        {
            UdpClient udpClient = new UdpClient();

            IPAddress multicastIp = IPAddress.Parse("239.0.0.222");
            udpClient.JoinMulticastGroup(multicastIp);
            IPEndPoint remoteEndPoint = new IPEndPoint(multicastIp, 2222);

            while (true)
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);

                // TODO Parse data
            }
        }
    }
}
