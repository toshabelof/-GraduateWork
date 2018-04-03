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

using System.IO;

namespace WpfApplication1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myProfile.Visibility = Visibility.Visible;
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            myProfile.Visibility = Visibility.Hidden;
        }

        private void myProfile_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();

            Profile myProfile = new Profile();

            using (StreamReader sr = new StreamReader("myProfile.txt", encoding: Encoding.GetEncoding(1251)))
            {
                myProfile.Add(sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine());
            };

            l_name.Content = myProfile.Name;
            l_position.Content = myProfile.Position;
            l_birth.Content = myProfile.Birth;
            l_work_phone.Content = myProfile.workPhone;
            l_mobile_phone.Content = myProfile.mobilePhone;
            l_home_phone.Content = myProfile.homePhone;
            l_skype.Content = myProfile.skypeID;
            l_vk_id.Content = myProfile.vkID;    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myProfile.Visibility = Visibility.Hidden;
        }

    }
}
