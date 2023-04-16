using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InitialProject.Contexts;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.Repository;

public class TourRequestRepository: ITourRequestRepository
{
    public List<TourRequest> GetAllPending()
    {
        using (var db = new UserContext())
        {
            return db.tourRequest.Where(tr=>tr.State == TourRequestState.Pending)
                .Include(tr=>tr.Location)
                .ToList();
        }
    }
}