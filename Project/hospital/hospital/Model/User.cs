namespace Model
{
    public class User
    {
        private string username;
        private string password;
        private string email;
        private bool isBlocked;

        public string Username
        {
            get => username;
            set => username = value;
        }
        public string Password
        {
            get => password;
            set => password = value;
        }
        public string Email
        {
            get => email;
            set => email = value;
        }
        public bool IsBlocked
        {
            get => isBlocked;
            set => isBlocked = value;
        }

    }
}