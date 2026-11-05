using CivicWatch.Models;
using CivicWatch.Services;
namespace CivicWatch.Pages;
public partial class HomePage : ContentPage
{
    public HomePage() => InitializeComponent();
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Total.Text = ReportStore.All.Count.ToString();
        Done.Text = ReportStore.All.Count(r => r.Status == Status.Resolved).ToString();
        List.ItemsSource = ReportStore.All.Take(2).ToList();
    }
    async void OnReport(object? s, EventArgs e) => await Shell.Current.GoToAsync("report");
    async void OnAll(object? s, EventArgs e) => await Shell.Current.GoToAsync("//main/reports");
    async void OnPick(object? s, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Report r) return;
        ((CollectionView)s!).SelectedItem = null;
        await Shell.Current.GoToAsync($"detail?id={r.Id}");
    }
}
