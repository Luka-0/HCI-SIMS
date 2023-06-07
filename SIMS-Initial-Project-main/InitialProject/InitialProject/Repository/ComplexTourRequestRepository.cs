using System.Collections.Generic;
using System.Linq;
using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

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

    public void SetGuide(int id, Model.User Guide)
    {
        using (var db = new UserContext())
        {
            ComplexTourRequest request   = 
            db.ComplexTourRequest.Where(tr => tr.Id == id).SingleOrDefault();

            request.Guides.Add(Guide);
            db.SaveChanges();

        }
    }

    public ComplexTourRequest Save(ComplexTourRequest complexTour)
    {
        using var db = new UserContext();

        // Add the new entity to the DbContext
        db.Add(complexTour);
        db.SaveChanges();

        return complexTour;

    }

    public ComplexTourRequest GetById(int id)
    {
        ComplexTourRequest tourRequest = new ComplexTourRequest();

        using (var dbContext = new UserContext())
        {
            tourRequest = (ComplexTourRequest)dbContext.ComplexTourRequest
                            .Include(tr => tr.Requests)
                            .Include(tr => tr.Guides)
                            .Where(t => t.Id == id)
                            .SingleOrDefault();

        }
        return tourRequest;
    }
}