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

    }
}
