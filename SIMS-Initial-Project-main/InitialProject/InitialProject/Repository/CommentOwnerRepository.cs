using InitialProject.Contexts;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public  class CommentOwnerRepository
    {
        ForumOwnerRepository ForumOwnerRepository =  new    ForumOwnerRepository();


        public List<ForumOwnerVs> GetOpenForums()
        {

            List<Location> locations = new();
            List<ForumOwnerVs> openForums = new();

            locations = ForumOwnerRepository.GetOwnerAccommodationLocations("owner");

            foreach (var l in locations)
            {

                foreach (var f in ForumOwnerRepository.GetAll())
                {
                    if (l.City.Equals(f.Location.City))
                    {

                        openForums.Add(f);
                    }
                }
            }

            return openForums;
        }

        public void Save(CommentOwnerVs commentOwnerVs)
        {
            using UserContext db = new();   

            db.ChangeTracker.TrackGraph(commentOwnerVs, node =>
            node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);
           
            db.SaveChanges();

            IsForumUseful(commentOwnerVs.Forum.Id);
        }

        public List<CommentOwnerVs> GetAll()
        {
            List<CommentOwnerVs> forums = new List<CommentOwnerVs>();

            using (UserContext db = new())
            {
                forums = db.CommentOwnerVs
                    .Include(t => t.Forum)
                    .ToList();
            }

            return forums;
        }

        public void IsForumUseful(int id)
        {
            List<CommentOwnerVs> coms = new List<CommentOwnerVs>();
            ForumOwnerVs forum;
            using (UserContext db = new())
            {
                coms = db.CommentOwnerVs
                    .Include(t => t.Forum)
                    .ToList();


                int countOwnerComm = 0;
                int countCom = 0;

                foreach (var c in coms)
                {

                    if (c.Forum.Id == id && c.ByOwner.Equals("*"))
                    {

                        countOwnerComm++;
                    }
                    if (c.Forum.Id == id)
                    {

                        countCom++;
                    }
                }

                if (countOwnerComm >= 2 && countCom >= 0)
                {


                    forum = db.ForumOwnerVs.Find(id);

                    forum.VeyUseful = true;
                    db.SaveChanges();
                    return;
                }


                 forum = db.ForumOwnerVs.Find(id);

                forum.VeyUseful = false; ;
                db.SaveChanges();
            }
        }
    }
}
