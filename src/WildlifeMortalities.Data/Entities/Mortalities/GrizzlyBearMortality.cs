﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildlifeMortalities.Data.Entities.BiologicalSubmissions;

namespace WildlifeMortalities.Data.Entities.Mortalities;

public class GrizzlyBearMortality : Mortality<GrizzlyBearMortality>, IHasBioSubmission
{
    public bool IsShotInConflict { get; set; }
    public GrizzlyBearBioSubmission? BioSubmission { get; set; }

    public override Species Species => Species.GrizzlyBear;

    public BioSubmission CreateDefaultBioSubmission() => new GrizzlyBearBioSubmission(this);

    public override IEntityTypeConfiguration<GrizzlyBearMortality> GetConfig() =>
        new GrizzlyBearMortalityConfig();
}

public class GrizzlyBearMortalityConfig : IEntityTypeConfiguration<GrizzlyBearMortality>
{
    public void Configure(EntityTypeBuilder<GrizzlyBearMortality> builder) =>
        builder
            .Property(m => m.IsShotInConflict)
            .HasColumnName(nameof(GrizzlyBearMortality.IsShotInConflict));
}
