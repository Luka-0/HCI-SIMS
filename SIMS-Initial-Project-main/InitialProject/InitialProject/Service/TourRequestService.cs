using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    public Location GetHottestLocation(string country, string city)
    {
        List<TourRequest> requests = GetAll().Where(r=> (DateTime.Today - r.LowerDateLimit.Date) <=  new TimeSpan(365,0,0,0)).ToList();
        //milsim da u tourRequesttreba datum da bude za koji je prihvacen zahtev
        return new Location();
    }

    public void Accept(int id, DateTime selectedDate)
    {
        _tourRequestRepository.Accept(id, selectedDate);
    }
}