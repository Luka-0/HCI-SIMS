﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    [Table("ForumComment")]
    public class ForumComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("ForumId")]
        public Forum? Forum { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        public char Special { get; set; } = ' '; // bice '*' ako je osoba posetila lokaciju na cijem forumu je postavila komentar

        ForumComment(string text)
        {
            Text = new("");
        }

        public ForumComment(string text, Forum forum, User user, char special)
        {
            Text = text;
            Forum = forum;
            User = user;
            Special = special;
        }

        public ForumComment(string text, Forum forum)
        {
            Text = text;
            Forum = forum;
            Special = '*';
        }
    }
}
