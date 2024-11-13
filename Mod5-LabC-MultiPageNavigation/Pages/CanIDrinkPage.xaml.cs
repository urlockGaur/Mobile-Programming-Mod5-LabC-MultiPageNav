namespace Mod5_LabC_MultiPageNavigation.Pages;

public partial class CanIDrinkPage : ContentPage
{
    // Dictionary to store legal drinking ages by country
    private readonly Dictionary<string, int> _legalDrinkingAges = new()
        {
            { "USA", 21 },
            { "Vietnam", 18 },
            { "Germany", 16 },
            { "Brazil", 18 }
        };

    public CanIDrinkPage()
    {
        InitializeComponent();
        CountryPicker.SelectedIndex = 0; // Set a default country
        BirthdayPicker.Date = DateTime.Now;
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        // Get the selected country from the Picker
        string selectedCountry = CountryPicker.SelectedItem?.ToString();

        // Get the legal drinking age for the selected country from the dictionary
        int legalAge = selectedCountry != null && _legalDrinkingAges.ContainsKey(selectedCountry)
            ? _legalDrinkingAges[selectedCountry]
            : 21; // Default to 21 if no country is selected or found

        // Display result based on age or birthday
        if (int.TryParse(AgeEntry.Text, out int age) && age > 0)
        {
            // If AgeEntry has valid input, use it to calculate and display result
            DisplayResultFromAge(age, legalAge, selectedCountry);
        }
        else
        {
            // If AgeEntry is empty, use the birthday picker to calculate and display result
            DisplayResultFromBirthday(BirthdayPicker.Date, legalAge, selectedCountry);
        }

        // Clear input fields after displaying the result
        AgeEntry.Text = string.Empty;         // Clear age entry text
        BirthdayPicker.Date = DateTime.Today; // Reset date picker to today's date
    }

    // Display the result based on the entered age
    private void DisplayResultFromAge(int age, int legalAge, string country)
    {
        // Calculate how many years until the legal age
        int yearsUntilLegal = legalAge - age;

        // Show the result to the user
        ResultLabel.Text = yearsUntilLegal > 0
            ? $"In {country}, you need to wait {yearsUntilLegal} more years to drink legally."
            : $"You are old enough to drink in {country}!";
    }

    // Display the result based on the selected birthdate
    private void DisplayResultFromBirthday(DateTime birthDate, int legalAge, string country)
    {
        // Calculate how many years until the legal age based on birthdate
        int yearsUntilLegal = CalculateYearsUntilLegalAge(birthDate, legalAge);

        // Show the result to the user
        ResultLabel.Text = yearsUntilLegal > 0
            ? $"In {country}, you need to wait {yearsUntilLegal} more years to drink legally."
            : $"You are old enough to drink in {country}!";
    }

    // Helper function to calculate the years until legal age from a birthdate
    private int CalculateYearsUntilLegalAge(DateTime birthDate, int legalAge)
    {
        // Get today’s date
        DateTime today = DateTime.Today;

        // Calculate current age by subtracting the birth year from the current year
        int currentAge = today.Year - birthDate.Year;

        // Adjust if birthdate has not yet occurred this year
        if (birthDate > today.AddYears(-currentAge)) currentAge--;

        // Calculate years until legal drinking age, ensuring no negative numbers
        return legalAge - currentAge > 0 ? legalAge - currentAge : 0;
    }

    // Event handler for when a date is selected in the DatePicker
    private void OnDatePickerSelected(object sender, DateChangedEventArgs e)
    {
        if (BirthdayPicker.Date != DateTime.Today) // If a date is selected, disable AgeEntry
        {
            AgeEntry.IsEnabled = false;
        }
        else // If DatePicker is reset to today, re-enable AgeEntry
        {
            AgeEntry.IsEnabled = true;
        }
    }

    // Event handler for when text is entered in AgeEntry
    private void OnAgeEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(AgeEntry.Text)) // If age is entered, disable DatePicker
        {
            BirthdayPicker.IsEnabled = false;
        }
        else // If AgeEntry is cleared, re-enable DatePicker
        {
            BirthdayPicker.IsEnabled = true;
        }
    }
    // Event handler for the Reset Date button
    private void OnResetDateClicked(object sender, EventArgs e)
    {
        // Reset the date picker to the default date
        BirthdayPicker.Date = DateTime.Now;
    }
}