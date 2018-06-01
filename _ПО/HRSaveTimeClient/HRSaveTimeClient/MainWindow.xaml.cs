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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRSaveTimeClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Если вы забыли пароль, то обратитесь к вашему HR Администратору для его сброса!", "Инструкция", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalForm gf = new GlobalForm();          
            this.Visibility = Visibility.Hidden;
            gf.ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitApp.Width = 20;
            ExitApp.Height = 20;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitApp.Width = 15;
            ExitApp.Height = 15;
        }

        
            

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            RememberPasswordButton.TextDecorations = TextDecorations.Underline;
            
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            RememberPasswordButton.TextDecorations = null;        
        }
    }
}
