using InitialProject.Contexts;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        public static void Save(Accommodation accommodation)
        {
            using var db = new UserContext();

            db.Add(accommodation);
            db.SaveChanges();
        }
    }
}
