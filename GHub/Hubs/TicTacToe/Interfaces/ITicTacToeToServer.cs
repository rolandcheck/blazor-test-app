using System.Collections.Generic;
using GHub.Data.Games.TicTacToe;

namespace GHub.Hubs.TicTacToe.Interfaces
{
    public interface ITicTacToeToServer
    {
        IAsyncEnumerable<TicTacToeGame> GetAllGames();
    }
}