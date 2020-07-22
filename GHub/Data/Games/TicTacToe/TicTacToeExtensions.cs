namespace GHub.Data.Games.TicTacToe
{
    public static class TicTacToeExtensions
    {
        public static void Deconstruct(this TicTacToeMove move, out TicTacToePlayer player, out int x, out int y) => (player, x, y) = move;
    }
}