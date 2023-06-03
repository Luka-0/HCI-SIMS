using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Interface;

namespace InitialProject.Controller
{
    public class ForumController
    {
        private readonly ForumService ForumService = new(new ForumRepository());

        public void Save(Forum forum)
        {
            ForumService.Save(forum);
        }

        public Forum GetByUser(int id)
        {
            return ForumService.GetByUser(id);
        }

        public Forum GetByCity(string city)
        {
            return ForumService.GetByCity(city);
        }
    }
}
