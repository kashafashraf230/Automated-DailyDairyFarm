using System;
using System.Collections.Generic;

namespace DailyDairyAuto.Models;

public partial class CattleDatum
{
    public int GroupId { get; set; }

    public double GrpAvgMilk { get; set; }

    public int? TotalCows { get; set; }

    public virtual ICollection<CowDatum> CowData { get; set; } = new List<CowDatum>();
}
