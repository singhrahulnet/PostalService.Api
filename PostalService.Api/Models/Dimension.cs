using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalService.Api.Models
{
    public class Dimension
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
    }
}
