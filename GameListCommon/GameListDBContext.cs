using Microsoft.EntityFrameworkCore;

namespace GameListCommon
{
    public class GameListDBContext : DbContext
    {
        public GameListDBContext(DbContextOptions<GameListDBContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"server=DESKTOP-CTS13TM;Database=GameList;Trusted_Connection=True;");
        //}
    }
}
