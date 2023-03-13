﻿using System;
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


        //Pavle:
        //public string path = @"C:\Users\Pavle\Desktop\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database.db";

        //Aleksandra:
        //public string path = @" F:\SIMS - PROJEKAT\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database.db";

        //Stajic:
        public string path = @"C:\Users\Luka stajic\Documents\Projekat SiMS-HCI\HCI-SIMS\SIMS-Initial-Project-main\InitialProject\InitialProject\Database.db";
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={path}");

    }
}