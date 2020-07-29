using System;

namespace GHub.Data.Games.Chatting
{
    public class ChatMessage : IEntity
    {
        public Guid Id { get; set; }
        public DateTime SendTime { get; set; }
        public string MessageText { get; set; }
        public ChatUser ChatUser { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}