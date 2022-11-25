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
    public class UserRepositorySQL : IRepository<User>
    {
        private Context db;

        public UserRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public void Create(User User)
        {
            db.Users.Add(User);
        }

        public void Delete(int id)
        {
            User User = db.Users.Find(id);
            if (User != null)
                db.Users.Remove(User);
        }

        public User GetItem(int id)
        {
            return db.Users.Find(id);
        }

        public List<User> GetList()
        {
            return db.Users.ToList();
        }

        public void Update(User User)
        {
            db.Entry(User).State = EntityState.Modified;
        }
    }
}
