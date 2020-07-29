using System;
using System.Collections.Generic;
using System.Linq;

namespace GHub.Data.Games.TicTacToe
{
    public class TicTacToeGame : IEntity
    {
        public Guid Id { get; set; }

        private const int BoardSize = 3;

        public TicTacToeMarkEnum[,] Board { get; } = new TicTacToeMarkEnum[BoardSize, BoardSize];

        public TicTacToePlayer PlayerA { get; set; }
        public TicTacToePlayer PlayerB { get; set; }

        public event Action OnGameEnd;

        public List<TicTacToeMove> GameHistory { get; } = new List<TicTacToeMove>();

        public TicTacToeGameResult TicTacToeGameResult { get; set; }

        public TicTacToeMarkEnum CurrentMove { get; private set; } = TicTacToeMarkEnum.X;
        public string RoomId { get; set; }

        public bool MakeMove(TicTacToeMove move)
        {
            var (player, i, j) = move;

            if (!CheckBounds(i, j))
            {
                return false;
            }

            if (player.Mark == CurrentMove && Board[i, j] == TicTacToeMarkEnum.NaN)
            {
                Board[i, j] = player.Mark;
                CurrentMove = CurrentMove == TicTacToeMarkEnum.O ? TicTacToeMarkEnum.X : TicTacToeMarkEnum.O;
            }
            else
            {
                return false;
            }

            // move is done
            GameHistory.Add(move);


            if (GameEnd())
            {
                OnGameEnd?.Invoke();
            }

            return true;
        }

        public bool MakeMove(TicTacToePlayer player, int i, int j)
        {
            var move = new TicTacToeMove(player, i, j);

            return MakeMove(move);
        }

        private static bool CheckBounds(in int i, in int j)
        {
            return i >= 0 && j >= 0 && i < BoardSize && j < BoardSize;
        }

        private bool GameEnd()
        {
            var boardEnumerable = Board.Cast<TicTacToeMarkEnum>().ToList();


            static bool Predicate(TicTacToeMarkEnum mark) => mark == TicTacToeMarkEnum.X || mark == TicTacToeMarkEnum.O;


            for (var i = 0; i < BoardSize; i++)
            {
                var row = boardEnumerable.Skip(BoardSize * i).Take(BoardSize);

                var rowWin = row.All(Predicate);
                if (rowWin)
                {
                    TicTacToeGameResult = row.First() == PlayerA.Mark ? TicTacToeGameResult.PlayerAWin : TicTacToeGameResult.PlayerBWin;
                    return true;
                }
            }



            for (var i = 0; i < BoardSize; i++)
            {
                var column = boardEnumerable.Where((_, k) => (k - i) % BoardSize == 0);

                var columnWin = column.All(Predicate);

                if (columnWin)
                {
                    TicTacToeGameResult = column.First() == PlayerA.Mark ? TicTacToeGameResult.PlayerAWin : TicTacToeGameResult.PlayerBWin;
                    return true;
                }
            }



            var range = Enumerable.Range(0, BoardSize).ToList();
            var mainDiagonal = range.Select(i => Board[i, i]);
            var mainDiagonalWin = mainDiagonal.All(Predicate);
            if (mainDiagonalWin)
            {
                TicTacToeGameResult = mainDiagonal.First() == PlayerA.Mark ? TicTacToeGameResult.PlayerAWin : TicTacToeGameResult.PlayerBWin;
                return true;
            }



            var counterDiagonalMarks = new List<TicTacToeMarkEnum>();
            for (var i = 0; i < range.Count; i++)
            {
                var x = range[i];
                var y = range[range.Count - i - 1];
                var temp = Board[x, y];
                counterDiagonalMarks.Add(temp);
            }

            var counterDiagonalWin = counterDiagonalMarks.All(Predicate);

            if (counterDiagonalWin)
            {
                TicTacToeGameResult = counterDiagonalMarks.First() == PlayerA.Mark ? TicTacToeGameResult.PlayerAWin : TicTacToeGameResult.PlayerBWin;
                return true;
            }

            if (boardEnumerable.Any(mark => mark == TicTacToeMarkEnum.NaN))
            {
                return false;
            }


            TicTacToeGameResult = TicTacToeGameResult.Draw;
            return true;

        }


    }
}