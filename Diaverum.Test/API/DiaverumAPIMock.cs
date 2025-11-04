
using Diaverum.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;

namespace Diaverum.Test.API
{
    public class DiaverumAPIMock() : WebApplicationFactory<Program>
    {
        private readonly Dictionary<string, string?> _inMemoryConfiguration = [];

        public readonly IDiaverumItemService MockDiaverumItemService = Substitute.For<IDiaverumItemService>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(_ => _.AddInMemoryCollection(_inMemoryConfiguration));
            builder.ConfigureServices(services =>
            {
                //SetMemeoryDatabase(services);

                services.AddControllers();

                services.Remove(new ServiceDescriptor(typeof(IDiaverumItemService), ServiceLifetime.Scoped));
                services.AddScoped(_ => MockDiaverumItemService);
            });
        }

        //private void SetMemeoryDatabase(IServiceCollection services)
        //{
        //    var datebaseRelatedServices = services.Where(_ =>
        //        _.ServiceType.Name.Contains(nameof(DiaverumDbContext))
        //    ).ToList();
        //    datebaseRelatedServices.ForEach(_ =>
        //    {
        //        services.Remove(_);
        //    });

        //    services.AddDbContext<DiaverumDbContext>(options =>
        //    {
        //        options
        //            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //            .EnableSensitiveDataLogging();
        //    });
        //}

        //public void UpdateConfiguration(string key, string value)
        //{
        //    var settingExists = _inMemoryConfiguration.ContainsKey(key);
        //    if (settingExists == true)
        //    {
        //        _inMemoryConfiguration.Remove(key);
        //    }
        //    _inMemoryConfiguration.Add(key, value);
        //}
    }
}