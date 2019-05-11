using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PostalService.Api.Infra;
using PostalService.Api.Models;
using PostalService.Api.Services;

namespace PostalService.Test.Unit
{
    public class ParcelHandlerTest
    {        
        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // InputArgs inputArgs, decimal cost


            yield return new object[] { new InputArgs(10, 20, 5, 20), 80 };
            yield return new object[] { new InputArgs(22, 5, 5, 5), 330 };
            yield return new object[] { new InputArgs(2, 3, 10, 12), 18 };
            yield return new object[] { new InputArgs(110, 20, 55, 120), 0 };

            yield return new object[] { new InputArgs(50, 2, 5, 20), 750 };
            yield return new object[] { new InputArgs(10, 2, 5, 20), 10 };
            yield return new object[] { new InputArgs(9, 15, 10, 10), 60 };
            yield return new object[] { new InputArgs(9, 10, 10, 10), 50 };
            yield return new object[] { new InputArgs(9, 20, 20, 20), 240 };
        }

        [Theory(DisplayName = "ParcelHandler: Chaining Works")]
        [MemberData(nameof(GetInputParams))]
        public void HandleParcel_returns_correct_values(InputArgs inputArgs, decimal cost)
        {
            //Given
            var parcelCollection = new ParcelCollection
            {
                Parcels = new List<Parcel>
                            {
                            new Parcel { Rate = 0, WeightLimit = 50, VolumeLimit = 0 },
                            new Parcel { Rate = 15, WeightLimit = 10, VolumeLimit = 0 },
                            new Parcel { Rate = 0.05M, WeightLimit = 0, VolumeLimit = 1500 },
                            new Parcel { Rate = 0.04M, WeightLimit = 0, VolumeLimit = 2500 },
                            new Parcel { Rate = 0.03M, WeightLimit = 0, VolumeLimit = 0 },
                            }
            };

            var inventory = new ParcelInventory(parcelCollection);
            var sut = inventory.FirstParcelHandler;

            //When
            var actual = sut.HandleParcel(inputArgs);

            //Then
            Assert.IsType<ParcelResult>(actual);
            Assert.Equal(cost, actual.CostOfDelivery);
        }
    }
}
