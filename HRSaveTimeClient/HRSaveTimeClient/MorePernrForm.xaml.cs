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

    }
}
