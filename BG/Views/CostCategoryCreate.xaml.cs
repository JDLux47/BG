using BG.Until;
using BLL.Interfaces;
using BLL.Until;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BG.Views
{
    /// <summary>
    /// Логика взаимодействия для CostCategoryCreate.xaml
    /// </summary>
    public partial class CostCategoryCreate : Window
    {
        public CostCategoryCreate()
        {
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("Context"));
            IDbCrud crudServ = kernel.Get<IDbCrud>();

            InitializeComponent();
            DataContext = new CreateCostCategory(crudServ, this);
        }
    }
}
