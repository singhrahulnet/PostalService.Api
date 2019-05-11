using PostalService.Api.Infra;
using PostalService.Api.Models;
using System.Linq;

namespace PostalService.Api.Domain
{
    public static class ParcelExtensions
    {
        public static int Volume(this InputArgs inputArgs)
        {
            return inputArgs.Height * inputArgs.Width * inputArgs.Depth;
        }
        public static bool TryCalculateCost(this ParcelHandler parcelHandler, InputArgs inputArgs, out decimal cost)
        {
            cost = 0;
            //No weight or volume limit. Price by volume only
            if (parcelHandler.WeightLimit <= 0 && parcelHandler.VolumeLimit <= 0)
            {
                cost = inputArgs.Volume() * parcelHandler.Rate;
                return true;
            }

            //Weight Limit. Price by weight
            if (parcelHandler.WeightLimit > 0 && inputArgs.Weight > parcelHandler.WeightLimit)
            {
                cost = inputArgs.Weight * parcelHandler.Rate;
                return true;
            }

            //Volume Limit. Price by volume
            if (parcelHandler.VolumeLimit > 0 && inputArgs.Volume() < parcelHandler.VolumeLimit)
            {
                cost = inputArgs.Volume() * parcelHandler.Rate;
                return true;
            }
            return false;
        }
        public static void SortByPriority(this ParcelCollection parcelCollection)
        {
            parcelCollection.Parcels = parcelCollection.Parcels.OrderBy(x => x.Priority).ToList();
        }
        public static void SetChain(this ParcelCollection parcelCollection)
        {
            for (int i = 0; i < parcelCollection.Parcels.Count - 1; i++)
            {
                parcelCollection.Parcels[i].SetNextParcel(parcelCollection.Parcels[i + 1]);
            }
        }
    }
}
