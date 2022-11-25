using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Income
    {
        public int ID { get; set; }

        public int? ID_IncomeCategory { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public int ID_User { get; set; }

        public Income() { }
        public Income(Income A)
        {
            ID = A.ID;
            ID_IncomeCategory = A.ID_IncomeCategory;
            Sum = A.Sum;
            Date = A.Date;
            ID_User = A.ID_User;
        }
    }
}
