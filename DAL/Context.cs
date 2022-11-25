using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public partial class Context : DbContext
    {
        public Context() : base("Context")
        {

        }

        public virtual DbSet<Costs> Costss { get; set; }
        public virtual DbSet<CostsCategory> CostsCategories { get; }
        public virtual DbSet<Income> Incomes { get; }
        public virtual DbSet<IncomeCategory> IncomeCategories { get; }
        public virtual DbSet<User> Users { get; }
    }
}
