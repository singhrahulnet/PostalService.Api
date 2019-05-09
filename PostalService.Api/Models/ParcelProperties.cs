using PostalService.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalService.Api.Models
{
    public interface IParcelProperties
    {
        int Priority { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Rate { get; set; }
        int WeightLimit { get; set; }
        int VolumeLimit { get; set; }
    }    
}
