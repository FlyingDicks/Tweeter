using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlyingDicksTweeter.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public ApplicationUser Author { get; set; }

        public Comment()
        {
            this.DateAdded = DateTime.Now;

        }
        public Comment(int id)
        {
            this.PostId = id;
            this.DateAdded = DateTime.Now;
        }
        public int PostId { get; set; }

        public bool IsAuthor(string name)
        {
            return this.Author.UserName.Equals(name);
        }

    }
}