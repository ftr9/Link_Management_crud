using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace WebApplicationRahul.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Link Name is reqiured")]
        public string Name { get; set; }

        [Url(ErrorMessage ="The Link must be in a URL format")]
        public string url { get; set; }


        public string? purpose { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
