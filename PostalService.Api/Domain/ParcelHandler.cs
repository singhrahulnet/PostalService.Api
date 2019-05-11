using PostalService.Api.Domain;
using PostalService.Api.Models;

namespace PostalService.Api.Domain
{
    public abstract class ParcelHandler : IParcel
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
        public virtual ParcelResult HandleParcel(InputArgs inputArgs)
        {
            decimal cost = 0;
            ParcelResult result = null;
            if (this.TryCalculateCost(inputArgs, out cost))
            {
                result = new ParcelResult(cost, Name);
            }

            //Cannot handle it. Passing it to a bigger guy
            else if (_nextParcel != null) result = _nextParcel.HandleParcel(inputArgs);

            return result;
        }
    }
}
