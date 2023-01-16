﻿using WildlifeMortalities.Data.Entities.Reports.SingleMortality;

namespace WildlifeMortalities.Data.Entities;

public class Violation
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Description { get; set; } = "";
    public ViolationType Type { get; set; }
    public List<MortalityReport> HarvestReports { get; set; } = null!;
}
