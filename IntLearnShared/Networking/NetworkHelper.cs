using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntLearnShared.Networking
{
    public class NetworkHelper
    {
        #region Singleton
        
        private static NetworkHelper _instance;

        public static NetworkHelper GetInstance()
        {
            if(_instance == null)
                _instance = new NetworkHelper();
            return new NetworkHelper();
        }

        #endregion

        public const string MulticastGroup = "224.5.6.7";
        public const int NetPackageMaxLength = 1024;

        public static int ClientPort = 25565;

        public delegate void NetworkCallback(NetCommand command, IPAddress sender);

        private event NetworkCallback _networkCallback;
        private Thread _listenerThread;

        public static IPAddress GetSelfIp()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        public NetworkCallback NetworkCallbackEvent
        {
            get => _networkCallback;
            set => _networkCallback = value;
        }

        public void SendCommand(NetCommand command, IPAddress destination)
        {
            // WORKS!

            UdpClient udpclient = new UdpClient();

            IPEndPoint remoteep = new IPEndPoint(destination, ClientPort);

            byte[] data = command.GetBytes();

            udpclient.Send(data, data.Length, remoteep);
        }

        public void SendCommandMulticast(NetCommand command)
        {
            // Not actually multicast, lol. =(

            // Get all subnet's IPs and send one package to all of them.
            IPAddress localIp = GetSelfIp();
            string strIp = localIp.ToString();
            string subnetIp = strIp.Substring(0, strIp.LastIndexOf('.') + 1);
            for (int i = 0; i < 255; i++)
            {
                string clientIp = subnetIp + i.ToString();
                SendCommand(command, IPAddress.Parse(clientIp));
            }
        }

        private NetworkHelper()
        {

        }

        public void StartListener()
        {
            _listenerThread = new Thread(ListenerThread);
            _listenerThread.Start();
        }

        public void StopListener()
        {
            _listenerThread.Abort();
            _listenerThread = null;
        }

        private void ListenerThread()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Create IP endpoint
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, NetworkHelper.ClientPort);

            // Bind endpoint to the socket
            s.Bind(ipep);

            // Multicast IP-address
            IPAddress ip = IPAddress.Parse(MulticastGroup);

            // Add socket to the multicast group
            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));

            // Receive messages
            while (true)
            {
                byte[] data = new byte[NetPackageMaxLength];
                s.Receive(data);

                Debug.WriteLine("Network!");

                NetCommand cmd = NetCommand.Parse(data);

                IPAddress sender = null;

                try
                {
                    if (s.RemoteEndPoint is IPEndPoint remoteIpEndPoint)
                    {
                        sender = remoteIpEndPoint.Address;
                    }
                }
                catch (SocketException ex)
                {
                    
                }

                _networkCallback?.Invoke(cmd, sender);
            }
        }
    }
}
