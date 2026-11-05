namespace CivicWatch.Pages;
public partial class SignInPage : ContentPage
{
    public SignInPage() => InitializeComponent();
    async void OnSignIn(object? s, EventArgs e) => await Shell.Current.GoToAsync("//main/home");
}
