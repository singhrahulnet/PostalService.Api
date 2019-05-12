using PostalService.Api.Domain;
using PostalService.Api.Models;
using System.Linq;

namespace PostalService.Api.Extensions
{
    public static class PostalServiceExtensions
    {
        public static int Volume(this Parcel parcel)
        {
            return parcel.Height * parcel.Width * parcel.Depth;
        }
        public static bool TryProcessRule(this ParcelRuleBase parcelRule, Parcel parcel, out decimal cost)
        {
            cost = 0;
            //No weight or volume limit. Price by volume only
            if (parcelRule.WeightLimit <= 0 && parcelRule.VolumeLimit <= 0)
            {
                cost = parcel.Volume() * parcelRule.Rate;
                return true;
            }

            //Weight Limit. Price by weight
            if (parcelRule.WeightLimit > 0 && parcel.Weight > parcelRule.WeightLimit)
            {
                cost = parcel.Weight * parcelRule.Rate;
                return true;
            }

            //Volume Limit. Price by volume
            if (parcelRule.VolumeLimit > 0 && parcel.Volume() < parcelRule.VolumeLimit)
            {
                cost = parcel.Volume() * parcelRule.Rate;
                return true;
            }
            return false;
        }
        public static void SortByPriority(this ParcelRuleCollection rules)
        {
            rules.ParcelRules = rules.ParcelRules.OrderBy(x => x.Priority);
        }
        public static void SetChain(this ParcelRuleCollection rules)
        {
            for (int i = 0; i < rules.ParcelRules.Count() - 1; i++)
            {
                rules.ParcelRules.ElementAt(i).SetNextRule(rules.ParcelRules.ElementAt(i + 1));
            }
        }
    }
}
