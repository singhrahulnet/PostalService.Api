using PostalService.Api.Domain;
using PostalService.Api.Models;
using System.Collections.Generic;
using Xunit;

namespace PostalService.Test.Unit
{
    public class ParcelRuleTest
    {        
        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // Parcel parcel, decimal cost


            yield return new object[] { new Parcel(10, 20, 5, 20), 80 };
            yield return new object[] { new Parcel(22, 5, 5, 5), 330 };
            yield return new object[] { new Parcel(2, 3, 10, 12), 18 };
            yield return new object[] { new Parcel(110, 20, 55, 120), 0 };

            yield return new object[] { new Parcel(50, 2, 5, 20), 750 };
            yield return new object[] { new Parcel(10, 2, 5, 20), 10 };
            yield return new object[] { new Parcel(9, 15, 10, 10), 60 };
            yield return new object[] { new Parcel(9, 10, 10, 10), 50 };
            yield return new object[] { new Parcel(9, 20, 20, 20), 240 };
        }

        [Theory(DisplayName = "ParcelRule: Processes Correct Rule")]
        [MemberData(nameof(GetInputParams))]
        public void ProcessRule_returns_correct_values(Parcel parcel, decimal cost)
        {
            //Given
            var rules = new ParcelRuleCollection
            {
                ParcelRules = new List<ParcelRule>
                            {
                            new ParcelRule { Rate = 0, WeightLimit = 50, VolumeLimit = 0 },
                            new ParcelRule { Rate = 15, WeightLimit = 10, VolumeLimit = 0 },
                            new ParcelRule { Rate = 0.05M, WeightLimit = 0, VolumeLimit = 1500 },
                            new ParcelRule { Rate = 0.04M, WeightLimit = 0, VolumeLimit = 2500 },
                            new ParcelRule { Rate = 0.03M, WeightLimit = 0, VolumeLimit = 0 },
                            }
            };

            var ruleProcessor = new ParcelRuleProcessor(rules);
            var sut = ruleProcessor.FirstRule;

            //When
            var actual = sut.ProcessRule(parcel);

            //Then
            Assert.IsType<ParcelCost>(actual);
            Assert.Equal(cost, actual.CostOfDelivery);
        }
    }
}
