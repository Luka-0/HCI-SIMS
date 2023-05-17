using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using InitialProject.Dto;
using InitialProject.Enumeration;
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

    public List<TourRequest> GetByState(TourRequestState state)
    {
        List<TourRequest> allRequests = _tourRequestRepository.GetAll();
        List<TourRequest> searchedRequests = new List<TourRequest>();

        foreach (TourRequest request in allRequests)
        {
            if(request.State == state)
            {
                searchedRequests.Add(request);
            }
        }
        return searchedRequests;
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

    public void Save(TourRequest request, User user)
    {
        _tourRequestRepository.Save(request, user);
    }

    public string[] GetStatistics()
    {
        string[] statistics = new string[3];

        int countRequests = _tourRequestRepository.GetAll().Count;
        int countAccepted = GetByState(TourRequestState.Accepted).Count();
        int countUnaccepted = countRequests - countAccepted;

        double acceptedPercentage = Math.Round(((double)countAccepted) / (double)countRequests * 100, 2);
        double unacceptedPercentage = Math.Round(100 - acceptedPercentage, 2);

        statistics[0] = acceptedPercentage.ToString();
        statistics[1] = unacceptedPercentage.ToString();

        int acceptedGuests = 0;
        List<TourRequest> requests = GetByState(TourRequestState.Accepted).ToList();
        foreach (TourRequest request in requests)
        {
            acceptedGuests += request.GuestNumber;
        }

        statistics[2] = acceptedGuests.ToString();

        return statistics;
    }

}