using ADStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADStarter.DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<TreatmentHistory> TreatmentHistories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Parent entity
            modelBuilder.Entity<Parent>()
                .HasKey(p => p.parent_ID);

            // Configure Child entity relationships
            modelBuilder.Entity<Child>()
                .HasOne(c => c.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.parent_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Child>()
                .Property(c => c.t_ID)
                .HasDefaultValue(null);

            // Configure Schedule entity relationships
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Therapist)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.t_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Program)
                .WithMany(p => p.Schedules)
                .HasForeignKey(s => s.prog_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Slot)
                .WithMany(sl => sl.Schedules)
                .HasForeignKey(s => s.slot_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Child)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.c_myKid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .Property(s => s.t_ID)
                .HasDefaultValue(null);

            // Configure Payment entity relationships
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Payments)
                .HasForeignKey(p => p.invoice_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Child)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.c_myKid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Report entity relationships
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Schedule)
                .WithMany(s => s.Reports)
                .HasForeignKey(r => r.schedule_ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
