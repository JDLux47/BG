using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        private int password;
        private string name;
        private double balance;
        private string login;
        private DateTime dateUpdateBalance;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public int Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public double Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        public int ID { get; set; }

        //public int Password { get; set; }

        //public string Name { get; set; }

       //public double Balance { get; set; }
        //public string Login { get; set; }
        public double? SumLimiter { get; set; }

        public DateTime DateUpdateBalance 
        {
            get { return dateUpdateBalance; }
            set
            {
                dateUpdateBalance = value;
                OnPropertyChanged("DateUpdateBalance");
            }
        }
        public UserModel() { }
        public UserModel(User A)
        {
            ID = A.ID;
            Password = A.Password;
            Name = A.Name;
            Balance = A.Balance;
            Login = A.Login;
            SumLimiter = A.SumLimiter;
            DateUpdateBalance = A.DateUpdateBalance;
        }
    }
}
