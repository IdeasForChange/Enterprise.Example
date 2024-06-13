using Enterprise.Example.Data.Repositories;
using Enterprise.Example.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Enterprise.Example.Service.UnitTests
{
    public class NotificationServiceTests
    {
        private readonly NotificationService _service;
        private readonly Mock<INotificationRepository> _notificationRepositoryMock = new Mock<INotificationRepository>();
        private readonly Mock<ILogger<NotificationService>> _loggerMock = new Mock<ILogger<NotificationService>>();

        public NotificationServiceTests()
        {
            _service = new NotificationService(_notificationRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNotification_WhenNotificationExists()
        {
            // Arrange
            var notificationName = "A Test Notification";
            var notificationDTO = new Notification
            {
                Name = notificationName
            };

            var expectedResult = notificationName;


            _notificationRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(notificationDTO);

            // Act
            var actualResult = await _service.GetByNameAsync(notificationName);

            // Assert
            Assert.Equal(expectedResult, actualResult.Name);
        }
    }
}