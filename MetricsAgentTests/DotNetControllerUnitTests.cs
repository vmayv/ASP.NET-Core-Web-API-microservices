using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;

        public DotNetControllerUnitTests()
        {
            _controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetErrorsCountMetrics_ReturnOk()
        {
            //Arrange
            var fromTime = new TimeSpan(0);
            var toTime = new TimeSpan(100);

            //Act
            var result = _controller.GetErrorsCountMetrics(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
