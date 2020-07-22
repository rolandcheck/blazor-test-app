namespace GHub.Data.Games
{
    public class GamePlayer : EntityBase
    {
        public AppUser User { get; set; }
        public string ConnectionId { get; set; }
    }
}