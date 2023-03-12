using InitialProject.Model;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Contexts
{
    public class TourKeyPointContext : DbContext
    {
        public DbSet<User> tourKeyPoints { get; set; }

        //public string path = @"C:\Users\Pavle\Desktop\simsoni\SIMS-Initial-Project-main\InitialProject\InitialProject\DemoDB.db";
        public string path =
            @"C:\Users\Pavle\Desktop\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\TourKeyPoint.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={path}");

    }
}