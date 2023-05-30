using System.Collections.Generic;
using InitialProject.Model;

namespace InitialProject.Interface;

public interface IComplexTourRequestRepository
{
    public List<ComplexTourRequest> GetAllPending();

    public void SetGuide(int id, User loggedInGuide);
}