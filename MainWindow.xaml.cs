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
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

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
        private string Runcript(string script)
        {
              
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
           
                Pipeline pipeline = runspace.CreatePipeline();
            
                pipeline.Commands.AddScript(script);
                pipeline.Commands.Add("Out-String");

                Collection<PSObject> results = pipeline.Invoke();
                runspace.Close();   
                StringBuilder sb = new StringBuilder(); 
                foreach (PSObject obj in results)
                {
                    sb.AppendLine(obj.ToString());  
                }
                return sb.ToString();
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

          
            using (PowerShell powerShell = PowerShell.Create())
            {
                PowerShell powershell = PowerShell.Create();
                using ( Runspace runspace = RunspaceFactory.CreateRunspace())
                {
                    runspace.Open();
                    powershell.Runspace = runspace;
                    System.IO.StreamReader sr = new System.IO.StreamReader("D:/getusertest.ps1");
                    powershell.AddScript(sr.ReadToEnd());
                    var results = powershell.Invoke();
                    if (powershell.Streams.Error.Count > 0)
                    {
                        // error records were written to the error stream.
                        // do something with the items found.
                    }
                }
                using (PowerShell PowerShellInstance = PowerShell.Create())
                {
                    PowerShellInstance.AddScript("D:/getusertest.ps1");
                    PowerShellInstance.Invoke();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           Close();
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