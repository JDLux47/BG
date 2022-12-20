using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BG.Views;

namespace BG.ViewModels
{
    internal class UpdateIncomeController : INotifyPropertyChanged
    {
        private IDbCrud db;
        private UpdateIncomeForm updateForm;
        private IncomeModel income;
        private IncomeCategoryModel incomeCategory;

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

        public List<IncomeCategoryModel> IncomeCategories { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public UpdateIncomeController(IDbCrud db, UpdateIncomeForm updateForm, IncomeModel income)
        {
            this.db = db;
            this.updateForm = updateForm;
            this.income = income;

            GetCategory();

            CategoriesLoad();
        }

        public void GetCategory()
        {
            if (income.ID_IncomeCategory != null)
                incomeCategory = db.GetIncomeCategory(Convert.ToInt32(income.ID_IncomeCategory));
            else
                incomeCategory = null;
        }

        public void CategoriesLoad()
        {
            IncomeCategories = db.GetAllIncomeCategory();
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

        private RelayCommand UpdateIncome;
        public RelayCommand updateIncome
        {
            get
            {
                return UpdateIncome ?? new RelayCommand(obj =>
                {
                    UpdIncome();
                });
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
            updateForm.Close();
        }

        public void UpdIncome()
        {
            if (IncomeCategory.ID == 0)
                income.ID_IncomeCategory = null;
            else
                income.ID_IncomeCategory = IncomeCategory.ID;

            db.UpdateIncome(income);

            CloseThisWindow();
        }

        public void CreateCategory()
        {
            updateForm.Hide();

            IncomeCategoryCreate incomeCategory = new IncomeCategoryCreate();
            incomeCategory.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            incomeCategory.ShowDialog();

            CloseThisWindow();
        }
    }
}