using System;

namespace GHub.Data.Games
{
    public class GamePlayer : IEntity
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public string ConnectionId { get; set; }
    }
}