﻿using InitialProject.Contexts;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class AccommodationServicecs
    {
        public static void Save(NewAccommodationDto record)
        {
            //Saving new accommodation into databse
            Accommodation accommodation = new Accommodation(record.Title, record.GuestLimit, record.Type, record.MinimumReservationDays, record.CancellationDeadline);
            AccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var tempRecord = db.accommodation.Find(accommodation.Id);   //Try creating method in Accommodation repository to return the same thing

            //Updating foreign key value of new accommodation record
            tempRecord.Location = LocationService.getBy(record.CityName);
            
            //saving all images refered to new accommodation.
            ImageService.Save(record.Images, accommodation);

            db.SaveChanges();
        }

    }
}
