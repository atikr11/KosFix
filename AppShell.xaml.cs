using CivicWatch.Pages;
namespace CivicWatch;
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("report", typeof(ReportPage));
        Routing.RegisterRoute("confirm", typeof(ConfirmPage));
        Routing.RegisterRoute("detail", typeof(DetailPage));
    }
}
