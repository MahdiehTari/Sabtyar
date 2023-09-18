using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DataGrid
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

        }

        private bool IsMaximize = false;
        private IEnumerable<string> scriptParameters;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
       
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("آیا از ثبت اطلاعات مطمن هستید و میخواهید خارج شوید؟", "خروج", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
            {
            }
            else
            {
                Close();
            }
        }

        private void txtOut_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           
        }
    }

    public class Member
    {
        public string Character { get; set; }
        public Brush BgColor { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}