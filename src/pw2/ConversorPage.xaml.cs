using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; // Make sure this is included for Preferences and FileSystem
using pw2.conversions;

namespace pw2
{
    public partial class ConversorPage : ContentPage
    {
        public ConversorPage()
        {
            InitializeComponent();
        }

        void IncrementOperationCount()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv");
            string currentUser = Preferences.Get("CurrentUser", "");
            List<User> users = new List<User>();

            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        users.Add(User.FromCsv(line));
                    }
                }
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == currentUser)
                {
                    users[i].OperationCount++;
                    break;
                }
            }

            using (var writer = new StreamWriter(filePath, false))
            {
                foreach (var user in users)
                {
                    writer.WriteLine(user.ToCsv());
                }
            }
        }

        private async void OnConvertClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string input = inputEntry.Text;
            string command = btn.CommandParameter?.ToString() ?? "";
            string result = "";

            try
            {
                switch (command)
                {
                    case "DecimalToBinary":
                        result = ConversionHelper.DecimalToBinary(input);
                        break;
                    case "DecimalToTwoComplement":
                        result = ConversionHelper.DecimalToTwoComplement(input);
                        break;
                    case "DecimalToOctal":
                        result = ConversionHelper.DecimalToOctal(input);
                        break;
                    case "DecimalToHexadecimal":
                        result = ConversionHelper.DecimalToHexadecimal(input);
                        break;
                    case "BinaryToDecimal":
                        result = ConversionHelper.BinaryToDecimal(input);
                        break;
                    case "TwoComplementToDecimal":
                        result = ConversionHelper.TwoComplementToDecimal(input);
                        break;
                    case "OctalToDecimal":
                        result = ConversionHelper.OctalToDecimal(input);
                        break;
                    case "HexadecimalToDecimal":
                        result = ConversionHelper.HexadecimalToDecimal(input);
                        break;
                    default:
                        await DisplayAlert("Error", "Select a valid conversion.", "OK");
                        return;
                }
                // You were missing these lines from your original code:
                resultLabel.Text = result; // Assuming you have a Label named resultLabel to display the output
                IncrementOperationCount(); // Call this after a successful conversion

            } 
            catch (Exception ex) 
            {
                await DisplayAlert("Error", $"Conversion failed: {ex.Message}", "OK");
            }
        } 

        private void OnKeypadClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            inputEntry.Text += btn.Text;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            inputEntry.Text = "";
            resultLabel.Text = "";
        }

        private async void OnOperationsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(OperationsPage));
        }
    }
}