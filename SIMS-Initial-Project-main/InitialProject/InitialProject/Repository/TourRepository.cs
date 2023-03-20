﻿using InitialProject.Contexts;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        //public TourRepository() { }

        public List<Tour> GetAll()
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour.Include(t => t.Location).ToList();
            }
            return Tours;
        }

        public Tour GetById(int id)
        {
            Tour tour = new Tour();

            using (var dbContext = new UserContext())
            {
                tour = (Tour)dbContext.tour
                                 .Where(t => t.Id == id);
            }
            return tour;
        }

        public List<Tour> GetBy(Location location)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Where(t => t.Location == location)
                                 .ToList();
            }
            return Tours;
        }

        public List<Tour> GetBy(TimeOnly duration)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Where(t => t.Duration == duration)
                                 .ToList();
            }
            return Tours;
        }

        public List<Tour> GetBy(string language)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Where(t => t.Language == language)
                                 .ToList();
            }
            return Tours;
        }

        public List<Tour> GetBy(int guestNumber)
        {
            List<Tour> Tours = new List<Tour>();

            using (var dbContext = new UserContext())
            {
                Tours = dbContext.tour
                                 .Where(t => t.GuestLimit >= guestNumber)
                                 .ToList();
            }
            return Tours;
        }



    }
}
