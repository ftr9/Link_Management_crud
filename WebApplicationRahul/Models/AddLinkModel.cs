using System.ComponentModel.DataAnnotations;

namespace WebApplicationRahul.Models
{
    public class AddLinkModel
    {
        public string Name { get; set; }
        public string url { get; set; }

        public string? purpose { get; set; }
    }
}
