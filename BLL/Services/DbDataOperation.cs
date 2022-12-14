using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DbDataOperation : IDbCrud
    {
        IUnitOfWork db;
        public DbDataOperation(IUnitOfWork work)
        {
            db = work;
        }

        public void CreateCost(CostsModel obj)
        {
            db.Costss.Create(new Costs() { ID_CostsCategory = obj.ID_CostsCategory, Sum = obj.Sum, Date = obj.Date, ID_User = obj.ID_User });
            Save();
        }

        public void CreateCostCategory(CostsCategoryModel obj)
        {
            db.CostsCategories.Create(new CostsCategory() { Name = obj.Name });
            Save();
        }

        public void CreateIncome(IncomeModel obj)
        {
            db.Incomes.Create(new Income() { ID_IncomeCategory = obj.ID_IncomeCategory, Sum = obj.Sum, Date = obj.Date, ID_User = obj.ID_User });
            Save();
        }

        public void CreateIncomeCategory(IncomeCategoryModel obj)
        {
            db.IncomeCategories.Create(new IncomeCategory() { Name = obj.Name });
            Save();
        }

        public void CreateUser(UserModel obj)
        {
            db.Users.Create(new User() { Password = obj.Password, Name = obj.Name, Balance = obj.Balance, Login = obj.Login, SumLimiter = obj.SumLimiter, DateUpdateBalance = obj.DateUpdateBalance });
            Save();
        }

        public void DeleteCost(int ID)
        {
            Costs obj = db.Costss.GetItem(ID);
            if (obj != null)
            {
                db.Costss.Delete(obj.ID);
                Save();
            }
        }

        public void DeleteCostCategory(int ID)
        {
            CostsCategory obj = db.CostsCategories.GetItem(ID);
            if (obj != null)
            {
                db.CostsCategories.Delete(obj.ID);
                Save();
            }
        }

        public void DeleteIncome(int ID)
        {
            Income obj = db.Incomes.GetItem(ID);
            if (obj != null)
            {
                db.Incomes.Delete(obj.ID);
                Save();
            }
        }

        public void DeleteIncomeCategory(int ID)
        {
            IncomeCategory obj = db.IncomeCategories.GetItem(ID);
            if (obj != null)
            {
                db.IncomeCategories.Delete(obj.ID);
                Save();
            }
        }

        public List<CostsModel> GetAllCosts()
        {
            return db.Costss.GetList().Select(i => new CostsModel(i)).ToList();
        }

        public List<UserModel> GetAllUsers()
        {
            return db.Users.GetList().Select(i => new UserModel(i)).ToList();
        }

        public List<CostsCategoryModel> GetAllCostsCategory()
        {
            return db.CostsCategories.GetList().Select(i => new CostsCategoryModel(i)).ToList();
        }

        public List<IncomeModel> GetAllIncome()
        {
            return db.Incomes.GetList().Select(i => new IncomeModel(i)).ToList();
        }

        public List<IncomeCategoryModel> GetAllIncomeCategory()
        {
            return db.IncomeCategories.GetList().Select(i => new IncomeCategoryModel(i)).ToList();
        }

        public List<string> GetAllIncomeCategoryNames()
        {
            return db.IncomeCategories.GetList().Select(i => i.Name).ToList();
        }

        public CostsModel GetCost(int ID)
        {
            return new CostsModel(db.Costss.GetItem(ID));
        }

        public CostsCategoryModel GetCostCategory(int ID)
        {
            return new CostsCategoryModel(db.CostsCategories.GetItem(ID));
        }

        public IncomeModel GetIncome(int ID)
        {
            return new IncomeModel(db.Incomes.GetItem(ID));
        }

        public IncomeCategoryModel GetIncomeCategory(int ID)
        {
            return new IncomeCategoryModel(db.IncomeCategories.GetItem(ID));
        }

        public UserModel GetUser(int ID)
        {
            return new UserModel(db.Users.GetItem(ID));
        }

        public void UpdateUser(UserModel obj)
        {
            User user = db.Users.GetItem(obj.ID);

            user.Login = obj.Login;
            user.Name = obj.Name;
            user.Password = obj.Password;
            user.SumLimiter = obj.SumLimiter;
            user.Balance = obj.Balance;

            Save();
        }

        public void UpdateIncome(IncomeModel obj)
        {
            Income income = db.Incomes.GetItem(obj.ID);

            income.ID_IncomeCategory = obj.ID_IncomeCategory;
            income.Sum = obj.Sum;
            income.Date = obj.Date;

            Save();
        }

        public void UpdateCosts(CostsModel obj)
        {
            Costs costs = db.Costss.GetItem(obj.ID);

            costs.ID_CostsCategory = obj.ID_CostsCategory;
            costs.Sum = obj.Sum;
            costs.Date = obj.Date;

            Save();
        }

        public bool Save()
        {
            if (db.Save() > 0)
                return true;
            return false;
        }
    }

}
