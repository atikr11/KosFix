using CivicWatch.Services;
namespace CivicWatch.Pages;
[QueryProperty(nameof(Id), "id")]
public partial class ConfirmPage : ContentPage
{
    string _id = "";
    public string Id
    {
        get => _id;
        set { _id = value; var r = ReportStore.Find(value); if (r is null) return; IdLbl.Text = r.Id; TitleLbl.Text = r.Title; LocLbl.Text = "📍 " + r.Location; }
    }
    public ConfirmPage() => InitializeComponent();
    async void OnView(object? s, EventArgs e) => await Shell.Current.GoToAsync($"//main/reports");
    async void OnHome(object? s, EventArgs e) => await Shell.Current.GoToAsync("//main/home");
}
