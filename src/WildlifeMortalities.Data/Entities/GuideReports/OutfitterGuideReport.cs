﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeMortalities.Data.Entities.People;

namespace WildlifeMortalities.Data.Entities.GuideReports;
public class OutfitterGuideReport
{
    public int Id { get; set; }
    public List<Client> Guides { get; set; } = null!;
    public List<HuntedHarvestReport> HuntedHarvestReports { get; set; } = null!;
}
