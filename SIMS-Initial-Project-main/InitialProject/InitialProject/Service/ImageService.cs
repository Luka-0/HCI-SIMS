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
            foreach (string url in urlsDto) {

                Image image = new Image(url);
                ImageRepository.Save(image);

                var db = new UserContext();
                var tempRecord = db.image.Find(image.Id);

                //note: null values for Tour are allowed by database structure.
                tempRecord.Accommodation = accommodation;

                db.SaveChanges();
            }
          
        }
    }
}
