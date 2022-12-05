using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        List<CostsModel> GetAllCosts();
        List<CostsCategoryModel> GetAllCostsCategory();
        List<IncomeModel> GetAllIncome();
        List<IncomeCategoryModel> GetAllIncomeCategory();

        CostsModel GetCost(int ID);
        CostsCategoryModel GetCostCategory(int ID);
        IncomeModel GetIncome(int ID);
        IncomeCategoryModel GetIncomeCategory(int ID);
        UserModel GetUser(int ID);

        void CreateCostCategory(CostsCategoryModel obj);
        void CreateIncomeCategory(IncomeCategoryModel obj);
        void CreateCost(CostsModel obj);
        void CreateIncome(IncomeModel obj);

        void UpdateUser(UserModel obj);

        void DeleteIncome(int ID);
        void DeleteIncomeCategory(int ID);
        void DeleteCostCategory(int ID);
        void DeleteCost(int ID);
    }
}
