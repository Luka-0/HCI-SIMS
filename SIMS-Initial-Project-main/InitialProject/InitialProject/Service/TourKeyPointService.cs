using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Windows;
using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Interface;
using InitialProject.Migrations;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service;

public class TourKeyPointService
{
    private readonly TourService tourService = new TourService(new TourRepository());
    private readonly ITourKeyPointRepository _tourKeyPointsRepository;

    public List<TourKeyPoint> GetAll()
    {
        return _tourKeyPointsRepository.GetAll();
    }

    public List<TourKeyPoint> GetByTour(Tour tour)
    {
        return _tourKeyPointsRepository.GetByTour(tour);
    }
    public TourKeyPointService(ITourKeyPointRepository repository)
    {
        _tourKeyPointsRepository = repository;
    }

    public TourKeyPoint GetById(int id)
    {
        return _tourKeyPointsRepository.GetById(id);
    }
    public List<TourKeyPoint> GetByTourKeyPointNames(List<string> names)
    {

        return _tourKeyPointsRepository.GetByTourKeyPointNames(names);
    }

    public TourKeyPointType GetTypesByIndex(List<String> names, String currentName)
    {
        String first= names.First();
        String last = names.Last();

        if (currentName.Equals(first))
        {
            return TourKeyPointType.Start;
        }

        if (currentName.Equals(last))
        {
            return TourKeyPointType.End;
        }
        return TourKeyPointType.Mid;
    }

    public void Save(TourKeyPoint tourKeyPoint, TourKeyPointType type)
    {
        _tourKeyPointsRepository.Save(tourKeyPoint, type);
    }

    public  List<TourKeyPoint> Save(List<string> keyPointNames)
    {
        List<TourKeyPoint> tourKeyPoints= new List<TourKeyPoint>();
        foreach (string name in keyPointNames)
        {

            TourKeyPoint tourKeyPoint= new TourKeyPoint(name);
            TourKeyPointType type = GetTypesByIndex(keyPointNames, name);
            _tourKeyPointsRepository.Save(tourKeyPoint, type);
            TourKeyPoint keyPointFromDb = _tourKeyPointsRepository.GetById(tourKeyPoint.Id);
            tourKeyPoints.Add(keyPointFromDb);

        }

        return tourKeyPoints;
    }

    public void Update(List<TourKeyPoint> tourKeyPoints, Tour tour)
    {
        {
            // foreach (var keyPoint in tourKeyPoints)
            // {
            //     _tourKeyPointsRepository.Update(keyPoint,tour);
            // }
            tourKeyPoints.ForEach(keyPoint => _tourKeyPointsRepository.Update(keyPoint, tour));
        }
    }

    public void SetTypes(List<TourKeyPoint> tourKeyPoints)
    {
        TourKeyPoint start = tourKeyPoints.First();
        TourKeyPoint end = tourKeyPoints.Last();
        _tourKeyPointsRepository.SetType(start.Id, end.Id);

    }

    public List<TourAndKeyPointsDto> GetByGuestAndActiveTour(User user)
    {
        List<TourAndKeyPointsDto> tourAndKeyPointsDtos = new List<TourAndKeyPointsDto> ();
        List<Tour> allActiveToursByGuest = tourService.GetAllActiveByGuest(user);

        foreach(Tour tour in allActiveToursByGuest)
        {
            tourAndKeyPointsDtos.Add(new TourAndKeyPointsDto(tour, GetByTour(tour)));
        }
        return tourAndKeyPointsDtos;
    }
    
    public void StartTour(int id)
    {
        _tourKeyPointsRepository.StartTour(id);
    }

    public void Reach(int id)
    {
        _tourKeyPointsRepository.Reach(id);
    }


    public List<TourKeyPoint> Create(List<string> tourKeyPointNames, Tour tour)
    {
        List<TourKeyPoint> keyPoints = Save(tourKeyPointNames);
        Update(keyPoints, tour);
        return GetAllByTour(tour.Id);
    }

    private List<TourKeyPoint> GetAllByTour(int id)
    {
       return _tourKeyPointsRepository.GetAllByTour(id);
    }
}