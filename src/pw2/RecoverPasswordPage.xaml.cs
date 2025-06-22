namespace pw2
{
    public partial class RecoverPasswordPage : ContentPage
    {
        public RecoverPasswordPage()
        {
            InitializeComponent();
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(".."); // goes back to loginpage
        }

        private async void OnRecoverClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Error", "Please enter your username.", "Ok");
                return;
            }

            string filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv");

            if (!File.Exists(filePath))
            {
                resultLabel.Text = "User not found.";
                return;
            }

            bool found = false;
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    User user = User.FromCsv(line);
                    if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        resultLabel.Text = $"Your password is: {user.Password}";
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                resultLabel.Text = "Username not found.";
            }
        }
    }
}
