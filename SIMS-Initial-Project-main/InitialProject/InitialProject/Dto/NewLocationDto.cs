using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Dto
{
    public class NewLocationDto
    {
        public string City { get; set; }
        public string Country { get; set; }

        public NewLocationDto(string city, string country) {

            this.City = city; ;
            this.Country = country;

        } 
    }
}
