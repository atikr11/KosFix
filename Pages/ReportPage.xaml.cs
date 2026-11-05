using CivicWatch.Services;
namespace CivicWatch.Pages;
public partial class ReportPage : ContentPage
{
    const string DemoLocation = "Main Street, Prishtina"; // replace with real GPS + reverse geocoding later
    bool _hasLoc; string? _photo;

    public ReportPage()
    {
        InitializeComponent();
        Cat.ItemsSource = ReportStore.Categories.Select(c => c.Name).ToList();
    }

    async void OnBack(object? s, EventArgs e) => await Shell.Current.GoToAsync("..");
    void OnDesc(object? s, TextChangedEventArgs e) => Count.Text = $"{e.NewTextValue?.Length ?? 0}/500";

    void OnLoc(object? s, EventArgs e)
    {
        _hasLoc = true;
        LocBtn.Text = "📍 " + DemoLocation;
        LocBtn.BackgroundColor = Theme.GreenBg; LocBtn.TextColor = Theme.Green;
    }

    async void OnPhoto(object? s, EventArgs e)
    {
        try
        {
            FileResult? f = MediaPicker.Default.IsCaptureSupported
                ? await MediaPicker.Default.CapturePhotoAsync()
                : await MediaPicker.Default.PickPhotoAsync();
            if (f is null) return;
            _photo = f.FullPath;
            Preview.Source = ImageSource.FromFile(_photo);
            Preview.IsVisible = true; PhotoHint.IsVisible = false;
        }
        catch (Exception ex) { Err.Text = "Couldn't open the camera: " + ex.Message; }
    }

    async void OnSubmit(object? s, EventArgs e)
    {
        if (Cat.SelectedItem is not string cat) { Err.Text = "Choose a category to continue."; return; }
        if (!_hasLoc) { Err.Text = "Set the problem location to continue."; return; }
        var r = ReportStore.Add(cat, DemoLocation, Desc.Text ?? "", _photo);
        await Shell.Current.GoToAsync($"confirm?id={r.Id}");
    }
}
