using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    interface IForumRepository
    {
        public void Save(Forum forum);
        public Forum GetByUser(int id);
        public Forum GetByCity(string city);
    }
}
