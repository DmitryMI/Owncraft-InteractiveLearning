using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace OwcLearningShared
{
    public class NetCommand
    {
        public static int ClientPort = 2222;
        public static int ServerPort = 2223;


        private XmlDocument _xmlDocument;

        public enum CommandType
        {
            SeekServer,
            ServerWhois,
            TaskListRequest,
            TaskListResponse,
            Other
        }

        private static string RemoveEofs(string str)
        {
            int i = 0;
            while (str[i] != '\0')
            {
                i++;
            }

            return str.Substring(0, i);
        }

        public static IPAddress GetSelfIp()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        public static NetCommand Parse(byte[] packageData)
        {
            NetCommand cmd = new NetCommand();
            string xmlString = Encoding.UTF8.GetString(packageData);

            xmlString = RemoveEofs(xmlString);

            XmlReader reader = XmlReader.Create(new StringReader(xmlString));
            cmd._xmlDocument = new XmlDocument();
            cmd._xmlDocument.Load(reader);
            reader.Close();

            return cmd;
        }

        public static NetCommand Parse(string xmlString)
        {
            NetCommand cmd = new NetCommand();
            XmlReader reader = XmlReader.Create(new StringReader(xmlString));
            cmd._xmlDocument = new XmlDocument();
            cmd._xmlDocument.Load(reader);
            reader.Close();

            return cmd;
        }

        public NetCommand(string header, string data)
        {
            _xmlDocument = new XmlDocument();

            XmlDeclaration xmlDeclaration = _xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = _xmlDocument.DocumentElement;
            _xmlDocument.InsertBefore(xmlDeclaration, root);

            XmlElement commandElement = _xmlDocument.CreateElement(string.Empty, "command", string.Empty);
            _xmlDocument.AppendChild(commandElement);

            commandElement.SetAttribute("header", header);
            commandElement.SetAttribute("data", data);

            IPAddress local = GetSelfIp();
            commandElement.SetAttribute("sender", local.ToString());
        }

        private NetCommand()
        {
            
        }
        

        public string GetCommandHeader()
        {
            XmlElement root = (XmlElement)_xmlDocument.GetElementsByTagName("command")[0];
            string headerAttribute = root.GetAttribute("header");
            return headerAttribute;
        }

        public string GetCommandData()
        {
            XmlElement root = (XmlElement)_xmlDocument.GetElementsByTagName("command")[0];
            string headerAttribute = root.GetAttribute("data");
            return headerAttribute;
        }

        public IPAddress GetSenderIp()
        {
            XmlElement root = (XmlElement)_xmlDocument.GetElementsByTagName("command")[0];
            string senderAttribute = root.GetAttribute("sender");
            return IPAddress.Parse(senderAttribute);
        }

        public override string ToString()
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            _xmlDocument.WriteTo(xmlTextWriter);

            return stringWriter.ToString();
        }

        public byte[] GetBytes()
        {
            string xmlString = ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            return bytes;
        }

        public CommandType CmdType
        {
            get
            {
                if (GetCommandHeader() == "seek-server")
                    return CommandType.SeekServer;
                if (GetCommandHeader() == "seek-whois")
                    return CommandType.ServerWhois;
                if (GetCommandHeader() == "task-list-request")
                    return CommandType.TaskListRequest;
                if (GetCommandHeader() == "task-list-response")
                    return CommandType.TaskListResponse;
                return CommandType.Other;
            } 
        }

        
    }
}
