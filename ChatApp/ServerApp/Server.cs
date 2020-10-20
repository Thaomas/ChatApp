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
        private static Dictionary<string, TcpClient> connectedUsers;

        public Server(int port)
        {
            _portNumber = port;



            users = new Dictionary<string, string>();


            connectedUsers = new Dictionary<string, TcpClient>();
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


            _listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private void LogMessage(string message, [CallerMemberName] string callername = "")
        {
            Console.WriteLine("[{0}] - Thread-{1}- {2}",
                    callername, Thread.CurrentThread.ManagedThreadId, message);
        }

        public static bool IsConnected(string username)
        {
            return connectedUsers.ContainsKey(username);
        }

        public static bool RegisterClient(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                users.Add(username, password);
                return true;
            }
            else
            return false;
        }
    }
}
