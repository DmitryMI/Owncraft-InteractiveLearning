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

        public static int ClientPort = 25565;

        private Thread _listenerThread;

        private OwcQueue<NetPackage> _incomingQueue = new OwcQueue<NetPackage>();

        public NetPackage PopPackage()
        {
            lock (_incomingQueue)
            {
                return _incomingQueue.Pop();
            }
        }

        public NetPackage PeekPackage()
        {
            lock (_incomingQueue)
            {
                return _incomingQueue.Peek();
            }
        }

        public int PackageQueueCount()
        {
            lock (_incomingQueue)
            {
                return _incomingQueue.Count;
            }
        }

        public static IPAddress GetSelfIp()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
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
            UdpClient receivingUdpClient = new UdpClient(ClientPort);
            

            while (true)
            {
                try
                {
                    IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, ClientPort);
                    // Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref remoteIpEndPoint);

                    NetCommand cmd = NetCommand.Parse(receiveBytes);
                    IPAddress sender = remoteIpEndPoint.Address;
                    lock (_incomingQueue)
                    {
                        _incomingQueue.Push(new NetPackage(){NetCommand = cmd, Sender = sender});
                    }
                    
                }
                catch (SocketException e)
                {
                    Debug.WriteLine(e);
                    throw;
                }
                
            }
        }
    }
}
