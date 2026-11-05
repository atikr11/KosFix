using CivicWatch.Models;
using CivicWatch.Services;
namespace CivicWatch.Pages;
public partial class MyReportsPage : ContentPage
{
    string _filter = "All";
    public MyReportsPage() => InitializeComponent();
    protected override void OnAppearing() { base.OnAppearing(); Build(); }

    void Build()
    {
        Chips.Children.Clear();
        foreach (var f in new[] { "All", "Submitted", "In progress", "Resolved" })
        {
            bool on = f == _filter;
            var b = new Button
            {
                Text = f, FontSize = 13, HeightRequest = 36, CornerRadius = 18, Padding = new Thickness(14, 0),
                BackgroundColor = on ? Theme.Primary : Colors.White, TextColor = on ? Colors.White : Theme.Ink,
                BorderColor = Theme.Line, BorderWidth = 1,
            };
            b.Clicked += (_, _) => { _filter = f; Build(); };
            Chips.Children.Add(b);
        }
        List.ItemsSource = ReportStore.All.Where(r => _filter == "All" || r.StatusText == _filter).ToList();
    }

    async void OnPick(object? s, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Report r) return;
        ((CollectionView)s!).SelectedItem = null;
        await Shell.Current.GoToAsync($"detail?id={r.Id}");
    }
}
