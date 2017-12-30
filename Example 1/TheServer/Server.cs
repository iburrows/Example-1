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
        Socket serverSocket;
        Thread acceptingThread;
        Action<string> GuiUpdater;
        List<Clients> theClients = new List<Clients>();

        public Server(string ip, int port, Action<string> guiUpdater)
        {
            GuiUpdater = guiUpdater;
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
                    theClients.Add(new Clients(serverSocket.Accept(), new Action<string, Socket>(NewMessageReceived)));
                }
                catch (Exception e)
                {

                    
                }
            }
        }

        private void NewMessageReceived(string message, Socket senderSocket)
        {
            GuiUpdater(message);

            foreach (var item in theClients)
            {
                if (item.ClientSocket != senderSocket)
                {
                    item.Send(message);
                }
            }
        }
    
    }
}
