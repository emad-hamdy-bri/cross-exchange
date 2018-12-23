using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Api.Controller;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class AnalysticsControllerTests
    {
        private readonly Mock<IAnalyticsService> _AnalyticsServiceMock = new Mock<IAnalyticsService>();

        private readonly AnalyticsController _AnalyticsController;

        public AnalysticsControllerTests()
        {
            _AnalyticsController = new AnalyticsController(_AnalyticsServiceMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _AnalyticsServiceMock.Reset();
        }

        [Test]
        public async Task ShouldGetDailySharePrice()
        {
            // Arrange
            string symbol = "CBI";
            int year = 2018;
            int month = 8;
            int day = 13;

            // Act
            var result = await _AnalyticsController.Daily(symbol,year,month,day);

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task ShouldGetWeeklySharePrice()
        {
            // Arrange
            string symbol = "CBI";
            int year = 2018;
            int week = 33;

            // Act
            var result = await _AnalyticsController.Weekly(symbol, year,week);

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task ShouldGetMonthlySharePrice()
        {
            // Arrange
            string symbol = "CBI";
            int year = 2018;
            int month = 8;

            // Act
            var result = await _AnalyticsController.Monthly(symbol, year, month);

            // Assert
            Assert.NotNull(result);
        }
    }
}
