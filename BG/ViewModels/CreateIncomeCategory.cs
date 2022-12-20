using BG.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BG.Views
{
    internal class CreateIncomeCategory : INotifyPropertyChanged
    {
        private IDbCrud db;
        private IncomeCategoryCreate incomeCategoryCreate;
        private IncomeCategoryModel incomeCategory;

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

        public CreateIncomeCategory(IDbCrud db, IncomeCategoryCreate incomeCategoryCreate)
        {
            this.db = db;
            this.incomeCategoryCreate = incomeCategoryCreate;

            incomeCategory = new IncomeCategoryModel();
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
            incomeCategoryCreate.Close();
        }

        private void AddCategory()
        {
            db.CreateIncomeCategory(incomeCategory);
            Close();
        }
    }
}