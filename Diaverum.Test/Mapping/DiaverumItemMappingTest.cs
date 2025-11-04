using AutoMapper;
using Diaverum.Data;
using Diaverum.Domain;
using Diaverum.Test.Helper;
using Diaverum.Test.Helper.Domain;

namespace Diaverum.Test.Mapping
{
    public class DiaverumItemMappingTest
    {
        private static readonly IMapper _mapper = MapperHelper.DefineMapper();

        public class InvoiceMapping
        {
            [Fact]
            public static void ToDTO()
            {
                // Arrange
                var diaverumItemDb = DiaverumItemHelper.NewDb();

                // Act
                var dto = _mapper.Map<DiaverumItemDTO>(diaverumItemDb);

                // Assert
                Assert.NotNull(dto);
            }

            [Fact]
            public static void ToDB()
            {
                // Arrange
                var diaverumItem = DiaverumItemHelper.New();

                // Act
                var dto = _mapper.Map<DiaverumItem>(diaverumItem);

                // Assert
                Assert.NotNull(dto);
            }
        }
    }
}
