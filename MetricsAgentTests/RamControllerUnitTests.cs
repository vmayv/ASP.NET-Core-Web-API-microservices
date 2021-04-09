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
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> _mockRepository;
        private Mock<ILogger<RamMetricsController>> _mockLogger;
        private Mock<IMapper> _mockMapper;

        public RamControllerUnitTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IRamMetricsRepository>();
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит RamMetric объект
            _mockRepository.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.RamMetricCreateRequest { Time = new DateTimeOffset(DateTime.Now), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.Once());
        }

        [Fact]
        public void GetMetricsByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит RamMetric объект
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetric>());

            // выполняем действие на контроллере
            var result = _controller.GetMetricsByTimePeriod(new DateTimeOffset(DateTime.Now), new DateTimeOffset(DateTime.Now));

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.Once());
        }

        [Fact]
        public void GetAvailableRam_ShouldCall_GetLast_From_Repository()
        {
            // устанавливаем параметр заглушки
            _mockRepository.Setup(repository => repository.GetLast()).Returns(new RamMetric());

            // выполняем действие на контроллере
            var result = _controller.GetAvaliableRam();

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.GetLast(), Times.Once());
        }

        [Fact]
        public void GetAvailableRam_ReturnsOk()
        {
            //Arrange
            _mockRepository.Setup(repository => repository.GetLast()).Returns(new RamMetric()).Verifiable();
            //Act
            var result = _controller.GetAvaliableRam();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByTimePeriod_ReturnOk()
        {
            //Arrange
            var fromTime = new DateTimeOffset(DateTime.Now);
            var toTime = new DateTimeOffset(DateTime.Now);
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Returns(new List<RamMetric>());

            //Act
            var result = _controller.GetMetricsByTimePeriod(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
