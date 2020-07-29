using System;

namespace GHub.Data.Games.TicTacToe
{
    public class TicTacToeMove : IEntity
    {
        public Guid Id { get; set; }

        public TicTacToeMove() { }

        public TicTacToeMove(TicTacToePlayer player, int x, int y)
        {
            Player = player;
            X = x;
            Y = y;
        }
        public TicTacToePlayer Player { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}