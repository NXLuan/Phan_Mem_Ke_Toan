﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    class ChucNangViewModel : BaseViewModel
    {
        public string icon { get; set; }
        public string iconColor { get; set; }
        public string text { get; set; }
        public UserControl page { get; set; }
        public ICommand DeletePageWorkingCommand { get; set; }
        public ICommand SelectPageCommand { get; set; }
        public ChucNangViewModel()
        {
            DeletePageWorkingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MainViewModel mainVM = MainViewModel.Instance;
                ObservableCollection<ChucNangViewModel> listWorking = mainVM.PageWorkings;
                if (listWorking.IndexOf(this) == mainVM.SelectedIndexWorking)
                {
                    mainVM.SelectedIndexWorking = 0;
                }
                listWorking.Remove(this);
            });

            SelectPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InsertPageInWorkingList();
            });
        }

        public void InsertPageInWorkingList()
        {
            ChucNangViewModel chucNangVM = this;
            MainViewModel mainVM = MainViewModel.Instance;
            ObservableCollection<ChucNangViewModel> listWorking = mainVM.PageWorkings;
            int findIndex = listWorking.IndexOf(chucNangVM);
            if (findIndex == -1)
            {
                listWorking.Add(chucNangVM);
                mainVM.SelectedIndexWorking = listWorking.Count - 1;
            }
            else mainVM.SelectedIndexWorking = findIndex;
        }
    }
}