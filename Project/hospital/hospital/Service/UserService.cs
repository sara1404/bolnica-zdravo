using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;
using System.Text;
using System.Security.Cryptography;

namespace Service
{
   public  class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository _repo) { _userRepository = _repo; }

        public bool Create(User user)
        {
            return _userRepository.Create(user);
        }

        public ObservableCollection<User> FindAll()
        {
            return _userRepository.FindAll();
        }

        public User FindByUsername(string username)
        {
            return _userRepository.FindByUsername(username);
        }

        public bool DeleteByUsername(string username)
        {
            return _userRepository.DeleteByUsername(username);
        }

        public bool UpdateByUsername(string username, User user)
        {
            return _userRepository.UpdateByUsername(username, user);
        }

        public void ChangePassword(string username, string newPassword)
        {
            _userRepository.ChangePassword(username, newPassword);
        }
        public User CheckCredentials(string username,string password)
        {
           ObservableCollection<User> users = _userRepository.FindAll();

            foreach (User u in users){
                if(u.Username.Equals(username) && u.Password.Equals(ComputeSha256Hash(password)))
                {
                    return u;
                }
            }
            return null;
        }

        public static string ComputeSha256Hash(string rawData)
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
    }
}
