using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class IncomeRepositorySQL : IRepository<Income>
    {
        private Context db;

        public IncomeRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public void Create(Income Income)
        {
            db.Incomes.Add(Income);
        }

        public void Delete(int id)
        {
            Income Income = db.Incomes.Find(id);
            if (Income != null)
                db.Incomes.Remove(Income);
        }

        public Income GetItem(int id)
        {
            return db.Incomes.Find(id);
        }

        public List<Income> GetList()
        {
            return db.Incomes.ToList();
        }

        public void Update(Income Income)
        {
            db.Entry(Income).State = EntityState.Modified;
        }
    }
}
