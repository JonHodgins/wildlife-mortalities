﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildlifeMortalities.Data.Entities.Mortalities;

namespace WildlifeMortalities.Data.Entities.BiologicalSubmissions;

public class MuleDeerBioSubmission : BioSubmission<MuleDeerMortality>
{
    public MuleDeerBioSubmission() { }

    public MuleDeerBioSubmission(int mortalityId)
        : base(mortalityId) { }

    public MuleDeerBioSubmission(MuleDeerMortality mortality)
        : base(mortality) { }

    public bool IsHornIncluded { get; set; }
    public bool IsHeadIncluded { get; set; }
}

public class MuleDeerBioSubmissionConfig : IEntityTypeConfiguration<MuleDeerBioSubmission>
{
    public void Configure(EntityTypeBuilder<MuleDeerBioSubmission> builder)
    {
        builder
            .ToTable("BioSubmissions")
            .HasOne(b => b.Mortality)
            .WithOne(m => m.BioSubmission)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasIndex(x => x.MortalityId)
            .HasFilter("[MuleDeerBioSubmission_MortalityId] IS NOT NULL");
    }
}
