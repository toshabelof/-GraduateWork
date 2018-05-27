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
    /// Логика взаимодействия для ViewBreak.xaml
    /// </summary>
    public partial class ViewBreak : Window
    {
        public ViewBreak()
        {
            InitializeComponent();
        }

        BrushConverter bc = new BrushConverter();

        private void OkMoreBreakButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OkMoreBreakButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void OkMoreBreakButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OkMoreBreakButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void OkMoreBreakButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CencelMoreBreakButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelMoreBreakButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelMoreBreakButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelMoreBreakButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelMoreBreakButton_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
