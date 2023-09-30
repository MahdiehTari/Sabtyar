using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Dapplo.ActiveDirectory.Extensions;

namespace DataGrid.View
{
    /// <summary>
    /// Interaction logic for Registers.xaml
    /// </summary>
    public partial class Registers : UserControl
    {
      
        int count = 0;
        string IRvalue;
        string Hesab;
        string company = "";
        string Services = "";
        int type = 0;
       
        OleDbConnection conn = new OleDbConnection();
        public Registers()
        {
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\192.168.1.62\FileServer\Network\Network-FinancialData\Registerr.mdb";
            InitializeComponent();

        }
        void createFile(string path, string data)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Create(path).Close(); // Create file
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(data);
            }
        }



        public Double totalcost()
        {
            Double tot;
            if (p1.Text != "" & c1.Text != "")
            {
                tot = Convert.ToDouble(p1.Text) * Convert.ToDouble(c1.Text);
                if (p2.Text != "" & c2.Text != "")
                {
                    tot = tot + (Convert.ToDouble(p2.Text) * Convert.ToDouble(c2.Text));
                    if (p3.Text != "" & c3.Text != "")
                    {
                        tot = tot + (Convert.ToDouble(p3.Text) * Convert.ToDouble(c3.Text));
                        if (p4.Text != "" & c4.Text != "")
                        {
                            tot = tot + (Convert.ToDouble(p4.Text) * Convert.ToDouble(c4.Text));
                            if (p5.Text != "" & c5.Text != "")
                            {
                                tot = tot + (Convert.ToDouble(p5.Text) * Convert.ToDouble(c5.Text));
                                if (p6.Text != "" & c6.Text != "")
                                {
                                    tot = tot + (Convert.ToDouble(p6.Text) * Convert.ToDouble(c6.Text));
                                    if (p7.Text != "" & c7.Text != "")
                                    {
                                        tot = tot + (Convert.ToDouble(p7.Text) * Convert.ToDouble(c7.Text));
                                    }
                                }else { return tot; }
                            }else { return tot; }
                        }else { return tot; }
                    }else { return tot; }
                } else { return tot; }
            } else { return 0; }
            return 0;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string htmlPath = @"C://Users/mahdieh.tarikhchi/index.html";
                string dataPath = @"C://Users/mahdieh.tarikhchi/data.js";
                string json =
               "let data = { \n" +
               "IRvalue: '" + IRvalue + "', \n" +
               "accountValue: '" + Hesab + "', \n" +
               "priceNumeric: '" + string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:N0}", double.Parse(totalcost().ToString())) + "', \n" +
               "purchaseDescription:'" + Services + "', \n" +
               "} \n" +
               "let table = { \n" +
               "description: ['" + product1.Text + "', '" + product2.Text + "', '" + product3.Text + "', '" + product4.Text + "', '" + product5.Text + "', '" + product6.Text + "',  '" + product7.Text + "'], \n" +
               "number: ['" + c1.Text + "', '" + c2.Text + "', '" + c3.Text + "', '" + c4.Text + "', '" + c5.Text + "', '" + c6.Text + "',  '" + c7.Text + "'], \n" +
               "amount:  ['" + p1.Text + "', '" + p2.Text + "', '" + p3.Text + "', '" + p4.Text + "', '" + p5.Text + "', '" + p6.Text + "',  '" + p7.Text + "'] \n" +
               "} \n" +
                "function test(t){t=t.toString().split(\".\");return 4<=t[0].length&&(t[0]=t[0].replace(/(\\d)(?=(\\d{3})+$)/g,\"$1,\")),t[1]&&4<=t[1].length&&(t[1]=t[1].replace(/(\\d{3})/g,\"$1 \")),t.join(\".\")}";
                string htmlData = "<!DOCTYPE html><html lang='en'><head> <meta charset='UTF-8'> <title>Title</title> <style> @font-face{font-family:'mitra';src:url(https://cdn.fontcdn.ir/Fonts/Mitra/d2c9177ac570a1e35290810e7aaccefbcc0055f0e208b830d2878566ad08fbd9.woff2) format('woff2');font-weight:400;font-style:normal}@font-face{font-family:'mitraBlack';src:url(https://cdn.fontcdn.ir/Fonts/Mitra/be99475c042fa2b38374f686e5c461d5c0f539982a12157bae62383294fa6210.woff2) format('woff2');font-weight:700;font-style:normal}p{font-family:'mitra',sans-serif;font-size:20px;margin:0}p b{font-family:'mitraBlack',sans-serif}@page{size:auto;margin:5px}.borderDesign{width:874px;border-width:5px;border-style:double;padding-top:15px;padding-bottom:15px}.tableContainer{margin-right:auto;margin-left:auto;width:840px}table{width:100%;border-collapse:collapse;border:1px solid #000;direction:rtl}.LeftText{text-align:left}tr{margin:0;padding:0}.TBorder{border:1px solid #000;width:50%;padding:5px}.JBorder{border:1px solid #000;text-align:center;padding:3px}td{padding:0}.TTeam{display:flex;width:874px;-ms-flex-pack:distribute;justify-content:space-around;margin-top:35px;margin-bottom:50px}.logoImg{width:150px;height:auto;padding-bottom:25px;padding-left:25px} </style></head><body><div class='borderDesign'> <div> <img class='logoImg' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAbIAAAClCAMAAAAOCYi5AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAI9UExURQAAANjY2JOTk2pqasbGxpeXl19fXzw8PDc3N1JSUtfX16ysrJ+fn8DAwNnZ2dHR0cnJycXFxby8vLq6uru7u729vcTExNDQ0NXV1cHBwampqYSEhElJSTY2NjQ0NDU1NWNjY9bW1oCAgEtLS0REREJCQnh4eJqampubm6KioqGhoXx8fGFhYVZWVk1NTUZGRj09PTo6OkFBQVBQUFtbW2BgYGhoaGtra2lpaWdnZ2JiYl5eXlhYWDk5OZ2dnVNTU0dHR0NDQ0pKSk5OTs7Oznt7e6qqqnFxcUBAQJCQkI6Ojs3Nza6urlVVVbS0tIeHh35+fm1tbVdXV0hISHZ2dqSkpMjIyG5ubrW1tX19fUVFRV1dXU9PT8rKyjMzM5GRkVlZWb6+voKCgq+vr0xMTM/Pz6Ojo3Nzc6amptzc3MLCwpycnNPT0zg4ODs7O3V1daCgoD8/Pz4+PqWlpaenp4WFhcfHx8vLy7a2tr+/v1FRUbKysnd3d4qKiszMzJKSkp6enmVlZXBwcNTU1ImJia2trdra2nJycrCwsIyMjIODg2RkZI2NjY+Pj7m5udLS0rOzs39/f1paWnl5ebi4uGxsbJWVlXp6esPDw29vb3R0dIaGhlRUVIGBgYiIiKurq7GxsaioqFxcXJmZmWZmZre3t4uLi5SUlJaWlpiYmNvb2/Hx8fn5+ff39////+7u7uLi4urq6v7+/unp6fr6+uvr6/v7++Dg4N3d3fLy8uTk5Ofn597e3vz8/AAAAED6BgwAAAC/dFJOU/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8ARW6EOQAAAAlwSFlzAAAOwQAADsEBuJFr7QAAIYZJREFUeF7tnY1f3MaZx48E5WJY3r0YNrgO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' alt='part logo'> </div> <div> <table class='tableContainer'> <tr> <td class='TBorder'> <p> عنوان کاربرگ: <b> درخواست کالا </b> </p> </td> <td class='TBorder LeftText'> <p> کد مدرک <b> HCADFM034 </b> </p> </td> </tr> <tr> <td colspan='2'> <p> خواهشمند است جهت در خواست کالا/ خدمت اطلاعات جدول زیر را تکمیل فرمایید: </p> </td> </tr> <tr> <td class='TBorder'> <p> نام و نام خانوادگی: <b> علیرضا تجلی </b> </p> </td> <td class='TBorder'> <p> سمت: <b> سرپرست شبکه </b> </p> </td> </tr> <tr> <td class='TBorder'> <p> واحد: <b> واحد شبکه </b> </p> </td> <td class='TBorder'> <p> تاریخ: <b class='date'></b> </p> </td> </tr> <tr> <td class='TBorder'> <p> <span> نوع کالا و خدمات: فناوری اطلاعات </span> <span style='padding-left: 35px; font-size: 18px;'> &boxtimes; </span> <span> پشتیبانی </span> <span style='font-size: 16px'> &EmptySmallSquare; </span> </p> </td> <td class='TBorder'> <p> شماره شبا: <b class='IRvalue'></b> </p> </td> </tr> <tr> <td class='TBorder'> <p> <span> کالا/ خدمت: با فاکتور رسمی </span> <span>&EmptySmallSquare;</span> <span> با فاکتور غیر رسمی </span> <span>&EmptySmallSquare;</span> <span> بدون فاکتور </span> <span>&EmptySmallSquare;</span> </p> </td> <td class='TBorder'> <p> شماره حساب: <b class='accountValue'></b> </p> </td> </tr> <tr> <td colspan='2' class='TBorder'> <p> <span> در صورتی که کالا دارای فاکتور رسمی است آیا پیش فاکتور کالا نیز موجود است؟ بله </span> <span style='padding-left: 25px'> &EmptySmallSquare; </span> <span> (پیش فاکتور به واحد حسابداری ارائه شود) خیر </span> <span> &EmptySmallSquare; </span> </p> </td> </tr> <tr> <td colspan='2' class='TBorder'> <p> <span> کالا دارای گواهی ارزش افزوده است: بله </span> <span style='padding-left: 25px'> &EmptySmallSquare; </span> <span> خیر </span> <span> &EmptySmallSquare; </span> </p> </td> </tr> <tr> <td colspan='2' CLASS='TBorder'> <p> <span> مستندات بارگذاری شده: پیش فاکتور </span> <span style='padding-left: 15px'> &EmptySmallSquare; </span> <span> فاکتور </span> <span style='padding-left: 15px'> &EmptySmallSquare; </span> <span> گواهی ارزش افزوده </span> <span style='padding-left: 15px'> &EmptySmallSquare; </span> <span> عکس </span> <span style='padding-left: 15px'> &EmptySmallSquare; </span> <span> سایر </span> <span> ............................................................. </span> <span> &EmptySmallSquare; </span> </p> </td> </tr> <tr> <td colspan='2'> <p style='padding: 5px'> توجیه خرید: </p> <P style='text-align: center; padding-bottom: 20px'> <b class='purchaseDescription'></b> </P> </td> </tr> <tr> <td colspan='2'> <table class='table'> <tr> <td style='width: 2%' class='JBorder'> <p> ردیف </p> </td> <td style='width: 25%' class='JBorder'> <p> شرح کالا / خدمت </p> </td> <td style='width: 2%' class='JBorder'> <p> تعداد </p> </td> <td style='width: 12%' class='JBorder'> <p> مبلغ هر واحد (ریال) </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 1 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 2 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 3 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b> </b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 4 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 5 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 6 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> <tr> <td style='width: 2%' class='JBorder'> <p> 7 </p> </td> <td style='width: 25%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 2%' class='JBorder'> <p> <b></b> </p> </td> <td style='width: 12%' class='JBorder'> <p> <b></b> </p> </td> </tr> </table> </td> </tr> <tr> <td class='TBorder'> <p> <span> مبلغ کل به عدد (ریال): </span> <span class='priceNumeric'> </span> </p> </td> <td class='TBorder'> <p> مبلغ کل به حروف (ریال): <br> <b class='priceLetters'> </b> </p> </td> </tr> </table> </div> <div class='TTeam'> <p> تاریخ و امضا <br> مدیرعامل </p> <p> تاریخ و امضا <br> مدیر ارشد عملیات </p> <p> تاریخ و امضا <br> مدیر گروه نرم افزاری پارت </p> <p> تاریخ و امضا <br> درخواست دهنده </p>   </div> </div><script src='data.js'></script><script> function toEnglishDigits(e){if(null==e)return null;if('string'!=typeof e||0===e.length)return e.toString();for(var t='',r=0;r<e.length;r++){var n='۰۱۲۳۴۵۶۷۸۹'.indexOf(e[r]);0<=n?t+=n.toString():t+=0<=(n='٠١٢٣٤٥٦٧٨٩'.indexOf(e[r]))?n.toString():e[r]}return t.replace(/,/g,'')}function wordifyfa(e,t){if(void 0===t&&(t=0),null===e)return'';var r=parseInt(toEnglishDigits(e));if(r<0)return'منفی '+wordifyfa(r*=-1,t);if(0===r)return 0===t?'صفر':'';e='';return 0<t&&(e+=' و ',--t),r<10?e+=['یک','دو','سه','چهار','پنج','شش','هفت','هشت','نه'][r-1]:r<20?e+=['ده','یازده','دوازده','سیزده','چهارده','پانزده','شانزده','هفده','هیجده','نوزده'][r-10]:r<100?e+=['بیست','سی','چهل','پنجاه','شصت','هفتاد','هشتاد','نود'][Math.floor(r/10)-2]+wordifyfa(r%10,t+1):r<1e3?e+=['یکصد','دویست','سیصد','چهارصد','پانصد','ششصد','هفتصد','هشتصد','نهصد'][Math.floor(r/100)-1]+wordifyfa(r%100,t+1):r<1e6?e+=wordifyfa(Math.floor(r/1e3),t)+' هزار'+wordifyfa(r%1e3,t+1):r<1e9?e+=wordifyfa(Math.floor(r/1e6),t)+' میلیون'+wordifyfa(r%1e6,t+1):r<1e12?e+=wordifyfa(Math.floor(r/1e9),t)+' میلیارد'+wordifyfa(r%1e9,t+1):r<1e15&&(e+=wordifyfa(Math.floor(r/1e12),t)+' تریلیارد'+wordifyfa(r%1e12,t+1)),e}function wordifyRials(e){return null==e||''===e?'':wordifyfa(e,0)+' ریال'}function toFarsiNumber(e){var t=['۰','۱','۲','۳','۴','۵','۶','۷','۸','۹'];return e.toString().replace(/\\d/g,e=>t[e])}document.getElementsByClassName('date')[0].innerText=(new Date).toLocaleDateString('fa-ir'),document.getElementsByClassName('IRvalue')[0].innerText=data.IRvalue,document.getElementsByClassName('accountValue')[0].innerText=toFarsiNumber(data.accountValue),document.getElementsByClassName('purchaseDescription')[0].innerText=toFarsiNumber(data.purchaseDescription),document.getElementsByClassName('priceNumeric')[0].innerText=toFarsiNumber(data.priceNumeric),document.getElementsByClassName('priceLetters')[0].innerText=wordifyfa(data.priceNumeric);for(let e=1;e<8;e++)document.getElementsByClassName('table')[0].children[0].children[e].children[1].children[0].innerText=table.description[e-1],document.getElementsByClassName('table')[0].children[0].children[e].children[2].children[0].innerText=table.number[e-1],document.getElementsByClassName('table')[0].children[0].children[e].children[3].children[0].innerText=table.amount[e-1];setTimeout(()=>{window.print()},2e3);</script></body></html>";
                createFile(htmlPath, htmlData);
                createFile(dataPath, json);

                var p = new Process();
                p.StartInfo = new ProcessStartInfo(htmlPath)
                {
                    UseShellExecute = true
                };
                p.Start();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }


        private void service_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblcomp.Visibility = Visibility.Visible;
            template.Visibility = Visibility.Visible;
            if (service.SelectedIndex == 0)
            {
                type = 1;
                template.Items.Clear();
                template.Items.Add("پارس پک");
                template.Items.Add("ایران سرور");
                template.Items.Add("آذرآنلاین");
                template.Items.Add("هتزنر");
                oprator.Text = "";
                اhes.Text = "";
                shab.Text = "";
                description.Text = "";

            }
            if (service.SelectedIndex == 1)
            {
                type = 6;
                template.Items.Clear();
                template.Items.Add("پارس آنلاین");
                template.Items.Add("آسیاتک");
                template.Items.Add("افرانت شیراز");
                template.Items.Add("افرانت تهران");
                template.Items.Add("رسپینا");
                template.Items.Add("امین آسیا");
                Comp.Visibility = Visibility.Hidden;
                oprator.Text = "";
                اhes.Text = "";
                shab.Text = "";
                description.Text = "";
            }
            if (service.SelectedIndex == 2)
            {
                type = 5;
                template.Items.Clear();
                template.Items.Add("مخابرات");
                template.Items.Add("پارسیس");
                template.Items.Add("شاتل");
               
                template.Items.Add("پارس");
                template.Items.Add("ایرانسل");
                Comp.Visibility = Visibility.Hidden;
                oprator.Text = "";
                اhes.Text = "";
                shab.Text = "";
                description.Text = "";

            }
            if (service.SelectedIndex == 3)
            {
                type =2;
                template.Items.Clear();
                template.Items.Add("پارس پک");
                template.Items.Add("ایران سرور");
                oprator.Text = "";
                اhes.Text = "";
                shab.Text = "";
                description.Text = "";

            }
            if (service.SelectedIndex == 4)
            {
                type = 10;
                template.Items.Clear();
                template.Items.Add("رشیدپور");
                template.Items.Add("فناوران اطلاعات ");
                template.Items.Add("نور توس");
                oprator.Text = "";
                اhes.Text = "";
                shab.Text = "";
                description.Text = "";
                Comp.Visibility = Visibility.Hidden;

            }
            if (service.SelectedIndex == 5)
            {

                oprator.Text = "";
                اhes.Text = "";
                shab.Text = "";
                description.Text = "";


            }


        }

        public string[] IR(string company)
        {
            string[] outpot = new string[2];

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT sheba FROM bank WHERE company = " + "'" + company + "'");
            OleDbCommand cmd2 = new OleDbCommand("SELECT hesab FROM bank WHERE company = " + "'" + company + "'");
            cmd.Connection = conn; 
            cmd.CommandType = CommandType.Text;
            cmd2.Connection = conn;     
            cmd2.CommandType = CommandType.Text;
            outpot[0] = cmd.ExecuteScalar().ToString();
            outpot[1] = cmd2.ExecuteScalar().ToString();
            conn.Close();
            return outpot;

        }

        public void queryexe(string query)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = query;
            conn.Open();
            cmd.Connection = conn;
            cmd.ExecuteNonQueryAsync();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable rows = new DataTable();
            da.Fill(rows);
            int count = rows.Rows.Count;
            if (count > 0)
            {
                //listView1.Items.Clear();
                Comp.Items.Clear();

                for (int i = 0; i < count; i++)
                {
                    Comp.Items.Add(rows.Rows[i]["name"].ToString());
                }
            }
            conn.Close();
        }
        public void queryexe1(int cost)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = ("SELECT * from  Register WHERE Cost = " + cost + " AND OprUser =" + "'"+oprator.Text+"'");
            conn.Open();
            cmd.Connection = conn;
            cmd.ExecuteNonQueryAsync();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable rows = new DataTable();
            da.Fill(rows);
            int count = rows.Rows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("همچین مبلغی در یتابیس موجوداست، آیا داده ثبت شود؟", "توجه", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    conn.Close();
                }
                else
                {
                    string OprDate = DateTime.Today.ToShortDateString();
                    OleDbCommand cmd1 = new OleDbCommand("INSERT into Register (Company , Services, RegDate,  Cost , OprUser, OprDate , type , Description ) Values(@Company , @Services, @RegDate, @Cost , @OprUser ,@OprDate ,@type, @Description)");
                    cmd1.Connection = conn;
                    if (conn.State == ConnectionState.Open)
                    {
                        cmd1.Parameters.Add("@Company", OleDbType.VarChar).Value = company;
                        cmd1.Parameters.Add("@Services", OleDbType.VarChar).Value = Services;
                        cmd1.Parameters.Add("@RegDate", OleDbType.VarChar).Value = finalldate();
                        cmd1.Parameters.Add("@Cost", OleDbType.Integer).Value = Convert.ToInt32(totalcost());
                        cmd1.Parameters.Add("@OprUser", OleDbType.VarChar).Value = oprator.Text;
                        cmd1.Parameters.Add("@OprDate", OleDbType.VarChar).Value = OprDate;
                        cmd1.Parameters.Add("@type", OleDbType.Integer).Value = type;
                        cmd1.Parameters.Add("@Description", OleDbType.VarChar).Value = description.Text;
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("اطلاعات اضافه شد","",MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                }
                conn.Close();
            
                //listView1.Items.Clear();
              
               
            }
            
                
        }

        private void template_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (service.SelectedIndex == 0)
            {
                if (template.SelectedIndex == 0)
                {
                    queryexe("SELECT name FROM Services WHERE panel = 'پارس پک' AND Type =1");
                                   }
                if (template.SelectedIndex == 1)
                {
                    queryexe("SELECT name FROM Services WHERE panel = 'ایران سرور' AND Type =1");
                }
                if (template.SelectedIndex == 2)
                {
                    queryexe("SELECT name FROM Services WHERE panel = 'آذرآنلاین' AND Type =1");
                }
                Comp.Visibility = Visibility.Visible;
            }
        
            shab.Text = "";
            اhes.Text = "";
            oprator.Text = "";
            description.Text = "";
            product1.Text = "";
            product2.Text = "";
            product3.Text = "";
            product4.Text = "";
            product5.Text = "";
            product6.Text = "";
            product7.Text = "";
            p1.Text = "";
            p2.Text = "";
            p3.Text = "";
            p4.Text = "";
            p5.Text = "";
            p6.Text = "";
            p7.Text = "";
            c1.Text = "";
            c2.Text = "";
            c3.Text = "";
            c4.Text = "";
            c5.Text = "";
            c6.Text = "";
            c7.Text = "";

            if (service.SelectedIndex == 0)
            {
                if (template.SelectedIndex == 0)
                {
                    //parspack
                    company = "پارس پک";
                    Services = "جهت استفاده کاربران - خرید از پارس پک";




                }
                if (template.SelectedIndex == 1)
                {
                    //iranserver
                    company = "ایران سرور";
                    Services = "جهت استفاده کاربران - خرید از ایران سرور";



                }
                if (template.SelectedIndex == 2)
                {
                    //azaronline
                    company = "آذرآنلاین";
                    Services = "جهت استفاده کاربران - خرید ازآذر آذرآنلاین";



                }
                if (template.SelectedIndex == 3)
                {
                    //azaronline
                    company = "هتزنر";
                    Services = "جهت استفاده کاربران - خرید از هتزنر";
                    product1.Text = "هزینه سرور ";
                    c1.Text = "1";



                }
            }
            else if (service.SelectedIndex == 1)
            {
                if (template.SelectedIndex == 0)
                {
                    oprator.Text = "وجیهه نباتیان";
                    company = "پارس آنلاین";
                    Services = "هزینه دیتاسنتر - خرید از پارس آنلاین";
                    product1.Text = "Colocation-Full-Rack";
                    product2.Text = "Power SupplyBundle";
                    product3.Text = "مالیات";
                    p1.Text = "433300000";
                    p2.Text = "900000";
                    p3.Text = "83178000";
                    c1.Text = "2";
                    c2.Text = "64";
                    c3.Text = "1";
                    count = 2;

                }
                if (template.SelectedIndex == 1)
                {
                    oprator.Text = "محمدرضا زارعی";
                    company = "آسیاتک";
                    Services = "هزینه دیتاسنتر - خرید از آسیاتک";
                    product1.Text = "پارت یک";
                    product2.Text = "پارت دو";
                    product3.Text = "پارت سه";
                    p1.Text = "970866594";
                    p2.Text = "1083282726";
                    p3.Text = "970866594";
                    c1.Text = "1";
                    c2.Text = "1";
                    c3.Text = "1";
                    count = 2;



                }
                if (template.SelectedIndex == 2)
                {
                    oprator.Text = "محمدرضا زارعی";
                    company = "افرانت";
                    Services = "هزینه دیتاسنتر شیراز - خرید از افرانت";
                    product1.Text = "فضای رک اختصاصی";
                    product2.Text = "مالیات";
                    c1.Text = "1";
                    c2.Text = "1";
                    p1.Text = "40000000";
                    p2.Text = "3600000";
                    count = 2;

                }
                if (template.SelectedIndex == 3)
                {
                    oprator.Text = "محمدرضا زارعی";
                    company = "افرانت";
                    Services = "هزینه دیتاسنتر تهران - خرید ازافرانت";
                    product1.Text = "فضای رک پیشرفته";
                    product2.Text = "اینترنت اختصاصی";
                    product3.Text = "آی پی ";
                    product4.Text = "مالیات ";
                    p1.Text = "630000000";
                    p2.Text = "10000000";
                    p3.Text = "700000"; 
                    p4.Text = "57663000";
                    c1.Text = "1";
                    c2.Text = "1";
                    c3.Text = "1";
                    c4.Text = "1";
                    count = 3;


                }
                if (template.SelectedIndex == 4)
                {
                    oprator.Text = "وجیهه نباتیان";
                    company = "رسپینا";
                    Services = "هزینه دیتاسنتر - خرید از رسپینا";
                    product1.Text = "تامین فضای رک";
                    product2.Text = "اینترنت اختصاصی";
                    p1.Text = "3400000";
                    p2.Text = "1600000";

                }
                if (template.SelectedIndex == 5)
                {
                    oprator.Text = "علیرضا تجلی";
                    company = "امین آسیا";
                    Services = "هزینه دیتاسنتر - خرید از داده های ابری امین آسیا";
                    product1.Text = "اجاره رک";
                    product2.Text = "اجاره آی پی";
                    product3.Text = "مالیات";
                    p1.Text = "10000000";
                    p2.Text = "700000";
                    p3.Text = "1843200";
                    c1.Text = "1.60";
                    c2.Text = "6.40";
                    c3.Text = "1";
                    count = 2;


                }


            }
            else if (service.SelectedIndex == 2)
            {
                if (template.SelectedIndex == 0)
                {
                    oprator.Text = "محمدرضا زارعی";
                    company = "مخابرات";
                    Services = "هزینه اینترنت - خرید مخابرات";
                    product1.Text = "خط اینترنت اختصاصی با پهنای باند 30 مگ پردیس یک";
                    product2.Text = "خط اینترنت اختصاصی با پهنای باند 30 مگ پردیس دو";
                    product3.Text = "خط اینترنت اختصاصی با پهنای باند 50 مگ پردیس سه";
                   
                    p1.Text = "45780000";
                    p2.Text = "45780000";
                    p3.Text = "76300000";
                    c1.Text = "1";
                    c2.Text = "1";
                    c3.Text = "1";
                    count = 2;







                }
                if (template.SelectedIndex == 1)
                {
                    oprator.Text = "مصطفی شاهچراغیان";
                    company = "پارسیس";
                    Services = "جهت استفاده کاربران - خرید از پارسیس";



                }
                if (template.SelectedIndex == 2)
                {
                    oprator.Text = "محمدرضا زارعی";
                    company = "شاتل";
                    Services = "جهت استفاده کاربران - خرید از شاتل";



                }
                if (template.SelectedIndex == 3)
                {

                    company = "پارس";
                    Services = "جهت استفاده کاربران - خرید از پارس";



                }
                if (template.SelectedIndex == 4)
                {

                    company = "ایرانسل";
                    Services = "جهت استفاده کاربران - خرید از ایرانسل";



                }
               
            }
            else if (service.SelectedIndex == 4)
            {
                if (template.SelectedIndex == 0)
                {
                    oprator.Text = "مصطفی شاهچراغیان";
                    company = "رشیدپور";
                    Services = "خرید سخت افزار - خرید از رشیدپور";
                  
                 }
                if (template.SelectedIndex == 1)
                {
                    oprator.Text = "علی نورانی";
                    company = "فناوران اطلاعات";
                    Services = "خرید سخت افزار - خرید از فناوران اطلاعات";

                }
                if (template.SelectedIndex == 2)
                {
                    oprator.Text = "مصطفی شاهچراغیان";
                    company = "نور توس";
                    Services = "خرید سخت افزار - خرید از نور توس";

                }


            }
            IRvalue = IR(company)[0];
            Hesab = IR(company)[1];



            shab.Text = IRvalue;
            اhes.Text = Hesab;
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            servicesss.Visibility = Visibility.Hidden;
            Comp.Visibility = Visibility.Hidden;
            produtshow.Text = "افزودن کالا";
            backitem.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
            service.Visibility = Visibility.Hidden;
            template.Visibility = Visibility.Hidden;
            oprator.Visibility = Visibility.Hidden;
            description.Visibility = Visibility.Hidden;
            shab.Visibility = Visibility.Hidden;
            اhes.Visibility = Visibility.Hidden;
            hinttxt_Copy2.Visibility = Visibility.Hidden;
            hinttxt_Copy3.Visibility = Visibility.Hidden;
            hinttxt_Copy4.Visibility = Visibility.Hidden;
            hinttxt_Copy1.Visibility = Visibility.Hidden;
           count++;
            switch (count)
            {
                case 1:
                    {
                        backitem.Visibility = Visibility.Visible;
                        back.Visibility = Visibility.Visible;

                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                    }
                    break;

                case 2:
                    {
                        back.Visibility = Visibility.Visible;
                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                        product2.Visibility = Visibility.Visible;
                        p2.Visibility = Visibility.Visible;
                        c2.Visibility = Visibility.Visible;
                    }
                    break;
                case 3:
                    {
                        back.Visibility = Visibility.Visible;
                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                        product2.Visibility = Visibility.Visible;
                        p2.Visibility = Visibility.Visible;
                        c2.Visibility = Visibility.Visible;
                        product3.Visibility = Visibility.Visible;
                        p3.Visibility = Visibility.Visible;
                        c3.Visibility = Visibility.Visible;
                    }
                    break;
                case 4:
                    {
                        back.Visibility = Visibility.Visible;
                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                        product2.Visibility = Visibility.Visible;
                        p2.Visibility = Visibility.Visible;
                        c2.Visibility = Visibility.Visible;
                        product3.Visibility = Visibility.Visible;
                        p3.Visibility = Visibility.Visible;
                        c3.Visibility = Visibility.Visible;
                        product4.Visibility = Visibility.Visible;
                        p4.Visibility = Visibility.Visible;
                        c4.Visibility = Visibility.Visible;
                    }
                    break;
                case 5:
                    {


                        back.Visibility = Visibility.Visible;
                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                        product2.Visibility = Visibility.Visible;
                        p2.Visibility = Visibility.Visible;
                        c2.Visibility = Visibility.Visible;
                        product3.Visibility = Visibility.Visible;
                        p3.Visibility = Visibility.Visible;
                        c3.Visibility = Visibility.Visible;
                        product4.Visibility = Visibility.Visible;
                        p4.Visibility = Visibility.Visible;
                        c4.Visibility = Visibility.Visible;
                        product5.Visibility = Visibility.Visible;
                        p5.Visibility = Visibility.Visible;
                        c5.Visibility = Visibility.Visible;
                    }
                    break;

                case 6:
                    {
                        back.Visibility = Visibility.Visible;
                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                        product2.Visibility = Visibility.Visible;
                        p2.Visibility = Visibility.Visible;
                        c2.Visibility = Visibility.Visible;
                        product3.Visibility = Visibility.Visible;
                        p3.Visibility = Visibility.Visible;
                        c3.Visibility = Visibility.Visible;
                        product4.Visibility = Visibility.Visible;
                        p4.Visibility = Visibility.Visible;
                        c4.Visibility = Visibility.Visible;
                        product5.Visibility = Visibility.Visible;
                        p5.Visibility = Visibility.Visible;
                        c5.Visibility = Visibility.Visible;
                        product6.Visibility = Visibility.Visible;
                        p6.Visibility = Visibility.Visible;
                        c6.Visibility = Visibility.Visible;
                    }
                    break;
                case 7:
                    {
                        back.Visibility = Visibility.Visible;
                        product1.Visibility = Visibility.Visible;
                        p1.Visibility = Visibility.Visible;
                        c1.Visibility = Visibility.Visible;
                        product2.Visibility = Visibility.Visible;
                        p2.Visibility = Visibility.Visible;
                        c2.Visibility = Visibility.Visible;
                        product3.Visibility = Visibility.Visible;
                        p3.Visibility = Visibility.Visible;
                        c3.Visibility = Visibility.Visible;
                        product4.Visibility = Visibility.Visible;
                        p4.Visibility = Visibility.Visible;
                        c4.Visibility = Visibility.Visible;
                        product5.Visibility = Visibility.Visible;
                        p5.Visibility = Visibility.Visible;
                        c5.Visibility = Visibility.Visible;
                        product6.Visibility = Visibility.Visible;
                        p6.Visibility = Visibility.Visible;
                        c6.Visibility = Visibility.Visible;
                        product7.Visibility = Visibility.Visible;
                        p7.Visibility = Visibility.Visible;
                        c7.Visibility = Visibility.Visible;
                    }
                    break;


                default:
                    {
                        MessageBox.Show("ماکزیمم کالای  قابل درخواست 7 عدد میباشد");
                    }
                    break;


            }








        }
        public string finalldate()
        {
            PersianCalendar jc = new PersianCalendar();
            string PDate = jc.GetYear(DateTime.Now).ToString() + "/" + jc.GetMonth(DateTime.Now).ToString() + "/" + jc.GetDayOfMonth(DateTime.Now).ToString();
            string[] chararacter = PDate.Split('/');
            string finall_date = "";
            string day = (Convert.ToInt32(chararacter[2])).ToString();
            string month = (Convert.ToInt32(chararacter[1])).ToString();
            string year = (Convert.ToInt32(chararacter[0])).ToString();
            finall_date += year;
            if (month.Length == 1)
            {
                finall_date += "0" + month;
            }
            else
            {
                finall_date += month;
            }

            if (day.Length == 1)
            {
                finall_date += "0" + day;
            }
            else
            {
                finall_date += day;
            }
            return finall_date;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            queryexe1(Convert.ToInt32(totalcost()));
            
            
        }

        private void product1_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            count--;
            servicesss.Visibility = Visibility.Visible;
            Comp.Visibility = Visibility.Visible;
            produtshow.Text = "اقلام";
            back.Visibility = Visibility.Hidden;
            service.Visibility = Visibility.Visible;
            template.Visibility = Visibility.Visible;
            oprator.Visibility = Visibility.Visible;
            description.Visibility = Visibility.Visible;
            shab.Visibility = Visibility.Visible;
            اhes.Visibility = Visibility.Visible;
            hinttxt_Copy2.Visibility = Visibility.Visible;
            hinttxt_Copy3.Visibility = Visibility.Visible;
            hinttxt_Copy4.Visibility = Visibility.Visible;
            hinttxt_Copy1.Visibility = Visibility.Visible;
            backitem.Visibility = Visibility.Hidden;
            product1.Visibility = Visibility.Hidden;
            p1.Visibility = Visibility.Hidden;
            c1.Visibility = Visibility.Hidden;
            product2.Visibility = Visibility.Hidden;
            p2.Visibility = Visibility.Hidden;
            c2.Visibility = Visibility.Hidden;
            product3.Visibility = Visibility.Hidden;
            p3.Visibility = Visibility.Hidden;
            c3.Visibility = Visibility.Hidden;
            product4.Visibility = Visibility.Hidden;
            p4.Visibility = Visibility.Hidden;
            c4.Visibility = Visibility.Hidden;
            product5.Visibility = Visibility.Hidden;
            p5.Visibility = Visibility.Hidden;
            c5.Visibility = Visibility.Hidden;
            product6.Visibility = Visibility.Hidden;
            p6.Visibility = Visibility.Hidden;
            c6.Visibility = Visibility.Hidden;
            product7.Visibility = Visibility.Hidden;
            p7.Visibility = Visibility.Hidden;
            c7.Visibility = Visibility.Hidden;
        }

        private void template_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {

           
            if(template.SelectedIndex == 0)
            {
               
                if (Comp.SelectedIndex == 0)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "علیرضا تجلی";
                }
                if (Comp.SelectedIndex == 1)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "رضا محمودی";
                }
            }
            if (template.SelectedIndex == 1)

            {
              
                if (Comp.SelectedIndex == 0)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "رضا محمودی";
                }
                if (Comp.SelectedIndex == 1)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "محسن مختاری";
                }
            }
            if (template.SelectedIndex == 2)

            {

                if (Comp.SelectedIndex == 0)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "عماد فتحی";
                }
                if (Comp.SelectedIndex == 1)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "حامد موسایی";
                }
            }
            if (template.SelectedIndex == 3)

            {

                if (Comp.SelectedIndex == 0)
                {
                    product1.Text = Comp.SelectedValue.ToString();
                    oprator.Text = "علی جاویدی";
                }
                
            }

        }
    }
   
    
}
