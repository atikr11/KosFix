namespace CivicWatch.Models;
public enum Status { Submitted, UnderReview, Assigned, InProgress, Resolved }
public class Report
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Location { get; set; } = "";
    public string Description { get; set; } = "";
    public string Emoji { get; set; } = "📍";
    public DateTime Date { get; set; } = DateTime.Today;
    public Status Status { get; set; }
    public string? PhotoPath { get; set; }

    public static string Name(Status s) => s switch { Status.UnderReview => "Under review", Status.InProgress => "In progress", _ => s.ToString() };
    public string StatusText => Name(Status);
    public string DateText => Date.ToString("MMM d, yyyy");
    public Color ChipBg => Status switch { Status.InProgress => Theme.Soft, Status.UnderReview => Theme.VioletBg, Status.Resolved => Theme.GreenBg, _ => Theme.Line };
    public Color ChipFg => Status switch { Status.InProgress => Theme.Primary, Status.UnderReview => Theme.Violet, Status.Resolved => Theme.Green, _ => Theme.Muted };
}
