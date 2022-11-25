using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.Repository
{
    public class CostsRepositorySQL : IRepository<Costs>
    {
        private Context db;

        public CostsRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public void Create(Costs Costs)
        {
            db.Costss.Add(Costs);
        }

        public void Delete(int id)
        {
            Costs Costs = db.Costss.Find(id);
            if (Costs != null)
                db.Costss.Remove(Costs);
        }

        public Costs GetItem(int id)
        {
            return db.Costss.Find(id);
        }

        public List<Costs> GetList()
        {
            return db.Costss.ToList();
        }

        public void Update(Costs Costs)
        {
            db.Entry(Costs).State = EntityState.Modified;
        }
    }
}
