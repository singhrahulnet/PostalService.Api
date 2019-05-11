﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using PostalService.Api.Controllers;
using PostalService.Api.Infra;
using PostalService.Api.Managers;
using PostalService.Api.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace PostalService.Test.Unit
{
    public class ParcelControllerTest : IDisposable
    {
        Mock<IParcelManager> mockParcelManager;
        public ParcelControllerTest()
        {
            mockParcelManager = new Mock<IParcelManager>();
        }
        public void Dispose()
        {
            mockParcelManager = null;
        }

        public static IEnumerable<object[]> GetInputParamsfor400()
        {
            // THE TEST DATA STRUCTURE
            // int weight, int height, int width, int depth

            yield return new object[] { 0, 0, 0, 0 };
            yield return new object[] { 0, 0, 12, 0 };
            yield return new object[] { -2, 23, 23, 23 };
            yield return new object[] { -2, -2, -2, -2 };
        }
        public static IEnumerable<object[]> GetInputParamsValid()
        {
            // THE TEST DATA STRUCTURE
            // int weight, int height, int width, int depth

            yield return new object[] { 5, 5, 5, 5 };
            yield return new object[] { 2, 1, 12, 8 };
            yield return new object[] { 2, 23, 23, 23 };
            yield return new object[] { 2, 2, 2, 2 };
        }

        [Theory(DisplayName = "Controller: GetParcelAndCost Returns Correct Object State ")]
        [MemberData(nameof(GetInputParamsValid))]
        public void GetParcelAndCost_returns_correct_object_state(int weight, int height, int width, int depth)
        {
            //Given
            ParcelResult res = new ParcelResult(20, "mock");
            mockParcelManager.Setup(m => m.FindParcel(It.IsAny<InputArgs>())).Returns(res);
            var sut = new ParcelController(mockParcelManager.Object);

            //When
            var actual = sut.GetParcelAndCost(weight, height, width, depth);

            //Then
            var result = Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<ParcelResult>(result.Value);
        }

        [Theory(DisplayName = "Controller: GetParcelAndCost Returns 400 ")]
        [MemberData(nameof(GetInputParamsfor400))]
        public void GetParcelAndCost_returns_400(int weight, int height, int width, int depth)
        {
            //Given
            var sut = new ParcelController(mockParcelManager.Object);

            //When
            var actual = sut.GetParcelAndCost(weight, height, width, depth);

            //Then
            var result = Assert.IsType<BadRequestObjectResult>(actual);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory(DisplayName = "Controller: GetParcelAndCost Returns 500 ")]
        [MemberData(nameof(GetInputParamsValid))]
        public void GetParcelAndCost_returns_500(int weight, int height, int width, int depth)
        {
            //Given
            mockParcelManager.Setup(m => m.FindParcel(It.IsAny<InputArgs>())).Throws<Exception>();

            var sut = new ParcelController(mockParcelManager.Object);

            //When
            var actual = sut.GetParcelAndCost(weight, height, width, depth);

            //Then
            var result = Assert.IsType<StatusCodeResult>(actual);
            Assert.Equal(500, result.StatusCode);
        }

        [Theory(DisplayName = "Controller: GetParcelAndCost Returns 404 ")]
        [MemberData(nameof(GetInputParamsValid))]
        public void GetParcelAndCost_returns_404(int weight, int height, int width, int depth)
        {
            //Given
            mockParcelManager.Setup(m => m.FindParcel(It.IsAny<InputArgs>())).Returns<ParcelResult>(null);

            var sut = new ParcelController(mockParcelManager.Object);

            //When
            var actual = sut.GetParcelAndCost(weight, height, width, depth);

            //Then
            var result = Assert.IsType<NotFoundResult>(actual);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
