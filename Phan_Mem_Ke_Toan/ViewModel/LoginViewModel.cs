using Newtonsoft.Json;
using Phan_Mem_Ke_Toan.API;
using Phan_Mem_Ke_Toan.Model;
using Phan_Mem_Ke_Toan.ValidRule;
using Phan_Mem_Ke_Toan.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public static Window window { get; set; }
        public static Window main { get; set; }
        public static AccountSystem currentUser { get; set; }
        public AccountSystem account { get; set; }
        private AccountSystem _accountSignUp;
        public AccountSystem accountSignUp
        {
            get => _accountSignUp;
            set
            {
                SetProperty(ref _accountSignUp, value);
            }
        }
        public ICommand SelectTabSignUp { get; set; }
        public ICommand SelectTabSignIn { get; set; }
        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand LoadedCommand { get; set; }
        private string _showError;
        public string ShowError
        {
            get => _showError;
            set
            {
                SetProperty(ref _showError, value);
            }
        }
        private string _showSuccesstSignUp;
        public string ShowSuccessSignUp
        {
            get => _showSuccesstSignUp;
            set
            {
                SetProperty(ref _showSuccesstSignUp, value);
            }
        }
        private string _showFailSignUp;
        public string ShowFailSignUp
        {
            get => _showFailSignUp;
            set
            {
                SetProperty(ref _showFailSignUp, value);
            }
        }
        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                SetProperty(ref _passwordConfirm, value);
            }
        }

        private ObservableCollection<BoPhan> _listData;
        public ObservableCollection<BoPhan> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public LoginViewModel()
        {
            account = new AccountSystem();
            accountSignUp = new AccountSystem();
            accountSignUp.Quyen = "user";
            ShowError = "Collapsed";
            ShowFailSignUp = "Hidden";
            ShowSuccessSignUp = "Hidden";
            Event();
        }

        private void Event()
        {
            LoadedCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                window = p;
                ShowError = "Collapsed";
                LoadTableData();
            });

            SelectTabSignIn = new RelayCommand<TabControl>((p) => { return p != null; }, (p) =>
            {
                if (p.SelectedIndex == 0) return;
                ShowError = "Collapsed";
                p.SelectedIndex = 0;
            });

            SelectTabSignUp = new RelayCommand<TabControl>((p) => { return p != null; }, (p) =>
            {
                if (p.SelectedIndex == 1) return;
                ShowFailSignUp = "Hidden";
                ShowSuccessSignUp = "Hidden";
                p.SelectedIndex = 1;
            });

            SignInCommand = new RelayCommand<StackPanel>((p) => { return Valid.IsValid(p); }, (p) =>
            {
                ShowError = "Collapsed";
                if (IsValidAccount())
                { 
                    main = new MainWindow();
                    main.Show();
                    window.Hide();
                }
                else
                {
                    ShowError = "Visible";
                }
            });

            SignUpCommand = new RelayCommand<StackPanel>((p) => { return Valid.IsValid(p); }, (p) =>
            {
                ShowFailSignUp = "Hidden";
                ShowSuccessSignUp = "Hidden";
                if (CRUD.InsertData("nguoidung", accountSignUp))
                {
                    accountSignUp = new AccountSystem();
                    accountSignUp.Quyen = "user";
                    PasswordConfirm = "";
                    ShowSuccessSignUp = "Visible";
                }
                else ShowFailSignUp = "Visible";
            });

        }

        public bool IsValidAccount()
        {
            string url = "nguoidung/token/?TenDangNhap=" + account.TenDangNhap + "&MatKhau=" + account.MatKhau;
            string JsonData = CRUD.GetJsonData(url);
            currentUser = JsonConvert.DeserializeObject<AccountSystem>(JsonData);
            return currentUser!=null;
        }

        public void LoadTableData()
        {
            string JsonData = CRUD.GetJsonData("bophan");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<BoPhan>>(JsonData);
        }
    }

    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event

            PasswordBox box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            // set a flag to indicate that we're updating the password
            SetUpdatingPassword(box, true);
            // push the new password into the BoundPassword property
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindPassword);
        }

        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }
}
