using Mod5_LabC_MultiPageNavigation.Services;

namespace Mod5_LabC_MultiPageNavigation.Pages;

public partial class CatFactsPage : ContentPage
{
    int count = 0;
    public CatFactsPage()
    {
        InitializeComponent();
    }
    private async void OnGetCatFactClicked(object sender, EventArgs e)
    {
        try
        {
            // Fetch a random cat fact using the CatFactService
            var catFact = await CatFactService.GetRandomCatFactAsync();
            FactLabel.Text = catFact.Fact;
        }
        catch (Exception ex)
        {
            FactLabel.Text = $"Error: {ex.Message}";
        }
    }
}