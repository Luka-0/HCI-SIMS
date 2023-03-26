using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Contexts;

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

    }

}
