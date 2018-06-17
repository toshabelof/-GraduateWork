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



namespace HRSaveTimeClient
{
    /// <summary>
    /// Логика взаимодействия для GlobalForm.xaml
    /// </summary>
    public partial class GlobalForm : System.Windows.Window
    {
        private BackgroundWorker backgroundWorker;
        public String Login = "";

        BrushConverter bc = new BrushConverter();
        Grid grid = new Grid();
        String connect;
        public static SortedList<string, string> setting = new SortedList<string, string>();
        List<string> list = new List<string>();

        String TypeReports = null;

        public GlobalForm()
        {
            InitializeComponent();
            backgroundWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
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

        private void NewProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NewProfileButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NewProfileButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }


        //***** Profiles: New ******

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
                        var data = new Inquiry { Num = reader[0].ToString(), Date = reader[1].ToString(), Descript = reader[2].ToString(), Status = reader[3].ToString() };
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

        private void SaveProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult other = MessageBox.Show("Вы действительно хотите сохранить внесенные изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (other)
            {
                case MessageBoxResult.Yes:
                    {
                        GetSetting();
                        String[] mas = GetBDType();
                        //IDPERS	DATAFROM	DATABY	LNAME	NAME	PATR	BIRTH	POSID	ORGID	PGRVID	RULE	PHOTO

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

        private void MorePernrReportsGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MorePernrForm mpf = new MorePernrForm();
            mpf.ShowDialog();
        }

        private void GenerateReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            GenerateReportsButton.Background = (Brush)bc.ConvertFrom("#00dc77");
        }

        private void GenerateReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            GenerateReportsButton.Background = (Brush)bc.ConvertFrom("#01a459");
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

        class AbsenceReport
        {
            public string Pernr { get; set; }
            public string LName { get; set; }
            public string Name { get; set; }
            public string Patr { get; set; }
            public string DateF { get; set; }

            public AbsenceReport(string Pernr, string LName, string Name, string Patr, string DateF)
            {
                this.Pernr = Pernr;
                this.LName = LName;
                this.Name = Name;
                this.Patr = Patr;
                this.DateF = DateF;
            }
        }


        struct timeParisReports
        {
            public List<TimeParisReport> timeParis;
            public List<AbsenceReport> Abscence;
            public string FileName { get; set; }
        }
        timeParisReports _inputTimeParisReportsReports;



        private void GenerateReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String[] pernr = PernrReports_tBox.Text.Split(',');
            String[] org = ORGLevelReports_tBox.Text.Split(',');
            String dateF = DateFromReports_tBox.Text;
            String DateB = DateByReports_tBox.Text;
            String view = ViewReports_tBox.Text;


            if (backgroundWorker.IsBusy)
            { return; }

            using (System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog() { Filter = "Excel FIle (*.xls)|*.xls " })
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    switch (view)
                    {
                        case "По присутствиям":
                            {
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
                                    foreach (string p in pernr)
                                    {
                                        com = new OracleCommand("Select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, TIME_PAIRS.DATAFROM, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY " +
                                            "from PERNR, PERS_INFO, RFID, TIME_PAIRS, ROOMS " +
                                            "where PERS_INFO.IDPERS = PERNR.PERSID AND RFID.PERSID = PERS_INFO.IDPERS AND TIME_PAIRS.RFIDID = RFID.IDRFID AND TIME_PAIRS.ROOMID = ROOMS.IDROOMS and ROOMS.NAME <> 'Вне офиса' and IDPERNR = '" + p + "' order by TIME_PAIRS.DATAFROM", con);
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
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);
                                break;
                            }

                        case "По прогулам":
                            {
                                TypeReports = "По прогулам";
                                _inputTimeParisReportsReports.FileName = sfd.FileName;

                                GetSetting();
                                String[] mas = GetBDType();
                                connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                                using (OracleConnection con = new OracleConnection(connect))
                                {
                                    con.Open();
                                    OracleCommand com;
                                    List<AbsenceReport> l = new List<AbsenceReport>();
                                    foreach (string p in pernr)
                                    {
                                        com = new OracleCommand("select PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR, ABSCENCE.DATEFROM " +
                                            "from PERNR, PERS_INFO, ABSCENCE, VIEW_ABS " +
                                            "where  PERNR.PERSID = PERS_INFO.IDPERS and ABSCENCE.VIEWID = VIEW_ABS.IDVIEW and VIEW_ABS.NAME = 'Прогул' and PERNR.IDPERNR  =  '" + p + "' order by ABSCENCE.DATEFROM", con);
                                        using (var reader = com.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                l.Add(new AbsenceReport(p, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                                            }
                                        }
                                        _inputTimeParisReportsReports.Abscence = l;

                                    }
                                }
                                backgroundWorker.RunWorkerAsync(_inputTimeParisReportsReports);

                                break;
                            }
                    }



                }
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(100);
                MessageBox.Show("Данные экспортированы в файл", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (TypeReports)
            {
                case "По присутствиям":
                    {
                        List<TimeParisReport> timeParis = ((timeParisReports)e.Argument).timeParis;
                        String FileName = ((timeParisReports)e.Argument).FileName;
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
                    }
                case "По прогулам":
                    {
                        List<AbsenceReport> absence = ((timeParisReports)e.Argument).Abscence;
                        String FileName = ((timeParisReports)e.Argument).FileName;
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
                        ws.Cells[1, 5] = "Дата с";

                        foreach (AbsenceReport s in absence)
                        {
                            if (!backgroundWorker.CancellationPending)
                            {
                                backgroundWorker.ReportProgress(index++ * 100 / process);
                                ws.Cells[index, 1] = s.Pernr;
                                ws.Cells[index, 2] = s.LName;
                                ws.Cells[index, 3] = s.Name;
                                ws.Cells[index, 4] = s.Patr;
                                ws.Cells[index, 5] = s.DateF;
                            }
                        }

                        ws.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        excel.Quit();
                        break;
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

        private void StrictChekBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StrictChekBox.Text == "")
            {
                StrictChekBox.Text = "X";
            }
            else { StrictChekBox.Text = ""; }
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

        private void MoreOGRVRuleGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            MoreOGRVRuleButton.Width = 30;
            MoreOGRVRuleButton.Height = 30;
        }

        private void MoreOGRVRuleGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            MoreOGRVRuleButton.Width = 25;
            MoreOGRVRuleButton.Height = 25;
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


        //**************************


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
            GetSetting();
            String[] mas = GetBDType();

            grid = ProfileGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Мой профиль";

            connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
            using (OracleConnection con = new OracleConnection(connect))
            {
                //вывод "Основная инфа по профилю"
                con.Open();
                OracleCommand com = new OracleCommand("SELECT IDPERNR, LNAME, PERS_INFO.NAME, PATR, BIRTH, POSITION.Name, ORG_LEVEL.NAME, PGRVID, RULE, LOGIN, PASSWORD, RFID.IDRFID, ROOMS.NAME " +
                    "FROM PERNR, PERS_INFO , POSITION, AUTHENTICATION, ORG_LEVEL, RFID, TIME_PAIRS, ROOMS " +
                    "WHERE PERS_INFO.IDPERS = PERNR.PERSID and POSITION.IDPOS = PERS_INFO.POSID and PERNR.IDPERNR= AUTHENTICATION.PERNR and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.IDPERS = RFID.PERSID and TIME_PAIRS.RFIDID = RFID.IDRFID and TIME_PAIRS.DATABY is Null  and TIME_PAIRS.ROOMID = ROOMS.IDROOMS and PERNR.IDPERNR =  '" + Pernr_tBox.Text + "'", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pernr_tBox.Text = reader[0].ToString();
                        FIO_tBox.Text = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                        Position_tBox.Text = reader[5].ToString();
                        ORG_tBox.Text = reader[6].ToString();
                        Login_tBox.Text = reader[9].ToString();
                        Password_tBox.Text = reader[10].ToString();
                        RFID_tBox.Text = reader[11].ToString();
                        Location_tBox.Text = reader[12].ToString();
                    }

                }

                //вывод "Контакты"
                com = new OracleCommand("SELECT DESCRIPT, VALUE " +
                        "FROM CONTACTS, PERS_INFO, PERNR " +
                        "WHERE  CONTACTS.PERSID = PERS_INFO.IDPERS and PERNR.PERSID = PERS_INFO.IDPERS and PERNR.IDPERNR = '" + Pernr_tBox.Text + "'", con);
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
                        "WHERE VIEW_ABS.IDVIEW = ABSCENCE.VIEWID and ABSCENCE.PERNRID = '" + Pernr_tBox.Text + "'", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Absence { View = reader[0].ToString(), DateFrom = reader[1].ToString(), DateBy = reader[2].ToString(), Document = reader[3].ToString() };
                        Absence_dG.Items.Add(data);
                    }
                }

                //вывод "Присутствия"
                com = new OracleCommand("SELECT TIME_PAIRS.DATAFROM, TIME_PAIRS.DATABY, TIME_PAIRS.TIMEFROM, TIME_PAIRS.TIMEBY, ROOMS.NAME " +
                        "FROM PERNR, RFID, TIME_PAIRS, ROOMS " +
                        "WHERE RFID.IDRFID = TIME_PAIRS.RFIDID and PERNR.PERSID = RFID.PERSID and TIME_PAIRS.ROOMID = ROOMS.IDROOMS and PERNR.IDPERNR = '" + Pernr_tBox.Text + "'", con);
                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new TimePairs { DateFrom = reader[0].ToString(), DateBy = reader[1].ToString(), TimeFrom = reader[2].ToString(), TimeBy = reader[3].ToString(), Location = reader[4].ToString() };
                        TimePairs_dG.Items.Add(data);
                    }
                }
            }
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

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }






        //***** BackGround Worker ***** 


    }
}