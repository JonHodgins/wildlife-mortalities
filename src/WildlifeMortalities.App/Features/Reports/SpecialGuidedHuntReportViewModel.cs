﻿using FluentValidation;
using MudBlazor;
using WildlifeMortalities.Data.Entities.People;
using WildlifeMortalities.Data.Entities.Reports.MultipleMortalities;
using WildlifeMortalities.Data.Enums;

namespace WildlifeMortalities.App.Features.Reports;

public class SpecialGuidedHuntReportViewModel
{
    public DateRange HuntingDateRange { get; set; } = new();
    public Client? Guide { get; set; }
    public GuidedHuntResult? Result { get; set; }

    public List<HuntedActivityViewModel> HuntedActivityViewModels { get; set; } = new();

    public SpecialGuidedHuntReport GetReport(int personId)
    {
        // Clear hunted activities if the hunter wasn't successful
        if (Result is not GuidedHuntResult.WentHuntingAndKilledWildlife)
        {
            HuntedActivityViewModels.Clear();
        }

        var report = new SpecialGuidedHuntReport
        {
            HuntStartDate = (DateTime)HuntingDateRange!.Start!,
            HuntEndDate = (DateTime)HuntingDateRange.End!,
            GuideId = Guide!.Id,
            Result = Result!.Value,
            ClientId = personId,
            HuntedActivities = HuntedActivityViewModels.Select(x => x.GetActivity()).ToList()
        };

        return report;
    }
}

public class SpecialGuidedHuntReportViewModelValidator
    : AbstractValidator<SpecialGuidedHuntReportViewModel>
{
    public SpecialGuidedHuntReportViewModelValidator()
    {
        RuleFor(x => x.HuntingDateRange.Start)
            .NotNull()
            .WithMessage("Please enter the hunting dates");
        RuleFor(x => x.Guide).NotNull();
        RuleFor(x => x.Result).IsInEnum().NotNull();
        //RuleForEach(report => report.HuntedActivityViewModels)
        //    .Must(
        //        (report, activity) =>
        //            activity.MortalityWithSpeciesSelectionViewModel.MortalityViewModel.DateOfDeath
        //                >= report.HuntingDateRange.Start
        //            && activity
        //                .MortalityWithSpeciesSelectionViewModel
        //                .MortalityViewModel
        //                .DateOfDeath <= report.HuntingDateRange.End
        //    )
        //    .WithMessage(
        //        "The date of death for each mortality must be between the specified hunting dates"
        //    );
    }
}
