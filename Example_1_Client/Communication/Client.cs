using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Example_1_Client.Communication
{
    class Client
    {
        byte[] buffer = new byte[512];
        Socket ClientSocket;
        Action<string> MessageInformer;
        Action AbortInformer;

        public Client(string ip, int port, Action<string> messageInformer, Action abortInformer)
        {
            try
            {
                this.AbortInformer = abortInformer;
                this.MessageInformer = messageInformer;
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                ClientSocket = client.Client;
                StartReceiving();
            }
            catch (Exception)
            {

                AbortInformer();
            }

        }

        private void StartReceiving()
        {
            Task.Factory.StartNew(Receive);
        }

        private void Receive()
        {
            string message = "";

            while (true)
            {
                int length = ClientSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                MessageInformer(message);
                break;
            }
            Close();
        }

        private void Close()
        {
            ClientSocket.Close();
            AbortInformer();
        }
    }
}
