using System;
using System.Collections.Generic;

namespace GHub.Data.Games.Chatting
{
    public class ChatRoom : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChatUser> ChatUsers { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}