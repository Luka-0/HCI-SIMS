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
using Microsoft.EntityFrameworkCore.Metadata;

namespace InitialProject.Service;

public class TourRequestService
{
    private readonly ITourRequestRepository _tourRequestRepository;
    private readonly SuperGuideService _superGuideService = new SuperGuideService(new SuperGuideRepository());
    private readonly TourReviewService _= new TourReviewService( new TourReviewRepository());

    public TourRequestService(ITourRequestRepository repository)
    {
        _tourRequestRepository= repository;
    }

    public List<TourRequest> GetAllPending()
    {
        return _tourRequestRepository.GetAllPending();
        

    }

    public void UpdateComplexTourRequest(TourRequest request, ComplexTourRequest complexTourRequest)
    {
        _tourRequestRepository.UpdateComplexTourRequest(request, complexTourRequest);
    }

    public TourRequest GetById(int id)
    {
        return _tourRequestRepository.GetById(id);
    }

    public List<TourRequest> GetAll()
    {
        return _tourRequestRepository.GetAll();
    }

    public List<Location> GetLocations()
    {
        List<Location> locations = new List<Location>();
        List<TourRequest> tourRequests = _tourRequestRepository.GetAll();

        foreach (TourRequest request in tourRequests)
        {
            locations.Add(request.Location);
        }
        locations = locations.Distinct().ToList();

        return locations;
    }

    public List<string> GetLanguages()
    {
        List<string> languages = new List<string>();
        List<TourRequest> tourRequests = _tourRequestRepository.GetAll();

        foreach (TourRequest request in tourRequests)
        {
            languages.Add(request.Language);
        }

        languages = languages.Distinct().ToList();

        return languages;
    }

    public int CountByLanguage(string language)
    {
        int count = 0;
        List<TourRequest> tourRequests = GetAll();

        foreach (TourRequest request in tourRequests)
        {
            if(request.Language.Equals(language))
            {
                count++;
            }
        }

        return count;
    }

    public int CountByLocation(Location location)
    {
        int count = 0;
        List<TourRequest> tourRequests = GetAll();

        foreach (TourRequest request in tourRequests)
        {
            if (request.Location.Id == location.Id)
            {
                count++;
            }
        }

        return count;
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

    public void Save(TourRequest request, User user, ComplexTourRequest complexTourRequest)
    {
        _tourRequestRepository.Save(request, user, complexTourRequest);
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

    public Dictionary<string, int> GetRequestCountByLanguage()
    {
        Dictionary<string, int> requestData = new Dictionary<string, int>();
        List<string> languages = GetLanguages();

        foreach(string language in languages)
        {
            requestData.Add(language, CountByLanguage(language));
        }

        return requestData;
    }

    public Dictionary<string, int> GetRequestCountByLocation()
    {
        Dictionary<string, int> requestData = new Dictionary<string, int>();
        List<Location> locations = GetLocations();

        foreach (Location location in locations)
        {
            requestData.Add(location.ToString(), CountByLocation(location));
        }

        return requestData;
    }

    public List<TourRequest> GetAllPendingByComplex(int id)
    {
        return _tourRequestRepository.GetAllPendingByComplex(id);
    }

    public  List<DateTime> GetOccupiedDatesForComplexTour(ComplexTourRequest request)
    {
        List<TourRequest> requests= _tourRequestRepository.GetOccupiedDatesForComplexTour(request);
        List<DateTime> dates = new List<DateTime>();
            
        foreach (TourRequest r in requests)
        {
            dates.Add((DateTime)r.SelectedDate);
        }
        return dates;
    }
}