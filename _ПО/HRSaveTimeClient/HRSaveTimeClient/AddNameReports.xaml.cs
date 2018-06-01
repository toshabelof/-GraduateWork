using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddNameReports.xaml
    /// </summary>
    public partial class AddNameReports : Window
    {
        public AddNameReports()
        {
            InitializeComponent();
        }

        BrushConverter bc = new BrushConverter();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CencelReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelReportsButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelReportsButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SaveReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveReportsButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveReportsButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void SaveReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
