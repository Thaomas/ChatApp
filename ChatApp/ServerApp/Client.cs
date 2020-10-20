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

        public Client(TcpClient tcpClient, string username)
        {
            this._tcpClient = tcpClient;
            _username = username;
            this._stream = this._tcpClient.GetStream();
            this._stream.BeginRead(buffer ,0, new AsyncCallback)

        }

        private void RecieveLength(IAsyncResult result)
        {
            int dataLenght = BitConverter.ToInt32(this._buffer);
            this._buffer = new byte[dataLenght];
            this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(), null);
        }

        private void recieveData
    }
}
