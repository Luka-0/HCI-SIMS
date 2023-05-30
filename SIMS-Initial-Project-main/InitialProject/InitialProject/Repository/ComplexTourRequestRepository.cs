using System.Collections.Generic;
using System.Linq;
using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Repository;

public class ComplexTourRequestRepository: IComplexTourRequestRepository
{
    public List<ComplexTourRequest> GetAllPending()
    {
        using (var db = new UserContext())
        {
            return db.ComplexTourRequest.Where(tr=> tr.State.Equals(TourRequestState.Pending))
                .Include(tr=>tr.Requests)
                .Include(tr=> tr.Guides)
                .ToList();
        }
    }

    public void SetGuide(int id, User Guide)
    {
        using (var db = new UserContext())
        {
            ComplexTourRequest request   = 
            db.ComplexTourRequest.Where(tr => tr.Id == id).SingleOrDefault();

            request.Guides.Add(Guide);
            db.SaveChanges();

        }
    }

}