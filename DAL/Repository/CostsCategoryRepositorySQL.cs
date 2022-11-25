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
    public class CostsCategoryRepositorySQL : IRepository<CostsCategory>
    {
        private Context db;

        public CostsCategoryRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public void Create(CostsCategory CostsCategory)
        {
            db.CostsCategories.Add(CostsCategory);
        }

        public void Delete(int id)
        {
            CostsCategory CostsCategory = db.CostsCategories.Find(id);
            if (CostsCategory != null)
                db.CostsCategories.Remove(CostsCategory);
        }

        public CostsCategory GetItem(int id)
        {
            return db.CostsCategories.Find(id);
        }

        public List<CostsCategory> GetList()
        {
            return db.CostsCategories.ToList();
        }

        public void Update(CostsCategory CostsCategory)
        {
            db.Entry(CostsCategory).State = EntityState.Modified;
        }
    }
}
