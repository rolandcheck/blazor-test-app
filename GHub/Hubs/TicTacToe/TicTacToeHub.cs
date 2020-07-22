using System;
using System.Collections.Generic;
using GHub.Data;
using GHub.Data.Games.TicTacToe;
using GHub.Hubs.TicTacToe.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GHub.Hubs.TicTacToe
{
    public class TicTacToeHub : Hub<ITicTacToeToClient>, ITicTacToeToServer
    {
        private readonly EFGenericRepository<TicTacToeGame> _repository;

        public TicTacToeHub(EFGenericRepository<TicTacToeGame> repository)
        {
            _repository = repository;
            var t = _repository.Get();
        }

        public string GetRoomId()
        {
            var roomId = Guid.NewGuid().ToString();
            return roomId;
        }

        public void AddToQueue(AppUser user)
        {
            var player = new TicTacToePlayer
            {
                ConnectionId = Context.ConnectionId,
                User = user,
            };
        }

        public async IAsyncEnumerable<TicTacToeGame> GetAllGames()
        {
            await foreach (var ticTacToeGame in _repository.Get())
            {
                yield return ticTacToeGame;
            }
        }
    }
}