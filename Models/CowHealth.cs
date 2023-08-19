using System;
using System.Collections.Generic;

namespace DailyDairyAuto.Models;

public partial class CowHealth
{
    public int CowId { get; set; }

    public string? HealthStatus { get; set; }

    public virtual CowDatum Cow { get; set; } = null!;
}
