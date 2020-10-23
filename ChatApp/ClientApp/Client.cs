using Newtonsoft.Json;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientApp
{

    public delegate void LoginCallback(bool status);
    public delegate void RegisterCallback(bool status);
    public delegate void ChatCallback(string sender, string message);
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[4];
        public string username;
        private bool loggedIn = false;
        private bool registered = false;

        public IPEndPoint RemoteEndPoint { get; internal set; }

        public event LoginCallback OnLogin;
        public event RegisterCallback OnRegister;
        public event ChatCallback OnChatReceived;

        public Client()
        {
            this.client = new TcpClient();
        }

        public void ConnectAsync(IPAddress iP, int port)
        {
            client.BeginConnect(iP, port, new AsyncCallback(Connect), null);
        }

        public void Connect(IAsyncResult ar)
        {
            this.client.EndConnect(ar);

            if (!this.loggedIn)
            {
                this.stream = client.GetStream();
            }
            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        public void SendLogin(string username, string password)
        {
            this.username = username;
            //Send username and password to check
            List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(
                new DataPacket<LoginPacket>()
            {
                type = "LOGIN",
                data = new LoginPacket()
                {
                    username = username,
                    password = password

                }
            })));
            // append the message length (in bytes)
            sendBuffer.InsertRange(0, BitConverter.GetBytes(sendBuffer.Count));

            // send the message
            this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
        }

        public void SendRegister(string username, string password)
        {
            this.username = username;
            //Send username and password to check
            List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(
                new DataPacket<RegisterPacket>()
            {
                type = "REGISTER",
                data = new RegisterPacket()
                {
                    username = username,
                    password = password

                }
            })));
            // append the message length (in bytes)
            sendBuffer.InsertRange(0, BitConverter.GetBytes(sendBuffer.Count));

            // send the message
            this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
        }

        private void ReceiveLengthInt(IAsyncResult ar)
        {
            int dataLength = BitConverter.ToInt32(this.buffer);

            // create data buffer
            this.buffer = new byte[dataLength];

            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(ReceiveData), null);
        }

        private void ReceiveData(IAsyncResult ar)
        {
            string data = System.Text.Encoding.ASCII.GetString(this.buffer);

            DataPacket dataPacket = JsonConvert.DeserializeObject<DataPacket>(data);
            handleData(dataPacket);

            this.buffer = new byte[4];
            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        public void SendChatMessage(string message)
        {
            if (this.loggedIn)
            {
                DataPacket<ChatPacket> dataPacket = new DataPacket<ChatPacket>()
                {
                    type = "CHAT",
                    data = new ChatPacket()
                    {
                        chatMessage = message
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, BitConverter.GetBytes(sendBuffer.Count));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        private void handleData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGINRESPONSE":
                    {
                        DataPacket<LoginResponse> d = data.GetData<LoginResponse>();
                        if (d.data.status == "OK")
                        {
                            this.loggedIn = true;
                            OnLogin?.Invoke(this.loggedIn);
                            Console.WriteLine("You are logged in!");
                        }
                        else if (d.data.status == ("ERROR"))
                        {
                            this.loggedIn = false;
                            OnLogin?.Invoke(this.loggedIn);
                            Console.WriteLine("Your username and/or password is not valid!");

                        }
                        break;
                    }
                case "REGISTERRESPONSE":
                    {
                        DataPacket<RegisterResponsePacket> d = data.GetData<RegisterResponsePacket>();
                        if (d.data.status == "OK")
                        {
                            this.registered = true;
                            OnRegister?.Invoke(this.registered);
                            Console.WriteLine("You are Registered");
                        }
                        else if (d.data.status == ("ERROR"))
                        {
                            this.registered = false;
                            OnRegister?.Invoke(this.registered);
                            Console.WriteLine("Your username is already taken");

                        }
                        break;
                    }
                case "CHAT":
                    {
                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();

                        OnChatReceived?.Invoke(d.sender, $"{d.sender}: {d.data.chatMessage}\r\n");
                        break;
                    }
                default:
                    Console.WriteLine("Type is not valid");
                    break;
            }

        }
    }

}
