using Diaverum.Common;

namespace Diaverum.Domain
{
    public class LabResultDTO
    {
        public string ClinicNo { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public int PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public DateOnly DBO { get; set; }
        public Gender Gender { get; set; }
        public DateOnly CollentionDate { get; set; }
        public TimeOnly CollentionTime { get; set; }
        public string TestCode { get; set; } = null!;
        public string TestName { get; set; } = null!;
        public double? Result { get; set; }
        public string? Unit { get; set; }
        public string? RefrangeLow { get; set; }
        public string? RefrangeHigh { get; set; }
        public string? Note { get; set; }
        public string? NonSpecRefs { get; set; }
        public bool Pending
        {
            get
            {
                return Result == null;
            }
        }
    }
}
