namespace pw2
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private List<User> LoadUsers()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv"); //path.combine to do it functional outside my local disk
            List<User> users = new List<User>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
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

        private void SaveUsers(List<User> users)
        {

            string filePath = Path.Combine(FileSystem.AppDataDirectory, "User.csv");

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var user in users)
                {
                    writer.WriteLine(user.ToCsv());
                }
            }
        }

        private bool IsValidPassword(string password)  //sees if password is valid comparing to rules 
        {
            return password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.IndexOfAny("!@#€&%/".ToCharArray()) >= 0;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string name = nameEntry.Text;
            string username = userEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;


            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "All must be filled.", "Ok");
                return;
            }

            if (name == username) //compare user and username
            {
                await DisplayAlert("Error", "Name and username must be different.", "Ok");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Warning!", "Passwords are different.", "Ok");
                return;
            }

            if (!IsValidPassword(password))  //password rules
            {
                await DisplayAlert("Error",
                    "Password must be at least 8 characters long and include:\n" +
                    "- Number \n- Lowercase letter\n- Uppercase letter\n- One Symbol (!@#€&%/)",
                    "OK");
                return;
            }

            List<User> users = LoadUsers();

            if (users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))  //oes username exist already?
            {
                await DisplayAlert("Error", "That username is already taken.", "OK");
                return;
            }

            users.Add(new User(name, username, password, 0));
            SaveUsers(users);

            await DisplayAlert("Done", "Account created", "Ok");
            await Shell.Current.GoToAsync("..");
        }
        
        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///RecoverPasswordPage");
        }

    }
}