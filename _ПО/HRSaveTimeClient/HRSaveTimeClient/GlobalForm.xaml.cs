using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Oracle.DataAccess.Client;
using System.IO;
using System.Data;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.Globalization;



namespace HRSaveTimeClient
{
    /// <summary>
    /// Логика взаимодействия для GlobalForm.xaml
    /// </summary>
    public partial class GlobalForm : System.Windows.Window
    {
        public GlobalForm()
        {
            InitializeComponent();
            backgroundWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
        }

        //***** AllVariables ******  

        public String ValueProfile = "";
        private BackgroundWorker backgroundWorker;
        public String Login = "";

        BrushConverter bc = new BrushConverter();
        Grid grid = new Grid();
        String connect;
        public static SortedList<string, string> setting = new SortedList<string, string>();
        List<string> list = new List<string>();

        String TypeReports = null;


        //***** AllFunc ******      

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
            if (ValueProfile == "HRALL")
            {
                GetSetting();
                String[] mas = GetBDType();

                grid = NewProfileGrid;
                grid.Visibility = System.Windows.Visibility.Visible;
                Title.Text = "Создание нового профиля";
                People.IsEnabled = false;
                Inquiry.IsEnabled = false;
                Reports.IsEnabled = false;
                Schedules.IsEnabled = false;
                Rules.IsEnabled = false;
                Monitoring.IsEnabled = false;
                myProfileButton.IsEnabled = false;
                Search.IsEnabled = false;
            }
            else
            {
                #region Вывод данных в профиль сотрудника, под которым осуществлялся вход
                GetSetting();
                String[] mas = GetBDType();

                grid = MyProfileGrid;
                grid.Visibility = System.Windows.Visibility.Visible;
                Title.Text = "Мой профиль";

                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                using (OracleConnection con = new OracleConnection(connect))
                {
                    //вывод "Основная инфа по профилю"
                    con.Open();
                    OracleCommand com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, PERS_INFO.BIRTH, POSITION.Name, ORG_LEVEL.NAME, PERS_INFO.PGRVID, AUTHENTICATION.LOGIN, AUTHENTICATION.PASSWORD, RFID.IDRFID " +
                        "FROM PERNR, PERS_INFO , POSITION, AUTHENTICATION, ORG_LEVEL, RFID " +
                        "WHERE PERS_INFO.IDPERS = PERNR.PERSID and POSITION.IDPOS = PERS_INFO.POSID and PERNR.IDPERNR = AUTHENTICATION.PERNR and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.IDPERS = RFID.PERSID  and PERNR.IDPERNR = '" + MyPernr_tBox.Text + "'", con);

                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MyFIO_tBox.Text = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString();
                            MyBith_tBox.Text = Convert.ToDateTime(reader[3]).ToString("dd.MM.yyyy");
                            MyPosition_tBox.Text = reader[4].ToString();
                            MyORG_tBox.Text = reader[5].ToString();
                            MyPGRV_tBox.Text = reader[6].ToString();
                            MyLogin_tBox.Text = reader[7].ToString();
                            MyPassword_pBox.Password = reader[8].ToString();
                            MyRFID_tBox.Text = reader[9].ToString();
                        }

                    }

                    //вывод местоположения
                    com = new OracleCommand("SELECT ROOMS.NAME " +
                       "FROM TIME_PAIRS, ROOMS " +
                       "WHERE ROOMS.IDROOMS = TIME_PAIRS.ROOMID and TIME_PAIRS.TIMEBY is null and TIME_PAIRS.RFIDID= '" + MyRFID_tBox.Text + "'", con);

                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MyLocation_tBox.Text = reader[0].ToString();
                        }

                    }

                    //вывод "Контакты"
                    com = new OracleCommand("SELECT DESCRIPT, VALUE " +
                            "FROM CONTACTS, PERS_INFO, PERNR " +
                            "WHERE  CONTACTS.PERSID = PERS_INFO.IDPERS and PERNR.PERSID = PERS_INFO.IDPERS and PERNR.IDPERNR = '" + MyPernr_tBox.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Contacts { Descript = reader[0].ToString(), Value = reader[1].ToString() };
                            MyContacts_dG.Items.Add(data);
                        }
                    }

                    //вывод "Отсутствия"
                    com = new OracleCommand("SELECT VIEW_ABS.NAME, DATEFROM, DATEBY, DOCUMENT " +
                            "FROM ABSCENCE, VIEW_ABS " +
                            "WHERE VIEW_ABS.IDVIEW = ABSCENCE.VIEWID and ABSCENCE.PERNRID = '" + MyPernr_tBox.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Absence { View = reader[0].ToString(), DateFrom = reader[1].ToString(), DateBy = reader[2].ToString(), Document = reader[3].ToString() };
                            MyAbsence_dG.Items.Add(data);
                        }
                    }

                    //вывод "Присутствия"
                    com = new OracleCommand("SELECT TIME_PAIRS.DATAFROM, TIME_PAIRS.DATABY, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY, ROOMS.NAME " +
                            "FROM PERNR, RFID, TIME_PAIRS, ROOMS " +
                            "WHERE RFID.IDRFID = TIME_PAIRS.RFIDID and PERNR.PERSID = RFID.PERSID and TIME_PAIRS.ROOMID = ROOMS.IDROOMS and PERNR.IDPERNR = '" + MyPernr_tBox.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new TimePairs { DateFrom = reader[0].ToString(), DateBy = reader[1].ToString(), TimeFrom = reader[2].ToString(), TimeBy = reader[3].ToString(), Location = reader[4].ToString() };
                            MyTimePairs_dG.Items.Add(data);
                        }
                    }
                }
                #endregion
            }
        }


        //***** GlobalForma ******

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

        private void GExit_MouseEnter(object sender, MouseEventArgs e)
        {
            GExit.Width = 20;
        }

        private void GExit_MouseLeave(object sender, MouseEventArgs e)
        {
            GExit.Width = 15;
        }


        //***** GlobalForma: Search ******

        private void Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            Search.Text = "Поиск...";
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                grid.Visibility = System.Windows.Visibility.Hidden;
                grid = ProfileGrid;
                grid.Visibility = System.Windows.Visibility.Visible;
                Title.Text = "Профиль сотрудника";

                using (OracleConnection con = new OracleConnection(connect))
                {
                    //вывод "Основная инфа по профилю"
                    con.Open();
                    OracleCommand com = new OracleCommand("SELECT IDPERNR, LNAME, PERS_INFO.NAME, PATR, BIRTH, POSITION.Name, ORG_LEVEL.NAME, PGRVID, RULE, LOGIN, PASSWORD, RFID.IDRFID, ROOMS.NAME " +
                        "FROM PERNR, PERS_INFO , POSITION, AUTHENTICATION, ORG_LEVEL, RFID, TIME_PAIRS, ROOMS " +
                        "WHERE PERS_INFO.IDPERS = PERNR.PERSID and POSITION.IDPOS = PERS_INFO.POSID and PERNR.IDPERNR= AUTHENTICATION.PERNR and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.IDPERS = RFID.PERSID and TIME_PAIRS.RFIDID = RFID.IDRFID and TIME_PAIRS.DATABY is Null  and TIME_PAIRS.ROOMID = ROOMS.IDROOMS and PERNR.IDPERNR =  '" + Search.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pernr_tBox.Text = reader[0].ToString();
                            FIO_tBox.Text = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                            Position_tBox.Text = reader[5].ToString();
                            ORG_tBox.Text = reader[6].ToString();
                            Login_tBox.Text = reader[9].ToString();
                            Password_pBox.Password = reader[10].ToString();
                            RFID_tBox.Text = reader[11].ToString();
                            Location_tBox.Text = reader[12].ToString();
                        }

                    }

                    //вывод "Контакты"
                    com = new OracleCommand("SELECT DESCRIPT, VALUE " +
                            "FROM CONTACTS, PERS_INFO, PERNR " +
                            "WHERE  CONTACTS.PERSID = PERS_INFO.IDPERS and PERNR.PERSID = PERS_INFO.IDPERS and PERNR.IDPERNR = '" + Search.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        Contacts_dG.Items.Clear();
                        while (reader.Read())
                        {
                            var data = new Contacts { Descript = reader[0].ToString(), Value = reader[1].ToString() };
                            Contacts_dG.Items.Add(data);
                        }
                    }

                    //вывод "Отсутствия"
                    com = new OracleCommand("SELECT VIEW_ABS.NAME, DATEFROM, DATEBY, DOCUMENT " +
                            "FROM ABSCENCE, VIEW_ABS " +
                            "WHERE VIEW_ABS.IDVIEW = ABSCENCE.VIEWID and ABSCENCE.PERNRID = '" + Search.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        Absence_dG.Items.Clear();
                        while (reader.Read())
                        {
                            var data = new Absence { View = reader[0].ToString(), DateFrom = reader[1].ToString(), DateBy = reader[2].ToString(), Document = reader[3].ToString() };
                            Absence_dG.Items.Add(data);
                        }
                    }

                    //вывод "Присутствия"
                    com = new OracleCommand("SELECT TIME_PAIRS.DATAFROM, TIME_PAIRS.DATABY, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY, ROOMS.NAME " +
                            "FROM PERNR, RFID, TIME_PAIRS, ROOMS " +
                            "WHERE RFID.IDRFID = TIME_PAIRS.RFIDID and PERNR.PERSID = RFID.PERSID and TIME_PAIRS.ROOMID = ROOMS.IDROOMS and PERNR.IDPERNR = '" + Search.Text + "'", con);
                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimePairs_dG.Items.Clear();
                            var data = new TimePairs { DateFrom = reader[0].ToString(), DateBy = reader[1].ToString(), TimeFrom = reader[2].ToString(), TimeBy = reader[3].ToString(), Location = reader[4].ToString() };
                            TimePairs_dG.Items.Add(data);
                        }
                    }
                }
            }
        }


        //***** GlobalForma: Menu ******

        private void people_MouseEnter(object sender, MouseEventArgs e)
        {
            People.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void people_MouseLeave(object sender, MouseEventArgs e)
        {
            People.Background = Brushes.Transparent;
        }

        private void People_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid != null)
            {
                grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            grid = PersGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Список сотрудников";
            UpdatePersTable();
        }

        private void Inquiry_MouseEnter(object sender, MouseEventArgs e)
        {
            Inquiry.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Inquiry_MouseLeave(object sender, MouseEventArgs e)
        {
            Inquiry.Background = Brushes.Transparent;
        }

        private void Inquiry_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = InquiryGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Запросы на отсутствия";

            UpdateTableInquiry();
        }

        private void Reports_MouseEnter(object sender, MouseEventArgs e)
        {
            Reports.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Reports_MouseLeave(object sender, MouseEventArgs e)
        {
            Reports.Background = Brushes.Transparent;
        }

        private void Reports_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid != null)
            {
                grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            grid = ReportsGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Сохраненные отчеты";

            UpdateTableReports();
        }

        private void Schedules_MouseEnter(object sender, MouseEventArgs e)
        {
            Schedules.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Schedules_MouseLeave(object sender, MouseEventArgs e)
        {
            Schedules.Background = Brushes.Transparent;
        }

        private void Schedules_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid != null)
            {
                grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            grid = SchedulesGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Список графиков рабочего времени";

            UpdateTableSchedules();
        }

        private void Rules_MouseEnter(object sender, MouseEventArgs e)
        {
            Rules.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Rules_MouseLeave(object sender, MouseEventArgs e)
        {
            Rules.Background = Brushes.Transparent;
        }

        private void Rules_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid != null)
            {
                grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            grid = RulesGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Список ролей";

            UpdateTableRules();
        }

        private void Monitoring_MouseEnter(object sender, MouseEventArgs e)
        {
            Monitoring.Background = (Brush)bc.ConvertFrom("#105ef9");
        }

        private void Monitoring_MouseLeave(object sender, MouseEventArgs e)
        {
            Monitoring.Background = Brushes.Transparent;
        }


        //***** Profiles ******

        class Pers
        {
            public string Pernr { get; set; }
            public string Lname { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public string ORG { get; set; }

        }

        public void UpdatePersTable()
        {
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("SELECT PERNR.IDPERNR, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ORG_LEVEL.NAME " +
                        "FROM PERS_INFO, ORG_LEVEL, PERNR " +
                        "WHERE ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERNR.PERSID = PERS_INFO.IDPERS", con);
                using (var reader = com.ExecuteReader())
                {
                    Pers_dG.Items.Clear();
                    while (reader.Read())
                    {
                        var data = new Pers { Pernr = reader[0].ToString(), Lname = reader[1].ToString(), Name = reader[2].ToString(), Patr = reader[3].ToString(), ORG = reader[4].ToString() };
                        Pers_dG.Items.Add(data);
                    }
                }
            }
        }

        private void NewProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NewProfileButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NewProfileButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }


        //***** Profiles: My ******

        private void MyEdit_btn_Click(object sender, RoutedEventArgs e)
        {
            MyPassword_pBox.Visibility = System.Windows.Visibility.Collapsed;
            MyPassword_tBox.Visibility = System.Windows.Visibility.Visible;
            MyPassword_tBox.Text = MyPassword_pBox.Password;
            MyEdit_btn.Visibility = System.Windows.Visibility.Collapsed;
            MyOk_btn.Visibility = System.Windows.Visibility.Visible;
        }

        private void MyOk_btn_Click(object sender, RoutedEventArgs e)
        {
            MyPassword_pBox.Visibility = System.Windows.Visibility.Visible;
            MyPassword_tBox.Visibility = System.Windows.Visibility.Collapsed;
            MyPassword_pBox.Password = MyPassword_tBox.Text;
            MyEdit_btn.Visibility = System.Windows.Visibility.Visible;
            MyOk_btn.Visibility = System.Windows.Visibility.Collapsed;

            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("update AUTHENTICATION set PASSWORD = '" + MyPassword_pBox.Password + "'where PERNR = '" + MyPernr_tBox.Text + "'", con);
                com.ExecuteNonQuery();
            }
        }


        //***** Profiles: New ******

        private void NewProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewProfileGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание профиля";

            GetSetting();
            String[] mas = GetBDType();

            connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("Select Name from ORG_LEVEL", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ORGComboBox.Items.Add(reader[0].ToString());
                    }
                }

                com = new OracleCommand("Select IDPGRV from PGRV", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SchedulesComboBox.Items.Add(reader[0].ToString());
                    }
                }

                com = new OracleCommand("Select CODE from Rules", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RulesComboBox.Items.Add(reader[0].ToString());
                    }
                }

                com = new OracleCommand("Select Name from POSITION", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PositionComboBox.Items.Add(reader[0].ToString());
                    }
                }
            }
        }

        private void GeneratePasswordButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < 16)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            PasswordNewProfile_tBox.Text = pass;
        }

        private void SaveProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveProfileButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveProfileButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void CencelProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelProfileButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelProfileButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void SaveProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult other = MessageBox.Show("Вы действительно хотите сохранить внесенные изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (other)
            {
                case MessageBoxResult.Yes:
                    {
                        GetSetting();
                        String[] mas = GetBDType();

                        int ID = 0;
                        String dateF = DateTime.Now.ToString("dd.MM.yyyy");
                        String dateB = "31.12.9999";
                        String lname = LNameNewProfile_tBox.Text;
                        String name = NameNewProfile_tBox.Text;
                        String patr = PatrNewProfile_tBox.Text;
                        String birth = BirthNewProfile_tBox.Text;
                        int posid = 0;
                        int orgid = 0;
                        String pgrv = SchedulesComboBox.Text;
                        String rule = RulesComboBox.Text;
                        String photo;


                        connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                        using (OracleConnection con = new OracleConnection(connect))
                        {
                            con.Open();
                            OracleCommand com = new OracleCommand("Select IDPOS from POSITION where NAME = '" + PositionComboBox.Text + "'", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    posid = Convert.ToInt32(reader[0].ToString());
                                }
                            }

                            com = new OracleCommand("Select IDORG from ORG_LEVEL where NAME = '" + ORGComboBox.Text + "'", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    orgid = Convert.ToInt32(reader[0].ToString());
                                }
                            }
                            com = new OracleCommand("Select MAX(IDREPORTS) from REPORTS", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ID = Convert.ToInt32(reader[0].ToString()) + 1;
                                }
                            }

                            com = new OracleCommand("Insert into PERS_INFO values('" +
                            ID + "',  to_date('" + dateF + "', 'dd.mm.yyyy'), to_date('" + dateB + "', 'dd.mm.yyyy'), '" + lname + "', '" +
                            name + "', '" + patr + "',  to_date('" + birth + "', 'dd.mm.yyyy'), '" + posid + "', '" + orgid + "', '" + pgrv + "', '" + rule + "', '')", con);
                            com.ExecuteNonQuery();
                        }

                        grid.Visibility = System.Windows.Visibility.Collapsed;
                        grid = PersGrid;
                        grid.Visibility = System.Windows.Visibility.Visible;
                        Title.Text = "Список сотрудников";
                        break;
                    }
                case MessageBoxResult.No: { break; }
            }
        }


        //***** Inquirys ******

        public void UpdateTableInquiry()
        {
            Inquiry_dG.Items.Clear();

            using (OracleConnection con = new OracleConnection(connect))
            {
                //вывод "Все отсутствия в виде запросов"
                con.Open();
                OracleCommand com = new OracleCommand("SELECT IDINQ, DATECREATE, DESCRIPT, STATUS " +
                    "FROM INQ_ABS order by IDINQ ", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Inquiry { Num = reader[0].ToString(), Date = Convert.ToDateTime(reader[1].ToString()).ToString("dd.MM.yyyy"), Descript = reader[2].ToString(), Status = reader[3].ToString() };
                        Inquiry_dG.Items.Add(data);
                    }
                }
            }

        }

        private void NewInqButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NewInqButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewInqButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NewInqButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void NewInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewInqGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание запроса на отсутствие";
        }


        //***** Inquirys: New ******


        private void SaveInqButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveInqButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveInqButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveInqButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void CencelInqButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelInqButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelInqButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelInqButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }


        //***** Inquirys: Preview ******

        private void OKEditInqButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OKEditInqButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void OKEditInqButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OKEditInqButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void NotEditInqButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NotEditInqButton.Background = (Brush)bc.ConvertFrom("#fe717b");
        }

        private void NotEditInqButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NotEditInqButton.Background = (Brush)bc.ConvertFrom("#ff2c3b");
        }

        private void CencelEditInqButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelEditInqButton1.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelEditInqButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelEditInqButton1.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void EditInqButton_MouseEnter(object sender, MouseEventArgs e)
        {
            EditInqButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void EditInqButton_MouseLeave(object sender, MouseEventArgs e)
        {
            EditInqButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void MorePernrReportsGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            MorePernrReportsImage.Width = 30;
            MorePernrReportsImage.Height = 30;
        }

        private void MorePernrReportsGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            MorePernrReportsImage.Width = 25;
            MorePernrReportsImage.Height = 25;
        }

        private void SaveInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult other = MessageBox.Show("Вы действительно хотите сохранить внесенные изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (other)
            {
                case MessageBoxResult.Yes:
                    {
                        String pernr = PernrInq_tBox.Text;
                        String dateC = DateTime.Now.ToString("dd.MM.yyyy");
                        String dateF = DateFromInq_tBox.Text;
                        String dateB = DateByInq_tBox.Text;
                        String view = ViewInq_tBox.Text;
                        String descript = DescriptInq_tBox.Text;
                        String document = DocumentInq_tBox.Text;
                        int ID = 0;

                        using (OracleConnection con = new OracleConnection(connect))
                        {
                            //вывод "Все отсутствия в виде запросов"
                            con.Open();
                            OracleCommand com = new OracleCommand("Select MAX(IDINQ) from INQ_ABS", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ID = Convert.ToInt32(reader[0].ToString()) + 1;
                                }
                            }
                            com = new OracleCommand(" Select VIEW_ABS.IDVIEW from VIEW_ABS WHERE VIEW_ABS.NAME = '" + view + "'", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    view = reader[0].ToString();
                                }
                            }


                            com = new OracleCommand("Insert into INQ_ABS values('" +
                                ID + "', to_date('" + dateC + "' , 'dd.mm.yyyy'), '" + Convert.ToInt32(view) + "', '" +
                                descript + "', to_date('" + dateF + "' , 'dd.mm.yyyy'),  to_date('" + dateB + "' , 'dd.mm.yyyy'),  '" +
                                document + "', 'На утверждении', '" + Convert.ToInt32(pernr) + "')", con);
                            com.ExecuteNonQuery();
                        }


                        grid.Visibility = System.Windows.Visibility.Collapsed;
                        grid = InquiryGrid;
                        grid.Visibility = System.Windows.Visibility.Visible;
                        Title.Text = "Запросы на отсутствия";
                        UpdateTableInquiry();
                        break;
                    }
                case MessageBoxResult.No: { break; }
            }

        }

        private void CencelInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = InquiryGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Запросы на отсутствия";
        }

        private void CencelProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = PersGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Список сотрудников";
        }

        private void OKEditInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = PreviewInqGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Просмотр завпроса NameInq";
        }

        private void NotEditInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = PreviewInqGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Просмотр завпроса NameInq";
        }

        private void CencelEditInqButton1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = PreviewInqGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Просмотр завпроса NameInq";
        }

        private void EditInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = EditInqGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Изменение запроса на отсутствие";
        }


        //***** Reports: ALL ******

        private void NewReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            EditInqButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            EditInqButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void NewReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewReportsGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание нового отчёта";
        }

        //***** Reports: Preview ******



        //***** Reports: New ******

        private void GenerateReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            GenerateReportsButton.Background = (Brush)bc.ConvertFrom("#00dc77");
        }

        private void GenerateReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            GenerateReportsButton.Background = (Brush)bc.ConvertFrom("#01a459");
        }

        private void MorePernrReportsGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MorePernrForm mpf = new MorePernrForm();
            mpf.ShowDialog();
        }

        private void GenerateReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String[] pernr = PernrReports_tBox.Text.Split(',');
            String[] org = ORGLevelReports_tBox.Text.Split(',');
            String dateF = DateFromReports_tBox.Text;
            String DateB = DateByReports_tBox.Text;
            String view = ViewReports_tBox.Text;


            if (backgroundWorker.IsBusy) { return; }

            using (System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog() { Filter = "Excel FIle (*.xlsx)|*.xlsx " })
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    switch (view)
                    {

                        case "По присутствиям":
                            {
                                #region
                                TypeReports = "По присутствиям";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<TimeParisReport> l = new List<TimeParisReport>();
                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("Select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, TIME_PAIRS.DATAFROM, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY " +
                                                "from PERNR, PERS_INFO, RFID, TIME_PAIRS, ROOMS " +
                                                "where TIME_PAIRS.RFIDID = RFID.IDRFID AND PERS_INFO.IDPERS = PERNR.PERSID AND RFID.PERSID = PERS_INFO.IDPERS AND  TIME_PAIRS.ROOMID = ROOMS.IDROOMS and ROOMS.NAME <> 'Вне офиса' and IDPERNR = '" + p + "' order by TIME_PAIRS.DATAFROM", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    l.Add(new TimeParisReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                                                }
                                            }
                                            _inputTimeParisReportsReports.timeParis = l;
                                        }
                                    }
                                    else
                                    {
                                        com = new OracleCommand("Select PERNR.IDPERNR, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, TIME_PAIRS.DATAFROM, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY " +
                                                    "from PERS_INFO, RFID, TIME_PAIRS, ROOMS " +
                                                    "where TIME_PAIRS.RFIDID = RFID.IDRFID and RFID.PERSID = PERS_INFO.IDPERS and PERNR.PERSID = PERS_INFO.IDPERS AND TIME_PAIRS.ROOMID = ROOMS.IDROOMS and ROOMS.NAME <> 'Вне офиса' order by TIME_PAIRS.DATAFROM", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                l.Add(new TimeParisReport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                                            }
                                        }
                                        _inputTimeParisReportsReports.timeParis = l;
                                    }
                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);
                                break;
                                #endregion
                            }

                        case "По прогулам":
                            {
                                #region
                                TypeReports = "По прогулам";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<AbsencePromenadeReport> l = new List<AbsencePromenadeReport>();
                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DOCUMENT " +
                                                "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                "where ABSCENCE.PERNRID = PERNR.IDPERNR and PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Прогул' and ABSCENCE.PERNRID  =  '" + p + "' order by ABSCENCE.DATEFROM", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    l.Add(new AbsencePromenadeReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()));
                                                }
                                            }
                                            _inputTimeParisReportsReports.Promenade = l;
                                        }
                                    }
                                    else
                                    {
                                        com = new OracleCommand("select ABSCENCE.PERNRID,PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DOCUMENT " +
                                                    "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                    "where  ABSCENCE.PERNRID = PERNR.IDPERNR and PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Прогул' order by ABSCENCE.DATEFROM", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                l.Add(new AbsencePromenadeReport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                                            }
                                        }
                                        _inputTimeParisReportsReports.Promenade = l;
                                    }
                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                                #endregion
                            }

                        case "По отгулам":
                            {
                                #region
                                TypeReports = "По отгулам";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<AbsenceTimeOFFReport> l = new List<AbsenceTimeOFFReport>();
                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DATEBY, ABSCENCE.DOCUMENT " +
                                                "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                "where  PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Отгул' and ABSCENCE.PERNRID  =  '" + p + "' order by ABSCENCE.DATEFROM", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    TimeSpan days = (DateTime)reader[4] - (DateTime)reader[3];
                                                    l.Add(new AbsenceTimeOFFReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), days.Days + 1, reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                                                }
                                            }
                                            _inputTimeParisReportsReports.TimeOFF = l;

                                        }
                                    }
                                    else
                                    {
                                        com = new OracleCommand("select ABSCENCE.PERNRID, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DATEBY, ABSCENCE.DOCUMENT " +
                                                    "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                    "where  ABSCENCE.PERNRID = PERNR.IDPERNR and PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Отгул' order by ABSCENCE.DATEFROM", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                TimeSpan days = (DateTime)reader[5] - (DateTime)reader[4];
                                                l.Add(new AbsenceTimeOFFReport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), days.Days + 1, reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                                            }
                                        }
                                        _inputTimeParisReportsReports.TimeOFF = l;
                                    }
                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                                #endregion
                            }

                        case "По больничным":
                            {
                                #region
                                TypeReports = "По больничным";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<AbsenceHospitalReport> l = new List<AbsenceHospitalReport>();

                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DATEBY, ABSCENCE.DOCUMENT " +
                                                "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                "where ABSCENCE.PERNRID = PERNR.IDPERNR and PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Больничный' and  ABSCENCE.PERNRID  =  '" + p + "' order by ABSCENCE.DATEFROM", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    TimeSpan days = (DateTime)reader[4] - (DateTime)reader[3];
                                                    l.Add(new AbsenceHospitalReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), days.Days + 1, reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                                                }
                                            }
                                            _inputTimeParisReportsReports.Hospital = l;
                                        }
                                    }
                                    else
                                    {
                                        com = new OracleCommand("select ABSCENCE.PERNRID, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DATEBY, ABSCENCE.DOCUMENT " +
                                                    "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                    "where ABSCENCE.PERNRID = PERNR.IDPERNR and PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Больничный' order by ABSCENCE.DATEFROM", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                TimeSpan days = (DateTime)reader[5] - (DateTime)reader[4];
                                                l.Add(new AbsenceHospitalReport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), days.Days + 1, reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                                            }
                                        }
                                        _inputTimeParisReportsReports.Hospital = l;
                                    }
                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                                #endregion
                            }

                        case "По командировкам":
                            {
                                #region
                                TypeReports = "По командировкам";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<AbsenceBTripReport> l = new List<AbsenceBTripReport>();
                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DATEBY, ABSCENCE.DOCUMENT " +
                                                "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                "where  PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Командировка' and ABSCENCE.PERNRID  =  '" + p + "' order by ABSCENCE.DATEFROM", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    TimeSpan days = (DateTime)reader[4] - (DateTime)reader[3];
                                                    l.Add(new AbsenceBTripReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), days.Days + 1, reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                                                }
                                            }
                                            _inputTimeParisReportsReports.BTrip = l;
                                        }
                                    }
                                    else
                                    {
                                        com = new OracleCommand("select ABSCENCE.PERNRID, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM, ABSCENCE.DATEBY, ABSCENCE.DOCUMENT " +
                                                "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                                "where  ABSCENCE.PERNRID = PERNR.IDPERNR and PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Командировка' order by ABSCENCE.DATEFROM", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                TimeSpan days = (DateTime)reader[5] - (DateTime)reader[4];
                                                l.Add(new AbsenceBTripReport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), days.Days + 1, reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));
                                            }
                                        }
                                        _inputTimeParisReportsReports.BTrip = l;
                                    }
                                }

                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                                #endregion
                            }

                        case "По сотрудникам":
                            {
                                #region
                                TypeReports = "По сотрудникам";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<PeopleReport> l = new List<PeopleReport>();
                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("select PERS_INFO.DATAFROM, PERS_INFO.DATABY, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, PERS_INFO.BIRTH, POSITION.Name, PERS_INFO.ORGID, ORG_LEVEL.NAME,  PERS_INFO.PGRVID, PGRV.DESCRIPT, PERS_INFO.RULE, RULES.DESCRIPT " +
                                                "from PERNR, PERS_INFO, POSITION, ORG_LEVEL, RULES, PGRV " +
                                                "where PERNR.PERSID = PERS_INFO.IDPERS and POSITION.IDPOS = PERS_INFO.POSID and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.RULE = RULES.CODE and PGRV.IDPGRV = PERS_INFO.PGRVID and PERNR.IDPERNR = '" + p + "'", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    l.Add(new PeopleReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString()));
                                                }
                                            }
                                            _inputTimeParisReportsReports.People = l;
                                        }
                                    }
                                    else
                                    {
                                        com = new OracleCommand("select PERNR.IDPERNR, PERS_INFO.DATAFROM, PERS_INFO.DATABY, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, PERS_INFO.BIRTH, POSITION.Name, PERS_INFO.ORGID, ORG_LEVEL.NAME,  PERS_INFO.PGRVID, PGRV.DESCRIPT, PERS_INFO.RULE, RULES.DESCRIPT " +
                                                "from PERNR, PERS_INFO, POSITION, ORG_LEVEL, RULES, PGRV " +
                                                "where PERNR.PERSID = PERS_INFO.IDPERS and POSITION.IDPOS = PERS_INFO.POSID and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.RULE = RULES.CODE and PGRV.IDPGRV = PERS_INFO.PGRVID", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                l.Add(new PeopleReport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString()));
                                            }

                                            _inputTimeParisReportsReports.People = l;
                                        }
                                    }

                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                                #endregion
                            }

                        case "По временным данным":
                            {
                                #region
                                TypeReports = "По временным данным";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<PeopleReport> l = new List<PeopleReport>();
                                    List<string> lOgrv = new List<string>();

                                    if (pernr[0] != "")
                                    {
                                        foreach (string p in pernr)
                                        {
                                            com = new OracleCommand("select OGRV1, OGRV2, OGRV3, OGRV4, OGRV5, OGRV6, OGRV7 " +
                                                "from PERS_INFO, PERNR, PGRV " +
                                                "where PERS_INFO.IDPERS = PERNR.PERSID and PGRV.IDPGRV = PERS_INFO.PGRVID and PERNR.IDPERNR = '" + p + "'", con);
                                            using (var reader = com.ExecuteReader())
                                            {
                                                int index = 0;
                                                while (reader.Read())
                                                {
                                                    while (index < 7)
                                                        lOgrv.Add(reader[index++].ToString());
                                                    //l.Add(new TimePiopleReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString()));
                                                }
                                            }

                                            List<OGRVTime> OGRVTime = new List<OGRVTime>();
                                            foreach (string o in lOgrv)
                                            {
                                                if (o != "Вых")
                                                {

                                                    com = new OracleCommand("select NORMFROM, NORMBY, TIMEFROM, TIMEBY " +
                                                        "from OGRV, BREAK " +
                                                        "where OGRV.BREAKID= BREAK.IDBREACK and OGRV.IDOGRV = '" + o + "'", con);
                                                    using (var reader = com.ExecuteReader())
                                                    {
                                                        while (reader.Read())
                                                        {
                                                            OGRVTime.Add(new OGRVTime(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    OGRVTime.Add(new OGRVTime("00:00", "00:00", "00:00", "00:00"));
                                                }
                                            }

                                            String[] dt = Convert.ToDateTime(DateFromReports_tBox.Text).ToString("MM.yyyy").Split('.');
                                            int kDays = DateTime.DaysInMonth(Convert.ToInt32(dt[1]), Convert.ToInt32(dt[0]));
                                            SortedList<int, string> MList = new SortedList<int, string>();
                                            for (int i = 1; i <= kDays; i++)
                                            {
                                                DateTime day = new DateTime(Convert.ToInt32(dt[1]), Convert.ToInt32(dt[0]), i);
                                                MList.Add(i, day.ToString("dddd", new CultureInfo("ru-RU")));
                                            }

                                            TimeSpan NormaTime = new TimeSpan();

                                            foreach (string d in MList.Values)
                                            {
                                                switch (d)
                                                {
                                                    case "понедельник":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[0].NORMBY - OGRVTime[0].NORMFROM;
                                                            P = OGRVTime[0].TIMEBY - OGRVTime[0].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }
                                                    case "вторник":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[1].NORMBY - OGRVTime[1].NORMFROM;
                                                            P = OGRVTime[1].TIMEBY - OGRVTime[1].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }
                                                    case "среда":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[2].NORMBY - OGRVTime[2].NORMFROM;
                                                            P = OGRVTime[2].TIMEBY - OGRVTime[2].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }
                                                    case "четверг":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[3].NORMBY - OGRVTime[3].NORMFROM;
                                                            P = OGRVTime[3].TIMEBY - OGRVTime[3].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }
                                                    case "пятница":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[4].NORMBY - OGRVTime[4].NORMFROM;
                                                            P = OGRVTime[4].TIMEBY - OGRVTime[4].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }

                                                    case "суббота":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[5].NORMBY - OGRVTime[5].NORMFROM;
                                                            P = OGRVTime[5].TIMEBY - OGRVTime[5].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }

                                                    case "воскресенье":
                                                        {
                                                            TimeSpan W, P;
                                                            W = OGRVTime[6].NORMBY - OGRVTime[6].NORMFROM;
                                                            P = OGRVTime[6].TIMEBY - OGRVTime[6].TIMEFROM;
                                                            NormaTime += W - P;
                                                            break;
                                                        }
                                                }
                                            }

                                            String h = NormaTime.TotalHours + ":" + NormaTime.Minutes + ":" + NormaTime.Seconds;
                                            _inputTimeParisReportsReports.People = l;
                                        }
                                    }

                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                                #endregion
                            }
                    }
                }
            }
        }








        private void SaveReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveReportsButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveReportsButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }


        public void UpdateTableReports()
        {
            Reports_dG.Items.Clear();

            using (OracleConnection con = new OracleConnection(connect))
            {
                //вывод "Все отсутствия в виде запросов"
                con.Open();
                OracleCommand com = new OracleCommand("SELECT IDREPORTS, DATACREATE, DESCRIPT " +
                    "FROM REPORTS " +
                    "where REPORTS.PERSID = '" + Pernr_tBox.Text + "' " +
                    "order by IDREPORTS DESC ", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Reports { Num = reader[0].ToString(), Date = reader[1].ToString(), Descript = reader[2].ToString() };
                        Reports_dG.Items.Add(data);
                    }
                }
            }
        }

        private void SaveReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddNameReports anr = new AddNameReports();
            anr.ShowDialog();

            GetSetting();
            String[] mas = GetBDType();

            int ID = 0;
            String persID = Pernr_tBox.Text;
            String dataC = DateTime.Now.ToString("dd.MM.yyyy");
            String descript = anr.NameRreport_tBox.Text;
            String morePernr = PernrReports_tBox.Text;
            String orgLevel = ORGLevelReports_tBox.Text;
            String dataF = DateFromReports_tBox.Text;
            String dataB = DateByReports_tBox.Text;
            String view = ViewReports_tBox.Text;


            connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("Select MAX(IDREPORTS) from REPORTS", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ID = Convert.ToInt32(reader[0].ToString()) + 1;
                    }
                }

                com = new OracleCommand("Insert into Reports values('" +
                ID + "', '" + persID + "', to_date('" + dataC + "', 'dd.mm.yyyy'), '" + descript + "', '" + morePernr + "', '" +
                orgLevel + "', to_date('" + dataF + "', 'dd.mm.yyyy'), to_date('" + dataB + "', 'dd.mm.yyyy'), '" + view + "')", con);
                com.ExecuteNonQuery();
            }
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
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = ReportsGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Сохраненные отчеты";
            UpdateTableReports();
        }

        public void UpdateTableSchedules()
        {
            Schedules_dG.Items.Clear();

            using (OracleConnection con = new OracleConnection(connect))
            {
                //вывод "Все отсутствия в виде запросов"
                con.Open();
                OracleCommand com = new OracleCommand("SELECT IDPGRV, DESCRIPT " +
                    "FROM PGRV " +
                    "order by IDPGRV", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Schedules { Code = reader[0].ToString(), Descript = reader[1].ToString() };
                        Schedules_dG.Items.Add(data);
                    }
                }
            }
        }

        public void UpdateTableRules()
        {
            Rules_dG.Items.Clear();

            using (OracleConnection con = new OracleConnection(connect))
            {
                //вывод "Все отсутствия в виде запросов"
                con.Open();
                OracleCommand com = new OracleCommand("SELECT CODE,DESCRIPT " +
                    "FROM RULES " +
                    "order by CODE", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Rules { Code = reader[0].ToString(), Descript = reader[1].ToString() };
                        Rules_dG.Items.Add(data);
                    }
                }
            }
        }




        private void SendMonitorButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SendMonitorButton.Width = 43;
            SendMonitorButton.Height = 43;
        }

        private void SendMonitorButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SendMonitorButton.Width = 38;
            SendMonitorButton.Height = 38;
        }

        private void Monitoring_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid != null)
            {
                grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            grid = MonitoringGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Мониторинг потока данных";
        }

        private void CencelSchedulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelSchedulesButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelSchedulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelSchedulesButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelOGRVButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewSchedulesGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание графика рабочего времени";
        }

        private void SaveSchedulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveSchedulesButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveSchedulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveSchedulesButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void SaveOGRVButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewSchedulesGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание графика рабочего времени";
        }

        private void NewSchedulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NewSchedulesButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewSchedulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NewSchedulesButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void NewSchedulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewSchedulesGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание графика рабочего времени";
        }

        private void Grid_MouseEnter_1(object sender, MouseEventArgs e)
        {
            MoreBreakButton.Width = 30;
            MoreBreakButton.Height = 30;
        }

        private void Grid_MouseLeave_1(object sender, MouseEventArgs e)
        {
            MoreBreakButton.Width = 25;
            MoreBreakButton.Height = 25;
        }

        private void MoreOGRVButtonFromNewPGRV_MouseEnter(object sender, MouseEventArgs e)
        {
            MoreOGRVButton.Width = 30;
            MoreOGRVButton.Height = 30;
        }

        private void MoreOGRVButtonFromNewPGRV_MouseLeave(object sender, MouseEventArgs e)
        {
            MoreOGRVButton.Width = 25;
            MoreOGRVButton.Height = 25;
        }

        private void MoreOGRVButtonFromNewPGRV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewOGRV vO = new ViewOGRV();
            vO.ShowDialog();
        }

        private void AddOGRVButtonFromNewPGRV_MouseEnter(object sender, MouseEventArgs e)
        {
            AddOGRVButton.Width = 30;
            AddOGRVButton.Height = 30;
        }

        private void AddOGRVButtonFromNewPGRV_MouseLeave(object sender, MouseEventArgs e)
        {
            AddOGRVButton.Width = 25;
            AddOGRVButton.Height = 25;
        }

        private void AddOGRVButtonFromNewPGRV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewOGRVGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание однодневного графика рабочего времени";
        }

        private void GenerateSchedulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            GenerateSchedulesButton.Background = (Brush)bc.ConvertFrom("#00dc77");
        }

        private void GenerateSchedulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            GenerateSchedulesButton.Background = (Brush)bc.ConvertFrom("#01a459");
        }

        private void GenerateSchedulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = SchedulesGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Список графиков рабочего времени";
        }

        private void CencelOGRVButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelOGRVButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelOGRVButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelOGRVButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void SaveOGRVButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveOGRVButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveOGRVButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveOGRVButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void SaveSchedulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = SchedulesGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Список графиков рабочего времени";
        }

        private void CencelSchedulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = SchedulesGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Список графиков рабочего времени";
        }

        private void MoreBreakButtonFromNewOGRV_MouseEnter(object sender, MouseEventArgs e)
        {
            MoreBreakButton.Width = 30;
            MoreBreakButton.Height = 30;
        }

        private void MoreBreakButtonFromNewOGRV_MouseLeave(object sender, MouseEventArgs e)
        {
            MoreBreakButton.Width = 25;
            MoreBreakButton.Height = 25;
        }

        private void MoreBreakButtonFromNewOGRV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewBreak vB = new ViewBreak();
            vB.ShowDialog();
        }

        private void AddBreakButtonFromNewOGRV_MouseEnter(object sender, MouseEventArgs e)
        {
            AddBreakButton.Width = 30;
            AddBreakButton.Height = 30;
        }

        private void AddBreakButtonFromNewOGRV_MouseLeave(object sender, MouseEventArgs e)
        {
            AddBreakButton.Width = 25;
            AddBreakButton.Height = 25;
        }

        private void AddBreakButtonFromNewOGRV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewBreakGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Создание перерыва";

        }

        private void SaveBreakButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveBreakButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveBreakButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveBreakButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void SaveBreakButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewOGRVGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание однодневного графика рабочего времени";
        }

        private void CencelBreakButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelBreakButton.Background = (Brush)bc.ConvertFrom("#cbcaca");

        }

        private void CencelBreakButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelBreakButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelBreakButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewOGRVGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание однодневного графика рабочего времени";
        }

        private void MoreOGRVRuleGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoreORGForm mof = new MoreORGForm();
            mof.ShowDialog();
        }

        private void NewRulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveBreakButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewRulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveOGRVButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void NewRulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewRulesGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание роли";
        }

        private void SaveRulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveRulesButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveRulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveRulesButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void SaveRulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = RulesGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Список ролей";
        }

        private void CencelRulesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CencelRulesButton.Background = (Brush)bc.ConvertFrom("#cbcaca");
        }

        private void CencelRulesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CencelRulesButton.Background = (Brush)bc.ConvertFrom("#8d8d8d");
        }

        private void CencelRulesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = RulesGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Список ролей";
        }

        private void myProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            myProfileButton.Background = (Brush)bc.ConvertFrom("#004ee8");
        }

        private void myProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            myProfileButton.Background = (Brush)bc.ConvertFrom("Transparent");
        }

        private void myProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grid != null)
            {
                grid.Visibility = System.Windows.Visibility.Collapsed;
            }
            grid = ProfileGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Мой профиль";
        }




        private void OpenDocument_btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Сканированный документ в формате PNG|*.png|Сканированный документ в формате JPEG|*.jpeg|Сканированный документ в формате PDF|*.pdf|Текстовый документ в формате DOC|*.doc";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DocumentInq_tBox.Text = openFileDialog.FileName.ToString();
                }
            }
        }


        //***** BackGround Worker ***** 

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        //Описываемм что делать в фоне
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (TypeReports)
            {
                case "По присутствиям":
                    {
                        #region
                        List<TimeParisReport> timeParis = ((sReports)e.Argument).timeParis;
                        String FileName = ((sReports)e.Argument).FileName;
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 1;
                        int process = timeParis.Count;

                        ws.Cells[1, 1] = "Табельный номер";
                        ws.Cells[1, 2] = "Фамилия";
                        ws.Cells[1, 3] = "Имя";
                        ws.Cells[1, 4] = "Отчество";
                        ws.Cells[1, 5] = "Дата";
                        ws.Cells[1, 6] = "Время с";
                        ws.Cells[1, 7] = "Время по";

                        foreach (TimeParisReport s in timeParis)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.Pernr;
                                ws.Cells[index, 2] = s.LName;
                                ws.Cells[index, 3] = s.Name;
                                ws.Cells[index, 4] = s.Patr;
                                ws.Cells[index, 5] = s.Date;
                                ws.Cells[index, 6] = s.TimeF;
                                ws.Cells[index, 7] = s.TimeB;
                            }
                        }

                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
                        #endregion
                    }
                case "По прогулам":
                    {
                        #region
                        List<AbsencePromenadeReport> absence = ((sReports)e.Argument).Promenade;
                        String FileName = ((sReports)e.Argument).FileName;
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 1;
                        int process = absence.Count;

                        ws.Cells[1, 1] = "Табельный номер";
                        ws.Cells[1, 2] = "Фамилия";
                        ws.Cells[1, 3] = "Имя";
                        ws.Cells[1, 4] = "Отчество";
                        ws.Cells[1, 5] = "Дата";
                        ws.Cells[1, 6] = "Документ";

                        foreach (AbsencePromenadeReport s in absence)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.Pernr;
                                ws.Cells[index, 2] = s.LName;
                                ws.Cells[index, 3] = s.Name;
                                ws.Cells[index, 4] = s.Patr;
                                ws.Cells[index, 5] = s.DateF;
                                ws.Cells[index, 6] = s.Doc;
                            }
                        }

                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
                        #endregion
                    }

                case "По отгулам":
                    {
                        #region
                        List<AbsenceTimeOFFReport> absence = ((sReports)e.Argument).TimeOFF;
                        String FileName = ((sReports)e.Argument).FileName;
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 1;
                        int process = absence.Count;

                        ws.Cells[1, 1] = "Табельный номер";
                        ws.Cells[1, 2] = "Фамилия";
                        ws.Cells[1, 3] = "Имя";
                        ws.Cells[1, 4] = "Отчество";
                        ws.Cells[1, 5] = "Количество дней";
                        ws.Cells[1, 6] = "Дата с";
                        ws.Cells[1, 7] = "Дата по";
                        ws.Cells[1, 8] = "Документ";

                        foreach (AbsenceTimeOFFReport s in absence)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.Pernr;
                                ws.Cells[index, 2] = s.LName;
                                ws.Cells[index, 3] = s.Name;
                                ws.Cells[index, 4] = s.Patr;
                                ws.Cells[index, 5] = s.qDays;
                                ws.Cells[index, 6] = s.DateF;
                                ws.Cells[index, 7] = s.DateB;
                                ws.Cells[index, 8] = s.Doc;
                            }
                        }

                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
                        #endregion
                    }

                case "По больничным":
                    {
                        #region
                        List<AbsenceHospitalReport> absence = ((sReports)e.Argument).Hospital;
                        String FileName = ((sReports)e.Argument).FileName;
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 1;
                        int process = absence.Count;

                        ws.Cells[1, 1] = "Табельный номер";
                        ws.Cells[1, 2] = "Фамилия";
                        ws.Cells[1, 3] = "Имя";
                        ws.Cells[1, 4] = "Отчество";
                        ws.Cells[1, 5] = "Количество дней";
                        ws.Cells[1, 6] = "Дата с";
                        ws.Cells[1, 7] = "Дата по";
                        ws.Cells[1, 8] = "Документ";

                        foreach (AbsenceHospitalReport s in absence)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.Pernr;
                                ws.Cells[index, 2] = s.LName;
                                ws.Cells[index, 3] = s.Name;
                                ws.Cells[index, 4] = s.Patr;
                                ws.Cells[index, 5] = s.qDays;
                                ws.Cells[index, 6] = s.DateF;
                                ws.Cells[index, 7] = s.DateB;
                                ws.Cells[index, 8] = s.Doc;
                            }

                        }


                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
                        #endregion
                    }

                case "По командировкам":
                    {
                        #region
                        List<AbsenceBTripReport> absence = ((sReports)e.Argument).BTrip;
                        String FileName = ((sReports)e.Argument).FileName;
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 1;
                        int process = absence.Count;

                        ws.Cells[1, 1] = "Табельный номер";
                        ws.Cells[1, 2] = "Фамилия";
                        ws.Cells[1, 3] = "Имя";
                        ws.Cells[1, 4] = "Отчество";
                        ws.Cells[1, 5] = "Количество дней";
                        ws.Cells[1, 6] = "Дата с";
                        ws.Cells[1, 7] = "Дата по";
                        ws.Cells[1, 8] = "Документ";

                        foreach (AbsenceBTripReport s in absence)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.Pernr;
                                ws.Cells[index, 2] = s.LName;
                                ws.Cells[index, 3] = s.Name;
                                ws.Cells[index, 4] = s.Patr;
                                ws.Cells[index, 5] = s.qDays;
                                ws.Cells[index, 6] = s.DateF;
                                ws.Cells[index, 7] = s.DateB;
                                ws.Cells[index, 8] = s.Doc;
                            }
                        }
                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
                        #endregion
                    }

                case "По сотрудникам":
                    {
                        #region
                        List<PeopleReport> absence = ((sReports)e.Argument).People;
                        String FileName = ((sReports)e.Argument).FileName;
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 1;
                        int process = absence.Count;

                        ws.Cells[1, 1] = "Дата принятия";
                        ws.Cells[1, 2] = "Дата увольнения";
                        ws.Cells[1, 3] = "Табельный номер";
                        ws.Cells[1, 4] = "Фамилия";
                        ws.Cells[1, 5] = "Имя";
                        ws.Cells[1, 6] = "Отчество";
                        ws.Cells[1, 7] = "Дата рождения";
                        ws.Cells[1, 8] = "Должность";
                        ws.Cells[1, 9] = "Орг.единица";
                        ws.Cells[1, 10] = "Наименование";
                        ws.Cells[1, 11] = "ГРВ";
                        ws.Cells[1, 12] = "Описание";
                        ws.Cells[1, 13] = "Роль";
                        ws.Cells[1, 14] = "Описание";


                        foreach (PeopleReport s in absence)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.DateF;
                                ws.Cells[index, 2] = s.DateB;
                                ws.Cells[index, 3] = s.Pernr;
                                ws.Cells[index, 4] = s.LName;
                                ws.Cells[index, 5] = s.Name;
                                ws.Cells[index, 6] = s.Patr;
                                ws.Cells[index, 7] = s.Bith;
                                ws.Cells[index, 8] = s.Position;
                                ws.Cells[index, 9] = s.cORG;
                                ws.Cells[index, 10] = s.nORG;
                                ws.Cells[index, 11] = s.cPGRV;
                                ws.Cells[index, 12] = s.nPGRV;
                                ws.Cells[index, 13] = s.cRule;
                                ws.Cells[index, 14] = s.nRule;
                            }
                        }
                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
                        #endregion
                    }
            }

        }

        //обработка кона выполненеия фоновой операции
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(100);
                MessageBox.Show("Данные успешно экспортированы в файл", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Thread.Sleep(100);
                MessageBox.Show(e.Error.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        class TimeParisReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public string Date { get; set; }
            public string TimeF { get; set; }
            public string TimeB { get; set; }

            public TimeParisReport(string Pernr, string LName, string Name, string Patr, string Date, string TimeF, string TimeB)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.Date = Date;
                this.TimeF = TimeF;
                this.TimeB = TimeB;
            }
        }

        class AbsencePromenadeReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public string DateF { get; set; }
            public string Doc { get; set; }

            public AbsencePromenadeReport(string Pernr, string LName, string Name, string Patr, string DateF, string Doc)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.DateF = DateF;
                this.Doc = Doc;
            }
        }

        class AbsenceTimeOFFReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public int qDays { get; set; }
            public string DateF { get; set; }
            public string DateB { get; set; }
            public string Doc { get; set; }

            public AbsenceTimeOFFReport(string Pernr, string LName, string Name, string Patr, int qDays, string DateF, string DateB, string Doc)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.qDays = qDays;
                this.DateF = DateF;
                this.DateB = DateB;
                this.Doc = Doc;
            }
        }

        class AbsenceHospitalReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public int qDays { get; set; }
            public string DateF { get; set; }
            public string DateB { get; set; }
            public string Doc { get; set; }

            public AbsenceHospitalReport(string Pernr, string LName, string Name, string Patr, int qDays, string DateF, string DateB, string Doc)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.qDays = qDays;
                this.DateF = DateF;
                this.DateB = DateB;
                this.Doc = Doc;
            }
        }

        class AbsenceBTripReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public int qDays { get; set; }
            public string DateF { get; set; }
            public string DateB { get; set; }
            public string Doc { get; set; }

            public AbsenceBTripReport(string Pernr, string LName, string Name, string Patr, int qDays, string DateF, string DateB, string Doc)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.qDays = qDays;
                this.DateF = DateF;
                this.DateB = DateB;
                this.Doc = Doc;
            }
        }

        class PeopleReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public string Bith { get; set; }
            public string DateF { get; set; }
            public string DateB { get; set; }
            public string Position { get; set; }
            public string cORG { get; set; }
            public string nORG { get; set; }
            public string cPGRV { get; set; }
            public string nPGRV { get; set; }
            public string cRule { get; set; }
            public string nRule { get; set; }

            public PeopleReport(string Pernr, string DateF, string DateB, string LName, string Name, string Patr, string Bith, string Position, string cORG, string nORG, string cPGRV, string nPGRV, string cRule, string nRule)
            {
                this.DateF = DateF;
                this.DateB = DateB;
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.Bith = Bith;
                this.Position = Position;
                this.cORG = cORG;
                this.nORG = nORG;
                this.cPGRV = cPGRV;
                this.nPGRV = nPGRV;
                this.cRule = cRule;
                this.nRule = nRule;
            }
        }

        class TimePiopleReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public string PlanTime { get; set; }
            public string FactTime { get; set; }
            public string WSTimeValue { get; set; }
            public string WSTime { get; set; }
            public string TardValue { get; set; }
            public string TardTime { get; set; }
            public string RecValue { get; set; }
            public string RecTime { get; set; }
            public string BTripValue { get; set; }
            public string HospValue { get; set; }
            public string HolyValue { get; set; }
            public string AbsentValue { get; set; }
            public string TimeOFFValue { get; set; }

            public TimePiopleReport(string Pernr, string LName, string Name, string Patr, string PlanTime, string FactTime, string WSTimeValue, string WSTime, string TardValue,
                string TardTime, string RecValue, string RecTime, string BTripValue, string HospValue, string HolyValue, string AbsentValue, string TimeOFFValue)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.PlanTime = PlanTime;
                this.FactTime = FactTime;
                this.WSTimeValue = WSTimeValue;
                this.WSTime = WSTime;
                this.TardValue = TardValue;
                this.TardTime = TardTime;
                this.RecValue = RecValue;
                this.RecTime = RecTime;
                this.BTripValue = BTripValue;
                this.HospValue = HospValue;
                this.HolyValue = HolyValue;
                this.AbsentValue = AbsentValue;
                this.TimeOFFValue = TimeOFFValue;
            }
        }

        class OGRVTime
        {
            public DateTime TYPE { get; set; }
            public DateTime NORMFROM { get; set; }
            public DateTime NORMBY { get; set; }
            public DateTime TIMEFROM { get; set; }
            public DateTime TIMEBY { get; set; }


            public OGRVTime(string NORMFROM, string NORMBY, string TIMEFROM, string TIMEBY)
            {
                this.NORMFROM = Convert.ToDateTime(NORMFROM);
                this.NORMBY = Convert.ToDateTime(NORMBY);
                this.TIMEFROM = Convert.ToDateTime(TIMEFROM);
                this.TIMEBY = Convert.ToDateTime(TIMEBY);
            }
        }

        struct sReports
        {
            public List<TimeParisReport> timeParis;
            public List<AbsencePromenadeReport> Promenade;
            public List<AbsenceTimeOFFReport> TimeOFF;
            public List<AbsenceHospitalReport> Hospital;
            public List<AbsenceBTripReport> BTrip;
            public List<PeopleReport> People;
            public List<TimePiopleReport> TimePiople;
            public string FileName { get; set; }
        }

        sReports _inputTimeParisReportsReports;



        private void Pers_dG_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int parse = Pers_dG.SelectedIndex;
            Pers rowView = Pers_dG.SelectedItem as Pers;

            grid.Visibility = Visibility.Hidden;
            grid = ProfileGrid;
            grid.Visibility = Visibility.Visible;
            Title.Text = "Профиль сотрудника";


            using (OracleConnection con = new OracleConnection(connect))
            {
                //вывод "Основная инфа по профилю"
                con.Open();
                OracleCommand com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, PERS_INFO.BIRTH, POSITION.Name, ORG_LEVEL.NAME, PERS_INFO.PGRVID, AUTHENTICATION.LOGIN, AUTHENTICATION.PASSWORD, RFID.IDRFID " +
                    "FROM PERNR, PERS_INFO , POSITION, AUTHENTICATION, ORG_LEVEL, RFID " +
                    "WHERE PERS_INFO.IDPERS = PERNR.PERSID and POSITION.IDPOS = PERS_INFO.POSID and PERNR.IDPERNR = AUTHENTICATION.PERNR and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.IDPERS = RFID.PERSID  and PERNR.IDPERNR = '" + rowView.Pernr + "'", con);

                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pernr_tBox.Text = rowView.Pernr;
                        FIO_tBox.Text = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString();
                        Bith_tBox.Text = Convert.ToDateTime(reader[3]).ToString("dd.MM.yyyy");
                        Position_tBox.Text = reader[4].ToString();
                        ORG_tBox.Text = reader[5].ToString();
                        PGRV_tBox.Text = reader[6].ToString();
                        Login_tBox.Text = reader[7].ToString();
                        Password_pBox.Password = reader[8].ToString();
                        RFID_tBox.Text = reader[9].ToString();
                    }

                }

                //вывод местоположения
                com = new OracleCommand("SELECT ROOMS.NAME " +
                   "FROM TIME_PAIRS, ROOMS " +
                   "WHERE ROOMS.IDROOMS = TIME_PAIRS.ROOMID and TIME_PAIRS.TIMEBY is null and TIME_PAIRS.RFIDID= '" + rowView.Pernr + "'", con);

                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Location_tBox.Text = reader[0].ToString();
                    }

                }

                //вывод "Контакты"
                com = new OracleCommand("SELECT DESCRIPT, VALUE " +
                        "FROM CONTACTS, PERS_INFO, PERNR " +
                        "WHERE  CONTACTS.PERSID = PERS_INFO.IDPERS and PERNR.PERSID = PERS_INFO.IDPERS and PERNR.IDPERNR = '" + rowView.Pernr + "'", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Contacts { Descript = reader[0].ToString(), Value = reader[1].ToString() };
                        Contacts_dG.Items.Add(data);
                    }
                }

                //вывод "Отсутствия"
                com = new OracleCommand("SELECT VIEW_ABS.NAME, DATEFROM, DATEBY, DOCUMENT " +
                        "FROM ABSCENCE, VIEW_ABS " +
                        "WHERE VIEW_ABS.IDVIEW = ABSCENCE.VIEWID and ABSCENCE.PERNRID = '" + rowView.Pernr + "'", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Absence { View = reader[0].ToString(), DateFrom = reader[1].ToString(), DateBy = reader[2].ToString(), Document = reader[3].ToString() };
                        MyAbsence_dG.Items.Add(data);
                    }
                }

                //вывод "Присутствия"
                com = new OracleCommand("SELECT TIME_PAIRS.DATAFROM, TIME_PAIRS.DATABY, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY, ROOMS.NAME " +
                        "FROM PERNR, RFID, TIME_PAIRS, ROOMS " +
                        "WHERE RFID.IDRFID = TIME_PAIRS.RFIDID and PERNR.PERSID = RFID.PERSID and TIME_PAIRS.ROOMID = ROOMS.IDROOMS and PERNR.IDPERNR = '" + rowView.Pernr + "'", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new TimePairs { DateFrom = reader[0].ToString(), DateBy = reader[1].ToString(), TimeFrom = reader[2].ToString(), TimeBy = reader[3].ToString(), Location = reader[4].ToString() };
                        MyTimePairs_dG.Items.Add(data);
                    }
                }
            }
        }

        private void GeneratePassEditProfile_btn_Click(object sender, RoutedEventArgs e)
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < 16)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            Password_pBox.Password = pass;
        }
    }
}