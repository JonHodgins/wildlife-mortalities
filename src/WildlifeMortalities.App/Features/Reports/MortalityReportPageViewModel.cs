﻿using FluentValidation;
using WildlifeMortalities.Data.Entities.Reports;
using WildlifeMortalities.Data.Entities.Reports.MultipleMortalities;
using WildlifeMortalities.Data.Entities.Reports.SingleMortality;

namespace WildlifeMortalities.App.Features.Reports;

public class MortalityReportPageViewModel
{
    private DateTime? _dateSubmitted;

    public bool IsUpdate { get; }

    public MortalityReportPageViewModel()
    {
        IsUpdate = false;
    }

    public MortalityReportPageViewModel(Report report)
    {
        IsUpdate = true;
        switch (report)
        {
            case IndividualHuntedMortalityReport individualHuntedMortalityReport:
                ReportType = ReportType.IndividualHuntedMortalityReport;
                IndividualHuntedMortalityReportViewModel =
                    new IndividualHuntedMortalityReportViewModel(individualHuntedMortalityReport);
                break;
            case OutfitterGuidedHuntReport outfitterGuidedHuntReport:
                ReportType = ReportType.OutfitterGuidedHuntReport;
                OutfitterGuidedHuntReportViewModel = new OutfitterGuidedHuntReportViewModel(
                    outfitterGuidedHuntReport
                );
                break;
            case SpecialGuidedHuntReport specialGuidedHuntReport:
                ReportType = ReportType.SpecialGuidedHuntReport;
                SpecialGuidedHuntReportViewModel = new SpecialGuidedHuntReportViewModel(
                    specialGuidedHuntReport
                );
                break;
            case TrappedMortalitiesReport trappedMortalitiesReport:
                ReportType = ReportType.TrappedMortalitiesReport;
                TrappedReportViewModel = new TrappedReportViewModel(trappedMortalitiesReport);
                break;
        }
        DateSubmitted = report.DateSubmitted.DateTime;
    }

    public ReportType ReportType { get; set; } = ReportType.IndividualHuntedMortalityReport;

    // Todo: refactor
    public DateTime? DateSubmitted
    {
        get { return _dateSubmitted; }
        set
        {
            _dateSubmitted = value;
            if (IndividualHuntedMortalityReportViewModel != null)
            {
                IndividualHuntedMortalityReportViewModel.DateSubmitted = value;
            }
            else if (OutfitterGuidedHuntReportViewModel != null)
            {
                OutfitterGuidedHuntReportViewModel.DateSubmitted = value;
            }
            else if (SpecialGuidedHuntReportViewModel != null)
            {
                SpecialGuidedHuntReportViewModel.DateSubmitted = value;
            }
            else if (TrappedReportViewModel != null)
            {
                TrappedReportViewModel.DateSubmitted = value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    public IndividualHuntedMortalityReportViewModel? IndividualHuntedMortalityReportViewModel { get; set; } =
        new();

    public OutfitterGuidedHuntReportViewModel? OutfitterGuidedHuntReportViewModel { get; set; }
    public SpecialGuidedHuntReportViewModel? SpecialGuidedHuntReportViewModel { get; set; }
    public TrappedReportViewModel? TrappedReportViewModel { get; set; }
}

public class MortalityReportPageViewModelValidator : AbstractValidator<MortalityReportPageViewModel>
{
    public MortalityReportPageViewModelValidator()
    {
        RuleFor(x => x.ReportType).NotEmpty();

        RuleFor(x => x.IndividualHuntedMortalityReportViewModel)
            .SetValidator(new IndividualHuntedMortalityReportViewModelValidator())
            .When(x => x.ReportType == ReportType.IndividualHuntedMortalityReport);

        RuleFor(x => x.OutfitterGuidedHuntReportViewModel)
            .SetValidator(new OutfitterGuidedHuntReportViewModelValidator())
            .When(x => x.ReportType == ReportType.OutfitterGuidedHuntReport);

        RuleFor(x => x.SpecialGuidedHuntReportViewModel)
            .SetValidator(new SpecialGuidedHuntReportViewModelValidator())
            .When(x => x.ReportType == ReportType.SpecialGuidedHuntReport);

        RuleFor(x => x.TrappedReportViewModel)
            .SetValidator(new TrappedReportViewModelValidator())
            .When(x => x.ReportType == ReportType.TrappedMortalitiesReport);
    }
}
