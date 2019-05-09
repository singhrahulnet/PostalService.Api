using PostalService.Api.Domain;
using System.Collections.Generic;

namespace PostalService.Api.Infra
{
    public class ParcelCollection
    {
        public List<Parcel> Parcels { get; set; }
    }
    public class Parcel : ParcelHandler
    {
        public override int Priority { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override decimal Rate { get; set; }
        public override int WeightLimit { get; set; }
        public override int VolumeLimit { get; set; }
    }   
}
