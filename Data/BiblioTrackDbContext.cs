using BiblioTrack.Data.Mapping;
using BiblioTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioTrack.Data
{
    public class BiblioTrackDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public BiblioTrackDbContext(DbContextOptions<BiblioTrackDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
