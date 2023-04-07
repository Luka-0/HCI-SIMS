﻿using InitialProject.Dto;
using InitialProject.Interface;
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
        private readonly AccommodationReservationService AccommodationReservationService = new(new AccommodationReservationRepository());

        public AccommodationReservation GetBy(int id)
        {
            return AccommodationReservationService.GetBy(id);
        }

        public List<StartEndDateDto> GetAvailableDates(Accommodation accommodation, int guestNumber, DateTime startingDate, DateTime endingDate, int daysToStay)
        {
            return AccommodationReservationService.GetAvailableDates(accommodation, guestNumber, startingDate, endingDate, daysToStay);
        }

        public void Add(AccommodationReservation accommodationReservation)
        {
            AccommodationReservationService.Add(accommodationReservation);
        }

        public List<AccommodationReservation> GetByAccommodation(int id)
        {
            return AccommodationReservationService.GetByAccommodation(id);
        }

        public List<AccommodationReservation> GetBy(User user)
        {
            return AccommodationReservationService.GetBy(user);
        }
    }
}
