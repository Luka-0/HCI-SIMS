using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("GuestGrade")]
    public class GuestGrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //For cistoca
        [Range(1, 5)]
        public int Tidiness { get; set; }

        //For postovanje pravila
        [Range(1, 5)]
        public int Obedience { get; set; }

        public String Comment { get; set; }

        //changes
        [ForeignKey("GuestId")]
        public User Guest { get; set; }

    }
}
