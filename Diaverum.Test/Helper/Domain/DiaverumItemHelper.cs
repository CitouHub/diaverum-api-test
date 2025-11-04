using Diaverum.Data;
using Diaverum.Domain;

namespace Diaverum.Test.Helper.Domain
{
    public static class DiaverumItemHelper
    {
        public static DiaverumItemDTO New(
            short? id = 1,
            string requredStringValue = "requredStringValue",
            string? optionalStringValue = "optionalStringValue",
            short evenNumber = 0,
            DateTime? dateValue = null)
        {
            return new DiaverumItemDTO()
            {
                Id = id,
                RequredStringValue = requredStringValue,
                OptionalStringValue = optionalStringValue,
                EvenNumber = evenNumber,
                DateValue = dateValue ?? DateTime.UtcNow
            };
        }

        public static DiaverumItem NewDb(
            short id = 1,
            string text = "text",
            string? textDetails = "textDetails",
            short evenNumber = 0,
            DateTime? eventDate = null)
        {
            return new DiaverumItem()
            {
                Id = id,
                Text = text,
                TextDetails = textDetails,
                EvenNumber = evenNumber,
                EventDate = eventDate ?? DateTime.UtcNow
            };
        }
    }
}
