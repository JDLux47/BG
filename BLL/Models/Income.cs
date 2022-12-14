using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class IncomeModel : INotifyPropertyChanged
    {
        private int? id_IncomeCategory;
        private decimal sum;
        private DateTime date;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public int ID { get; set; }

        public int? ID_IncomeCategory
        {
            get { return id_IncomeCategory; }
            set
            {
                id_IncomeCategory = value;
                OnPropertyChanged("ID_IncomeCategory");
            }
        }
        public decimal Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public int ID_User { get; set; }

        //public List<IncomeCategoryModel> IncomeCategories { get; set; }

        public IncomeModel() { }
        public IncomeModel(Income A)
        {
            ID = A.ID;
            ID_IncomeCategory = A.ID_IncomeCategory;
            Sum = A.Sum;
            Date = A.Date;
            ID_User = A.ID_User;
        }
    }
}
