using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service;

public class UserService
{
    public User GetBy(int id)
    {
        return UserRepository.GetBy(id);
    }
}