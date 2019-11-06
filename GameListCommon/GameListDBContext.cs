using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameListCommon
{
    public class GameListDBContext : DbContext
    {
        public GameListDBContext(DbContextOptions<GameListDBContext> options) : base(options) { }
        public DbSet<Game> Games { get; set; }

    }
}
