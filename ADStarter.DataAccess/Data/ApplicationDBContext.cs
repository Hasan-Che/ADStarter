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

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccType> AccTypes { get; set; }
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
        public DbSet<SessionPrice> SessionPrices { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<TreatmentHistory> TreatmentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Parent>()
                .HasIndex(p => p.acc_ID)
                .IsUnique();

            modelBuilder.Entity<Parent>()
                .HasKey(p => p.parent_ID);
// <<<<<<< HEAD

//             modelBuilder.Entity<Parent>()
//                 .HasOne(p => p.Account)
//                 .WithOne(a => a.Parent)
//                 .HasForeignKey<Parent>(p => p.acc_ID);

//             modelBuilder.Entity<Account>()
//                 .HasMany(a => a.Admins)
//                 .WithOne(ad => ad.Account)
//                 .HasForeignKey(ad => ad.acc_ID);

//             modelBuilder.Entity<Account>()
//                 .HasMany(a => a.CustomerServices)
//                 .WithOne(cs => cs.Account)
//                 .HasForeignKey(cs => cs.acc_ID);

//             modelBuilder.Entity<Account>()
//                 .HasMany(a => a.Therapists)
//                 .WithOne(t => t.Account)
//                 .HasForeignKey(t => t.acc_ID);

// =======
                
            modelBuilder.Entity<Child>()
                .HasOne(c => c.Program)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.prog_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Child>()
                .HasOne(c => c.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.parent_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.SessionPrice)
                .WithMany(sp => sp.Schedules)
                .HasForeignKey(s => s.session_ID)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Admin)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.a_ID)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Child)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.c_myKid)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Therapist)
                .WithMany(t => t.Reports)
                .HasForeignKey(r => r.t_ID)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<AccType>().HasData(
            //   new AccType
            //   {
            //       atype_ID = 1,
            //       atype_desc = "Admin",
            //   },
            //   new AccType
            //   {
            //       atype_ID = 2,
            //       atype_desc = "Customer Service",
            //   },
            //   new AccType
            //   {
            //       atype_ID = 3,
            //       atype_desc = "Therapist",
            //   },
            //   new AccType
            //   {
            //       atype_ID = 4,
            //       atype_desc = "Parent",
            //   }
            //   );

            //modelBuilder.Entity<Account>().HasData(
            //  new Account
            //  {
            //      acc_ID = 1,
            //      acc_email = "Admin@gmail.com",
            //      acc_pass = "123",
            //      acc_status = "verified",
            //      astype_ID = 1,
            //  },
            // new Account
            // {
            //     acc_ID = 2,
            //     acc_email = "cs@gmail.com",
            //     acc_pass = "321",
            //     acc_status = "verified",
            //     astype_ID = 2,
            // },
            // new Account
            // {
            //     acc_ID = 3,
            //     acc_email = "therapistn@gmail.com",
            //     acc_pass = "456",
            //     acc_status = "verified",
            //     astype_ID = 3,
            // },
            // new Account
            // {
            //     acc_ID = 4,
            //     acc_email = "parent@gmail.com",
            //     acc_pass = "654",
            //     acc_status = "verified",
            //     astype_ID = 4,
            // }
            //  );

            //modelBuilder.Entity<Therapist>().HasData(
            //new Therapist
            //{
            //    t_ID = 1,
            //    t_name = "Therapist 1",
            //    t_phoneNum = "1234567890",
            //    t_address = "123 Street, City",
            //}
            //   );
            base.OnModelCreating(modelBuilder);
        }
    }
}
