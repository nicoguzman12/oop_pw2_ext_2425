using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;

namespace oop_pw2_ext_2425
{
    public partial class LoginRealPage : ContentPage
    {
        public LoginRealPage()
        {
            InitializeComponent();
        }

        private List<User> LoadUsers()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv");
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

            return users;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            List<User> users = LoadUsers();

            User userFound = null;
            for (int i = 0; i < users.Count; i++)
            {
                if (string.Equals(users[i].Username, username, StringComparison.OrdinalIgnoreCase) &&
                    users[i].Password == password)
                {
                    userFound = users[i];
                    break;
                }
            }

            if (userFound == null)
            {
                await DisplayAlert("Error", "Invalid username or password.", "Ok");
                return;
            }

            await DisplayAlert("Success", $"Welcome, {userFound.Name}!", "Ok");

            await Shell.Current.GoToAsync("ConversorPage");
        }
    }
}
