using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlyingDicksTweeter.Models
{
    public class Post
    {
        private ICollection<Tag> tags;
        public Post()
        {
            this.tags = new HashSet<Tag>();
            this.DateAdded = DateTime.Now;
        }

        //!
        public Post(string authorId, string content)
        {
            this.Author.Id = authorId;
            this.Content = content;
            this.tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public ApplicationUser Author { get; set; }

        public bool IsAuthor(string name)
        {
            return this.Author.UserName.Equals(name);
        }

        public byte[] Image { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}