using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Repository;

public class SuperGuideRepository: ISuperGuideRepository

{
    public void Add(SuperGuide superGuide)
    {
        using (var db = new UserContext())
        {
            db.Entry(superGuide).State = EntityState.Added;
            db.SuperGuide.Add(superGuide);
            db.SaveChanges();
        }
    }



}