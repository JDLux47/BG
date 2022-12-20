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
    public class CostsM
    {
        public int ID { get; set; }

        public int Password { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string Login { get; set; }

        public double? SumLimiter { get; set; }

        public DateTime DateUpdateBalance { get; set; }

        public CostsM() { }

        public CostsM(BLL.Models.UserModel user)
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
}
