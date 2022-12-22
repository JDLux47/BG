using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BG.Models;
using BLL.Interfaces;
using BLL.Models;

namespace BG.ViewModels
{
    public class EnterController : INotifyPropertyChanged
    {
        IDbCrud db;
        Registration registration;
        private UserModel user;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public List<UserModel> Users { get; set; }

        public EnterController(IDbCrud dbCrud, Registration registration)
        {
            db = dbCrud;
            this.registration = registration;
            user = new UserModel();
            //user.Login = "JD921Lux";
            //user.Password = 12345678;
        }

        public void UsersLoad()
        {
            Users = db.GetAllUsers().ToList();
        }


        #region COMMANDs

        private RelayCommand closeRegistration;
        public RelayCommand CloseRegistration
        {
            get
            {
                return closeRegistration ?? new RelayCommand(obj =>
                {
                    CloseRegistrationWindow();
                });
            }
        }


        private RelayCommand openNewMainWindow;
        public RelayCommand OpenNewMainWindow
        {
            get
            {
                return openNewMainWindow ?? new RelayCommand(obj =>
                {
                    OpenMainWindow();
                });
            }
        }


        private RelayCommand registrationNew;
        public RelayCommand RegistrationNew
        {
            get
            {
                return registrationNew ?? new RelayCommand(obj =>
                
                    RegistrationNewUser(obj)
                );
            }
        }

        #endregion

        public void OpenMainWindow()
        {
            if (user.Password == 0  || user.Login == null)
            {
                MessageBox.Show("Не все поля входа заполнены!");
            }
            else if (user.Password > 99999999 || user.Password < 10000000)
            {
                MessageBox.Show("Пароль должен состоять из 8 символов!");
            }
            else
            {
                bool search = false;

                UsersLoad();

                for (int i = 0; i < Users.Count(); i++)
                {
                    if (user.Login == Users[i].Login && user.Password == Users[i].Password)
                    {
                        search = true;
                        MainWindow mainWindow = new MainWindow(Users[i]);
                        mainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        mainWindow.Show();
                        CloseRegistrationWindow();
                    }
                }

                if (search == false)
                    MessageBox.Show("Неправильно введён логин/пароль или пользователя с таким данными не существует!");
            }
        }


        public void CloseRegistrationWindow()
        {
            registration.Close();
        }


        public void RegistrationNewUser(object obj)
        {
            if (user.Password == 0 || user.Login == null || user.Name == null)
            {
                MessageBox.Show("Не все поля регистрации заполнены!");
            }
            else if (user.Password > 99999999 || user.Password < 10000000)
            {
                MessageBox.Show("Пароль должен состоять из 8 символов!");
            }
            else
            {
                user.DateUpdateBalance = DateTime.Now;

                UsersLoad();

                bool search = false;

                for (int i = 0; i < Users.Count(); i++)
                    if (user.Login == Users[i].Login)
                    {
                        search = true;
                        MessageBox.Show("Пользователь с таким логином уже существует!");
                        break;
                    }

                if (search == false)
                {
                    db.CreateUser(user);
                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                }
            }
        }
    }
}
