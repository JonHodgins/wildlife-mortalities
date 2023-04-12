﻿using WildlifeMortalities.Data.Entities.People;

namespace WildlifeMortalities.Data.Entities.Reports;

public class DraftReport
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public DateTimeOffset DateLastModified { get; set; }
    public DateTimeOffset DateSubmitted { get; set; }
    public string SerializedData { get; set; } = string.Empty;
}
