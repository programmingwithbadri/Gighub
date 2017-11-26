using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    /// <summary>
    /// DB Context to link with migrations of EF
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> DbSetGig { get; set; }
        public DbSet<Genre> DbSetGenre { get; set; }
        public DbSet<Attendance> DbSetAttendance { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false); // Delete cascade delete using fluent API.
            base.OnModelCreating(modelBuilder);
        }
    }
}