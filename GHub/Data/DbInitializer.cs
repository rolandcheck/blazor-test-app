using System;
using System.Drawing;
using GHub.Data.Games.Chatting;

namespace GHub.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();


            var chatUser = new ChatUser()
            {
                Id = 132,
                AppUser = new AppUser()
                {
                    Email = "example@email.com"
                },
                UserNameColor = Color.Purple,
            };

            var chatRoom = new ChatRoom()
            {
                Id = 1,
                ChatUsers = { chatUser }
            };

            var chatMessage0 = new ChatMessage()
            {
                Id = 1231,
                ChatUser = chatUser,
                MessageText = "test text",
                SendTime = DateTime.UtcNow,
                ChatRoom = chatRoom,
            };

            var chatMessage1 = new ChatMessage()
            {
                Id = 1232,
                ChatUser = chatUser,
                MessageText = "test text2",
                SendTime = DateTime.UtcNow.AddSeconds(0.5),
                ChatRoom = chatRoom,
            };

            chatRoom.ChatMessages.Add(chatMessage0);
            chatRoom.ChatMessages.Add(chatMessage1);

            context.ChatRooms.Add(chatRoom);

            context.SaveChanges();
        }
    }
}