using PostalService.Api.Domain;
using PostalService.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalService.Api.Extensions
{
    public static class ParcelExtensions
    {
        public static int Volume(this Dimension dimension)
        {
            return dimension.Height * dimension.Width * dimension.Depth;
        }
        public static bool TryCalculateCost(this ParcelHandler parcelHandler, Dimension dimensions, out decimal cost)
        {
            cost = 0;
            if (parcelHandler.WeightLimit <= 0 && parcelHandler.VolumeLimit <= 0)
            {
                cost = dimensions.Volume() * parcelHandler.Rate;
                return true;
            }
            if (parcelHandler.WeightLimit > 0 && dimensions.Weight > parcelHandler.WeightLimit)
            {
                cost = dimensions.Weight * parcelHandler.Rate;
                return true;
            }
            if (parcelHandler.VolumeLimit > 0 && dimensions.Volume() < parcelHandler.VolumeLimit)
            {
                cost = dimensions.Volume() * parcelHandler.Rate;
                return true;
            }
            return false;
        }
    }
}
