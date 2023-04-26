﻿using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Repository
{
    public class AccommodationReviewRepository : IAccommodationReviewRepository
    {
        public AccommodationReviewRepository() { }

        public List<AccommodationReview> GetAllBy(User owner)
        {
            List<AccommodationReview> accommodationReviews = new List<AccommodationReview>();

            using (UserContext db = new())
            {
                accommodationReviews = db.accommodationReview.
                    Include(t => t.Reservation)
                        .ThenInclude(t => t.Accommodation)
                            .ThenInclude(t => t.Owner).Where(t => t.Reservation.Accommodation.Owner.Equals(owner))
                    .Include(t => t.Reservation)
                        .ThenInclude(t => t.Guest).ToList();
            }

            return accommodationReviews;
        }

        public void Save(AccommodationReview accommodationReview)
        {
            using var db = new UserContext();

            var existingLocation = db.location.Find(accommodationReview.Reservation.Accommodation.Location.Id);
            var existingAccommodation = db.accommodation.Find(accommodationReview.Reservation.Accommodation.Id);
            var existingAccommodationReservation = db.accommodationReservation.Find(accommodationReview.Reservation.Id);

            accommodationReview.Reservation.Accommodation.Location = existingLocation;
            accommodationReview.Reservation.Accommodation = existingAccommodation;
            accommodationReview.Reservation = existingAccommodationReservation;

            db.location.Attach(existingLocation);
            db.accommodation.Attach(existingAccommodation);
            db.accommodationReservation.Attach(existingAccommodationReservation);

            db.Add(accommodationReview);
            db.SaveChanges();
        }

        public List<AccommodationReview> GetBy(User user)
        {
            List<AccommodationReview> accommodationReviews = new();

            /*using var db = new UserContext();

            foreach(AccommodationReview ar in db.accommodationReview)
            {
                if(ar.Reservation != null && ar.Reservation.Guest.Id != user.Id)
                {
                    accommodationReviews.Add(ar);
                }
            }
            */

            using(var db = new UserContext())
            {
                accommodationReviews = db.accommodationReview//.Where(t => t.Reservation.Guest.Id == user.Id)
                                                             .Include(t => t.Reservation)
                                                             .ToList();
            }

            return accommodationReviews;
        }

    }
}
