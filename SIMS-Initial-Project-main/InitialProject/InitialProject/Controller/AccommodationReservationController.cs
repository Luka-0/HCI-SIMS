using InitialProject.Dto;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    internal class AccommodationReservationController
    {
        private AccommodationReservationService _service = new(new AccommodationReservationRepository());

       
    }
}
