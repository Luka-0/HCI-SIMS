using InitialProject.Contexts;
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
        public static void Save(List<String> urlsDto, Accommodation accommodation)
        {
            foreach (string url in urlsDto)
            {

                Image image = new Image(url);
                ImageRepository.Save(image);

                var db = new UserContext();
                var tempRecord = db.image.Find(image.Id);

                //note: null values for Tour are allowed by database structure.
                tempRecord.Accommodation = accommodation;

                db.SaveChanges();
            }


        }

        public List<Image> save(List<String> urlsDto)
        {
            List<Image> images = new List<Image>();
            foreach (string url in urlsDto)
            {

                Image image = new Image(url);
                ImageRepository.Save(image);
                images.Add(image);
                var db = new UserContext();
                var tempRecord = db.image.Find(image.Id);

                db.SaveChanges();
            }

            return images;
        }

        public void setTourId(List<Image> images, int tourID)
        {
            using (var db = new UserContext())
            {
                foreach (var image in images)
                {
                    image.Tour.Id = tourID;

                }

                db.SaveChanges();
            }
            
        }
    }
}
