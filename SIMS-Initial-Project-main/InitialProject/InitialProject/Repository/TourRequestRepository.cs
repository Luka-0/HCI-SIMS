using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Repository;

public class TourRequestRepository: ITourRequestRepository
{
    public List<TourRequest> GetAllPending()
    {
        using var db = new UserContext();
        return db.tourRequest.Where(tr=>tr.State == TourRequestState.Pending)
            .Include(tr=>tr.Location)
            .ToList();
    }

    public List<TourRequest> GetAll()
    {
        using var db = new UserContext();
        return db.tourRequest.Include(t=>t.Location).ToList();
    }

    public void Accept(int id, DateTime selectedDate)
    {
        using var db = new UserContext();
        TourRequest request =  db.tourRequest.Find(id);
        request.State = TourRequestState.Accepted;
        request.SelectedDate = selectedDate;
        db.SaveChanges();
    }

    public List<string> GetLanguages()
    {
        using var db = new UserContext();
        return db.tourRequest.Select(tr=>tr.Language).ToList();
    }

    public TourRequest GetById(int id)
    {
        TourRequest tourRequest = new TourRequest();

        using (var dbContext = new UserContext())
        {
            tourRequest = (TourRequest)dbContext.tourRequest
                            .Include(t => t.Location)
                            .Include(t => t.Tourist)
                            .Where(t => t.Id == id)
                            .SingleOrDefault();

        }
        return tourRequest;
    }

    /*
    public void Save(TourRequest tourRequest)
    {
        using var db = new UserContext();
        
        db.Add(tourRequest);
        db.SaveChanges();
    }*/

    public void Save(TourRequest request, User user)
    {
        using var db = new UserContext();

        var existingLocation = db.location.Find(request.Location.Id);
        var existingUser = db.users.Find(user.Id);

        request.Tourist = existingUser;
        request.Location = existingLocation;

        db.location.Attach(existingLocation);
        db.users.Attach(existingUser);

        db.Add(request);

        db.SaveChanges();

    }

    public List<TourRequest> GetAllPendingByComplex(int id)
    {
        using var db = new UserContext();
        return db.tourRequest.Where(tr => (tr.State == TourRequestState.Pending) && (tr.ComplexTourRequest.Id == id))
            .Include(tr => tr.Location)
            .ToList();
    }




}