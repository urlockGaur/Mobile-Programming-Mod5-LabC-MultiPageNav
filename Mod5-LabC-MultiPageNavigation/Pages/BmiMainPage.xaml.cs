namespace Mod5_LabC_MultiPageNavigation.Pages;

public partial class BmiMainPage : ContentPage
{
    public BmiMainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateBMIClicked(object sender, EventArgs e)
    {
        // Try parsing weight and height inputs
        if (double.TryParse(Inp_weight.Text, out double weight) &&
            double.TryParse(Inp_height.Text, out double height))
        {
            // Convert weight from pounds to kilograms and height from inches to meters
            weight = weight * 0.453592; // pounds to kg
            height = height * 0.0254;   // inches to meters

            // Calculate BMI
            double bmi = weight / (height * height);

            // Display BMI
            BmiResult.Text = $"Your BMI is {bmi:F2}";
        }
        else
        {
            // Show error if input is invalid
            BmiResult.Text = "Please enter valid numbers for weight and height.";
        }

        SemanticScreenReader.Announce(BmiResult.Text);
    }
}
