using Microsoft.EntityFrameworkCore;
using ViberWalkBot.Models;

namespace ViberWalkBot.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrackLocation> TrackLocation { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
