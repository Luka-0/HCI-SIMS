using InitialProject.Enumeration;
using InitialProject.Serializer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public UserType Type { get; set; }
        public int Age { get; set; }
        public User()
        {
            Username = "default";
            Password = "default";
            Type = 0;
            SuperTitle = false;
        }

        public User(string username, string password)
        {   
            Username = username;
            Password = password;
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
