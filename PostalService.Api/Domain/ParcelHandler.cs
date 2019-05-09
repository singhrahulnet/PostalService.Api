using PostalService.Api.Extensions;
using PostalService.Api.Models;

namespace PostalService.Api.Domain
{
    public abstract class ParcelHandler : IParcelProperties
    {
        protected ParcelHandler _nextParcel;

        public abstract int Priority { get; set; }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract decimal Rate { get; set; }
        public abstract int WeightLimit { get; set; }
        public abstract int VolumeLimit { get; set; }

        public void SetNextParcel(ParcelHandler nextParcel)
        {
            _nextParcel = nextParcel;
        }
        public virtual void HandlePackage(Dimension dimensions, ref ParcelResult result)
        {
            decimal cost = 0;
            if (this.TryCalculateCost(dimensions, out cost))
            {
                result = new ParcelResult(cost, Name);
            }

            //Cannot handle it. Passing it to a bigger guy
            else if (_nextParcel != null) _nextParcel.HandlePackage(dimensions, ref result);
        }        
    }
}
