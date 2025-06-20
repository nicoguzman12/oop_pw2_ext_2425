public class User
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int OperationCount { get; set; }

    public User()
    {

        public User(string name, string username, string password, int operationCount)
        {
            Name = name;
            Username = username;
            Password = password;
            OperationCount = operationCount;
        }

        public string ToCsv()
        {
            return $"{Name},{Username},{Password},{OperationCount}";
        }

        public static User FromCsv(string line)
        {
            var parts = line.Split(',');
            return new User(parts[0], parts[1], parts[2], int.Parse(parts[3]));
        }
    }
}
