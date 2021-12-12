using System;
using System.Drawing;
using System.Net;

using TicTacToe.Common.Requests;
using TicTacToe.Common.Responses;

namespace TicTacToe.Client
{
    class GameClient
    {
        const int Port = 3000;

        readonly SocketConnection socketConnection;

        public GameClient()
        {
            socketConnection = new SocketConnection(Dns.GetHostName(), Port);
        }

        public void Start()
        {
            var connection = socketConnection.Open();

            Console.WriteLine($"Lets start! Enter your player name:");
            var playerName = Console.ReadLine();

            SocketConnection.Send(connection, new Requests
            {
                StartGameRequest = new StartGameRequest
                {
                    PlayerName = playerName
                }
            });

            var gameStartedResponse = SocketConnection.Receive<Responses>(connection);

            if (gameStartedResponse.GameStartedResponse is not null)
            {
                Console.WriteLine($"Game started! Time to make first move!");

                while (true)
                {
                    Console.WriteLine($"Make your move! <x: 0-2> <y: 0-2>");

                    var coordinates = Console.ReadLine().Split(' ');

                    SocketConnection.Send(connection, new Requests
                    {
                        MakeMoveRequest = new MakeMoveRequest
                        {
                            Point = new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]))
                        }
                    });

                    var response = SocketConnection.Receive<Responses>(connection);

                    if (response.BoardUpdatedResponse is not null)
                    {
                        Console.WriteLine(response.BoardUpdatedResponse.Board);
                    }
                    else if (response.GameFinishedResponse is not null)
                    {
                        Console.WriteLine(response.GameFinishedResponse.WinnerMessage);

                        Console.ReadKey();
                        return;
                    }
                }
            }
        }
    }
}
