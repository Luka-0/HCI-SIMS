﻿using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    public class ForumRepository : IForumRepository
    {
        public ForumRepository() { }

        public void Save(Forum forum)
        {
            using var db = new UserContext();

            var existingLocation = db.location.Find(forum.Location.Id);
            var existingUser = db.users.Find(forum.User.Id);

            forum.Location = existingLocation;
            forum.User = existingUser;

            db.location.Attach(existingLocation);
            db.users.Attach(existingUser);

            db.Add(forum);
            db.SaveChanges();
        }

        public List<Forum> GetByUser(int id)
        {
            List<Forum> retVal = new();

            using (var db = new UserContext())
            {
                retVal = db.forum.Include(t => t.User).Include(t => t.Location).Where(t => t.User.Id == id).ToList();
            }

            return retVal;
        }

        public Forum GetByCity(string city)
        {
            Forum retVal = new();

            using (UserContext db = new())
            {
                try
                {
                    retVal = db.forum.Include(t => t.User).Include(t => t.Location).Where(t => t.Location.City.Equals(city)).First();
                }
                catch(System.InvalidOperationException)
                {
                    MessageBox.Show("There are no forums with selected location");
                    return null;
                }
            }

            return retVal;
        }

        public void UpdateNumberOfSpecials(Forum forum)
        {
            using (var db = new UserContext())
            {
                foreach (Forum f in db.forum)
                {
                    if (forum.Id == f.Id)
                    {
                        ++f.NumOfSpecials;
                        if (f.NumOfSpecials > 19) f.Special = '*';

                        db.SaveChanges();
                        return;
                    }
                }
            }

        }

        public void UpdateActivity(Forum forum)
        {
            using (var db = new UserContext())
            {
                Forum f = db.forum.Find(forum.Id);
                f.IsClosed = true;
                db.SaveChanges();
            }
        }

        public List<Forum> GetAll()
        {
            List<Forum> forums = new List<Forum>();

            using (UserContext db = new())
            {
                forums = db.forum
                    .Include(t => t.Location)
                    .ToList();
            }

            return forums;
        }



    }
}
