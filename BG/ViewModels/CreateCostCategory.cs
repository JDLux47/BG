using BG.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BG.Views
{
    internal class CreateCostCategory : INotifyPropertyChanged
    {
        private IDbCrud db;
        private CostCategoryCreate costCategoryCreate;
        private CostsCategoryModel costCategory;

        public CostsCategoryModel CostCategory
        {
            get { return costCategory; }
            set
            {
                costCategory = value;
                OnPropertyChanged("CostCategory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public CreateCostCategory(IDbCrud db, CostCategoryCreate costCategoryCreate)
        {
            this.db = db;
            this.costCategoryCreate = costCategoryCreate;

            costCategory = new CostsCategoryModel();
        }

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? new RelayCommand(obj =>
                {
                    AddCategory();
                });
            }
        }

        private RelayCommand closeWindow;
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow ?? new RelayCommand(obj =>
                {
                    Close();
                });
            }
        }

        private void Close()
        {
            costCategoryCreate.Close();
        }

        private void AddCategory()
        {
            db.CreateCostCategory(costCategory);
            Close();
        }
    }
}