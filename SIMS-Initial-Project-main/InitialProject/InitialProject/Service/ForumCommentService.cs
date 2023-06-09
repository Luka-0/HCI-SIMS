using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class ForumCommentService
    {
        private readonly IForumCommentRepository _forumCommentRepository;
        private ForumService ForumService;

        public ForumCommentService(IForumCommentRepository forumCommentRepository)
        {
            _forumCommentRepository = forumCommentRepository;
            this.ForumService = new(new ForumRepository());
        }

        public void Save(ForumComment forumComment)
        {
            _forumCommentRepository.Save(forumComment);
        }

        public List<ForumComment> GetByForum(int id)
        {
            return _forumCommentRepository.GetByForum(id);
        }

        public List<Forum> GetOpenForums()
        {

            List<Location> locations = new();
            List<Forum> openForums = new();

            locations = ForumService .GetOwnerAccommodationLocations("owner");

            foreach (var l in locations)
            {

                foreach (var f in ForumService.GetAll())
                {
                    if (l.City.Equals(f.Location.City))
                    {

                        openForums.Add(f);
                    }
                }
            }

            return openForums;
        }

        public List<ForumComment> GetAll() {
            return this._forumCommentRepository.GetAll();
        }

        public void SaveComment(ForumComment comment) {

            this._forumCommentRepository.SaveComment(comment);
        }

    }
}
