using System.Collections.Generic;

namespace GHub.Data.Games.Chatting
{
    public class ChatRoom : EntityBase
    {
        public List<ChatUser> ChatUsers { get; private set; } = new List<ChatUser>();

        public List<ChatMessage> ChatMessages { get; private set; } = new List<ChatMessage>();
    }
}