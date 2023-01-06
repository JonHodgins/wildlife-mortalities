﻿using WildlifeMortalities.Data.Entities.Mortalities;

namespace WildlifeMortalities.Data.Entities.BiologicalSubmissions;

public class AmericanBlackBearBioSubmission : BioSubmission
{
    public string SkullCondition { get; set; } = "";
    public int SkullLengthMillimetres { get; set; }
    public int SkullHeightMillimetres { get; set; }
    public int MortalityId { get; set; }
    public AmericanBlackBearMortality Mortality { get; set; } = null!;
}
