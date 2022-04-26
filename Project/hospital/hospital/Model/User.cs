using hospital.Model;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class User
    {
        private string username;
        private string password;
        private bool isBlocked;
        private Role role;


        public User() { }
        public User(string _username,string _password,Role role,bool blocked)
        {
            this.username = _username;
            this.password = ComputeSha256Hash(_password);
            this.isBlocked=blocked;
            this.role = role;
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
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
        public bool IsBlocked
        {
            get => isBlocked;
            set => isBlocked = value;
        }

        public Role Role
        {
            get => role;
            set => role = value;
        }

    }
}