using PostalService.Api.Domain;
using PostalService.Api.Models;
using PostalService.Api.Services;
using System;

namespace PostalService.Api.Managers
{
    public interface IParcelManager
    {
        ParcelCost GetCost(Parcel parcel);
    }
    public class ParcelManager : IParcelManager
    {
        private readonly IConfigService _configService;
        private ParcelRuleCollection _rules => _configService.GetSection<ParcelRuleCollection>(nameof(ParcelRuleCollection));
        private IParcelRuleProcessor _ruleProcessor => new ParcelRuleProcessor(_rules);


        public ParcelManager(IConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }
        public ParcelCost GetCost(Parcel parcel)
        {
            ParcelCost parcelCost = null;
            try
            {
                parcelCost = _ruleProcessor.FirstRule.ProcessRule(parcel);
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw
            }
            return parcelCost;
        }
    }
}
