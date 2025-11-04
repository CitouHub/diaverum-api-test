namespace Diaverum.Domain
{
    public class DiaverumItemDTO
    {
        public int? Id { get; set; }

        public string? OptionalStringValue { get; set; }

        public string RequredStringValue { get; set; } = null!;

        public short EvenNumber { get; set; }

        public DateTime DateValue { get; set; }
    }
}
