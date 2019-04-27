using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntLearnShared.Utils;

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

            return _instance;
        }

        #endregion

        public static int ClientPort = 25565;

        public delegate void NetworkCallback(NetCommand command, IPAddress sender);

        /// <summary>
        /// This event is raised when a new command is received from network.
        /// Handler must pop the last command from queue, if it processed it properly.
        /// If this command was not addressed to a handler, it must leave the queue untouched
        /// </summary>
        public event NetworkCallback NetworkEvent;

        private Thread _listenerThread;

        private OwcQueue<NetPackage> _incomingQueue = new OwcQueue<NetPackage>();

        /// <summary>
        /// Removes network package from queue and returns it
        /// </summary>
        /// <returns>Latest network package</returns>
        public NetPackage PopPackage()
        {
            lock (_incomingQueue)
            {
                return _incomingQueue.Pop();
            }
        }

        /// <summary>
        /// Returns latest network package but does not remove it from queue
        /// </summary>
        /// <returns>Latest network package</returns>
        public NetPackage PeekPackage()
        {
            lock (_incomingQueue)
            {
                return _incomingQueue.Peek();
            }
        }

        /// <summary>
        /// Count of packages, stored in queue
        /// </summary>
        /// <returns>Count of packages</returns>
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
            udpclient.ExclusiveAddressUse = false;

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
            int dotIndex = strIp.LastIndexOf('.');
            string subnetIp = strIp.Substring(0, dotIndex + 1);
            int currentIpNumber = Int32.Parse(strIp.Substring(dotIndex + 1, strIp.Length - dotIndex - 1));
            
            for (int i = 0; i < 255; i++)
            {
                if(currentIpNumber == i)
                    continue;
                
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
            UdpClient receivingUdpClient = new UdpClient();

            receivingUdpClient.ExclusiveAddressUse = false;
            receivingUdpClient.Connect(new IPEndPoint(IPAddress.None, ClientPort));

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
                        _incomingQueue.Push(new NetPackage() { NetCommand = cmd, Sender = sender });
                        Debug.WriteLine($"Queue count: {_incomingQueue.Count}");
                    }

                    Debug.WriteLine("Network data received!");
                    if (NetworkEvent != null)
                    {
                        Debug.WriteLine("Invoking event handlers: " + NetworkEvent.GetInvocationList().Length);
                    }
                    else
                    {
                        Debug.WriteLine($"No event handlers attached! Read from queue manually");
                    }

                    NetworkEvent?.Invoke(cmd, sender);
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
