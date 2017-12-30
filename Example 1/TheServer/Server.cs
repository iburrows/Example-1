using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_1_Server.TheServer
{
    public class Server:ViewModelBase
    {
        private Socket serverSocket;
        private Thread acceptingThread;
        private Action<string> GuiUpdater;
        private List<Clients> theClients = new List<Clients>();

        public Server(string ip, int port, Action<string> guiUpdater)
        {
            this.GuiUpdater = guiUpdater;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(5);
        }

        public void StartAccepting()
        {
            acceptingThread = new Thread(new ThreadStart(Accept));
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                try
                {
                    //theClients.Add(new Clients(serverSocket.Accept(), new Action<string, Socket>(NewMessageReceived)));
                    theClients.Add(new Clients(serverSocket.Accept()));
                }
                catch (Exception e)
                {

                    
                }
            }
        }

        //public void NewMessageReceived(string message, Socket senderSocket)
        public void NewMessageReceived(string message)
        {
            GuiUpdater(message);

            foreach (var item in theClients)
            {
                item.Send(message);
            }
        }
    
    }
}
