using ChangeMonitor.Contracts;
using ChangeMonitor.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChangeMonitor.Repository {
    public class TestdataRepository: ITestdataRepository {
        private readonly MainDbContext _context;

        public TestdataRepository(MainDbContext context) {
            _context = context;
        }

        public IEnumerable<Testdata> GetAll() => _context.Testdatas.ToList();

        public Testdata GetTestdata(int? id) => _context.Testdatas
            .SingleOrDefault(e => e.Id.Equals(id));

        public void CreateTestdata(Testdata testdata) {
            _context.Add(testdata);
            _context.SaveChanges();
        }

        public void UpdateTestdata(Testdata testdata) {
            _context.Update(testdata);
            _context.SaveChanges();
        }

        public Testdata RemoveTestdata(int id) {
            var testdata = _context.Testdatas.Find(id);
            _context.Testdatas.Remove(testdata);
            _context.SaveChangesAsync();
            return testdata;
        }

    }
}
