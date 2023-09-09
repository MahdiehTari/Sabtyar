using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGrid.View
{
    /// <summary>
    /// Interaction logic for Registers.xaml
    /// </summary>
    public partial class Registers : UserControl
    {
        string Company = "";
        string Services = "";
        string RegDate = "";
        string ExDate = "";
        int Period = 0;
        int Cost = 0;
        string OprUser = "";
        string Description = "";
        int FactorNum = 0;
        int type = 0;
        int OprType = 0;
        string OprDate = "";
        OleDbConnection conn = new OleDbConnection();
        public Registers()
        {
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\192.168.1.62\FileServer\Network\Network-FinancialData\Registerr.mdb";
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
                   
                    OleDbCommand cmd = new OleDbCommand("INSERT into Register (Company , Services, RegDate, ExDate, Period , Cost , OprUser, OprDate , type , Description , OprType , FactorNum) Values(@Company , @Services, @RegDate, @ExDate, @Period , @Cost , @OprUser ,@OprDate ,@type, @Description, @OprType , @FactorNum)");
                    cmd.Connection = conn;

                    conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    /*cmd.Parameters.Add("@Company", OleDbType.VarChar).Value = Company;
                    cmd.Parameters.Add("@Services", OleDbType.VarChar).Value = Services;
                    cmd.Parameters.Add("@RegDate", OleDbType.VarChar).Value = RegDate;
                    cmd.Parameters.Add("@ExDate", OleDbType.VarChar).Value = ExDate;
                    cmd.Parameters.Add("@Period", OleDbType.Integer).Value = Period;
                    cmd.Parameters.Add("@Cost", OleDbType.Integer).Value = Cost;
                    cmd.Parameters.Add("@OprUser", OleDbType.VarChar).Value = OprUser;
                    cmd.Parameters.Add("@OprDate", OleDbType.VarChar).Value = OprDate;
                    cmd.Parameters.Add("@type", OleDbType.Integer).Value = type;
                    cmd.Parameters.Add("@Description", OleDbType.VarChar).Value = Description;
                    cmd.Parameters.Add("@OprType", OleDbType.Integer).Value = OprType;
                    cmd.Parameters.Add("@FactorNum", OleDbType.Integer).Value = FactorNum;*/


                    cmd.ExecuteNonQuery();
                    //load();
                    MessageBox.Show("اطلاعات اضافه شد");


                }

                    }
               

        private void service_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (service.SelectedIndex == 0)
            {
                template.Items.Clear();
                template.Items.Add("پارس پک");
                template.Items.Add("ایران سرور");
                template.Items.Add("آذرآنلاین");
            }
            if (service.SelectedIndex == 1)
            {
                template.Items.Clear();
                template.Items.Add("پارس آنلاین");
                template.Items.Add("آسیاتک");
                template.Items.Add("افرانت شیراز");
                template.Items.Add("افرانت تهران");
                template.Items.Add("رسپینا");
            }
            hinttxt.Visibility = Visibility.Hidden;  
        }

        private void template_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (service.SelectedIndex == 0)
            {
                if (template.SelectedIndex == 0)
                {
                    //parspack
                    Company = "پارس پک";
                    Services = "جهت استفاده کاربران - خرید از پارس پک";



                }
                if (template.SelectedIndex == 1)
                {
                    //iranserver
                    Company = "ایران سرور";



                }
                if (template.SelectedIndex == 2)
                {
                    //azaronline
                    Company = "آذرآنلاین";



                }
            }
        }
    }
}
