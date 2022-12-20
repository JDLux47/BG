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
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            Canvas();
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

            Canvas2();
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

        private RelayCommand reload;
        public RelayCommand Reload
        {
            get
            {
                return reload ?? new RelayCommand(obj =>
                {
                    Canvas();
                });
            }
        }

        private RelayCommand reload2;
        public RelayCommand Reload2
        {
            get
            {
                return reload2 ?? new RelayCommand(obj =>
                {
                    Canvas2();
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


        private List<Category> Categories { get; set; }

        public void Canvas()
        {
            ////////
            Random random = new Random();

            List <IncomeCategoryModel> incomeCategories;
            incomeCategories = db.GetAllIncomeCategory();

            List<IncomeModel> income;
            income = db.GetAllIncome();

            decimal sum = 0, generalsum = 0, without_category = 0;

            Categories = new List<Category>();

            for (int i = 0; i < income.Count; i++)
            {
                generalsum += income[i].Sum;
                if (income[i].ID_IncomeCategory == null)
                    without_category += income[i].Sum;
            }

            for (int i = 0; i < incomeCategories.Count; i++)
            {
                Categories.Add(new Category { });

                Categories[i].Title = incomeCategories[i].Name;

                for (int j = 0; j < income.Count; j++)
                {
                    if (income[j].ID_IncomeCategory == incomeCategories[i].ID)
                        sum += income[j].Sum;
                }

                Categories[i].Percentage = (float)( sum / generalsum) * 100;

                sum = 0;

                Categories[i].ColorBrush = new SolidColorBrush(Color.FromRgb((byte)random.Next(200), (byte)random.Next(200), (byte)random.Next(200)));
            }

            Categories.Add(new Category
            {
                Title = "Без категории",
                Percentage = (float)(without_category / generalsum) * 100,
                ColorBrush = new SolidColorBrush(Color.FromRgb((byte)random.Next(200), (byte)random.Next(200), (byte)random.Next(200)))
            });

            //////////
            float pieWidth = 500, pieHeight = 500, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainWindow.mainCanvas.Width = pieWidth;
            mainWindow.mainCanvas.Height = pieHeight;

            mainWindow.detailsItemsControl.ItemsSource = Categories;

            // draw pie
            float angle = 0, prevAngle = 0;
            foreach (var category in Categories)
            {
                double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                angle = category.Percentage * (float)360 / 100 + prevAngle;
                Debug.WriteLine(angle);

                double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                double arcWidth = radius, arcHeight = radius;
                bool isLargeArc = category.Percentage > 50;
                var arcSegment = new ArcSegment()
                {
                    Size = new Size(arcWidth, arcHeight),
                    Point = new Point(arcX, arcY),
                    SweepDirection = SweepDirection.Clockwise,
                    IsLargeArc = isLargeArc,
                };
                var line2Segment = new LineSegment(new Point(centerX, centerY), false);

                var pathFigure = new PathFigure(
                    new Point(centerX, centerY),
                    new List<PathSegment>()
                    {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                    },
                    true);

                var pathFigures = new List<PathFigure>() { pathFigure, };
                var pathGeometry = new PathGeometry(pathFigures);
                var path = new Path()
                {
                    Fill = category.ColorBrush,
                    Data = pathGeometry,
                };
                mainWindow.mainCanvas.Children.Add(path);

                prevAngle = angle;


                // draw outlines
                var outline1 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = line1Segment.Point.X,
                    Y2 = line1Segment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };
                var outline2 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = arcSegment.Point.X,
                    Y2 = arcSegment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };

                mainWindow.mainCanvas.Children.Add(outline1);
                mainWindow.mainCanvas.Children.Add(outline2);
            }
        }

        public void Canvas2()
        {
            ////////
            Random random = new Random();

            List<CostsCategoryModel> costsCategories;
            costsCategories = db.GetAllCostsCategory();

            List<CostsModel> costs;
            costs = db.GetAllCosts();

            decimal sum = 0, generalsum = 0, without_category = 0;

            Categories = new List<Category>();

            for (int i = 0; i < costs.Count; i++)
            {
                generalsum += costs[i].Sum;
                if (costs[i].ID_CostsCategory == null)
                    without_category += costs[i].Sum;
            }

            for (int i = 0; i < costsCategories.Count; i++)
            {
                Categories.Add(new Category { });

                Categories[i].Title = costsCategories[i].Name;

                for (int j = 0; j < costs.Count; j++)
                {
                    if (costs[j].ID_CostsCategory == costsCategories[i].ID)
                        sum += costs[j].Sum;
                }

                Categories[i].Percentage = (float)(sum / generalsum) * 100;

                sum = 0;
                Categories[i].ColorBrush = new SolidColorBrush(Color.FromRgb((byte)random.Next(200), (byte)random.Next(200), (byte)random.Next(200)));
            }

            Categories.Add(new Category
            {
                Title = "Без категории",
                Percentage = (float)(without_category / generalsum) * 100,
                ColorBrush = new SolidColorBrush(Color.FromRgb((byte)random.Next(200), (byte)random.Next(200), (byte)random.Next(200)))
            });
            //////////
            float pieWidth = 500, pieHeight = 500, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainWindow.mainCanvas2.Width = pieWidth;
            mainWindow.mainCanvas2.Height = pieHeight;

            mainWindow.detailsItemsControl2.ItemsSource = Categories;

            // draw pie
            float angle = 0, prevAngle = 0;
            foreach (var category in Categories)
            {
                double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                angle = category.Percentage * (float)360 / 100 + prevAngle;
                Debug.WriteLine(angle);

                double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                double arcWidth = radius, arcHeight = radius;
                bool isLargeArc = category.Percentage > 50;
                var arcSegment = new ArcSegment()
                {
                    Size = new Size(arcWidth, arcHeight),
                    Point = new Point(arcX, arcY),
                    SweepDirection = SweepDirection.Clockwise,
                    IsLargeArc = isLargeArc,
                };
                var line2Segment = new LineSegment(new Point(centerX, centerY), false);

                var pathFigure = new PathFigure(
                    new Point(centerX, centerY),
                    new List<PathSegment>()
                    {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                    },
                    true);

                var pathFigures = new List<PathFigure>() { pathFigure, };
                var pathGeometry = new PathGeometry(pathFigures);
                var path = new Path()
                {
                    Fill = category.ColorBrush,
                    Data = pathGeometry,
                };
                mainWindow.mainCanvas2.Children.Add(path);

                prevAngle = angle;


                // draw outlines
                var outline1 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = line1Segment.Point.X,
                    Y2 = line1Segment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };
                var outline2 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = arcSegment.Point.X,
                    Y2 = arcSegment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };

                mainWindow.mainCanvas2.Children.Add(outline1);
                mainWindow.mainCanvas2.Children.Add(outline2);
            }
        }
    }

    public class Category
    {
        public float Percentage { get; set; }
        public string Title { get; set; }
        public Brush ColorBrush { get; set; }
    }
}