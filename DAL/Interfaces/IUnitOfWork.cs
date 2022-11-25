using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Costs> Costss { get; }
        IRepository<CostsCategory> CostsCategories { get; }
        IRepository<Income> Incomes { get; }
        IRepository<IncomeCategory> IncomeCategories { get; }
        IRepository<User> Users { get; }
        int Save();
    }
}
