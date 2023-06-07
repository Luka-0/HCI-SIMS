using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{

    [Table("ForumOwnerVs")]
    public class ForumOwnerVs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User Guest { get; set; }

        [ForeignKey("locationID")]
        public Location? Location { get; set; }

        public bool VeyUseful { get; set; }

        public ForumOwnerVs() { }

      

    }
}
