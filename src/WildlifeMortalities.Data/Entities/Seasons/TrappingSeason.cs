﻿using WildlifeMortalities.Data.Entities.Reports;

namespace WildlifeMortalities.Data.Entities.Seasons;

public class TrappingSeason : Season
{
    private TrappingSeason() { }

    public TrappingSeason(int startYear)
    {
        StartDate = new DateTimeOffset(startYear, 7, 1, 0, 0, 0, TimeSpan.FromHours(-7));
        EndDate = new DateTimeOffset(startYear + 1, 6, 30, 23, 59, 59, TimeSpan.FromHours(-7));
    }

    public override TrappingSeason GetSeason(Report report)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{StartDate.Year % 100:00}/{EndDate.Year % 100:00}";
    }
}

public class TrappingSeasonConfig : SeasonConfig<TrappingSeason> { }
