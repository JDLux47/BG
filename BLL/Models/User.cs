using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public int Password { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }
        public string Login { get; set; }

        public double? SumLimiter { get; set; }

        public DateTime DateUpdateBalance { get; set; }

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
