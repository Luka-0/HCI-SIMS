﻿using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourReservationService
    {
        private readonly VoucherService voucherService= new VoucherService(new VoucherRepository());

        private readonly ITourReservationRepository _tourReservationRepository;

        
        public TourReservationService(ITourReservationRepository repository)
        {
            _tourReservationRepository = repository;
        }

        private TourReservationRepository tourReservationRepository = new TourReservationRepository();
        private TourRepository tourRepository = new TourRepository();
        public List<TourReservation> GetAll()
        {
            return tourReservationRepository.GetAll();
        }

        public List<TourReservation> GetByTour(Tour tour)
        {
            return tourReservationRepository.GetByTour(tour);
        }

        public List<TourReservation> GetByGuest(User user)
        {
            return _tourReservationRepository.GetByGuest(user);
        }

        

        public TourReservation GetById(int id)
        {
            return tourReservationRepository.GetById(id);
        }

        public void Save(TourReservation reservation, Tour tour, User guest, int guestNumber)
        {
            tourReservationRepository.Save(reservation, tour, guest, guestNumber);
        }

        //TODO ispravi
        public bool IsReserved(Tour tour)
        {
            List<TourReservation> reservations = tourReservationRepository.GetByTour(tour);
            if(reservations.Find(r => r.Tour.Id == tour.Id) != null)
            {
                return true;
            }
            return false;
        }

        public int CountGuestsOnTour(Tour tour)
        {
            List<TourReservation> reservations = tourReservationRepository.GetByTour(tour);
            return reservations.Where(r => r.Tour.Id == tour.Id).Sum(r => r.GuestNumber);
        }

        public int CountTourReservations(Tour tour)
        {
            return tourReservationRepository.GetByTour(tour).Count();
        }

        public bool IsFull(Tour tour)
        {
            if(tour.GuestLimit == CountGuestsOnTour(tour))
                return true;
            else 
                return false;
        }

        public int GetAvailableSpace(Tour tour)
        {
            return tour.GuestLimit - CountGuestsOnTour(tour);
        }

        public List<TourReservation> GetByTour(int id)
        {
            return _tourReservationRepository.GetByTour(id);
        }

        public void SetArrivalKeyPoint(TourKeyPoint keyPoint, int id)
        {
            _tourReservationRepository.SetArrivalKeyPoint(keyPoint, id);
        }

        public TourReservation GetByPAVLE(int id)
        {
            return _tourReservationRepository.GetByPAVLE(id);
        }

        public string GetArrivalKeyPointName(int id)
        {
            TourReservation reservation = GetByPAVLE(id);
            return reservation.ArrivalPoint.Name;
        }

        public void GiveOutVouchers(int id)
        {
            List<TourReservation> reservations = GetByTour(id);

            List<User?> users = reservations.Select(reservation => reservation.BookingGuest).ToList();

            voucherService.GiveOut(users);
           // _tourReservationRepository.Cancel(reservations);
        }

    }
}
