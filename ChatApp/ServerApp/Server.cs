using System;
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
        public Server(int port)
        {
            _portNumber = port;
        }

        // Start listening for connection:
        public async void Start()
        {
            IPAddress ipAddres = IPAddress.Loopback;
            TcpListener listener = new TcpListener(ipAddres, _portNumber);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            LogMessage("Server is running");
            LogMessage($"Listening on {IPAddress.Loopback.ToString()}:{_portNumber}");
        }

        private void OnConnect(IAsyncResult result)
        {
            var client = this.li
        }

        /// Process Individual client

        private async Task HandleClientAsync(TcpClient tcpClient)
        {
            string clientInfo = tcpClient.Client.RemoteEndPoint.ToString();
            LogMessage(string.Format("Got connection request from {0}", clientInfo));
            try
            {
                using (var networkStream = tcpClient.GetStream())
                {
                    using (var reader = new StreamReader(networkStream))
                    {
                        using (var writer = new StreamWriter(networkStream))
                        {
                            writer.AutoFlush = true;
                            while (true)
                            {
                                var dataFromServer = await reader.ReadLineAsync();
                                if (string.IsNullOrEmpty(dataFromServer))
                                {
                                    break;
                                }

                                LogMessage(dataFromServer);
                                await writer.WriteLineAsync("FromServer-" + dataFromServer);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogMessage(exp.Message);
            }
            finally
            {
                LogMessage($"Closing the client connection - {clientInfo}");
                tcpClient.Close();
            }
        }

        private void LogMessage(string message, [CallerMemberName] string callername = "")
        {
            Console.WriteLine("[{0}] - Thread-{1}- {2}",
                    callername, Thread.CurrentThread.ManagedThreadId, message);
        }
    }
}
