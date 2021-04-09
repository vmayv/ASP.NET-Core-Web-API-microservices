using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private Mock<IDotNetMetricsRepository> _mockRepository;
        private Mock<ILogger<DotNetMetricsController>> _mockLogger;
        private Mock<IMapper> _mockMapper;

        public DotNetControllerUnitTests()
        {
            _mockRepository = new Mock<IDotNetMetricsRepository>();
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит DotNetMetric объект
            _mockRepository.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.DotNetMetricCreateRequest { Time = new DateTimeOffset(DateTime.Now), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.Once());
        }

        [Fact]
        public void GetErrorsCountMetricsByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит DotNetMetric объект
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetric>()).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.GetMetricsByTimePeriod(new DateTimeOffset(DateTime.Now), new DateTimeOffset(DateTime.Now));

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.Once());
        }

        [Fact]
        public void GetErrorsCountMetrics_ReturnOk()
        {
            //Arrange
            var fromTime = new DateTimeOffset(DateTime.Now);
            var toTime = new DateTimeOffset(DateTime.Now);
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<DotNetMetric>());

            //Act
            var result = _controller.GetMetricsByTimePeriod(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
