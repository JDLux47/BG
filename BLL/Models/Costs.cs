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
    public class CostsModel : INotifyPropertyChanged
    {
        private int? id_CostsCategory;
        private decimal sum;
        private DateTime date;
        private string costsCategory;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public int ID { get; set; }

        public int? ID_CostsCategory
        {
            get { return id_CostsCategory; }
            set
            {
                id_CostsCategory = value;
                OnPropertyChanged("ID_CostsCategory");
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

        public string CostsCategory
        {
            get { return costsCategory; }
            set
            {
                costsCategory = value;
                OnPropertyChanged("CostsCategory");
            }
        }

        public int ID_User { get; set; }

        public CostsModel() { }
        public CostsModel(Costs A)
        {
            ID = A.ID;
            ID_CostsCategory = A.ID_CostsCategory;
            Sum = A.Sum;
            Date = A.Date;
            ID_User = A.ID_User;
        }
    }
}
