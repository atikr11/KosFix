using CivicWatch.Models;
namespace CivicWatch.Services;

// In-memory only for now. Swap this class for an API/database later; pages only talk to it.
public static class ReportStore
{
    public static readonly (string Name, string Title, string Emoji)[] Categories =
    {
        ("Roads", "Road damage", "🕳️"), ("Waste", "Overflowing bin", "🗑️"), ("Traffic", "Broken traffic light", "🚦"),
        ("Buildings", "Damaged building", "🏢"), ("Lighting", "Street light out", "💡"), ("Other", "Other problem", "📍"),
    };

    public static IReadOnlyList<string> Municipalities { get; } =
        Array.AsReadOnly(new[] { "Mitrovica e Jugut", "Pejë", "Prishtina" });

    static readonly List<Report> Reports = new()
    {
        new() { Id = "CW-000124", Title = "Road damage", Municipality = "Prishtina", Location = "Main Street, Prishtina", Date = new(2024, 10, 12), Status = Status.Submitted, Emoji = "🕳️", Description = "Large pothole near the intersection." },
        new() { Id = "CW-000123", Title = "Overflowing bin", Municipality = "Pejë", Location = "Rruga e Drenicës, Pejë", Date = new(2024, 10, 10), Status = Status.InProgress, Emoji = "🗑️", Description = "Bin has not been emptied for a week." },
        new() { Id = "CW-000122", Title = "Broken traffic light", Municipality = "Prishtina", Location = "City Center, Prishtina", Date = new(2024, 10, 8), Status = Status.UnderReview, Emoji = "🚦", Description = "Light is stuck on red." },
        new() { Id = "CW-000121", Title = "Damaged building", Municipality = "Prishtina", Location = "Bulevardi, Prishtina", Date = new(2024, 10, 5), Status = Status.Resolved, Emoji = "🏢", Description = "Cracked facade above the pavement." },
    };

    public static IReadOnlyList<Report> All { get; } = Reports.AsReadOnly();

    public static List<Report> ForMunicipality(string municipality) =>
        Reports.Where(r => r.Municipality == municipality).ToList();

    public static bool UpdateStatus(string municipality, string id, Status status)
    {
        var report = Find(id);
        if (report is null || report.Municipality != municipality || !Enum.IsDefined(typeof(Status), status))
            return false;

        report.Status = status;
        return true;
    }

    public static Report? Find(string? id) => All.FirstOrDefault(r => r.Id == id);

    public static Report Add(string category, string municipality, string location, string description, string? photoPath)
    {
        if (!Municipalities.Contains(municipality))
            throw new ArgumentException("Choose a supported demo municipality.", nameof(municipality));

        var c = Categories.First(x => x.Name == category);
        int next = All.Max(r => int.Parse(r.Id[3..])) + 1;
        var r = new Report
        {
            Id = $"CW-{next:D6}", Title = c.Title, Emoji = c.Emoji, Municipality = municipality, Location = location, PhotoPath = photoPath,
            Description = string.IsNullOrWhiteSpace(description) ? "No description added." : description,
            Date = DateTime.Today, Status = Status.Submitted,
        };
        Reports.Insert(0, r);
        return r;
    }
}
