using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHub.Data;
using GHub.Data.Games.Chatting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace GHub.Components
{
    public class BlazorChatComponent : ComponentBase
    {
        [Inject] private UserManager<AppUser> UserManager { get; set; }
        [Inject] private ApplicationDbContext DbContext { get; set; }

        protected List<ChatRoom> UserChats { get; private set; } = new List<ChatRoom>();

        protected ChatRoom SelectedChatRoom { get; set; }
        protected string TypedMessage { get; set; }
        private ChatUser _chatUser;

        protected override async Task OnInitializedAsync()
        {
            var user = new AppUser(){Email = "example@email.com" };

            _chatUser = await DbContext.ChatUsers.FirstOrDefaultAsync(x => x.AppUser == user);

            await AsyncEnumerable.Where(DbContext.ChatRooms, room => room.ChatUsers.Contains(_chatUser)).ForEachAsync(x => UserChats.Add(x));


            await base.OnInitializedAsync();
        }


        protected void SelectedChatChanged(ChatRoom room)
        {
            SelectedChatRoom = room;
            StateHasChanged();
        }

        protected void OnSendMessage()
        {
            if (string.IsNullOrWhiteSpace(TypedMessage))
            {
                return;
            }
            var message = new ChatMessage
            {
                ChatRoom = SelectedChatRoom,
                ChatUser = _chatUser,
                MessageText = TypedMessage,
                SendTime = DateTime.UtcNow,
            };
            SelectedChatRoom.ChatMessages.Add(message);
            DbContext.SaveChanges();
            TypedMessage = null;
        }
    }
}