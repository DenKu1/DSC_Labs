using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using TicTacToe.Core.Structures;

namespace TicTacToe.Core.Models
{
    public class Board
    {
        public const int BoardSize = 3;

        public Mark?[,] Field { get; }

        public bool IsEnded { get; private set; }
        public bool IsDraw { get; private set; }

        public Board()
        {
            Field = new Mark?[BoardSize, BoardSize];
        }

        public void PlaceMark(Mark mark, Point point)
        {
            ValidatePoint(point);

            SetMark(mark, point);

            CheckGameEnd(mark);
        }

        void ValidatePoint(Point point)
        {
            if (point.X is < 0 or > BoardSize - 1)
                throw new ArgumentOutOfRangeException(nameof(point));

            if (point.Y is < 0 or > BoardSize - 1)
                throw new ArgumentOutOfRangeException(nameof(point));

            if (Field.GetValue(point.X, point.Y) is not null)
                throw new ArgumentException("Unable to place at already taken point.", nameof(point));
        }

        void SetMark(Mark mark, Point point)
        {
            Field.SetValue(mark, point.X, point.Y);
        }

        void CheckGameEnd(Mark mark)
        {
            var hasEmptyCells = false;

            foreach (var row in GetAllRows())
            {
                if (row.All(cellMark => cellMark == mark))
                    IsEnded = true;

                if (row.Any(cell => cell is null))
                    hasEmptyCells = true;
            }

            if (!IsEnded && !hasEmptyCells)
            {
                IsDraw = true;
            }

            IEnumerable<List<Mark?>> GetAllRows()
            {
                yield return new List<Mark?> { Field[0, 0], Field[0, 1], Field[0, 2] };
                yield return new List<Mark?> { Field[1, 0], Field[1, 1], Field[1, 2] };
                yield return new List<Mark?> { Field[2, 0], Field[2, 1], Field[2, 2] };

                yield return new List<Mark?> { Field[0, 0], Field[1, 0], Field[2, 0] };
                yield return new List<Mark?> { Field[0, 1], Field[1, 1], Field[2, 1] };
                yield return new List<Mark?> { Field[0, 2], Field[1, 2], Field[2, 2] };

                yield return new List<Mark?> { Field[0, 0], Field[1, 1], Field[2, 2] };
                yield return new List<Mark?> { Field[0, 2], Field[1, 1], Field[2, 0] };
            }
        }
    }
}
