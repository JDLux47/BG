using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CostsModel
    {
        public int ID { get; set; }

        public int? ID_CostsCategory { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public int ID_User { get; set; }

        public CostsModel() { }
        public CostsModel(Costs A)
        {
            ID = A.ID;
            ID_CostsCategory = A.ID_CostsCategory;
            Sum = A.Sum;
            Date = A.Date;
            ID_User = A.ID_User;
        }
    }
}
