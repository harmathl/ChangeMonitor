using ChangeMonitor.Models;
using System.Collections.Generic;

namespace ChangeMonitor.Contracts {
    public interface ITestdataRepository {
        IEnumerable<Testdata> GetAll();
        Testdata GetTestdata(int? id);
        void CreateTestdata(Testdata testdata);
        void UpdateTestdata(Testdata testdata);
        Testdata RemoveTestdata(int id);
    }
}
