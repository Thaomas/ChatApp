using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace ServerApp
{
    class Client
    {
        private TcpClient _tcpClient;
        public TcpClient TcpClient { get { return this._tcpClient; } }
        private NetworkStream _stream;
        private byte[] _buffer = new byte[4];
        private string _username;
        public string Username { set { this._username = value; } }

        public Client(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
            this._stream = this._tcpClient.GetStream();
            this._username = "";
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length,  new AsyncCallback(RecieveLength), null);
            Console.WriteLine("Begin read client");
        }

        private void RecieveLength(IAsyncResult result)
        {
            int dataLenght = BitConverter.ToInt32(this._buffer);
            this._buffer = new byte[dataLenght];
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(RecieveData), null);
        }

        private void RecieveData(IAsyncResult result)
        {
            string data = System.Text.Encoding.ASCII.GetString(this._buffer);

            DataPacket packet = JsonConvert.DeserializeObject<DataPacket>(data);
            parseDataAsync(packet);

            this._buffer = new byte[4];
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(RecieveLength), null);
        }

        private async Task parseDataAsync(DataPacket data)
        {

            Console.WriteLine(data.type);
            switch (data.type)
            {
                case "LOGIN":
                    {
                        DataPacket<LoginPacket> d = data.GetData<LoginPacket>();
                        List<string> chatlog = new List<string>();
                        string response = Server.LoginClient(this , d.data.username, d.data.password);
                        if (response.Equals("OK"))
                        {
                            chatlog = Server.GetChatLog();
                        }

                        SendPacket(new DataPacket<LoginResponsePacket>()
                        {
                            type = "LOGINRESPONSE",
                            data = new LoginResponsePacket()
                            {
                                status = response,
                                chatLog = chatlog
                            }
                        }.ToJson());
                        break;
                    }
                case "REGISTER":
                    {
                        DataPacket<RegisterPacket> d = data.GetData<RegisterPacket>();
                        List<string> chatlog = new List<string>();
                        string response = Server.RegisterClient(this, d.data.username, d.data.password);
                        if (response.Equals("OK"))
                        {
                            chatlog = Server.GetChatLog();
                        }
                        SendPacket(new DataPacket<RegisterResponsePacket>()
                        {
                            type = "REGISTERRESPONSE",
                            data = new RegisterResponsePacket()
                            {
                                status = response,
                                chatLog = chatlog
                            }
                        }.ToJson());
                        break;
                    }
                case "CHAT":
                    {
                        if (!_username.Equals("")) {
                            DataPacket<ChatPacket> d = data.GetData<ChatPacket>();
                            Server.ChatMessage($"{_username}: {d.data.chatMessage}");
                        }
                        break;
                    }
                case "DISCONNECT":
                    {
                        Server.DisconnectClient(this);
                        SendPacket(new DataPacket<DisconnectResponsePacket>
                        {
                            type = "DISCONNECTRESPONSE",
                            data = new DisconnectResponsePacket()
                            {
                                status = "OK"
                            }
                        }.ToJson());
                        _tcpClient.Close();
                    break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void SendPacket(string packet)
        {
            List<byte> buffer = new List<byte>(Encoding.ASCII.GetBytes(packet));
            buffer.InsertRange(0, BitConverter.GetBytes(buffer.Count));
            this._stream.Write(buffer.ToArray(), 0, buffer.Count);
        }

        public void messageClient(string message)
        {
            SendPacket(new DataPacket<ChatPacket>()
            {
                type = "CHATMESSAGE", 
                data = new ChatPacket()
                {
                    chatMessage = message
                }
            }.ToJson());
        }
    }
}
