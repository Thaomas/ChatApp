﻿using System;
using System.Collections.Generic;
using System.IO;
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
        public Dictionary<string, string> users;
        private Dictionary<Client, string> connectedUsers;
        private List<string> _chatLog;
        private List<Client> tempConn;

        public Server(int port)
        {
            Console.WriteLine("Begin server");
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
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

        }

        private void SaveChatLog()
        {
            List<string> log = GetChatLog();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataStorage\ChatLog.txt");
            File.WriteAllLines(path, log);
        }

        private void SaveUsers()
        {
            Dictionary<string, string> listDict;
            lock (users)
            {
                listDict = new Dictionary<string, string>(users);
            }
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataStorage\Users.txt");
            List<string> userList = new List<string>();
            foreach (KeyValuePair<string, string> kvPair in listDict)
            {
                userList.Add($"{kvPair.Key}|{kvPair.Value}");
            }
            File.WriteAllLines(path, userList);
        }

        private void OnConnect(IAsyncResult result)
        {
            var client = this._listener.EndAcceptTcpClient(result);

            tempConn.Add(new Client(client, this));

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
                    users.Add(line.Substring(0, index), line.Substring(index + 1));
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error in loading Users");
                Console.WriteLine(e.ToString());
            }
            return users;
        }
        public List<string> GetChatLog()
        {
            return _chatLog;
        }

        public string RegisterClient(Client client, string username, string password)
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
                            client.Username = username;
                        }

                    return "OK";
                }
                else
                    return "ERROR";
            }
        }



        public string LoginClient(Client client, string username, string password)
        {
            lock (connectedUsers) lock (users)
                {
                    if (users.ContainsKey(username) && users[username].Equals(password))
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

        public void DisconnectClient(Client client)
        {
            connectedUsers.Remove(client);
            client.Username = "";

        }

        public void ChatMessage(string message)
        {
            string time = DateTime.Now.ToString();
            time += " | ";
            time += message;
            _chatLog.Add(time);
            foreach (KeyValuePair<Client, string> keyPair in connectedUsers)
            {
                if (!keyPair.Value.Equals(""))
                {
                    keyPair.Key.messageClient(time);
                    Console.WriteLine(time + " to " + keyPair.Value);
                }

            }
        }
    }
}
