﻿using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BG.ViewModels
{
    internal class UpdateCostController : INotifyPropertyChanged
    {
        private IDbCrud db;
        private UpdateCostForm updateCostForm;
        private CostsModel cost;
        private CostsCategoryModel costCategory;

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

        public List<CostsCategoryModel> CostCategories { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public UpdateCostController(IDbCrud db, UpdateCostForm updateCostForm, CostsModel cost)
        {
            this.db = db;
            this.updateCostForm = updateCostForm;
            this.cost = cost;

            GetCategory();
            CategoriesLoad();
        }

        public void GetCategory()
        {
            if (cost.ID_CostsCategory != 0)
                costCategory = db.GetCostCategory(Convert.ToInt32(cost.ID_CostsCategory));
            else
                costCategory = null;
        }

        public void CategoriesLoad()
        {
            CostCategories = db.GetAllCostsCategory();
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

        private RelayCommand UpdateCost;
        public RelayCommand updateCost
        {
            get
            {
                return UpdateCost ?? new RelayCommand(obj =>
                {
                    UpdCost();
                });
            }
        }



        public void CloseThisWindow()
        {
            updateCostForm.Close();
        }

        public void UpdCost()
        {
            if (CostCategory.ID == 0)
                cost.ID_CostsCategory = null;
            else
                cost.ID_CostsCategory = CostCategory.ID;

            db.UpdateCosts(cost);

            CloseThisWindow();
        }
    }
}