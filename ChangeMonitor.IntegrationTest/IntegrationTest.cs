using ChangeMonitor.Contracts;
using ChangeMonitor.Controllers;
using ChangeMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SignalRNotify;
using System.Reflection;
using Xunit;

namespace ChangeMonitor.IntegrationTest {
    public class IntegrationTest {
        private ILogger<HomeController> _logger;
        private MainDbContext _context;
        private readonly Mock<ITestdataRepository> _mockRepo;
        private readonly Mock<IHubContext<NotificationHub>> _hubContext;

        public IntegrationTest() {
            DbContextOptions<MainDbContext> options;
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            builder.UseSqlite("Filename=TestDatabase.db", options => {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            }
            );
            options = builder.Options;
            _context = new MainDbContext(options);
            _mockRepo = new Mock<ITestdataRepository>();
            _hubContext = new Mock<IHubContext<NotificationHub>>();

        }

        [Fact]
        public void CheckHomeIndex() {
            // Arrange
            HomeController controller = new HomeController(_context, _logger);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CheckHomeIndexReturnType() {
            // Arrange
            HomeController controller = new HomeController(_context, _logger);
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            var viewResult = new ViewResult();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CheckCRUDCreate() {
            // Arrange
            CRUDController controller = new CRUDController(_mockRepo.Object, _logger, _hubContext.Object);
            // Act
            Testdata testdata = new Testdata() { Title = "Teszt cím", Value = "Teszt érték" };
            var result = controller.Create(0, testdata);
            // Assert
            Assert.NotNull(result);
        }

    }
}
