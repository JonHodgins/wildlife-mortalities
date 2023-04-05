﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildlifeMortalities.Data.Entities.Reports;
using WildlifeMortalities.Data.Entities.Reports.SingleMortality;
using static WildlifeMortalities.Data.Constants;

namespace WildlifeMortalities.Data.Entities.Authorizations;

public class HuntingSeal : Authorization
{
    public enum SealType
    {
        [Display(Name = SpeciesConstants.AmericanBlackBear)]
        AmericanBlackBear = 10,

        [Display(Name = SpeciesConstants.Caribou)]
        Caribou = 20,

        [Display(Name = SpeciesConstants.MuleDeer)]
        MuleDeer = 30,

        [Display(Name = SpeciesConstants.Elk)]
        Elk = 40,

        [Display(Name = SpeciesConstants.GrizzlyBear)]
        GrizzlyBear = 50,

        [Display(Name = SpeciesConstants.Moose)]
        Moose = 60,

        [Display(Name = SpeciesConstants.MountainGoat)]
        MountainGoat = 70,

        [Display(Name = SpeciesConstants.ThinhornSheep)]
        ThinhornSheep = 80,

        [Display(Name = SpeciesConstants.WoodBison)]
        WoodBison = 90
    }

    public HuntingSeal() { }

    public HuntingSeal(SealType type) => Type = type;

    public SealType Type { get; set; }

    // Todo: Rename to "HuntedActivityId", and fix System.InvalidOperationException: The property 'HuntedActivityId' cannot be added to entity type 'Authorization' because it is declared on the CLR type 'HuntingSeal'.
    public int? HuntedActivityyId { get; set; }
    public HuntedActivity? HuntedActivityy { get; set; }

    public override AuthorizationResult GetResult(Report report) =>
        throw new NotImplementedException();
}

public class SealConfig : IEntityTypeConfiguration<HuntingSeal>
{
    public void Configure(EntityTypeBuilder<HuntingSeal> builder) =>
        builder.ToTable("Authorizations");
}
