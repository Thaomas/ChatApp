using ServerApp;
using System;

namespace ServerAppp
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(51510);
            server.Start();
            Console.ReadLine();
        }
    }
}
