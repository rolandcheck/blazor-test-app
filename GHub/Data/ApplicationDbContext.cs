using GHub.Components;
using GHub.Data.Games.Chatting;
using GHub.Data.Games.TicTacToe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GHub.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            DbInitializer.Initialize(this);
        }
        public DbSet<TicTacToeGame> TicTacToeGames { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TicTacToeGame>().HasData(
                new TicTacToeGame{

                    Id = 1,
                    TicTacToeGameResult = TicTacToeGameResult.PlayerAWin,
                });


            builder.Entity<MyColor>().Ignore(x => x.Color);

            base.OnModelCreating(builder);
        }

    }

    public class AppUser : IdentityUser
    {
        public static bool operator ==(AppUser userA, AppUser userB)
        {
            if (userA is null)
                return userB is null;

            return userA.Equals(userB);
        }

        public static bool operator !=(AppUser userA, AppUser userB)
        {
            return !(userA == userB);
        }

        public override bool Equals(object obj)
        {
            if (obj is AppUser user)
            {
                return Email == user.Email;
            }

            return false;
        }
    }
}
