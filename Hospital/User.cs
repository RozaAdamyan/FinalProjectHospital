using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hospital
{
    class User
    {
        public static List<User> users = new List<User>();
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public int Balance { get; set; }

        public User(string name, string surname, string login, string password, string cardNumber, int balance)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Password = password;
            CardNumber = cardNumber;
            Balance = balance;
            users.Add(this);
        }

        public User()
        { }

        public void ChangePassword(string new_password)
        {
            Password = new_password;
        }

        public void AddMoney(int money)
        {
            Balance += money;
        }

        public int ShowBalance()
        {
            return Balance;
        }

        public static bool IsValidLogin(string login)
        {
            bool isvalid = true;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login.Equals(login))
                {
                    isvalid = false;
                    break;
                }
            }
            return isvalid;
        }

    }
}
