﻿using WildlifeMortalities.App.Features.Shared.Mortalities;
using WildlifeMortalities.Data.Entities.BiologicalSubmissions;
using WildlifeMortalities.Data.Entities.Reports.MultipleMortalities;
using WildlifeMortalities.Data.Entities.Reports.SingleMortality;
using WildlifeMortalities.Shared.Services;

namespace WildlifeMortalities.App.Features.Reports;

public class TrappedActivityViewModel : ActivityViewModel
{
    private readonly int _id = Constants.EfCore.TemporaryAutoGeneratedKey;
    private readonly int _reportId = Constants.EfCore.TemporaryAutoGeneratedKey;

    public TrappedActivityViewModel() { }

    public TrappedActivityViewModel(TrappedActivity activity, ReportDetail? reportDetail = null)
        : base(activity, reportDetail)
    {
        _id = activity.Id;
        _reportId = reportDetail?.report.Id ?? Constants.EfCore.TemporaryAutoGeneratedKey;
    }

    public TrappedActivityViewModel(TrappedActivity activity, TrappedMortalitiesReport report)
        : this(activity, new ReportDetail(report, Array.Empty<(int, BioSubmission)>())) { }

    public TrappedActivity GetActivity() =>
        new()
        {
            Mortality = MortalityWithSpeciesSelectionViewModel.MortalityViewModel.GetMortality(),
            Comment = Comment,
            Id = _id,
            TrappedMortalitiesReportId = _reportId,
        };
}

public class TrappedActivityViewModelValidator
    : ActivityViewModelValidator<TrappedActivityViewModel> { }
