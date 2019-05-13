using PostalService.Api.Extensions;
using PostalService.Api.Models;

namespace PostalService.Api.Domain
{
    public abstract class ParcelRuleBase
    {
        protected ParcelRuleBase _nextRule;

        public abstract int Priority { get; set; }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract decimal Rate { get; set; }
        public abstract int WeightLimit { get; set; }
        public abstract int VolumeLimit { get; set; }

        public void SetNextRule(ParcelRuleBase nextRule)
        {
            _nextRule = nextRule;
        }
        public virtual ParcelCost ProcessRule(Parcel parcel)
        {
            ParcelCost parcelCost = null;
            if (this.TryProcessRule(parcel, out decimal cost))
            {
                parcelCost = new ParcelCost(cost, Name);
            }

            //Cannot handle it. Passing it to a bigger guy
            else if (_nextRule != null) parcelCost = _nextRule.ProcessRule(parcel);

            return parcelCost;
        }
    }
}
