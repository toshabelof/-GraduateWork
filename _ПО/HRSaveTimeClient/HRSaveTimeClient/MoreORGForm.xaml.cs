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
    /// Логика взаимодействия для MoreORGForm.xaml
    /// </summary>
    public partial class MoreORGForm : Window
    {
        public MoreORGForm()
        {
            InitializeComponent();
        }

        BrushConverter bc = new BrushConverter();

        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OkMoreORGButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OkMoreORGButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void OkMoreORGButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OkMoreORGButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void OkMoreORGButton_PreviewMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void CencelMoreORGButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelMoreORGButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelMoreORGButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelMoreORGButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelMoreORGButton_PreviewMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
