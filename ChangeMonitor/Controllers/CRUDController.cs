using ChangeMonitor.Contracts;
using ChangeMonitor.Helpers;
using ChangeMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRNotify;
using System;

namespace ChangeMonitor.Controllers {

    public class CRUDController : Controller {
        private readonly ITestdataRepository _repo;
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public CRUDController(ITestdataRepository repo, ILogger<HomeController> logger, IHubContext<NotificationHub> hubContext) {
            _repo = repo;
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Index() {
            var testdatas = _repo.GetAll();
            return View(testdatas);
        }

        [HttpGet]
        public IActionResult Create() {
            var testdata = new Testdata { CreateDate = DateTime.Now };
            return View(testdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int Id, [Bind("Id, Title, Value, CreateDate")] Testdata testdata) {
            if (!ModelState.IsValid) {
                return View(testdata);
            }
            _repo.CreateTestdata(testdata);

            SignalRDispatcher dispatcher = new SignalRDispatcher(_hubContext);
            SignalRMessage srm = new SignalRMessage($"{DateTime.Now}", "Create", testdata.Title, testdata.Value);
            dispatcher.SendToAllClient(srm.Serialize());

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? Id) {
            if (Id == null) {
                return NotFound();
            }
            var testdata = _repo.GetTestdata(Id);
            if (testdata == null) {
                return NotFound();
            }
            return View(testdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id, Title, Value, CreateDate")] Testdata testdata) {
            if (!ModelState.IsValid) {
                return View(testdata);
            }
            _repo.UpdateTestdata(testdata);

            SignalRDispatcher dispatcher = new SignalRDispatcher(_hubContext);
            SignalRMessage srm = new SignalRMessage($"{DateTime.Now}", "Update", testdata.Title, testdata.Value);
            dispatcher.SendToAllClient(srm.Serialize());

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? Id) {
            if (Id == null) {
                return NotFound();
            }
            var testdata = _repo.GetTestdata(Id);
            if (testdata == null) {
                return NotFound();
            }
            return View(testdata);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            if (!ModelState.IsValid) {
                return View(id);
            }
            Testdata testdata = _repo.RemoveTestdata(id);

            SignalRDispatcher dispatcher = new SignalRDispatcher(_hubContext);
            SignalRMessage srm = new SignalRMessage($"{DateTime.Now}", "Remove", testdata.Title, testdata.Value);
            dispatcher.SendToAllClient(srm.Serialize());

            return RedirectToAction(nameof(Index));
        }
    }
}
