using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace pw2
{
    public partial class OperationsPage : ContentPage
    {
        public OperationsPage()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv");
            string currentUsername = Preferences.Get("CurrentUser", "");

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        User user = User.FromCsv(line);

                        if (user.Username == currentUsername)
                        {
                            nameLabel.Text = "Name: " + user.Name;
                            usernameLabel.Text = "Username: " + user.Username;
                            passwordLabel.Text = "Password: " + user.Password;
                            operationsLabel.Text = "Operations done: " + user.OperationCount;
                            break;
                        }
                    }
                }
            }

            // Load operations history
            string opFilePath = Path.Combine(FileSystem.AppDataDirectory, "operations.csv");
            if (File.Exists(opFilePath))
            {
                using (StreamReader reader = new StreamReader(opFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 5 && parts[0] == currentUsername)
                        {
                            Label opLabel = new Label
                            {
                                Text = $"Input: {parts[1]}, Bits: {parts[2]}, Type: {parts[3]}, Output: {parts[4]}",
                                FontSize = 14
                            };
                            operationsContainer.Children.Add(opLabel);
                        }
                    }
                }
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("CurrentUser");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
