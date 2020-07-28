using ChangeMonitor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace ChangeMonitor.Helpers {
    public class DBInit {
        public void Initialization(IHost host) { 
        string dbName = "TestDatabase.db";
            if (File.Exists(dbName)) {
                File.Delete(dbName);
            }
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                using (var dbContext = new MainDbContext(services.GetRequiredService<
                    DbContextOptions<MainDbContext>>())) {
                    //Ensure database is created
                    dbContext.Database.EnsureCreated();
                    if (!dbContext.Testdatas.Any()) {
                        dbContext.Testdatas.AddRange(new Testdata[]
                            {
                             new Testdata{ Id=1, Title="Key1", Value="First value" },
                             new Testdata{ Id=2, Title="Key2", Value="Second value" },
                             new Testdata{ Id=3, Title="Key3", Value="Third value" }
                            });
                        dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
