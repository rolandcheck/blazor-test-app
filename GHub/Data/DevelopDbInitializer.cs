using System;
using System.Drawing;
using GHub.Data.Games.Chatting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace GHub.Data
{
    public class DevelopDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHostEnvironment _env;

        public DevelopDbInitializer(UserManager<AppUser> userManager, IHostEnvironment env)
        {
            _env = env;
            _userManager = userManager;
        }

        public async void Initialize(ApplicationDbContext context)
        {
            if (!_env.IsDevelopment()) return;

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var user0 = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "RolandCheck",
                Email = "example@email.com",
            };

            const string pass = "passwordRCh";
            await _userManager.CreateAsync(user0, pass);

            var color0 = new MyColor(Color.Purple)
            {
                Id = Guid.NewGuid(),
            };

            var chatUser0 = new ChatUser()
            {
                Id = Guid.NewGuid(),
                AppUser = user0,
                UserNameColor = color0,
            };


            var user1 = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "NfReal",
                Email = "nathan@email.com",
            };

            await _userManager.CreateAsync(user1, "passwordNf");

            var color1 = new MyColor(Color.CadetBlue)
            {
                Id = Guid.NewGuid(),
            };
            var chatUser1 = new ChatUser()
            {
                Id = Guid.NewGuid(),
                AppUser = user1,
                UserNameColor = color1,
            };


            var chatRoom = new ChatRoom()
            {
                Id = Guid.NewGuid(),
                ChatUsers = new[]{chatUser0, chatUser1},
                Name = "My Cool Chat",
            };

            var chatMessage0 = new ChatMessage()
            {
                Id = Guid.NewGuid(),
                ChatUser = chatUser0,
                MessageText = "test text",
                SendTime = DateTime.UtcNow,
                ChatRoom = chatRoom,
            };

            var chatMessage1 = new ChatMessage()
            {
                Id = Guid.NewGuid(),
                ChatUser = chatUser0,
                MessageText = "test text2",
                SendTime = DateTime.UtcNow.AddMinutes(1.5),
                ChatRoom = chatRoom,
            };

            var chatMessage2 = new ChatMessage()
            {
                Id = Guid.NewGuid(),
                ChatUser = chatUser1,
                MessageText = "Nf’s message",
                SendTime = DateTime.UtcNow.AddMinutes(3),
                ChatRoom = chatRoom,
            };

            chatRoom.ChatMessages = new[] {chatMessage0, chatMessage1, chatMessage2};




            await context.AddRangeAsync(chatRoom);
            await context.SaveChangesAsync();
        }
    }
}