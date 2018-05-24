using System;
using System.Collections.Generic;
using System.Text;
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
    /// Логика взаимодействия для GlobalForm.xaml
    /// </summary>
    public partial class GlobalForm : Window
    {
        public GlobalForm()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void GExit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void GCurt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void people_PreviewMouseMove(object sender, MouseEventArgs e)
        {
           
        }

        BrushConverter bc = new BrushConverter(); 
        private void people_MouseEnter(object sender, MouseEventArgs e)
        {
            People.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void people_MouseLeave(object sender, MouseEventArgs e)
        {
            People.Background = Brushes.Transparent;
        }

        private void Reports_MouseEnter(object sender, MouseEventArgs e)
        {
            Reports.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Reports_MouseLeave(object sender, MouseEventArgs e)
        {
            Reports.Background = Brushes.Transparent;
        }

        private void Inquiry_MouseEnter(object sender, MouseEventArgs e)
        {
            Inquiry.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Inquiry_MouseLeave(object sender, MouseEventArgs e)
        {
            Inquiry.Background = Brushes.Transparent;
        }

        private void Schedules_MouseEnter(object sender, MouseEventArgs e)
        {
            Schedules.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Schedules_MouseLeave(object sender, MouseEventArgs e)
        {
            Schedules.Background = Brushes.Transparent;
        }
                 

        private void Rules_MouseEnter(object sender, MouseEventArgs e)
        {
            Rules.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Rules_MouseLeave(object sender, MouseEventArgs e)
        {
            Rules.Background = Brushes.Transparent;
        }


        private void Monitoring_MouseEnter(object sender, MouseEventArgs e)
        {
            Monitoring.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Monitoring_MouseLeave(object sender, MouseEventArgs e)
        {
            Monitoring.Background = Brushes.Transparent;
        }


        private void Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
        }


        private void GExit_MouseEnter(object sender, MouseEventArgs e)
        {
            GExit.Width = 20;
        }

        private void GExit_MouseLeave(object sender, MouseEventArgs e)
        {
            GExit.Width = 15;
        }
    }
}
