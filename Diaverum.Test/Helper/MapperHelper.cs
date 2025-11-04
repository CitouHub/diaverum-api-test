using AutoMapper;
using Diaverum.Mapping;
using Microsoft.Extensions.Logging;

namespace Diaverum.Test.Helper
{
    public static class MapperHelper
    {
        public static IMapper DefineMapper()
        {
            var logger = LoggerFactory.Create(config =>
            {
                config.AddConsole();
            });
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfiles([
                    new DiaverumProfile()
                ]);
            }, logger);

            return mapperConfig.CreateMapper();
        }
    }
}
