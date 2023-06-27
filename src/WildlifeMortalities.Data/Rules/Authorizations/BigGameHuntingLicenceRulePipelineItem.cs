﻿using WildlifeMortalities.Data.Entities;
using WildlifeMortalities.Data.Entities.Authorizations;
using WildlifeMortalities.Data.Entities.Reports;
using WildlifeMortalities.Data.Entities.Reports.MultipleMortalities;
using WildlifeMortalities.Data.Entities.Reports.SingleMortality;
using WildlifeMortalities.Data.Extensions;

namespace WildlifeMortalities.Data.Rules.Authorizations;

public class BigGameHuntingLicenceRulePipelineItem : AuthorizationRulePipelineItem
{
    public override Task<bool> Process(Report report, AuthorizationRulePipelineContext context)
    {
        if (report.GeneralizedReportType is not GeneralizedReportType.Hunted)
        {
            return Task.FromResult(true);
        }

        foreach (var activity in report.GetActivities())
        {
            if (!activity.Mortality.Species.IsBigGameSpecies())
            {
                continue;
            }

            var authorization = report
                .GetPerson()
                .Authorizations.GetValidAuthorization<BigGameHuntingLicence>(activity);
            if (authorization == null)
            {
                context.Violations.Add(
                    new Violation(
                        activity,
                        Violation.RuleType.Authorization,
                        Violation.SeverityType.Illegal,
                        $"No big game hunting licence valid on {activity.Mortality.DateOfDeath:yyyy-MM-dd}."
                    )
                );
            }
            else
            {
                context.RelevantAuthorizations.Add(authorization);

                if (
                    report is SpecialGuidedHuntReport
                    && authorization.Type
                        is not BigGameHuntingLicence.LicenceType.CanadianResidentSpecialGuided
                ) {
                    context.Violations.Add(
                            new Violation(
                                activity,
                                Violation.RuleType.Authorization,
                                Violation.SeverityType.Illegal,
                                "Hunted big game as a Canadian resident without a valid licence."
                            )
                        );
                }
                else if (
                    (
                        report is IndividualHuntedMortalityReport
                        && authorization.Type
                            is BigGameHuntingLicence.LicenceType.CanadianResident
                                or BigGameHuntingLicence.LicenceType.CanadianResidentSpecialGuided
                    )
                )
                {
                    context.Violations.Add(
                        new Violation(
                            activity,
                            Violation.RuleType.Authorization,
                            Violation.SeverityType.Illegal,
                            "Hunted big game as a Canadian resident without a guide."
                        )
                    );
                } else if(report is OutfitterGuidedHuntReport)
            }
        }

        return Task.FromResult(true);
    }
}
