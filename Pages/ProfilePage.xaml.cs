namespace CivicWatch.Pages;
public partial class ProfilePage : ContentPage
{
    public ProfilePage() => InitializeComponent();
    async void OnSignOut(object? s, EventArgs e) => await Shell.Current.GoToAsync("//signin");
}
