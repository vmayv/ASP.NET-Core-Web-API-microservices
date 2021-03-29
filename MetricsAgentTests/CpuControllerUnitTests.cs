using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;

        public CpuControllerUnitTests()
        {
            _controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = _controller.GetMetrics(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPercentile_ReturnOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = ClassLibrary.Class.Percentile.P75;

            //Act
            var result = _controller.GetMetricsByPercentile(fromTime, toTime, percentile);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
