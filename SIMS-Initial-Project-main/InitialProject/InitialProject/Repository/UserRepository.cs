using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Contexts;

namespace InitialProject.Repository
{
    /*
    public class UserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public static void AddUserToDbl(User user)
        {
            using var db = new UserContext();
            db.Add(user);
            db.SaveChanges();
        }

        public static User getUser(String username)
        {
            using (var db = new UserContext())
            {
                foreach (User user in db.users)
                {
                    if (user.Username == username)
                    {
                        return user;
                    }
                }

            }
            return null;
        }
    }
    */


    public class UserRepository
    {
        public UserRepository() { }

        public static User GetUser(String username)
        {
            using (var db = new UserContext())
            {
                foreach (User user in db.users)
                {
                    if (user.Username == username)
                    {
                        return user;
                    }
                }

            }
            return null;
        }

        public static Boolean AddUser(User user)
        {

            using var db = new UserContext();
            db.Add(user);
            db.SaveChanges();

            return true;
        }

    }

}
