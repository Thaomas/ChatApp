using Newtonsoft.Json;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace ServerApp
{
    class Client
    {
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private byte[] _buffer = new byte[4];
        private string _username;
        private bool loggedIn;

        public Client(TcpClient tcpClient)
        {
            this.loggedIn = false;
            this._tcpClient = tcpClient;
            this._stream = this._tcpClient.GetStream();
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length,  new AsyncCallback(RecieveLength), null);

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
            parseData(packet);

            this._buffer = new byte[4];
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(RecieveLength), null);
        }

        private void parseData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGIN":
                    {
                        DataPacket<LoginPacket> d = data.GetData<LoginPacket>();

                        if (Server.users.ContainsKey(d.data.username) && Server.users[d.data.username].Equals(d.data.password)){

                        }
                        else
                        {

                        }

                        break;
                    }
                case "REGISTER":
                    {
                        break;
                    }
                case "CHAT":
                    {
                        break;
                    }
            }
        }
    }
}
