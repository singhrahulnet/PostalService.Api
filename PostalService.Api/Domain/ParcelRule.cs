using System.Collections.Generic;

namespace PostalService.Api.Domain
{
    public class ParcelRuleCollection
    {
        public IEnumerable<ParcelRule> ParcelRules { get; set; }
        public bool IsRuleSetup { get; set; }
    }
    public class ParcelRule : ParcelRuleBase
    {
        public override int Priority { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override decimal Rate { get; set; }
        public override int WeightLimit { get; set; }
        public override int VolumeLimit { get; set; }
    }   
}
