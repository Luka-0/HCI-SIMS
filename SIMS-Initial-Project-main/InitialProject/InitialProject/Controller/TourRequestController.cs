using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public void Accept(int id)
    {
        tourRequestService.Accept(id);
    }
}