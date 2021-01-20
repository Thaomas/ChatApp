using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientApp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace ClientApp.Tests
{
    [TestClass()]
    public class ClientTests
    {

        Client client = new Client();

        [TestMethod()]
        public void ConnectAsyncTest()
        {
            try
            {
                IPAddress ip = IPAddress.Parse("198.168.112.2");

                client.ConnectAsync(ip, 51510);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        [TestMethod()]
        public void SendLoginTest()
        {
            try
            {
                client.SendLogin("test", "test");
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}