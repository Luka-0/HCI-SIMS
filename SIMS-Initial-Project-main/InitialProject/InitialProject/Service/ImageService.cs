using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Service
{
    public class ImageService
    {
        private readonly IImageRepository iimageRepository;

        public ImageService(IImageRepository iimageRepository)
        {
            this.iimageRepository = iimageRepository;
        }

        //Aleksandra
        public  void Save(List<String> urlsDto, Accommodation accommodation)
        {
            foreach (string url in urlsDto)
            {

                Image image = new Image(url);
                iimageRepository.Save(image);

                var db = new UserContext();
                var existingImage = db.image.Find(image.Id);

                //note: null values for Tour are allowed by database structure.
                existingImage.Accommodation = accommodation;

                db.SaveChanges();
            }


        }
        
        public List<Image> Save(List<String> urlsDto)
        {
            // List<Image> images = new List<Image>();
            // foreach (string url in urlsDto)
            // {
            //
            //     Image image = new Image(url);
            //     iimageRepository.Save(image);
            //     images.Add(image);
            // }
            //
            // return images;
            List<Image> images = urlsDto.Select(url =>
            {
                Image image = new Image(url);
                iimageRepository.Save(image);
                return image;
            }).ToList();

            return images;

        }
        
        public void SetTourId(List<Image> images, Tour tour)
        {
            using( var db = new UserContext())
            {
                images.ForEach(image => iimageRepository.Update(image, tour));
                
            }
            
        }
    }
}
