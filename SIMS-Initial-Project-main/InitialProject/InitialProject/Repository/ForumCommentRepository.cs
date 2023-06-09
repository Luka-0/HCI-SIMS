using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        public ForumCommentRepository() { }

        public void Save(ForumComment forumComment)
        {
            using var db = new UserContext();

            var existingForum = db.forum.Find(forumComment.Forum.Id);
            var existingUser = db.users.Find(forumComment.User.Id);

            forumComment.Forum = existingForum;
            forumComment.User = existingUser;

            db.forum.Attach(existingForum);
            db.users.Attach(existingUser);

            db.Add(forumComment);
            db.SaveChanges();
        }

        public List<ForumComment> GetByForum(int id)
        {
            List<ForumComment> retVal = new();

            using (UserContext db = new())
            {
                retVal = db.forumComment.Include(t => t.Forum).Where(t => t.Forum.Id == id).ToList();
            }

            return retVal;
        }

        public List<ForumComment> GetAll()
        {
            List<ForumComment> forums = new List<ForumComment>();

            using (UserContext db = new())
            {
                forums = db.forumComment
                    .Include(t => t.Forum)
                    .ToList();
            }
            return forums;
        }

        public void SaveComment(ForumComment comment)
        {
            using UserContext db = new();

            db.ChangeTracker.TrackGraph(comment, node =>
            node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

            db.SaveChanges();

            IsForumUseful(comment.Forum.Id);
        }

        public void IsForumUseful(int id)
        {
            List<ForumComment> coms = new List<ForumComment>();
            Forum forum;
            using (UserContext db = new())
            {
                coms = db.forumComment
                    .Include(t => t.Forum)
                    .ToList();


                int countOwnerComm = 0;
                int countCom = 0;

                foreach (var c in coms)
                {

                    if (c.Forum.Id == id && c.Special=='*')
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


                    forum = db.forum.Find(id);

                    forum.Useful = true;
                    db.SaveChanges();
                    return;
                }


                forum = db.forum.Find(id);

                forum.Useful = false; ;
                db.SaveChanges();
            }
        }
    }
}
