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
    public class TourController
    {
        private TourService tourService = new TourService();
        public List<GetTourDto> GetAll()
        {
            List<Tour> allTours = tourService.GetAll();
            List<GetTourDto> getTourDtos = new List<GetTourDto>();

            foreach (Tour tour in allTours)
            {
                getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
            }
            return getTourDtos;
        }

        public List<GetTourDto> GetBy(Location location)
        {
            List<Tour> allTours = tourService.GetBy(location);
            List<GetTourDto> getTourDtos = new List<GetTourDto>();

            foreach (Tour tour in allTours)
            {
                getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
            }
            return getTourDtos;
        }

        public List<GetTourDto> GetBy(TimeOnly duration)
        {
            List<Tour> allTours = tourService.GetBy(duration);
            List<GetTourDto> getTourDtos = new List<GetTourDto>();

            foreach (Tour tour in allTours)
            {
                getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
            }
            return getTourDtos;
        }

        public List<GetTourDto> GetBy(string language)
        {
            List<Tour> allTours = tourService.GetBy(language);
            List<GetTourDto> getTourDtos = new List<GetTourDto>();

            foreach (Tour tour in allTours)
            {
                getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
            }
            return getTourDtos;
        }

        public List<GetTourDto> GetBy(int guestNumber)
        {
            List<Tour> allTours = tourService.GetBy(guestNumber);
            List<GetTourDto> getTourDtos = new List<GetTourDto>();

            foreach (Tour tour in allTours)
            {
                getTourDtos.Add(new GetTourDto(tour.Name, tour.Description, tour.Location, tour.Language, tour.GuestLimit, tour.Duration, tour.StartDateAndTime, tour.TourKeyPoints, tour.images));
            }
            return getTourDtos;
        }

        public Tour Reserve(Tour tour, int guestNumber)
        {
            Tour chosenTour = tourService.GetById(tour.Id);

            //TODO: ispravi ovo
            return null;
        }

    }
}
