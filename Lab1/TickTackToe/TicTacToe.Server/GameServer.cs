using System.Net.Sockets;

using TicTacToe.Common.Requests;
using TicTacToe.Common.Responses;
using TicTacToe.Core.Models;
using TicTacToe.Core.Structures;

namespace TicTacToe.Server
{
    class GameServer
    {
        const int Port = 3000;

        readonly SocketListener listener;

        public GameServer()
        {
            listener = new SocketListener(Port);
        }

        public void Start()
        {
            listener.Start(Handle);
        }

        void Handle(Socket connection)
        {
            SinglePlayerGame game = null;

            while (true)
            {
                var request = SocketListener.Receive<Requests>(connection);

                if (request.StartGameRequest is not null)
                {
                    var playerName = request.StartGameRequest.PlayerName;

                    game = new SinglePlayerGame(playerName);

                    SocketListener.Send(connection, new Responses
                    {
                        GameStartedResponse = new GameStartedResponse()
                    });
                }
                else if (request.MakeMoveRequest is not null)
                {
                    var winner = game.MakeMove(request.MakeMoveRequest.Point);

                    if (winner is not null)
                    {
                        var winnerMessage = RenderWinnerMessage((Winner)winner, game.PlayerName);

                        SocketListener.Send(connection, new Responses
                        {
                            GameFinishedResponse = new GameFinishedResponse { WinnerMessage = winnerMessage }
                        });

                        return;
                    }

                    var boardView = RenderBoard(game.Board);

                    SocketListener.Send(connection, new Responses
                    {
                        BoardUpdatedResponse = new BoardUpdatedResponse { Board = boardView }
                    });
                }
            }
        }
        
        string RenderBoard(Board board)
        {
            var boardView = string.Empty;

            for (var x = 0; x < board.Field.GetLength(0); x++)
            {
                for (var y = 0; y < board.Field.GetLength(1); y++)
                {
                    if (board.Field[x, y] is null)
                    {
                        boardView += "-";
                    }
                    else
                    {
                        boardView += board.Field[x, y];
                    }

                    if (y == 2)
                    {
                        boardView += "\r\n_____\r\n";
                    }
                    else
                    {
                        boardView += "|";
                    }
                }
            }

            return boardView;
        }

        string RenderWinnerMessage(Winner winner, string playerName) => winner switch
        {
            Winner.Player => $"Congrats {playerName}! You have won this super extra bot!",
            Winner.Bot => "Sorry, but you lost to bot(",
            Winner.Draw => "It is draw! But you were close"
        };
    }
}
