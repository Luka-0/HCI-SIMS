using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using InitialProject.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Repository
{
    public class UserRepository
    {
        public UserRepository() { }

        public static User Get(String username)
        {
            using var db = new UserContext();
            foreach (User user in db.users)
            {
                if (user.Username.Equals(username))
                {
                    return user;
                }
            }
            return null;
        }

        public static Boolean Add(User user)
        {

            using var db = new UserContext();
            db.Add(user);
            db.SaveChanges();

            return true;
        }
        public static User GetBy(int id)
        {
            using var db = new UserContext();
            List<User> users = db.users.ToList();
            return users.Find(user => user.Id == id);

        }


    }

}
