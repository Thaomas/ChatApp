using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerApp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp.Tests
{
    [TestClass()]
    public class ServerTests
    {

        private Server _server;
        private Client _client;

        [TestMethod()]
        public void LoginClientTest()
        {
            string username = "Wrong username";
            string password = "Wrong password";
            int _portNumber = 51510;
            IPAddress ipAddres = IPAddress.Parse("192.168.112.2");

            _server = new Server(_portNumber);

            string response = _server.LoginClient(_client, username, password);

            Assert.AreEqual("ERROR", response);
        }
    }
}