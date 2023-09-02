using System.ComponentModel.DataAnnotations;

namespace WebApplicationRahul.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage =
           "Name is required")]
        [MaxLength(50,ErrorMessage ="Name Exceeds more than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email Address")]
        public string Email { get; set; }

        
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        public ICollection<Link> Links { get; set; }

    }
}
