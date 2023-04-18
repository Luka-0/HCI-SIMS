using InitialProject.Contexts;
using InitialProject.Interface;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ImageRepository : IImageRepository
    {
        public void Save(Image image)
        {
            using var db = new UserContext();

            db.Add(image);
            db.SaveChanges();
        }

        public void Update(Image image, Tour tour)
        {
            using var db = new UserContext();
            image.Tour = tour;
            db.Update(image);
            db.SaveChanges();
        }


    }
}
