using System;
using System.Collections.Generic;

namespace Diaverum.Data;

public partial class DiaverumItem
{
    public short Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public string Text { get; set; } = null!;

    public string? TextDetails { get; set; }

    public short EvenNumber { get; set; }

    public DateTime EventDate { get; set; }
}
