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
    public class IncomeCategoryRepositorySQL : IRepository<IncomeCategory>
    {
        private Context db;

        public IncomeCategoryRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public void Create(IncomeCategory IncomeCategory)
        {
            db.IncomeCategories.Add(IncomeCategory);
        }

        public void Delete(int id)
        {
            IncomeCategory IncomeCategory = db.IncomeCategories.Find(id);
            if (IncomeCategory != null)
                db.IncomeCategories.Remove(IncomeCategory);
        }

        public IncomeCategory GetItem(int id)
        {
            return db.IncomeCategories.Find(id);
        }

        public List<IncomeCategory> GetList()
        {
            return db.IncomeCategories.ToList();
        }

        public void Update(IncomeCategory IncomeCategory)
        {
            db.Entry(IncomeCategory).State = EntityState.Modified;
        }
    }
}
