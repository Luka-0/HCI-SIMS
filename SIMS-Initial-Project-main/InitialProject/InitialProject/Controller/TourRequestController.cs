using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;

namespace InitialProject.Controller;

public class TourRequestController
{
    private TourRequestService tourRequestService = new TourRequestService(new TourRequestRepository());

    public List<TourRequest> GetAllPending()
    {
        return tourRequestService.GetAllPending();
    }

    public List<TourRequest> GetAll()
    {
        return tourRequestService.GetAll();
    }

    public List<TourRequest> GetByState(TourRequestState state)
    {
        return tourRequestService.GetByState(state);
    }

    public void Accept(int id, DateTime selectedDate)
    {
        tourRequestService.Accept(id, selectedDate);
    }

    public string GetHottestLanguage()
    {
       return tourRequestService.GetHottestLanguage();
    }

    public Location GetHottestLocation()
    {
        return tourRequestService.GetHottestLocation();
    }

    public void Save(TourRequest request, User user)
    {
        tourRequestService.Save(request, user);
    }

    public string[] GetStatistics()
    {
        return tourRequestService.GetStatistics();
    }
}