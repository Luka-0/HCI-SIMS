using System.Collections.Generic;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;

namespace InitialProject.Controller;

public class ComplexTourRequestController
{
    private ComplexTourRequestService complexTourRequestService = new ComplexTourRequestService(new ComplexTourRequestRepository());


    public List<ComplexTourRequest> GetAllPending()
    {
        return complexTourRequestService.GetAllPending();

    }

    public void SetGuide(int id, User loggedInGuide)
    {
        complexTourRequestService.SetGuide(id, loggedInGuide);
    }
}