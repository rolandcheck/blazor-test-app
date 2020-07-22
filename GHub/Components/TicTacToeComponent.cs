using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GHub.Data;
using GHub.Data.Games.TicTacToe;
using GHub.Hubs.TicTacToe.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Client;

namespace GHub.Components
{
    public class TicTacToeComponent : ComponentBase
    {
        [Inject] private UserManager<AppUser> UserManager { get; set; }
        [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; }

        private HubConnection _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var host = HttpContextAccessor.HttpContext.Request.GetAbsoluteFullHost();
            var uri = new Uri($"{host}{CommonStrings.TicTacToeHubAction}");
            
            _hubConnection = new HubConnectionBuilder().WithUrl(uri).Build();

            _hubConnection.On<TicTacToeGame>(nameof(ITicTacToeToClient.StartGame), game =>
            {
                // do smth with game
            });

            // starting to communicate with the server
            await _hubConnection.StartAsync();
        }

        protected List<TicTacToeGame> Games { get; } = new List<TicTacToeGame>();

        protected async Task ConnectClick()
        {
            await foreach (var game in _hubConnection.StreamAsync<TicTacToeGame>(nameof(ITicTacToeToServer.GetAllGames)))
            {
                Games.Add(game);
            }
        }
    }
}
