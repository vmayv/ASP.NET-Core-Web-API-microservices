using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;

        public NetworkControllerUnitTests()
        {
            _controller = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = new TimeSpan(0);
            var toTime = new TimeSpan(100);

            //Act
            var result = _controller.GetMetrics(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
