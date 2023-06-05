﻿using InitialProject.Interface;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class ForumService
    {
        private readonly IForumRepository _forumRepository;

        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public void Save(Forum forum)
        {
            _forumRepository.Save(forum);
        }

        public List<Forum> GetByUser(int id)
        {
            return _forumRepository.GetByUser(id);
        }

        public Forum GetByCity(string city)
        {
            return _forumRepository.GetByCity(city);
        }

        public void UpdateNumberOfSpecials(Forum forum)
        {
            _forumRepository.UpdateNumberOfSpecials(forum);
        }

        public void UpdateActivity(Forum forum)
        {
            _forumRepository.UpdateActivity(forum);
        }
    }
}
