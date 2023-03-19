using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> Stashed changes
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
<<<<<<< Updated upstream
    [Table("Location")]
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }


        public Location()
        {

        }

        public Location(string city, string country) {
            this.City = city;
            this.Country = country;
        }
    }
   
=======
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Contry { get; set;  }
    }
>>>>>>> Stashed changes
}
