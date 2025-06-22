namespace pw2
{
    public partial class MainPage : ContentPage
    {
        string filePath;

        public MainPage()
        {
            InitializeComponent();
            filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv");
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///RecoverPasswordPage");
        }

        private List<User> LoadUsers()
        {
            List<User> users = new List<User>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please fill in both username and password.", "OK");
                return;
            }

            List<User> users = LoadUsers();

            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    Preferences.Set("CurrentUser", username);
                    await DisplayAlert("Welcome", $"Hello, {user.Name}!", "OK");
                    await Shell.Current.GoToAsync("///ConversorPage");
                    return;
                }
            }

            await DisplayAlert("Error", "Incorrect username or password.", "Try Again");
        }
    }
}
