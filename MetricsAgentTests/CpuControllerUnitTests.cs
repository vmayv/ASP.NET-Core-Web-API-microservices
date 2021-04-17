using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> _mockRepository;
        private Mock<ILogger<CpuMetricsController>> _mockLogger;
        private Mock<IMapper> _mockMapper;

        public CpuControllerUnitTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = new DateTimeOffset(DateTime.Now), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.Once());
        }

        [Fact]
        public void GetMetricsByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetric>()).Verifiable();
            // выполняем действие на контроллере
            var result = _controller.GetMetricsByTimePeriod(new DateTimeOffset(DateTime.Now), new DateTimeOffset(DateTime.Now));

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.Once());
        }


        [Fact]
        public void GetMetricsByTimePeriod_ReturnOk()
        {
            //Arrange
            var fromTime = new DateTimeOffset(DateTime.Now);
            var toTime = new DateTimeOffset(DateTime.Now);
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<CpuMetric>());

            //Act
            var result = _controller.GetMetricsByTimePeriod(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByTimePeriodByPercentile_ReturnOk()
        {
            //Arrange
            var fromTime = new DateTimeOffset(DateTime.Now);
            var toTime = new DateTimeOffset(DateTime.Now);
            var percentile = Core.Percentile.P75;

            //Act
            var result = _controller.GetMetricsByTimePeriodByPercentile(fromTime, toTime, percentile);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
