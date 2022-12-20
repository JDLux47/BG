using BG.Views;
using BLL.Interfaces;
using BLL.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BG.ViewModels
{
    internal class CreateCostController : INotifyPropertyChanged
    {
        private IDbCrud db;
        private CreateCostForm createCostForm;
        private CostsCategoryModel costCategory;
        private CostsModel cost;
        private UserModel user;

        public CostsModel Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public CostsCategoryModel CostCategory
        {
            get { return costCategory; }
            set
            {
                costCategory = value;
                OnPropertyChanged("CostCategory");
            }
        }

        public List<CostsModel> Costs { get; set; }

        public List<CostsCategoryModel> CostCategories { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public CreateCostController(IDbCrud db, CreateCostForm createCostForm, UserModel user)
        {
            this.db = db;
            this.createCostForm = createCostForm;
            this.user = user;

            cost = new CostsModel();
            costCategory = new CostsCategoryModel();

            CostsLoad();
            CategoriesLoad();
        }

        public void CategoriesLoad()
        {
            CostCategories = db.GetAllCostsCategory();
        }

        public void CostsLoad()
        {
            Costs = db.GetAllCosts();
        }

        private RelayCommand closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? new RelayCommand(obj =>
                {
                    CloseThisWindow();
                });
            }
        }

        private RelayCommand create;
        public RelayCommand Create
        {
            get
            {
                return create ?? new RelayCommand(obj =>

                    CreateCost(obj)
                );
            }
        }

        private RelayCommand createNewCategory;
        public RelayCommand CreateNewCategory
        {
            get
            {
                return createNewCategory ?? new RelayCommand(obj =>
                {
                    CreateCategory();
                });
            }
        }

        public void CloseThisWindow()
        {
            createCostForm.Close();
        }

        public void CreateCost(object obj)
        {
            if (CostCategory.ID == 0)
                cost.ID_CostsCategory = null;
            else
                cost.ID_CostsCategory = CostCategory.ID;

            cost.ID_User = user.ID;
            db.CreateCost(cost);

            CloseThisWindow();
        }

        public void CreateCategory()
        {
            createCostForm.Hide();

            CostCategoryCreate costCategory = new CostCategoryCreate();
            costCategory.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            costCategory.ShowDialog();

            CloseThisWindow();
        }

    }
}