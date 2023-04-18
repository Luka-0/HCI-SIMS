using System.Collections.Generic;
using System.Collections.ObjectModel;
using InitialProject.Model;

namespace InitialProject.Interface;

public interface ITourRequestRepository
{
    public List<TourRequest> GetAllPending();
    public List<TourRequest> GetAll();
}