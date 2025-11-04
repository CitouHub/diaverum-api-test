using Diaverum.Data;
using Microsoft.EntityFrameworkCore;

namespace Diaverum.Test.Helper
{
    public static class DatabaseHelper
    {
        public static DiaverumDbContext GetContextAsync()
        {
            var options = new DbContextOptionsBuilder<DiaverumDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new DiaverumDbContext(options);
        }
    }
}
