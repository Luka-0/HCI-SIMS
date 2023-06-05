using InitialProject.Interface;
using InitialProject.Model;
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

        public ForumCommentService(IForumCommentRepository forumCommentRepository)
        {
            _forumCommentRepository = forumCommentRepository;
        }

        public void Save(ForumComment forumComment)
        {
            _forumCommentRepository.Save(forumComment);
        }

        public List<ForumComment> GetByForum(int id)
        {
            return _forumCommentRepository.GetByForum(id);
        }

    }
}
