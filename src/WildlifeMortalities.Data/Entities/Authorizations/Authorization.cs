﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildlifeMortalities.Data.Entities.People;
using WildlifeMortalities.Data.Entities.Reports.SingleMortality;

namespace WildlifeMortalities.Data.Entities.Authorizations;

public class ViolationResult
{
}

public class AuthorizationResult
{
    private AuthorizationResult(
        Authorization authorization,
        IEnumerable<ViolationResult> violations,
        bool isApplicable
    )
    {
        Authorization = authorization;
        Violations = violations;
        IsApplicable = isApplicable;
    }

    private AuthorizationResult(Authorization authorization)
        : this(authorization, Array.Empty<ViolationResult>())
    {
    }

    private AuthorizationResult(
        Authorization authorization,
        IEnumerable<ViolationResult> violations
    ) : this(authorization, violations, true)
    {
    }

    public Authorization Authorization { get; }
    public IEnumerable<ViolationResult> Violations { get; }
    public bool HasViolations => Violations.Any();
    public bool IsApplicable { get; }

    public static AuthorizationResult IsLegal(Authorization authorization) => new(authorization);

    public static AuthorizationResult NotApplicable(Authorization authorization) =>
        new(authorization, Array.Empty<ViolationResult>(), false);

    public static AuthorizationResult IsIllegal(
        Authorization authorization,
        IEnumerable<ViolationResult> violations
    ) => new(authorization, violations);
}

public abstract class Authorization
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTimeOffset? ActiveFromDate { get; set; }
    public DateTimeOffset? ActiveToDate { get; set; }

    public string Season =>
        ActiveFromDate is null || ActiveToDate is null
            ? string.Empty
            : $"{ActiveFromDate?.Year}-{ActiveToDate?.Year}";

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public static AuthorizationsSummary GetSummary(
        IEnumerable<Authorization> authorizations,
        MortalityReport report
    )
    {
        List<AuthorizationResult> applicableAuthorizationResults = new();
        foreach (var authorization in authorizations)
        {
            var authorizationResult = authorization.GetResult(report);
            if (authorizationResult.IsApplicable)
            {
                applicableAuthorizationResults.Add(authorizationResult);
            }
        }

        return new AuthorizationsSummary(applicableAuthorizationResults);
    }

    public abstract AuthorizationResult GetResult(MortalityReport report);

    public record AuthorizationsSummary(
        IEnumerable<AuthorizationResult> ApplicableAuthorizationResults
    );
}

public class AuthorizationConfig : IEntityTypeConfiguration<Authorization>
{
    public void Configure(EntityTypeBuilder<Authorization> builder)
    {
    }
}
