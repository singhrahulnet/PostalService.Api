using System.ComponentModel.DataAnnotations;

namespace PostalService.Api.Models
{
    public class InputArgs
    {
        [Required]
        public int Weight { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Depth { get; set; }
    }
}
