using DataGrid.Utilities;
using DataGrid.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DataGrid.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {

            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; set; }
       


        private void Register(object obj) => CurrentView = new RegisterVM();
       

        public NavigationVM()
        {

            RegisterCommand = new RelayCommand(Register);
            CurrentView = new RegisterVM();

        }
    }

}


