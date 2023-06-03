using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InitialProject.Model;

namespace InitialProject.Interface;

public interface ITourRequestRepository
{
    public List<TourRequest> GetAllPending();
    public List<TourRequest> GetAll();
    public void Accept(int id, DateTime selectedDate);
    public List<string> GetLanguages();
    public void Save(TourRequest request, User user);
    public List<TourRequest> GetAllPendingByComplex(int id);

}