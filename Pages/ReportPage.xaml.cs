using CivicWatch.Services;
namespace CivicWatch.Pages;
public partial class ReportPage : ContentPage
{
    string? _photo;

    public ReportPage()
    {
        InitializeComponent();
        MunicipalityPicker.ItemsSource = ReportStore.Municipalities.ToList();
        Cat.ItemsSource = ReportStore.Categories.Select(c => c.Name).ToList();
    }

    async void OnBack(object? s, EventArgs e) => await Shell.Current.GoToAsync("..");
    void OnDesc(object? s, TextChangedEventArgs e) => Count.Text = $"{e.NewTextValue?.Length ?? 0}/500";

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
        if (MunicipalityPicker.SelectedItem is not string municipality) { Err.Text = "Choose a demo municipality to continue."; return; }
        var location = LocationEntry.Text?.Trim();
        if (string.IsNullOrEmpty(location)) { Err.Text = "Enter a test street or landmark."; return; }
        Err.Text = "";
        var r = ReportStore.Add(cat, municipality, location, Desc.Text ?? "", _photo);
        await Shell.Current.GoToAsync($"confirm?id={r.Id}");
    }
}
