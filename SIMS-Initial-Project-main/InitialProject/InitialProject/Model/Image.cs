using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public string Url { get; set; }

        [ForeignKey("AccommodationId")] public Accommodation? Accommodation { get; set; }

        [ForeignKey("TourId")] public Tour? Tour { get; set; }

        public Image(string url)
        {

            this.Url = url;
        }

    }
}