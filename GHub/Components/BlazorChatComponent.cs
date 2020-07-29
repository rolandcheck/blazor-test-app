using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHub.Data;
using GHub.Data.Games.Chatting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GHub.Components
{
    public class BlazorChatComponent : ComponentBase
    {
        private ChatUser _currentChatUser;
        [Inject] private UserManager<AppUser> UserManager { get; set; }
        [Inject] private ApplicationDbContext DbContext { get; set; }
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected List<ChatRoom> UserChats { get; } = new List<ChatRoom>();
        protected ChatRoom SelectedChatRoom { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var claimsPrincipal = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(claimsPrincipal.User);

            if (user is null)
            {
                var userName = UserManager.GetUserName(claimsPrincipal.User);

                if (userName is null) return;

                user = await UserManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            }

            _currentChatUser = DbContext.ChatUsers.Include(x => x.UserNameColor).FirstOrDefault(x => x.AppUser == user);
            //.Include(x => x.AppUser)
            //.Include(x => x.UserNameColor)
            //.FirstOrDefaultAsync(x => x.AppUser == user);

            // todo: rewrite query
            var query = DbContext.ChatRooms
                .Include(x => x.ChatMessages)
                .Include(x => x.ChatUsers).ThenInclude(x => x.UserNameColor)
                .Include(x => x.ChatUsers).ThenInclude(x => x.AppUser);


            foreach (var chatRoom in query)
                if (chatRoom.ChatUsers.Contains(_currentChatUser))
                    UserChats.Add(chatRoom);

            await base.OnInitializedAsync();
        }





        protected void SelectedChatChanged(ChatRoom room)
        {
            SelectedChatRoom = room;
            StateHasChanged();
        }

        protected void OnSendMessage(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            var message = new ChatMessage
            {
                ChatRoom = SelectedChatRoom,
                ChatUser = _currentChatUser,
                MessageText = text,
                SendTime = DateTime.UtcNow
            };
            SelectedChatRoom.ChatMessages.Add(message);
            DbContext.SaveChanges();
            StateHasChanged();
        }

        protected void OnNewChatClick()
        {
        }
    }
}