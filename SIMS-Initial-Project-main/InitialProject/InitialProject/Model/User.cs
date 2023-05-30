using InitialProject.Enumeration;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InitialProject.View.Guide;

namespace InitialProject.Model
{
    [Table("User")]
    public class User : ISerializable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SuperTitle { get; set; }
        public int BonusPoints { get; set; }
        public DateTime SuperTitleValidTill { get; set; }
        public UserType Type { get; set; }
        public int Age { get; set; }

        [InverseProperty("Guides")]
        public List<ComplexTourRequest>? ComplexTourRequests { get; set; } = new List<ComplexTourRequest>();
        public User()
        {
            Username = "default";
            Password = "default";

            SuperTitle = false;
            SuperTitleValidTill = DateTime.Now;
            BonusPoints = 0;
        }

        public User(string username, string password)
        {   
            Username = username;
            Password = password;

            SuperTitle = false;
            SuperTitleValidTill = DateTime.Now;
            BonusPoints = 0;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
        }

        public override string ToString()
        {
            return Username + "\n";
        }

    }
}
