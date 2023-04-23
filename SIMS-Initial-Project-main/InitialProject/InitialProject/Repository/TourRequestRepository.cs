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
}