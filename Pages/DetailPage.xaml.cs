using CivicWatch.Models;
using CivicWatch.Services;
using Microsoft.Maui.Controls.Shapes;
namespace CivicWatch.Pages;
[QueryProperty(nameof(Id), "id")]
public partial class DetailPage : ContentPage
{
    public DetailPage() => InitializeComponent();

    public string Id { set { var r = ReportStore.Find(value); if (r is not null) Show(r); } }

    void Show(Report r)
    {
        Emoji.Text = r.Emoji; TitleLbl.Text = r.Title; IdLbl.Text = r.Id; LocLbl.Text = "📍 " + r.Location; DescLbl.Text = r.Description;
        if (r.PhotoPath is not null) { Photo.Source = ImageSource.FromFile(r.PhotoPath); Photo.IsVisible = true; }

        Timeline.Children.Clear();
        foreach (var s in Enum.GetValues<Status>())
        {
            bool done = s < r.Status, cur = s == r.Status;
            var dot = new Border
            {
                WidthRequest = 14, HeightRequest = 14, StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(7) }, StrokeThickness = 2,
                Stroke = cur ? Theme.Primary : done ? Theme.Green : Theme.Line,
                BackgroundColor = done ? Theme.Green : cur ? Theme.Soft : Colors.White,
                VerticalOptions = LayoutOptions.Start, Margin = new Thickness(0, 3, 0, 0),
            };
            var text = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = Report.Name(s), FontAttributes = FontAttributes.Bold, TextColor = cur ? Theme.Primary : done ? Theme.Ink : Theme.Muted },
                    new Label { Text = s <= r.Status ? r.DateText : "Pending", FontSize = 12, TextColor = Theme.Muted },
                },
            };
            Timeline.Children.Add(new HorizontalStackLayout { Spacing = 12, Margin = new Thickness(0, 0, 0, 14), Children = { dot, text } });
        }
    }

    async void OnBack(object? s, EventArgs e) => await Shell.Current.GoToAsync("..");
    async void OnLocation(object? s, EventArgs e) => await DisplayAlert("Location", "Map view comes in the next step.", "OK");
}
