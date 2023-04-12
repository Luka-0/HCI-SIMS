using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Location> location { get; set; }
        public DbSet<Tour> tour { get; set; }
        public DbSet<TourKeyPoint> tourKeyPoints { get; set; }

        public DbSet<Accommodation> accommodation { get; set; }

        public DbSet<Image> image { get; set; }

        public DbSet<GuestReview> guestReview { get; set; }
       // public DbSet<GuestGrade> guestGrade { get; set; }

        public DbSet<TourReservation> tourReservation { get; set; } 

        public DbSet<AccommodationReservation> accommodationReservation { get; set; }

        public DbSet<Voucher> voucher { get; set; }

        public DbSet<AccommodationReview> accommodationReview { get; set; }

        public DbSet<TourReview> tourReview { get; set; }

        public DbSet<ReservationReschedulingRequest> reservationReschedulingRequest { get; set; }

        //Pavle:
        //public string path = @"C:\Users\Pavle\Desktop\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database4.db";

        //Aleksandra:
        // public string path = @" F:\SIMS - PROJEKAT\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database4.db";

        //Stajic:
        //public string path = @"C:\Users\Luka stajic\Documents\Projekat SiMS-HCI\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database4.db";

        //Pele:
        public string path = @" E:\JobGitRepos\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database4.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={path}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasMany(d => d.images)
                    .WithOne(p => p.Tour)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasMany(d => d.TourKeyPoints)
                    .WithOne(p => p.Tour)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}