﻿namespace WildlifeMortalities.App;

public static class Constants
{
    public static class CascadingValues
    {
        public const string EditMode = "EditMode";
        public const string ReportType = "ReportType";
        public const string HasAttemptedFormSubmission = "HasAttemptedFormSubmission";
        public const string ReportViewModel = "ReportViewModel";
    }

    public static class EfCore
    {
        public const int TemporaryAutoGeneratedKey = 0;
    }

    public static class Routes
    {
        public const string HomePage = "/";

        public static string GetHomePageLink() => HomePage;

        #region report routes
        public const string ReportsOverviewPage = "reports";

        public static string GetReportsOverviewPageLink() => ReportsOverviewPage;

        public const string ReportDetailsPage = "reports/{humanReadablePersonId}/{reportId:int}";

        public static string GetReportDetailsPageLink(string humanReadablePersonId, int reportId) =>
            $"reports/{humanReadablePersonId}/{reportId}";

        public const string CreateReportPage = "/reports/{humanReadablePersonId}/new";

        public static string GetCreateReportPageLink(string humanReadablePersonId) =>
            $"reports/{humanReadablePersonId}/new";

        public const string EditReportPage = "reports/{humanReadablePersonId}/edit/{reportId:int}";

        public static string GetEditReportPageLink(string humanReadablePersonId, int reportId) =>
            $"reports/{humanReadablePersonId}/edit/{reportId}";

        public const string EditDraftReportPage =
            "reports/{humanReadablePersonId}/editdraft/{draftId:int}";

        public static string GetEditDraftReportPageLink(
            string humanReadablePersonId,
            int reportId
        ) => $"reports/{humanReadablePersonId}/editdraft/{reportId}";
        #endregion

        #region client routes
        public const string ClientLookupPage = "reporters/clients";

        public static string GetClientLookupPageLink() => ClientLookupPage;

        public const string ClientOverviewPage = "reporters/clients/{envClientId}";

        public static string GetClientOverviewPageLink(string envClientId) =>
            $"reporters/clients/{envClientId}";
        #endregion

        #region conservation officer routes
        public const string ConservationOfficerLookupPage = "reporters/conservation-officers";

        public static string GetConservationOfficerLookupPageLink() =>
            ConservationOfficerLookupPage;

        public const string ConservationOfficerOverviewPage =
            "reporters/conservation-officers/{badgeNumber}";

        public static string GetConservationOfficerOverviewPageLink(string badgeNumber) =>
            $"reporters/conservation-officers/{badgeNumber}";
        #endregion
    }
}
