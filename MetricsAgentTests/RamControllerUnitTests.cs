using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;

        public RamControllerUnitTests()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetAvailableRam_ReturnsOk()
        {
            //Arrange

            //Act
            var result = _controller.GetAvailableRam();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
