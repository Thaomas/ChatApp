﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

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



            users = LoadUsers();
            _chatLog = LoadChatLog();



            connectedUsers = new Dictionary<Client, string>();
            tempConn = new List<Client>();
            IPAddress ipAddres = IPAddress.Loopback;
            this._listener = new TcpListener(ipAddres, _portNumber);
            this._listener.Start();
            this._listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);

            LogMessage("Server is running");
            LogMessage($"Listening on {IPAddress.Loopback.ToString()}:{_portNumber}");

            Timer timer = new Timer((e) =>
            {
                SaveUsers();
                SaveChatLog();
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

        }

        private void SaveChatLog()
        {
            List<string> log = GetChatLog();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataStorage\ChatLog.txt");

            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
            {
                foreach (string message in log)
                {
                    writer.WriteLine(message);
                }
            }
            Console.WriteLine("ChatLog saved");
        }

        private void SaveUsers()
        {
            Dictionary<string, string> listDict;
            lock (users)
            {
            listDict = new Dictionary<string, string>(users);
            }
            string path = Environment.CurrentDirectory + @"\DataStorage\Users.txt";

            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
            {
                foreach (KeyValuePair<string, string> kvPair in listDict)
                {
                    writer.WriteLine($"{kvPair.Key}|{kvPair.Value}");
                }
            }
            Console.WriteLine("Users saved");
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


        private List<string> LoadChatLog()
        {
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataStorage\ChatLog.txt");
                var log = File.ReadAllLines(path);
                return new List<string>(log);
            }
            catch
            {
                Console.WriteLine("Error in loading ChatLog");
                return new List<string>();
            }
        }

        private Dictionary<string, string> LoadUsers()
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataStorage\Users.txt");
                var log = File.ReadAllLines(path);
                foreach (string line in log)
                {
                    int index = line.IndexOf('|');
                    users.Add(line.Substring(0, index), line.Substring(index+1));
                }
            }
            catch(Exception e)
            {
                
                Console.WriteLine("Error in loading Users");
                Console.WriteLine(e.ToString());
            }
            return users;
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
