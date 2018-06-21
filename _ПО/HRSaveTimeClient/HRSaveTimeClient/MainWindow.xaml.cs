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
using Oracle.DataAccess.Client;
using System.IO;


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

        public static SortedList<string, string> setting = new SortedList<string, string>();
        List<string> list = new List<string>();

        public string TextBox
        {
            get
            {
                return Login_tBox.Text;
            }
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

        public void GetSetting(string Way)
        {
            StreamReader sr = new StreamReader(Way);
            try
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
                sr.Close();

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

        public void SetSetting()
        {
            StreamWriter sw = new StreamWriter("settings.txt");
            try
            {
                foreach (string k in setting.Keys)
                {
                    foreach (string v in setting.Values)
                    {
                        sw.WriteLine(k);
                        sw.WriteLine(v);
                    }
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
            }
        }

        public void SetSetting(string key, string value)
        {
            if (!setting.ContainsKey(key))
            {
                setting.Add(key, value);
            }
            else
            {
                setting.Remove(key);
                setting.Add(key, value);
            }

            StreamWriter sw = new StreamWriter("settings.txt");
            try
            {
                foreach (string k in setting.Keys)
                {
                    foreach (string v in setting.Values)
                    {
                        sw.WriteLine(k);
                        sw.WriteLine(v);
                    }
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
            }
        }

        public String[] GetBDType()
        {
            var result = "";
            setting.TryGetValue("BD", out result);
            String[] mas = result.Split('/');
            return mas;
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
            switch (Login_tBox.Text)
            {
                case "SysAdmin":
                    {
                        #region
                        using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
                        {

                            openFileDialog.Filter = "Settings File (*.txt) | *.txt";

                            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    GetSetting(openFileDialog.FileName.ToString());
                                    SetSetting();
                                    MessageBox.Show("Настройки HRSaveTime успешно скопированы на локальный ПК.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }

                        }
                        break;
                        #endregion
                    }
                case "HRALL":
                    {
                        #region
                        if (Password_tBox.Password == "ROOT")
                        {
                            GlobalForm gf = new GlobalForm();                        

                            this.Visibility = Visibility.Hidden;
                            gf.ValueProfile = "HRALL";
                            gf.ShowDialog();

                            this.Visibility = Visibility.Visible;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Вы ввели неверный пароль.", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                            Password_tBox.Password = "";
                            Password_tBox.Focus();
                        }
                        break;
                        #endregion
                    }
                default:
                    {
                        #region
                        GetSetting();
                        String[] mas = GetBDType();
                        String connect;


                        String login = Login_tBox.Text;
                        String password = Password_tBox.Password;
                        var lists = new List<string>();

                        connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                        using (OracleConnection con = new OracleConnection(connect))
                        {
                            con.Open();
                            OracleCommand com = new OracleCommand("select PASSWORD, PERNR from AUTHENTICATION where LOGIN = '" + login + "'", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    lists.Add(reader[0].ToString());
                                    lists.Add(reader[1].ToString());
                                }
                                con.Close();
                            }
                        }

                        if (lists.Count != 0)
                        {
                            if (Password_tBox.Password == lists[0])
                            {
                                GlobalForm gf = new GlobalForm();
                                gf.MyPernr_tBox.Text = lists[1];

                                this.Visibility = Visibility.Hidden;
                              gf.ShowDialog();
                                this.Visibility = Visibility.Visible;
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Вы ввели неверный пароль.", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                                Password_tBox.Password = "";
                                Password_tBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким Login не найден в системе HRSaveTime.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            Password_tBox.Password = "";
                            Login_tBox.Focus();
                        }
                        break;
                        #endregion
                    }
            }
            
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
