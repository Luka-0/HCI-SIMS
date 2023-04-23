using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InitialProject.Dto;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service;

public class TourRequestService
{
    private readonly ITourRequestRepository _tourRequestRepository;

    public TourRequestService(ITourRequestRepository repository)
    {
        _tourRequestRepository= repository;
    }

    public List<TourRequest> GetAllPending()
    {
        return _tourRequestRepository.GetAllPending();
        

    }

    public List<TourRequest> GetAll()
    {
        return _tourRequestRepository.GetAll();
    }

    public void Accept(int id, DateTime selectedDate)
    {
        _tourRequestRepository.Accept(id, selectedDate);
    }

    public string GetHottestLanguage()
    {
      List<TourRequest> requests =  GetLastYearsRequests();


      Dictionary<string, int> ratings = new Dictionary<string, int>();

        foreach (TourRequest request in requests)
        {
            if (!ratings.ContainsKey(request.Language))
            {
                ratings.Add(request.Language, 1);
            }
            else
            {
                ratings[request.Language]++;
            }
        }
        return ratings.MaxBy(x => x.Value).Key;
    }
    public Location GetHottestLocation()
    {
        List<TourRequest> requests = GetLastYearsRequests();

        Dictionary<Location, int> ratings = new Dictionary<Location, int>();

        foreach (TourRequest request in requests)
        {
            if (!ratings.ContainsKey(request.Location))
            {
                ratings.Add(request.Location, 1);
            }
            else
            {
                ratings[request.Location]++;
            }
        }

        return ratings.MaxBy(x => x.Value).Key;
    }

    private List<TourRequest> GetLastYearsRequests()
    {

        return GetAll().Where(r => (DateTime.Today - r.SelectedDate) <= new TimeSpan(365, 0, 0, 0)).ToList();

    }
}