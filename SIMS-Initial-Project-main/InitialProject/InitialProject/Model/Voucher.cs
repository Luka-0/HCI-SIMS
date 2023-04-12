using InitialProject.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("Voucher")]
    public class Voucher
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReceivedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public VoucherObtainedReason ObtainedReason { get; set; }

        public Voucher(User user)
        {
            //User = user;
            ReceivedDate = DateTime.Today;
            ExpirationDate = ReceivedDate.AddYears(1);
            ObtainedReason = VoucherObtainedReason.CanceledTour;
        }

        public Voucher()
        {
        }


        public override string ToString()
        {
            return ReceivedDate.ToString() + " | " + ExpirationDate.ToString()  + " | " + ObtainedReason.ToString();
        }
    }
}
