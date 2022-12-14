using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BG.Models;
using BLL.Interfaces;
using BLL.Models;

namespace BG.ViewModels
{
    internal class CreateIncomeController : INotifyPropertyChanged
    {
        private IDbCrud db;
        private CreateIncomeForm createForm;
        private IncomeCategoryModel incomeCategory;
        private IncomeModel income;
        private UserModel user;

        public IncomeModel Income
        {
            get { return income; }
            set
            {
                income = value;
                OnPropertyChanged("Income");
            }
        }

        public IncomeCategoryModel IncomeCategory
        {
            get { return incomeCategory; }
            set
            {
                incomeCategory = value;
                OnPropertyChanged("IncomeCategory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public List<IncomeModel> Incomes { get; set; }

        public List<IncomeCategoryModel> IncomeCategories { get; set; }

        public CreateIncomeController(IDbCrud db, CreateIncomeForm createForm, UserModel user)
        {
            this.db = db;
            this.createForm = createForm;
            this.user = user;

            income = new IncomeModel();
            incomeCategory = new IncomeCategoryModel();

            IncomeLoad();
            CategoriesLoad();
        }

        public void CategoriesLoad()
        {
            IncomeCategories = db.GetAllIncomeCategory();
        }

        public void IncomeLoad()
        {
            Incomes = db.GetAllIncome();
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
                
                    CreateIncome(obj)
                );  
            }
        }


        public void CloseThisWindow()
        {
            createForm.Close();
        }

        public void CreateIncome(object obj)
        {
            if (IncomeCategory.ID == 0)
                income.ID_IncomeCategory = null;
            else
                income.ID_IncomeCategory = IncomeCategory.ID;

            income.ID_User = user.ID;
            db.CreateIncome(income);

            CloseThisWindow();
        }
    }
}