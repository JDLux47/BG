using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class IncomeCategory
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IncomeCategory() { }
        public IncomeCategory(IncomeCategory A)
        {
            ID = A.ID;
            Name = A.Name;
        }
    }
}
