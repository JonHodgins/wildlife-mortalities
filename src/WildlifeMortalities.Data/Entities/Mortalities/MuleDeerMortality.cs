﻿using WildlifeMortalities.Data.Entities.BiologicalSubmissions;

namespace WildlifeMortalities.Data.Entities.Mortalities;

public class MuleDeerMortality : Mortality, IHasBioSubmission
{
    public MuleDeerBioSubmission? BioSubmission { get; set; }
    public override Species Species => Species.MuleDeer;
}
