using CivicWatch.Models;
using CivicWatch.Services;
namespace CivicWatch.Pages;

public partial class MockInboxPage : ContentPage
{
    static readonly Status[] Statuses = Enum.GetValues<Status>();

    public MockInboxPage()
    {
        InitializeComponent();
        MunicipalityPicker.ItemsSource = ReportStore.Municipalities.ToList();
    }

    protected override void OnAppearing() { base.OnAppearing(); Build(); }
    void OnMunicipalityChanged(object? s, EventArgs e) => Build();

    void Build()
    {
        Reports.Children.Clear();
        if (MunicipalityPicker.SelectedItem is not string municipality)
        {
            Empty.Text = "Choose a municipality to view its local mock inbox.";
            Empty.IsVisible = true;
            return;
        }

        var reports = ReportStore.ForMunicipality(municipality);
        Empty.Text = "No demo reports for this municipality yet.";
        Empty.IsVisible = reports.Count == 0;

        foreach (var report in reports)
        {
            var status = new Picker { Title = "Demo status", TextColor = Theme.Ink };
            status.ItemsSource = Statuses.Select(Report.Name).ToList();
            status.SelectedIndex = Array.IndexOf(Statuses, report.Status);
            var saved = new Label { FontSize = 13, TextColor = Theme.Muted };
            status.SelectedIndexChanged += (_, _) =>
            {
                int index = status.SelectedIndex;
                if (index >= 0 && ReportStore.UpdateStatus(municipality, report.Id, Statuses[index]))
                    saved.Text = "Demo status updated in My Reports.";
            };

            var card = new Border
            {
                Style = (Style)Application.Current!.Resources["Card"],
                Content = new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = $"{report.Emoji} {report.Title}", FontAttributes = FontAttributes.Bold },
                        new Label { Text = $"{report.Id} · {report.Location}", TextColor = Theme.Muted, FontSize = 13 },
                        new Label { Text = report.Description, TextColor = Theme.Muted },
                        new Label { Text = "Change status", FontAttributes = FontAttributes.Bold, FontSize = 13 },
                        status,
                        saved,
                    },
                },
            };
            Reports.Children.Add(card);
        }
    }
}
