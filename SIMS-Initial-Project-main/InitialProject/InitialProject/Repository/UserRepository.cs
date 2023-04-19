using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using InitialProject.Contexts;
using Microsoft.EntityFrameworkCore;
using InitialProject.Interface;
using InitialProject.Enumeration;

namespace InitialProject.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() { }

        public  User GetBy(String username)
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

        public void UpdateStatusBy(String username, bool titleFlag) {

            using var db = new UserContext();
            foreach (User user in db.users)
            {
                if (user.Username.Equals(username))
                {
                    user.SuperTitle = titleFlag;
                    db.SaveChanges();
                }
            }
        }

        public  Boolean Add(User user)
        {
            using var db = new UserContext();
            db.Add(user);
            db.SaveChanges();

            return true;
        }
        public  User GetBy(int id)
        {
            using var db = new UserContext();
            List<User> users = db.users.ToList();
            return users.Find(user => user.Id == id);

        }

        public List<User> GetOwners() {

            List<User> owners = new List<User>();

            using var db = new UserContext();
            foreach (User user in db.users)
            {
                if (user.Type == UserType.Owner)
                {
                    owners.Add(user);
                }
            }
            return owners;
        }

        public void UpdateBy(int id, bool superTitle, int bonusPoints)
        {
            using var db = new UserContext();

            User user = GetBy(id);

            var entityToUpdate = db.users.Find(user.Id);

            if (entityToUpdate != null)
            {
                entityToUpdate.SuperTitle = superTitle;
                entityToUpdate.BonusPoints = bonusPoints;
                db.SaveChanges();
            }
        }

        public void UpdateBy(int id, bool superTitle, int bonusPoints, DateTime validTill)
        {
            using var db = new UserContext();

            User user = GetBy(id);

            var entityToUpdate = db.users.Find(user.Id);

            if (entityToUpdate != null)
            {
                entityToUpdate.SuperTitle = superTitle;
                entityToUpdate.BonusPoints = bonusPoints;
                entityToUpdate.SuperTitleValidTill = validTill;
                db.SaveChanges();
            }
        }

    }

}
