using Newtonsoft.Json;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientApp
{

    public delegate void LoginCallback(bool status, List<string> chatlog);
    public delegate void RegisterCallback(bool status);
    public delegate void ChatCallback(string message);
    public delegate void ConnectionLostCallback();
    public class Client
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private byte[] _buffer = new byte[4];
        private bool _loggedIn = false;
        private bool _registered = false;

        public IPEndPoint RemoteEndPoint { get; internal set; }

        public event LoginCallback OnLogin;
        public event RegisterCallback OnRegister;
        public event ChatCallback OnChatReceived;
        public event ConnectionLostCallback OnConnectionLost;

        public Client()
        {
            this._client = new TcpClient();
        }

        public void ConnectAsync(IPAddress iP, int port)
        {
            _client.BeginConnect(iP, port, new AsyncCallback(Connect), null);
        }

        public void Connect(IAsyncResult ar)
        {
            if (_client != null)
            {
                try
                {
                    this._client.EndConnect(ar);
                }
                catch (Exception e)
                {
                    return;
                }

                if (!this._loggedIn)
                {
                    this._stream = _client.GetStream();
                }
                this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
            }
        }

        private void ReceiveLengthInt(IAsyncResult ar)
        {
            int dataLength = BitConverter.ToInt32(this._buffer);

            this._buffer = new byte[dataLength];
            try
            {
                this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(ReceiveData), null);
            }catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error with connection to the server.");
                OnConnectionLost.Invoke();
            }
        }

        private void ReceiveData(IAsyncResult ar)
        {
            string data = Encoding.ASCII.GetString(this._buffer);

            DataPacket dataPacket = JsonConvert.DeserializeObject<DataPacket>(data);
            handleData(dataPacket);

            this._buffer = new byte[4];
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        private void handleData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGINRESPONSE":
                    {
                        DataPacket<LoginResponsePacket> d = data.GetData<LoginResponsePacket>();
                        if (d.data.status == "OK")
                        {
                            this._loggedIn = true;
                            OnLogin?.Invoke(this._loggedIn, d.data.chatLog);
                            Console.WriteLine("You are logged in!");
                        }
                        else if (d.data.status == ("ERROR"))
                        {
                            this._loggedIn = false;
                            OnLogin?.Invoke(this._loggedIn, d.data.chatLog);
                            Console.WriteLine("Your username and/or password is not valid!");

                        }
                        break;
                    }
                case "REGISTERRESPONSE":
                    {
                        DataPacket<RegisterResponsePacket> d = data.GetData<RegisterResponsePacket>();
                        if (d.data.status == "OK")
                        {
                            this._registered = true;
                            OnRegister?.Invoke(this._registered);
                            Console.WriteLine("You are Registered");

                            this._loggedIn = true;
                            OnLogin?.Invoke(this._loggedIn, d.data.chatLog);
                            Console.WriteLine("You are logged in!");
                        }
                        else if (d.data.status == ("ERROR"))
                        {
                            this._registered = false;
                            OnRegister?.Invoke(this._registered);
                            Console.WriteLine("Your username is already taken");

                        }
                        break;
                    }
                case "CHAT":
                    {
                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();

                        OnChatReceived?.Invoke($"{d.data.chatMessage}\r\n");
                        break;
                    }
                default:
                    Console.WriteLine("Type is not valid");
                    break;
            }

        }

        public void SendLogin(string username, string password)
        {
            sendPackage(new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(
                 new DataPacket<LoginPacket>()
                 {
                     type = "LOGIN",
                     data = new LoginPacket()
                     {
                         username = username,
                         password = password

                     }
                 }))));
        }

        public void SendRegister(string username, string password)
        {
            sendPackage(new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(
                 new DataPacket<RegisterPacket>()
                 {
                     type = "REGISTER",
                     data = new RegisterPacket()
                     {
                         username = username,
                         password = password

                     }
                 }))));
        }

        public void SendChatMessage(string message)
        {
            if (this._loggedIn)
            {
                sendPackage(new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new DataPacket<ChatPacket>()
                {
                    type = "CHAT",
                    data = new ChatPacket()
                    {
                        chatMessage = message
                    }
                }))));
            }
        }

        private void sendPackage(List<byte> sendBuffer)
        {
            sendBuffer.InsertRange(0, BitConverter.GetBytes(sendBuffer.Count));
            Console.WriteLine(_stream);
            if (_stream != null)
            {
                try
                {
                    _stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Error when connecting to server");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No server connected");
            }
        }

        public void Disconnect()
        {
            if (this._client.Connected)
            {
                this._client.Close();
            }
        }
    }
}
