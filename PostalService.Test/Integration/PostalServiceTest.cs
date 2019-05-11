using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PostalService.Api.Controllers;
using PostalService.Api.Managers;
using PostalService.Api.Models;
using PostalService.Api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PostalService.Test.Integration
{
    public class PostalServiceTest : IDisposable
    {
        IConfigService _configService = null;
        IParcelManager _parcelManager = null;
        public PostalServiceTest()
        {
            _configService = new ConfigService(new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json")
                             .Build());
            _parcelManager = new ParcelManager(_configService);
        }
        public void Dispose()
        {
            _configService = null;
            _parcelManager = null;
        }

        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // int weight, int height, int width, int depth, decimal cost

            yield return new object[] { 10, 20, 5, 20, 80 };
            yield return new object[] { 22, 5, 5, 5, 330 };
            yield return new object[] { 2, 3, 10, 12, 18 };
            yield return new object[] { 110, 20, 55, 12, 0 };
        }

        [Theory(DisplayName = "Integration: StartProcess ")]
        [MemberData(nameof(GetInputParams))]
        public void StartProcess(int weight, int height, int width, int depth, decimal cost)
        {
            //Given
            var sut = new ParcelController(_parcelManager);

            //When
            var actual = sut.GetParcelAndCost(weight, height, width, depth);

            //Then
            var result = Assert.IsType<OkObjectResult>(actual);
            var parcelResult = Assert.IsType<ParcelResult>(result.Value);
            Assert.Equal(cost, parcelResult.CostOfDelivery);
        }
    }
}
