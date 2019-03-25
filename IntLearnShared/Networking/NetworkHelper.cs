using System;
using System.Collections.Generic;
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

        public const string MulticastGroup = "239.0.0.222";
        public const int NetPackageMaxLength = 1024;

        public static int ClientPort = 2222;
        public static int ServerPort = 2223;

        public delegate void NetworkCallback(NetCommand command, IPAddress sender);

        private UdpClient _senderUdpClient;
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
            IPEndPoint remoteEndPoint = new IPEndPoint(destination, ServerPort);

            byte[] data = command.GetBytes();

            _senderUdpClient.Send(data, data.Length, remoteEndPoint);
        }

        public void SendCommandMulticast(NetCommand command)
        {
            IPAddress multicastIp = IPAddress.Parse(MulticastGroup);
            _senderUdpClient.JoinMulticastGroup(multicastIp);
            IPEndPoint remoteEndPoint = new IPEndPoint(multicastIp, ClientPort);

            byte[] data = command.GetBytes();

            _senderUdpClient.Send(data, data.Length, remoteEndPoint);
            _senderUdpClient.DropMulticastGroup(multicastIp);
        }

        private NetworkHelper()
        {
            _senderUdpClient = new UdpClient();
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
