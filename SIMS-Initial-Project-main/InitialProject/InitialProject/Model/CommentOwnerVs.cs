using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class CommentOwnerVs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("forumId")]
        public ForumOwnerVs? Forum { get; set; }
        public string Comment { get; set; }
       

        public string ByOwner { get; set; }

        public CommentOwnerVs() { }

        public CommentOwnerVs(ForumOwnerVs f, string comment) {

            this.Forum = f;
            this.Comment = comment;
            this.ByOwner = "*";


        }


    }
}
