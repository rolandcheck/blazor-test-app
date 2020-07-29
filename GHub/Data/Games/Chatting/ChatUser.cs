using System;

namespace GHub.Data.Games.Chatting
{
    public class ChatUser : IEntity
    {
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public MyColor UserNameColor { get; set; }
    }
}