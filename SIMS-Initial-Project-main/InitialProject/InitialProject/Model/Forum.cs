using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("Forum")]
    public class Forum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("locationID")]
        public Location Location { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        public string InitialComment { get; set; }

        [NotMapped]
        public List<string> Comments { get; set; } = new();

        public bool isClosed { get; set; } = false;

        public Forum()
        {
            Location = new();
            InitialComment = new("");
        }

        public Forum(Location location, string initialComment, User user)
        {
            Location = location;
            InitialComment = initialComment;
            User = user;
        }
    }
}
