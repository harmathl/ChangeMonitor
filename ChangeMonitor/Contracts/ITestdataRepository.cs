using ChangeMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeMonitor.Contracts {
    public interface ITestdataRepository {
        IEnumerable<Testdata> GetAll();
        Testdata GetTestdata(int? id);
        void CreateTestdata(Testdata testdata);
        void UpdateTestdata(Testdata testdata);
        Testdata RemoveTestdata(int id);
    }
}
