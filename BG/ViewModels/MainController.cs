using BG.ViewModels;
using BLL.Interfaces;
using BLL.Models;
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
        }

        public void CostsLoad()
        {
            Costs = new ObservableCollection<CostsModel>(db.GetAllCosts().Where(i => i.ID_User == user.ID));
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

        #endregion


        public void AddNewCosts()
        {
            CreateCostForm CreateCost = new CreateCostForm(user);
            CreateCost.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CreateCost.ShowDialog();

            Costs.Insert(Costs.Count, db.GetAllCosts().Last());
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

            CostsLoad();
        }

        public void AddNewIncome()
        {
            CreateIncomeForm CreateIncome = new CreateIncomeForm(user);
            CreateIncome.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CreateIncome.ShowDialog();
            
            if(Incomes.Count < db.GetAllIncome().Count)
                Incomes.Insert(Incomes.Count, db.GetAllIncome().Last());
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

            IncomesLoad();
        }
    }
}