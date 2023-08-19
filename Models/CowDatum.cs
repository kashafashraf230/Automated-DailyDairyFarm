using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyDairyAuto.Models;

public partial class CowDatum
{

    [Key]
    public int CowId { get; set; }

    public string Age { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public string? Status { get; set; }

    public string? Vaccinated { get; set; }

    public double AvgMilkProd { get; set; }

    public int GroupId { get; set; }

    public virtual CattleDatum Group { get; set; } = null!;
}
