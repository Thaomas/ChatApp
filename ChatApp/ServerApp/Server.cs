using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApp
{
    class Server
    {
        private int _portNumber;
        private TcpListener _listener;
        public static Dictionary<string, string> users;
        private static Dictionary<Client, string> connectedUsers;
        private static List<string> _chatLog;
        private static List<Client> tempConn;

        public Server(int port)
        {
            _portNumber = port;



            users = new Dictionary<string, string>();
            _chatLog = new List<string>();



            connectedUsers = new Dictionary<Client, string>();
            tempConn = new List<Client>();
            IPAddress ipAddres = IPAddress.Loopback;
            this._listener = new TcpListener(ipAddres, _portNumber);
            this._listener.Start();
            this._listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            LogMessage("Server is running");
            LogMessage($"Listening on {IPAddress.Loopback.ToString()}:{_portNumber}");
        }

        private void OnConnect(IAsyncResult result)
        {
            var client = this._listener.EndAcceptTcpClient(result);

            tempConn.Add(new Client(client));

            _listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private void LogMessage(string message, [CallerMemberName] string callername = "")
        {
            Console.WriteLine("[{0}] - Thread-{1}- {2}",
                    callername, Thread.CurrentThread.ManagedThreadId, message);
        }

        public static string RegisterClient(Client client, string username, string password)
        {
            lock (users)
            {
                if (!users.ContainsKey(username))
                {
                    users.Add(username, password);
                    lock (connectedUsers) lock (tempConn)
                    {
                        tempConn.Remove(client);

                        connectedUsers.Add(client, username);
                    }

                    return "OK";
                }
                else
                    return "ERROR";
            }
        }

        public static List<string> GetChatLog()
        {
            return _chatLog;
        }

        public static string LoginClient(Client client, string username, string password)
        {
            lock (connectedUsers) lock (users)
            {
                    if (users.ContainsKey(username) && Server.users[username].Equals(password))
                    {
                        if (!connectedUsers.ContainsValue(username) && !connectedUsers.ContainsKey(client))
                        {
                        connectedUsers.Add(client, username);
                            lock (tempConn)
                        tempConn.Remove(client);
                            client.Username = username;
                        return "OK";
                        }
                        else
                        {
                            return "ALREADYCONNECTED";
                        }
                    }
                    else
                    {
                        return "ERROR";
                    }
            
            }
        }

        public static void DisconnectClient(Client client)
        {
            connectedUsers.Remove(client);
            client.Username = "";

        }

        public static void ChatMessage(string message)
        {
            _chatLog.Add(message);
            foreach (KeyValuePair<Client, string> keyPair in connectedUsers)
            {
                if (!keyPair.Value.Equals(""))
                    keyPair.Key.messageClient(message);
            }
        }
    }
}
