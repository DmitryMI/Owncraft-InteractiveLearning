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

        delegate void ListenerAction();

        private NetCommand cachedCommand;
        private IPAddress cachedAddress;
        private bool _shouldStop;

        public void StartListener()
        {
            /*_listenerThread = new Thread(ListenerThread);
            _listenerThread.Start();*/
            _shouldStop = false;

            ListenerAction listener = ListenerThread;
            AsyncCallback callback = NetworkListenerCallback;
            IAsyncResult result = listener.BeginInvoke(callback, null);
        }

        private void NetworkListenerCallback(IAsyncResult ar)
        {
            _networkCallback?.Invoke(cachedCommand, cachedAddress);
            if(!_shouldStop)
                StartListener();
        }

        public void StopListener()
        {
            _shouldStop = false;
        }

        private void ListenerThread()
        {
            UdpClient receivingUdpClient = new UdpClient(ClientPort);
            
            try
            {
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, ClientPort);
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = receivingUdpClient.Receive(ref remoteIpEndPoint);

                cachedCommand = NetCommand.Parse(receiveBytes);
                cachedAddress = remoteIpEndPoint.Address;
            }
            catch (SocketException e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}
