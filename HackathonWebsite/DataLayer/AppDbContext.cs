using HackathonWebsite.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonWebsite.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<CaseEntity> Cases { get; set; }
        public DbSet<HackathonEntity> Hackathons { get; set; }
        public DbSet<ApplyToTeamEntity> ApplyToTeams { get; set; }
        public DbSet<ApplyToHackEntity> ApplyToHacks { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Team)
                .WithMany(t => t.Participants)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TeamEntity>()
                .HasOne(t => t.Leader)
                .WithOne()
                .HasForeignKey<TeamEntity>(t => t.LeaderId);

            modelBuilder.Entity<TeamEntity>()
                .HasOne(t => t.Case)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.CaseId);

            modelBuilder.Entity<TeamEntity>()
                .HasMany(t => t.Participants)
                .WithOne(p => p.Team)
                .HasForeignKey(t => t.TeamId);

            modelBuilder.Entity<CaseEntity>()
                .HasOne(c => c.Hackathon)
                .WithMany(h => h.Cases)
                .HasForeignKey(c=>c.HackathonId);

            modelBuilder.Entity<CaseEntity>()
                .HasMany(c => c.Teams)
                .WithOne(t => t.Case)
                .HasForeignKey(x=>x.CaseId);

            modelBuilder.Entity<ApplyToHackEntity>()
                .HasOne(h => h.Team)
                .WithMany(u => u.AppliesToHack)
                .HasForeignKey(x => x.TeamId);

            modelBuilder.Entity<ApplyToHackEntity>()
                .HasOne(h => h.Case)
                .WithMany(c => c.AppliesToHack)
                .HasForeignKey(x => x.CaseId);

            modelBuilder.Entity<ApplyToTeamEntity>()
                .HasOne(t => t.Team)
                .WithMany(t => t.AppliesToTeam)
                .HasForeignKey(x => x.TeamId);

            modelBuilder.Entity<ApplyToTeamEntity>()
                .HasOne(t => t.User)
                .WithMany(t => t.AppliesToTeam)
                .HasForeignKey(x => x.UserId);
        }
    }
}
