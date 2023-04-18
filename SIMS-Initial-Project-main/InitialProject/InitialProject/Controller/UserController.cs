using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class UserController
    {
        private readonly UserService UserService = new(new UserRepository());

        public User GetBy(int id)
        {
            return UserService.GetBy(id);
        }

        public void UpdateStatusBy(String username, bool titleFlag)
        {

            UserService.UpdateStatusBy(username, titleFlag);
        }
        public Boolean Add(User user)
        {

            return this.UserService.Add(user);
        }

        public User GetBy(String username)
        {

            return this.UserService.GetBy(username);
        }

        
    }
}
