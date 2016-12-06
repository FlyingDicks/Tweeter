using System.ComponentModel.DataAnnotations;

namespace FlyingDicksTweeter.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

    }
}