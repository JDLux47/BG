using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BG.Models
{
    public class UserM
    {
        public int ID { get; set; }

        public int Password { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string Login { get; set; }

        public double? SumLimiter { get; set; }

        public DateTime DateUpdateBalance { get; set; }

        public UserM() { }

        public UserM(BLL.Models.UserModel user)
        {
            ID = user.ID;
            Password = user.Password;
            Name = user.Name;
            Balance = user.Balance;
            Login = user.Login;
            SumLimiter = user.SumLimiter;
            DateUpdateBalance = user.DateUpdateBalance;
        }

    }
    //public class User : INotifyPropertyChanged
    //{
    //    private int id;
    //    private int password;
    //    private string name;
    //    private double balance;
    //    private string login;
    //    private double? sumLimiter;
    //    private DateTime dateUpdateBalance;

    //    public int ID
    //    {
    //        get { return id; }
    //        set
    //        {
    //            id = value;
    //            OnPropertyChanged("ID");
    //        }
    //    }

    //    public int Password
    //    {
    //        get { return password; }
    //        set
    //        {
    //            password = value;
    //            OnPropertyChanged("Password");
    //        }
    //    }

    //    public string Name
    //    {
    //        get { return name; }
    //        set
    //        {
    //            name = value;
    //            OnPropertyChanged("Name");
    //        }
    //    }

    //    public double Balance
    //    {
    //        get { return balance; }
    //        set
    //        {
    //            balance = value;
    //            OnPropertyChanged("Balance");
    //        }
    //    }

    //    public string Login
    //    { 
    //        get { return login; }
    //        set
    //        {
    //            login = value;
    //            OnPropertyChanged("Login");
    //        }
    //    }

    //    public double? SumLimiter
    //    {
    //        get { return sumLimiter; }
    //        set
    //        {
    //            sumLimiter = value;
    //            OnPropertyChanged("SumLimiter");
    //        }
    //    }

    //    public DateTime DateUpdateBalance
    //    {
    //        get { return dateUpdateBalance; }
    //        set
    //        {
    //            dateUpdateBalance = value;
    //            OnPropertyChanged("DateUpdateBalance");
    //        }
    //    }

    //    public ObservableCollection<string> Models { get; set; }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public void OnPropertyChanged(string prop = "")
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    //    }
    //}
}
