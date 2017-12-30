using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Example_1_Server.TheServer
{
    public class Clients
    {
        private Action<string, Socket> action;
        private byte[] buffer = new byte[512];
        private Thread clientReceiveThread;
        

        public Socket ClientSocket
        {
            get;
            private set;
        }

        public Clients(Socket socket, Action<string, Socket> action)
        {
            this.ClientSocket = socket;
            this.action = action;

            clientReceiveThread = new Thread(Receive);
            clientReceiveThread.Start();
        }

        private void Receive()
        {
            string message = "";

            while (true)
            {
                int length = ClientSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);

                action(message, ClientSocket);
            }
     
        }

        public void Send(string message)
        {
            ClientSocket.Send(Encoding.UTF8.GetBytes(message));
        }


    }
}