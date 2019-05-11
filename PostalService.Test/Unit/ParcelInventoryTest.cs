using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using PostalService.Api.Domain;
using PostalService.Api.Infra;
using PostalService.Api.Services;
using Xunit;


namespace PostalService.Test.Unit
{
    public class ParcelInventoryTest 
    {       
        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // ParcelCollection parcelCollection, int firstPriority

            //Already ordered
            yield return new object[] { new ParcelCollection { Parcels = new List<Parcel> {
                                                               new Parcel { Priority = 1 },
                                                               new Parcel { Priority = 2 },
                                                               new Parcel { Priority = 3 },
                                                               new Parcel { Priority = 4 },
            } }, 1 };

            //Un-ordered
            yield return new object[] { new ParcelCollection { Parcels = new List<Parcel> {
                                                               new Parcel { Priority = 4 },
                                                               new Parcel { Priority = 2 },
                                                               new Parcel { Priority = 1 },
                                                               new Parcel { Priority = 3 },
            } }, 1 };

            //non-sequential
            yield return new object[] { new ParcelCollection { Parcels = new List<Parcel> {
                                                               new Parcel { Priority = 40 },
                                                               new Parcel { Priority = 20 },
                                                               new Parcel { Priority = 10 },
                                                               new Parcel { Priority = 30 },
            } }, 10 };

            //single value
            yield return new object[] { new ParcelCollection { Parcels = new List<Parcel> {
                                                               new Parcel { Priority = 40 }
            } }, 40 };
        }

        [Theory(DisplayName = "ParcelInventory: FirstParcelHandler_returns_correct_firstHandler")]
        [MemberData(nameof(GetInputParams))]
        public void FirstParcelHandler_returns_correct_firstHandler(ParcelCollection parcelCollection, int firstPriority)
        {
            //Given
            var sut = new ParcelInventory(parcelCollection);

            //When
            var actual = sut.FirstParcelHandler;

            //Then
            Assert.IsAssignableFrom<ParcelHandler>(actual);
            Assert.Equal(firstPriority, actual.Priority);
        }
    }
}
