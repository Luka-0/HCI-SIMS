using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;

namespace InitialProject.Service;

public class UserService
{
    private readonly IUserRepository IUserRepository;

    public UserService(IUserRepository iUserRepository)
    {
        this.IUserRepository = iUserRepository;
 
    }
    public User GetBy(int id)
    {
        return IUserRepository.GetBy(id);
    }

    public void UpdateStatusBy(String username, bool titleFlag) {

        IUserRepository.UpdateStatusBy(username,titleFlag);
    }
    public Boolean Add(User user){

        return this.IUserRepository.Add(user);
    }

    public User GetBy(String username) {

        return this.IUserRepository.GetBy(username);
    }


}