using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;

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

    public List<User> GetOwners(){

        return this.IUserRepository.GetOwners();
    }

    public void UpdateBy(int id, bool superTitle, int bonusPoints)
    {
        IUserRepository.UpdateBy(id, superTitle, bonusPoints);
    }

    public void UpdateBy(int id, bool superTitle, int bonusPoints, DateTime validTill)
    {
        IUserRepository.UpdateBy(id, superTitle, bonusPoints, validTill);
    }

    public void Delete(User loggedInGuide)
    {
        IUserRepository.Delete(loggedInGuide);
    }
}