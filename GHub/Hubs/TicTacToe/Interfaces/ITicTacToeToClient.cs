using System.Threading.Tasks;
using GHub.Data.Games.TicTacToe;

namespace GHub.Hubs.TicTacToe.Interfaces
{
    public interface ITicTacToeToClient
    {
        Task StartGame(TicTacToeGame game);
    }
}