using MetricsAgent.Controllers;
using MetricsAgent.DAL;
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
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<INetworkMetricsRepository> _mockRepository;
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;

        public NetworkControllerUnitTests()
        {
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит NetworkMetric объект
            _mockRepository.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.NetworkMetricCreateRequest { Time = new DateTimeOffset(DateTime.Now), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.Once());
        }

        [Fact]
        public void GetMetricsByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит NetworkMetric объект
            _mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.GetMetricsByTimePeriod(new DateTimeOffset(DateTime.Now), new DateTimeOffset(DateTime.Now));

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.Once());
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = new DateTimeOffset(DateTime.Now);
            var toTime = new DateTimeOffset(DateTime.Now);

            //Act
            var result = _controller.GetMetricsByTimePeriod(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
