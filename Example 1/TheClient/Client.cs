using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_1.TheClient
{
    class Client
    {
        private string ip;
        private int port;
        private Action<string> MessageInformer;
        private Action AbortInformer;
        Socket clientSocket;
        byte[] buffer = new byte[512];
        Thread receivingThread;

        public Client(string ip, int port, Action<string> messageInformer, Action clientDisconnected)
        {
            try
            {
                this.ip = ip;
                this.port = port;
                this.MessageInformer = messageInformer;
                this.AbortInformer = clientDisconnected;
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                clientSocket = client.Client;
                StartReceiving();
            }
            catch (Exception)
            {

                clientDisconnected();
            }

        }

        private void StartReceiving()
        {
            //Task.Factory.StartNew(Receive);
            receivingThread = new Thread(new ThreadStart(Receive));
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Receive()
        {
            string message = "";
            while (receivingThread.IsAlive)
            {
                if (!message.Equals("@quit"))
                {
                    int length = clientSocket.Receive(buffer);
                    message = Encoding.UTF8.GetString(buffer, 0, length);
                    MessageInformer(message);
                }  
            }
            Close();
        }

        //public void Send(string message)
        //{
        //    if (clientSocket != null)
        //    {
        //        clientSocket.Send(Encoding.UTF8.GetBytes(message));
        //    }
        //}
        private void Close()
        {
            clientSocket.Close();
            AbortInformer();
        }
    }
}
