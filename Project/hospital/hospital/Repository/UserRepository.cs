using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;
using hospital.FileHandler;

namespace hospital.Repository
{
    public class UserRepository
    {
        public UserFileHandler userFileHandler;
        public ObservableCollection<User> users;

        public UserRepository()
        {
            userFileHandler = new FileHandler.UserFileHandler();

            
            List<User> deserializedList = userFileHandler.Read();
            if (deserializedList != null)
            {
                users = new ObservableCollection<User>(userFileHandler.Read());
            }
            else
            {
                users = new ObservableCollection<User>();
            }

        }


        public bool Create(User user)
        {
            this.users.Add(user);
            userFileHandler.Write(this.users.ToList());
            return true;
        }

        public ObservableCollection<User> FindAll()
        {
            return users;
        }

        public User FindByUsername(string username)
        {
            foreach (User p in users)
            {
                if (p.Username.Equals(username))
                {
                    return p;
                }
            }
            return null;
        }


        public bool DeleteByUsername(string username)
        {
            bool reVal = users.Remove(FindByUsername(username));
            userFileHandler.Write(this.users.ToList());
            return reVal;
        }

        public bool UpdateByUsername(string username, User user)
        {
            DeleteByUsername(username);
            Create(user);
            return true;
        }


        public ObservableCollection<User> Users
        {
            get
            {
                if (users == null)
                {
                    users = new ObservableCollection<User>();
                }

                return users;
            }
            set
            {
                RemoveAllUsers();
                if (value != null)
                {
                    foreach (User oUser in value)
                    {
                        AddUser(oUser);
                    }
                }
            }
        }


        public void AddUser(User newUser)
        {
            if (newUser == null)
            {
                return;
            }

            if (users == null)
            {
                users = new ObservableCollection<User>();
            }

            if (!users.Contains(newUser))
            {
                users.Add(newUser);
            }
        }


        public void RemoveUser(Patient oldUser)
        {
            if (oldUser == null)
            {
                return;
            }

            if (users != null)
            {
                if (users.Contains(oldUser))
                {
                    users.Remove(oldUser);
                }
            }
        }


        public void RemoveAllUsers()
        {
            if (users != null)
            {
                users.Clear();
            }
        }

    }
}
