using System.Collections.Generic;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service;

public class ComplexTourRequestService
{
    private readonly IComplexTourRequestRepository _complexTourRequestRepository;
    private readonly TourRequestService tourRequestService = new TourRequestService(new TourRequestRepository());

    public ComplexTourRequestService(IComplexTourRequestRepository repository)
    {
        _complexTourRequestRepository = repository;
    }

    public List<ComplexTourRequest> GetAllPending()
    {
        List<ComplexTourRequest> complexRequests = _complexTourRequestRepository.GetAllPending();
        foreach (ComplexTourRequest complexRequest in complexRequests)
        {
            complexRequest.Requests = tourRequestService.GetAllPendingByComplex(complexRequest.Id);
        }
        return complexRequests;
        
    }

    public void SetGuide(int id, User loggedInGuide)
    {
        _complexTourRequestRepository.SetGuide(id, loggedInGuide);
    }

    public ComplexTourRequest Save(ComplexTourRequest complexTour)
    {
        return _complexTourRequestRepository.Save(complexTour);
    }

    public ComplexTourRequest GetById(int id)
    {
        return _complexTourRequestRepository.GetById(id);
    }

    public List<ComplexTourRequest> GetAll()
    {
        return _complexTourRequestRepository.GetAll();
    }

}