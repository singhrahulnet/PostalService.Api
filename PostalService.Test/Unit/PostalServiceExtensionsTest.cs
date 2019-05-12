using PostalService.Api.Domain;
using PostalService.Api.Extensions;
using PostalService.Api.Models;
using System.Collections.Generic;
using Xunit;

namespace PostalService.Test.Unit
{
    public class PostalServiceExtensionsTest
    {
        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // Parcel parcel, int volume

            yield return new object[] { new Parcel(1, 10, 10, 10), 1000 };
            yield return new object[] { new Parcel(1, 1, 1, 1), 1 };
            yield return new object[] { new Parcel(1, 0, 10, 10), 0 };
        }
        public static IEnumerable<object[]> GetInputParamsForCost()
        {
            // THE TEST DATA STRUCTURE
            // ParcelRuleBase parcelRule, Parcel parcel, decimal expectedCost
            var rejectParcelRule = new ParcelRule { Rate = 0, WeightLimit = 25, VolumeLimit = 0 };
            var heavyParcelRule = new ParcelRule { Rate = 15, WeightLimit = 10, VolumeLimit = 0 };
            var smallParcelRule = new ParcelRule { Rate = 0.05M, WeightLimit = 0, VolumeLimit = 1500 };
            var mediumParcelRule = new ParcelRule { Rate = 0.04M, WeightLimit = 0, VolumeLimit = 2500 };
            var largeParcelRule = new ParcelRule { Rate = 0.03M, WeightLimit = 0, VolumeLimit = 0 };

            //In Range
            yield return new object[] { mediumParcelRule,
                                        new Parcel(10, 20, 5, 20),
                                        80 };            
            yield return new object[] { heavyParcelRule,
                                        new Parcel(22, 5, 5, 5),
                                        330 };
            yield return new object[] { smallParcelRule,
                                        new Parcel(2, 3, 10, 12),
                                        18 };
            yield return new object[] { rejectParcelRule,
                                        new Parcel(110, 20, 55, 120),
                                        0 };

            //Out of range
            yield return new object[] { mediumParcelRule,
                                        new Parcel(10, 200, 5, 20),
                                        0 };
            yield return new object[] { heavyParcelRule,
                                        new Parcel(9, 5, 5, 5),
                                        0 };
            yield return new object[] { smallParcelRule,
                                        new Parcel(2, 300, 10, 12),
                                        0 };
        }

        [Theory(DisplayName = "PostalServiceExtensions: Volume Returns Correct Value")]
        [MemberData(nameof(GetInputParams))]
        public void Volume_returns_correct_value(Parcel parcel, int volume)
        {
            //Given
            var sut = parcel;

            //When
            var actual = sut.Volume();

            //Then
            Assert.Equal(volume, actual);
        }

        [Theory(DisplayName = "PostalServiceExtensions: TryProcessRule Returns Correct Cost")]
        [MemberData(nameof(GetInputParamsForCost))]
        public void TryProcessRule_returns_correct_cost(ParcelRuleBase parcelRule, Parcel parcel, decimal expectedCost)
        {
            //Given
            var sut = parcelRule;

            //When
            sut.TryProcessRule(parcel, out decimal cost);

            //Then
            Assert.Equal(expectedCost, cost);
        }
    }
}
