using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Model
{
    public class UserContext: DbContext 
    {
        public DbSet<User> users { get; set; }

        //public string path = @"C:\Users\Pavle\Desktop\simsoni\SIMS-Initial-Project-main\InitialProject\InitialProject\DemoDB.db";
        public string path = @"C:\Users\Pavle\Desktop\simsoni\SIMS-Initial-Project-main\InitialProject\InitialProject\BazaSaUlogama.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={path}");

    }
}
