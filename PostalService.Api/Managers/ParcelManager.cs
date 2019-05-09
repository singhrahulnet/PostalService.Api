using PostalService.Api.Infra;
using PostalService.Api.Models;
using PostalService.Api.Services;
using System;

namespace PostalService.Api.Managers
{
    public interface IParcelManager
    {
        ParcelResult FindParcel(InputArgs inputArgs);
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
        public ParcelResult FindParcel(InputArgs inputArgs)
        {
            ParcelResult result = null;
            _parcelInventory.FirstParcelHandler.HandlePackage(inputArgs, ref result);
            return result;
        }
    }
}
