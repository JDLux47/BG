using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWorkRepositorySQL : IUnitOfWork
    {
        private Context db;
        private CostsRepositorySQL costsRepository;
        private IncomeRepositorySQL incomeRepository;
        private IncomeCategoryRepositorySQL incomeCategoryRepository;
        private CostsCategoryRepositorySQL costsCategoryRepository;
        private UserRepositorySQL userRepository;

        public UnitOfWorkRepositorySQL()
        {
            db = new Context();
        }

        public IRepository<Costs> Costss
        {
            get
            {
                if (costsRepository == null)
                    costsRepository = new CostsRepositorySQL(db);
                return costsRepository;
            }
        }

        public IRepository<Income> Incomes
        {
            get
            {
                if (incomeRepository == null)
                    incomeRepository = new IncomeRepositorySQL(db);
                return incomeRepository;
            }
        }

        public IRepository<CostsCategory> CostsCategories
        {
            get
            {
                if (costsCategoryRepository == null)
                    costsCategoryRepository = new CostsCategoryRepositorySQL(db);
                return costsCategoryRepository;
            }
        }

        public IRepository<IncomeCategory> IncomeCategories
        {
            get
            {
                if (incomeCategoryRepository == null)
                    incomeCategoryRepository = new IncomeCategoryRepositorySQL(db);
                return incomeCategoryRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepositorySQL(db);
                return userRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
