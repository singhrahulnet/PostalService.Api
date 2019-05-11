using PostalService.Api.Domain;
using PostalService.Api.Infra;
using PostalService.Api.Models;
using System.Collections.Generic;
using Xunit;

namespace PostalService.Test.Unit
{
    public class ParcelExtensionsTest
    {
        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // InputArgs inputArgs, int volume

            yield return new object[] { new InputArgs(1, 10, 10, 10), 1000 };
            yield return new object[] { new InputArgs(1, 1, 1, 1), 1 };
            yield return new object[] { new InputArgs(1, 0, 10, 10), 0 };
        }
        public static IEnumerable<object[]> GetInputParamsForCost()
        {
            // THE TEST DATA STRUCTURE
            // InputArgs inputArgs, int volume
            var rejectParcel = new Parcel { Rate = 0, WeightLimit = 25, VolumeLimit = 0 };
            var heavyParcel = new Parcel { Rate = 15, WeightLimit = 10, VolumeLimit = 0 };
            var smallParcel = new Parcel { Rate = 0.05M, WeightLimit = 0, VolumeLimit = 1500 };
            var mediumParcel = new Parcel { Rate = 0.04M, WeightLimit = 0, VolumeLimit = 2500 };
            var largeParcel = new Parcel { Rate = 0.03M, WeightLimit = 0, VolumeLimit = 0 };

            //In Range
            yield return new object[] { mediumParcel,
                                        new InputArgs(10, 20, 5, 20),
                                        80 };            
            yield return new object[] { heavyParcel,
                                        new InputArgs(22, 5, 5, 5),
                                        330 };
            yield return new object[] { smallParcel,
                                        new InputArgs(2, 3, 10, 12),
                                        18 };
            yield return new object[] { rejectParcel,
                                        new InputArgs(110, 20, 55, 120),
                                        0 };

            //Out of range
            yield return new object[] { mediumParcel,
                                        new InputArgs(10, 200, 5, 20),
                                        0 };
            yield return new object[] { heavyParcel,
                                        new InputArgs(9, 5, 5, 5),
                                        0 };
            yield return new object[] { smallParcel,
                                        new InputArgs(2, 300, 10, 12),
                                        0 };
        }

        [Theory(DisplayName = "ParcelExtensions: Volume Returns Correct Value")]
        [MemberData(nameof(GetInputParams))]
        public void Volume_returns_correct_value(InputArgs inputArgs, int volume)
        {
            //Given
            var sut = inputArgs;

            //When
            var actual = sut.Volume();

            //Then
            Assert.Equal(volume, actual);
        }

        [Theory(DisplayName = "ParcelExtensions: TryCalculateCost Returns Correct Cost")]
        [MemberData(nameof(GetInputParamsForCost))]
        public void TryCalculateCost_returns_correct_cost(ParcelHandler parcelHandler, InputArgs inputArgs, decimal expectedCost)
        {
            //Given
            var sut = parcelHandler;

            //When
            sut.TryCalculateCost(inputArgs, out decimal cost);

            //Then
            Assert.Equal(expectedCost, cost);
        }
    }
}
