using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IUserRepository
    {
        public User GetBy(String username);
        public void UpdateStatusBy(String username, bool titleFlag);
        public Boolean Add(User user);
        public User GetBy(int id);
        public List<User> GetOwners();

        }
}
