using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CostsCategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CostsCategoryModel() { }
        public CostsCategoryModel(CostsCategory A)
        {
            ID = A.ID;
            Name = A.Name;
        }
    }
}
