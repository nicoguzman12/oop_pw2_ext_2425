using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; 
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
                resultLabel.Text = result; 
                string username = Preferences.Get("CurrentUser", "");
                string bits = "";
                SaveOperation(username, input, bits, command, result);               
                IncrementOperationCount(); 

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
            await Shell.Current.GoToAsync("///OperationsPage");
        }

        private void SaveOperation(string username, string input, string bits, string operationType, string output)
        {
            string filePath = "operations.csv";

            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(username + "," + input + "," + bits + "," + operationType + "," + output);
            writer.Close();
        }

    }
    

}