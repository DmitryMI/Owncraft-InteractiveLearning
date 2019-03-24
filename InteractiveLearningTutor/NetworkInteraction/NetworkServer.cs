using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkShared;

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
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Create IP endpoint
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, NetCommand.ClientPort);

            // Bind endpoint to the socket
            s.Bind(ipep);

            // Multicast IP-address
            IPAddress ip = IPAddress.Parse("239.0.0.222");

            // Add socket to the multicast group
            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));

            // Receive messages
            while (true)
            {
                byte[] data = new byte[1024];
                s.Receive(data);
                NetworkShared.NetCommand cmd = NetCommand.Parse(data);

                McListenerProcess(cmd);
            }
        }

        private void McListenerProcess(NetCommand cmd)
        {
            switch (cmd.CmdType)
            {
                case NetCommand.CommandType.SeekServer:
                    ServerRespondWhois(cmd.GetSenderIp());
                    break;
                case NetCommand.CommandType.TaskListRequest:
                    break;
                case NetCommand.CommandType.Other:
                    break;
            }
        }

        private void ServerRespondWhois(IPAddress sender)
        {
            UdpClient udpClient = new UdpClient();
            
            IPEndPoint remoteEndPoint = new IPEndPoint(sender, 2222);

            NetworkShared.NetCommand cmd = new NetCommand("server-whois", NetCommand.GetSelfIp().ToString());

            byte[] data = cmd.GetBytes();

            udpClient.Send(data, data.Length, remoteEndPoint);
        }
    }
}
