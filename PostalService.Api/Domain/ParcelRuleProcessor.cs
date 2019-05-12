using PostalService.Api.Extensions;
using System.Linq;

namespace PostalService.Api.Domain
{
    public interface IParcelRuleProcessor
    {
        ParcelRuleBase FirstRule { get; }
    }
    public class ParcelRuleProcessor : IParcelRuleProcessor
    {
        private ParcelRuleCollection _rules;

        public ParcelRuleProcessor(ParcelRuleCollection rules)
        {
            _rules = rules;
        }
        public ParcelRuleBase FirstRule
        {
            get
            {
                if(!_rules.IsRuleSetup) SetupRules();

                return _rules.ParcelRules.FirstOrDefault();
            }
        }

        private void SetupRules()
        {
            _rules.SortByPriority();
            _rules.SetChain();
            _rules.IsRuleSetup = true;
        }
    }
}
