using System.ComponentModel.DataAnnotations;

namespace WebApplicationRahul.Models
{
    public class UpdateLinkViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string url { get; set; }

        public string? purpose { get; set; }

        public int UserId { get; set; }
    }
}
