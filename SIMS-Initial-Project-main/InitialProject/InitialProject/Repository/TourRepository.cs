using InitialProject.Contexts;
using InitialProject.Model;
using System;

namespace InitialProject.Repository;

public class TourRepository
{
    public void save(Tour tour)
    {

        using (var db = new UserContext()) {
            db.Add(tour);
            db.SaveChanges();
        }

}
}