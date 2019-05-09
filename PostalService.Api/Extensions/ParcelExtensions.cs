using PostalService.Api.Domain;
using PostalService.Api.Models;

namespace PostalService.Api.Extensions
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
            if (parcelHandler.WeightLimit <= 0 && parcelHandler.VolumeLimit <= 0)
            {
                cost = inputArgs.Volume() * parcelHandler.Rate;
                return true;
            }
            if (parcelHandler.WeightLimit > 0 && inputArgs.Weight > parcelHandler.WeightLimit)
            {
                cost = inputArgs.Weight * parcelHandler.Rate;
                return true;
            }
            if (parcelHandler.VolumeLimit > 0 && inputArgs.Volume() < parcelHandler.VolumeLimit)
            {
                cost = inputArgs.Volume() * parcelHandler.Rate;
                return true;
            }
            return false;
        }
    }
}
