using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace TicTacToe.Server
{
    class SocketListener
    {
        readonly int portNumber;

        readonly Socket listenerSocket;
        readonly IPAddress ipAddress;

        public SocketListener(int portNumber)
        {
            this.portNumber = portNumber;

            ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            listenerSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(Action<Socket> handleConnection)
        {
            Console.WriteLine("Server started.");

            listenerSocket.Bind(new IPEndPoint(ipAddress, portNumber));
            listenerSocket.Listen(5);

            while (true)
            {
                Console.WriteLine("Waiting for connection...");

                var connectionSocket = listenerSocket.Accept();
                
                handleConnection(connectionSocket);
                Console.WriteLine("Connection handled.");

                connectionSocket.Shutdown(SocketShutdown.Both);
                connectionSocket.Close();
                Console.WriteLine("Connection closed.");
            }
        }

        public static void Send<T>(Socket socket, T data)
        {
            var dataBytes = JsonSerializer.SerializeToUtf8Bytes(data);
            socket.Send(dataBytes);
        }

        public static T Receive<T>(Socket socket)
        {
            var buffer = new byte[1024];
            var bytesCount = socket.Receive(buffer);

            return JsonSerializer.Deserialize<T>(new ReadOnlySpan<byte>(buffer, 0, bytesCount));
        }
    }
}
