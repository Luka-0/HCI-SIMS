﻿using InitialProject.AuxiliaryClass;
using InitialProject.Contexts;
using InitialProject.Controller;
using InitialProject.Dto;
using InitialProject.Enumeration;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Service
{
    class AccommodationService
    {
        private readonly IAccommodationRepository IAccommodationRepository;
        private LocationService LocationService;
        private ImageService ImageService;
        private UserService UserService;
        private AccommodationReservationService AccommodationReservationService;

        public AccommodationService(IAccommodationRepository iAccommodationRepository)
        {
            this.IAccommodationRepository = iAccommodationRepository;
            this.LocationService = new(new LocationRepository());
            this.ImageService = new(new ImageRepository());
            this.UserService = new(new UserRepository());
            this.AccommodationReservationService = new(new AccommodationReservationRepository());

        }

        public void Save(Accommodation accommodation, string cityName, List<String> images, string ownerUsername)
        {
            //finished-->  TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserService.GetBy(ownerUsername);

            //Saving new accommodation into databse
            this.IAccommodationRepository.Save(accommodation);

            var db = new UserContext();
            var existinAccommodation = db.accommodation.Find(accommodation.Id);   //Try creating method in Accommodation repository to return the same thing

            //Updating foreign key values of new accommodation record
            existinAccommodation.Location = LocationService.GetByCity(cityName);
            existinAccommodation.Owner = owner;

            //saving all images refered to new accommodation.
            ImageService.Save(images, accommodation);

            db.SaveChanges();
        }

        public void UpdateClassBy(string ownerUsername, bool superOwner) {

            //finished--> TODO: napraviti interface za USER repository, povezati ga sa servisom i ovde pozvati taj servis
            User owner = UserService.GetBy(ownerUsername);
            
            string accommodationClass = "B";

            if (superOwner) {
                accommodationClass = "A";
            }

            this.IAccommodationRepository.UpdateClassBy(owner, accommodationClass);
        }

        public List<Accommodation> GetAllBy(string ownerUsername)
        {
            User owner = UserService.GetBy(ownerUsername);

            return IAccommodationRepository.GetAllBy(owner);
        }

        // Stajic
        public void Save(Accommodation accommodation)
        {
            IAccommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return IAccommodationRepository.GetAll();
        }

        public List<Accommodation> GetBy(string name)
        {
            return IAccommodationRepository.GetBy(name);
        }

        public List<Accommodation> GetBy(Location location)
        {
            return IAccommodationRepository.GetBy(location);
        }

        public List<Accommodation> GetByCity(string city)
        {
            return IAccommodationRepository.GetByCity(city);
        }

        public List<Accommodation> GetBy(AccommodationType accommodationType)
        {
            return IAccommodationRepository.GetBy(accommodationType);
        }

        public List<Accommodation> GetByGuestNumber(int guestNumber)
        {
            return IAccommodationRepository.GetByGuestNumber(guestNumber);
        }

        public List<Accommodation> GetByReservationDays(int reservationDays)
        {
            return IAccommodationRepository.GetByReservationDays(reservationDays);
        }

        public List<DateSuggestion> GetDateSuggestions(Accommodation accommodation, DateTime desiredStart, DateTime desiredEnd, TimeSpan desiredDuration)
        {

            List<DateSuggestion> renovationDatesSuggestions = new List<DateSuggestion>();
            List<AccommodationReservation> preservedRenovations = new List<AccommodationReservation>();

            DateTime startPoint = desiredStart;
            DateTime limit;
            DateTime endPoint;
            TimeSpan day = new System.TimeSpan(1, 0, 0, 0);

            preservedRenovations = AccommodationReservationService.GetAllByDateInterval(accommodation, desiredStart, desiredEnd);

            foreach (var reservation in preservedRenovations)
            {
                DateTime checkpoint = startPoint.Add(desiredDuration);

                if (checkpoint <= desiredEnd)
                {
                    if (startPoint < reservation.BegginingDate)
                    {                       
                         endPoint = startPoint.Add(desiredDuration);

                        if (endPoint < reservation.BegginingDate)
                        {
                            limit = reservation.BegginingDate.Subtract(day);

                            while (endPoint <= limit) {
                                //izvodljivo zbog ogranicenja u bazi da su ove rezervacije sortirane po datumu pocetka ASC
                                DateSuggestion dateSuggestion = new DateSuggestion(startPoint, endPoint);
                                renovationDatesSuggestions.Add(dateSuggestion);
                                
                                startPoint = startPoint.Add(day);
                                endPoint = endPoint.Add(day);
                            }

                            startPoint = reservation.BegginingDate;  //izjednaci se sa pocetkom rezervacije,
                                                            //da bi se sledecom selekcijom preskocila trenutna rezervacija
                        }
                        else
                        {
                            startPoint = reservation.BegginingDate; //ukoliko trajanje nije zadovoljeno, dopunimo datum da postane bas pocetak,
                                                                    //kako bi se rezervacija preskocila
                        }
                    }

                    //ukoliko je zakazana rezervacija bas kada je i potencijalni pocetak  renoviranja, preskocimo je
                    //ali zapamtimo prvi sledeci datum nakon njenog zavrsetka
                    //sada ce se taj datum ponasati kao pocetna tacka
                    if (startPoint >= reservation.BegginingDate)
                    {
                        startPoint = reservation.EndingDate;
                        startPoint = startPoint.Add(day);
                    }
                }
            }

            endPoint = startPoint.Add(desiredDuration);
            //ukoliko su svi intervali izmedju rezervacija provereni, a i dalje postoji 
            //dovoljno dugacak period do datuma kraja renoviranja i taj period se cuva kao predlog
            while(endPoint <= desiredEnd) {
                //podintervali
                DateSuggestion dateSuggestion = new DateSuggestion(startPoint, endPoint);
                renovationDatesSuggestions.Add(dateSuggestion);

                startPoint = startPoint.Add(day);
                endPoint = endPoint.Add(day);
            }

            return renovationDatesSuggestions;
        }

    }
}
