using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ImageRepository
    {
        public static void Save(Image image)
        {
            using var db = new UserContext();

            db.Add(image);
            db.SaveChanges();
        }


    }
}
