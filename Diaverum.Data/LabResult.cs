using System;
using System.Collections.Generic;

namespace Diaverum.Data;

public partial class LabResult
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public string ClinicNo { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public int PatientId { get; set; }

    public string PatientName { get; set; } = null!;

    public DateOnly Dbo { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly CollentionDate { get; set; }

    public TimeOnly CollentionTime { get; set; }

    public string TestCode { get; set; } = null!;

    public string TestName { get; set; } = null!;

    public decimal? Result { get; set; }

    public string? Unit { get; set; }

    public string? RefrangeLow { get; set; }

    public string? RefrangeHigh { get; set; }

    public string? Note { get; set; }

    public string? NonSpecRefs { get; set; }
}
