using ChangeMonitor.Contracts;
using ChangeMonitor.Controllers;
using ChangeMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using SignalRNotify;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChangeMonitor.IntegrationTest.Controller {
    public class ChangeMonitorControllerTest {
        private readonly Mock<ITestdataRepository> _mockRepo;
        private readonly CRUDController _controller;
        private readonly Mock<IHubContext<NotificationHub>> _hubContext;

        public ChangeMonitorControllerTest() {
            _mockRepo = new Mock<ITestdataRepository>();
            _hubContext = new Mock<IHubContext<NotificationHub>>();
            _controller = new CRUDController(_mockRepo.Object, null, _hubContext.Object);
            //_hubContext = new HubContext();
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex() {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfTestdatas() {
            _mockRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Testdata>() { new Testdata(), new Testdata() });

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var employees = Assert.IsType<List<Testdata>>(viewResult.Model);
            Assert.Equal(2, employees.Count);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate() {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ModelStateValid_CreateEmployeeCalledOnce() {
            Testdata testdata = null;
            _mockRepo.Setup(r => r.CreateTestdata(It.IsAny<Testdata>()))
                .Callback<Testdata>(x => testdata = x);

             testdata = new Testdata {
                Title = "qqq",
                Value = "aaa"
            };

            _controller.Create(0, testdata);

            _mockRepo.Verify(x => x.CreateTestdata(It.IsAny<Testdata>()), Times.Once);

            Assert.Equal(testdata.Title, testdata.Title);
            Assert.Equal(testdata.Value, testdata.Value);
        }

    }
}
