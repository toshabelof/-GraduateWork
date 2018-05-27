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
    /// Логика взаимодействия для ViewOGRV.xaml
    /// </summary>
    public partial class ViewOGRV : Window
    {
        public ViewOGRV()
        {
            InitializeComponent();
        }

        BrushConverter bc = new BrushConverter();

        private void OkMoreOGRVButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OkMoreOGRVButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void OkMoreOGRVButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OkMoreOGRVButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void OkMoreOGRVButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CencelMoreOGRVButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelMoreOGRVButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelMoreOGRVButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelMoreOGRVButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelMoreOGRVButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
