using System;
using System.Drawing;

using TicTacToe.Core.Structures;

namespace TicTacToe.Core.Models
{
    public class SinglePlayerGame
    {
        public string PlayerName { get; }

        public Board Board { get; }

        public SinglePlayerGame(string playerName)
        {
            PlayerName = playerName;

            Board = new Board();
        }

        public Winner? MakeMove(Point playerMark)
        {
            Board.PlaceMark(Mark.X, playerMark);

            if (Board.IsEnded)
            {
                return Board.IsDraw ? Winner.Draw : Winner.Player;
            }

            var botMark = GenerateBotMove();
            Board.PlaceMark(Mark.O, botMark);

            if (Board.IsEnded)
            {
                return Board.IsDraw ? Winner.Draw : Winner.Bot;
            }

            return null;
        }

        Point GenerateBotMove()
        {
            for (var x = 0; x < Board.Field.GetLength(0); x++)
            {
                for (var y = 0; y < Board.Field.GetLength(1); y++)
                {
                    if (Board.Field[x, y] is null)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new Exception("No available moves for bot left");
        }
    }
}