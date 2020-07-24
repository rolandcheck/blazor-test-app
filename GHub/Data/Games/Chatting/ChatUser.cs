using System.Drawing;
using GHub.Components;

namespace GHub.Data.Games.Chatting
{
    public class ChatUser : EntityBase
    {
        public AppUser AppUser { get; set; }
        public MyColor UserNameColor { get; set; }
    }
}