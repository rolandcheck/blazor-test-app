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
        }
        public DbSet<TicTacToeGame> TicTacToeGames { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TicTacToeGame>().HasData(new[]
            {
                new TicTacToeGame()
                {
                    Id = 1,
                    TicTacToeGameResult = TicTacToeGameResult.PlayerAWin,
                },
            });

            base.OnModelCreating(builder);
        }

    }

    public class AppUser : IdentityUser
    {
    }
}
