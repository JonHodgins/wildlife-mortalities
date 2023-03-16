﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildlifeMortalities.Data.Entities.People;
using WildlifeMortalities.Data.Entities.Reports;

namespace WildlifeMortalities.Data.Entities.Authorizations;

public class SpecialGuideLicence : Authorization, IHasBigGameHuntingLicence
{
    public int GuidedClientId { get; set; }
    public Client GuidedClient { get; set; } = null!;
    public int BigGameHuntingLicenceId { get; set; }
    public BigGameHuntingLicence BigGameHuntingLicence { get; set; } = default!;

    public override AuthorizationResult GetResult(Report report) =>
        throw new NotImplementedException();
}

public class SpecialGuideLicenceConfig : IEntityTypeConfiguration<SpecialGuideLicence>
{
    public void Configure(EntityTypeBuilder<SpecialGuideLicence> builder)
    {
        builder
            .ToTable("Authorizations")
            .HasOne(s => s.BigGameHuntingLicence)
            .WithOne(h => h.SpecialGuideLicence)
            .HasForeignKey<SpecialGuideLicence>(s => s.BigGameHuntingLicenceId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(s => s.GuidedClient)
            .WithMany(c => c.SpecialGuideLicencesAsClient)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
