using PostalService.Api.Infra;
using PostalService.Api.Models;
using PostalService.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostalService.Api.Managers
{
    public interface IParcelManager
    {
        ParcelResult FindParcel(Dimension dimension);
    }
    public class ParcelManager : IParcelManager
    {
        private readonly IConfigService _configService;
        private IParcelInventory _parcelInventory;

        private ParcelCollection _parcelCollection => _configService.GetSection<ParcelCollection>(nameof(ParcelCollection));

        public ParcelManager(IConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _parcelInventory = new ParcelInventory(_parcelCollection);
        }
        public ParcelResult FindParcel(Dimension dimension)
        {
            ParcelResult result = null;
            _parcelInventory.FirstParcelHandler.HandlePackage(dimension, ref result);
            return result;
        }
    }
}
