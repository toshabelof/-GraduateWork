using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace HRSaveTimeClient
{
    /// <summary>
    /// Логика взаимодействия для MorePernrForm.xaml
    /// </summary>
    public partial class MorePernrForm : Window
    {
        public MorePernrForm()
        {
            InitializeComponent();
        }

        BrushConverter bc = new BrushConverter();


        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OkMorePernrReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OkMorePernrReportsButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void OkMorePernrReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OkMorePernrReportsButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void OkMorePernrReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CencelMorePernrReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelMorePernrReportsButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelMorePernrReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelMorePernrReportsButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelMorePernrReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        public static SortedList<string, string> setting = new SortedList<string, string>();
        List<string> list = new List<string>();
        String connect;

        public String[] GetBDType()
        {
            var result = "";
            setting.TryGetValue("BD", out result);
            String[] mas = result.Split('/');
            return mas;
        }

        public void GetSetting()
        {
            StreamReader sr = new StreamReader("settings.txt");
            try
            {
                list.Clear();
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
                sr.Close();

                setting.Clear();
                if (list.Count != 0)
                {
                    for (int i = 0; i < list.Count; i += 2)
                    {
                        setting.Add(list[i], list[i + 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetSetting();
            String[] mas = GetBDType();


            connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("Select IDPERNR from PERNR", con);
                using (var reader = com.ExecuteReader())
                {
                    Pernr_dG.Items.Clear();
                    while (reader.Read())
                    {
                        var data = new Pernr { Pernr1 = reader[0].ToString() };
                        Pernr_dG.Items.Add(data);
                    }
                }
            }
        }

    }
}
