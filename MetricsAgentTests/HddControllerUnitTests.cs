using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController _controller;

        public HddControllerUnitTests()
        {
            _controller = new HddMetricsController();
        }

        [Fact]
        public void GetHddLeft_ReturnOk()
        {
            //Arrange

            //Act
            var result = _controller.GetHddLeft();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
