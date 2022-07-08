using BackEndProject.Models;
using ClientApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event_Participans>().HasKey(ep => new
            {
                ep.EventId,
                ep.UserId
            });
            modelBuilder.Entity<Event_Participans>().HasOne(e => e.Event).WithMany(ep => ep.Event_Participans).HasForeignKey(e => e.EventId);
            modelBuilder.Entity<Event_Participans>().HasOne(e => e.User).WithMany(ep => ep.Event_Participans).HasForeignKey(e => e.UserId);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Playground> playgrounds { get; set; }
        public DbSet<Event_Participans> Event_Participans { get; set; }

    }
}
