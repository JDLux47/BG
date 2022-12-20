using BG.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BG
{
    public class MainController : INotifyPropertyChanged
    {
        private IDbCrud db;
        private MainWindow mainWindow;
        private UserModel user;
        private CostsModel selectedCost;
        private IncomeModel selectedIncome;

        public CostsModel SelectedCost
        {
            get { return selectedCost; }
            set
            {
                selectedCost = value;
                OnPropertyChanged("SelectedCost");
            }
        }

        public IncomeModel SelectedIncome
        {
            get { return selectedIncome; }
            set
            {
                selectedIncome = value;
                OnPropertyChanged("SelectedIncome");
            }
        }

        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public ObservableCollection<IncomeModel> Incomes { get; set; }

        public ObservableCollection<CostsModel> Costs { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainController(IDbCrud db, MainWindow mainWindow, UserModel user)
        {
            this.db = db;
            this.mainWindow = mainWindow;
            this.user = user;

            IncomesLoad();
            CostsLoad();
        }

        public void IncomesLoad()
        {
            Incomes = new ObservableCollection<IncomeModel>(db.GetAllIncome().Where(i => i.ID_User == user.ID));

            ChangeIndexToCategoryIncome();
        }

        public void ChangeIndexToCategoryIncome()
        {
            for (int i = 0; i < Incomes.Count; i++)
                if (Incomes[i].ID_IncomeCategory != null)
                    Incomes[i].IncomeCategory = db.GetIncomeCategory(Convert.ToInt32(Incomes[i].ID_IncomeCategory)).Name;
                else
                    Incomes[i].IncomeCategory = "без категории";
        }

        public void CostsLoad()
        {
            Costs = new ObservableCollection<CostsModel>(db.GetAllCosts().Where(i => i.ID_User == user.ID));

            ChangeIndexCategoryCosts();
        }

        public void ChangeIndexCategoryCosts()
        {
            for (int i = 0; i < Costs.Count; i++)
                if (Costs[i].ID_CostsCategory != null)
                    Costs[i].CostsCategory = db.GetCostCategory(Convert.ToInt32(Costs[i].ID_CostsCategory)).Name;
                else
                    Costs[i].CostsCategory = "без категории";
        }

        #region COMMANDS

        private RelayCommand AddIncome;
        public RelayCommand addIncome
        {
            get
            {
                return AddIncome ?? new RelayCommand(obj =>
                {
                    AddNewIncome();
                });
            }
        }

        private RelayCommand AddCosts;
        public RelayCommand addCosts
        {
            get
            {
                return AddCosts ?? new RelayCommand(obj =>
                {
                    AddNewCosts();
                });
            }
        }

        private RelayCommand DeleteIncome;
        public RelayCommand deleteIncome
        {
            get
            {
                return DeleteIncome ?? new RelayCommand(obj =>
                {
                    DelIncome();
                },

                (obj) => (Incomes.Count > 0 && selectedIncome != null));
            }
        }

        private RelayCommand DeleteCosts;
        public RelayCommand deleteCosts
        {
            get
            {
                return DeleteCosts ?? new RelayCommand(obj =>
                {
                    DelCosts();
                },
                (obj) => (Costs.Count > 0 && selectedCost != null));
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
                },
                (obj) => (Incomes.Count > 0 && selectedIncome != null));
            }
        }

        private RelayCommand UpdateCosts;
        public RelayCommand updateCosts
        {
            get
            {
                return UpdateCosts ?? new RelayCommand(obj =>
                {
                    UpdCosts();
                },
                (obj) => (Costs.Count > 0 && selectedCost != null));
            }
        }

        private RelayCommand exit;
        public RelayCommand Exit
        {
            get
            {
                return exit ?? new RelayCommand(obj =>
                {
                    ExitWindow();
                });
            }
        }

        #endregion


        public void AddNewCosts()
        {
            CreateCostForm CreateCost = new CreateCostForm(user);
            CreateCost.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CreateCost.ShowDialog();

            List<CostsModel> costs = db.GetAllCosts();

            if (Costs.Count < costs.Count)
            {
                Costs.Insert(Costs.Count, costs.Last());
                ChangeIndexCategoryCosts();
                user.Balance -= Costs.Last().Sum;
                db.UpdateUser(user);
            }
        }

        public void DelCosts()
        {
            db.DeleteCost(selectedCost.ID);
            Costs.Remove(selectedCost);
        }

        public void UpdCosts()
        {
            UpdateCostForm updateForm = new UpdateCostForm(selectedCost);
            updateForm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            updateForm.ShowDialog();

            ChangeIndexCategoryCosts();
        }

        public void AddNewIncome()
        {
            CreateIncomeForm CreateIncome = new CreateIncomeForm(user);
            CreateIncome.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CreateIncome.ShowDialog();

            List<IncomeModel> incomes = db.GetAllIncome();

            if (Incomes.Count < incomes.Count)
            {
                Incomes.Insert(Incomes.Count, incomes.Last());
                ChangeIndexToCategoryIncome();
                user.Balance += Incomes.Last().Sum;
                db.UpdateUser(user);
            }
        }

        public void DelIncome()
        {
            db.DeleteIncome(selectedIncome.ID);
            Incomes.Remove(selectedIncome);
        }

        public void UpdIncome()
        {
            UpdateIncomeForm updateForm = new UpdateIncomeForm(selectedIncome);
            updateForm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            updateForm.ShowDialog();

            ChangeIndexToCategoryIncome();
        }

        public void ExitWindow()
        {
            db.UpdateUser(user);
            mainWindow.Close();
        }
    }
}