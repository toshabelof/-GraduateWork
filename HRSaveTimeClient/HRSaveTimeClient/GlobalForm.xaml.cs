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

        BrushConverter bc = new BrushConverter();
        Grid grid = new Grid();

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

        private void NewProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NewProfileButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void NewProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NewProfileButton.Background = (Brush)bc.ConvertFrom("#0049db");
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

        private void Inquiry_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = InquiryGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Запросы на отсутствия";
        }

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

        private void NewProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = NewProfileGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Создание профиля";
        }

        private void SaveInqButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult other = MessageBox.Show("Вы действительно хотите сохранить внесенные изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (other)
            {
                case MessageBoxResult.Yes:
                    {
                        grid.Visibility = System.Windows.Visibility.Collapsed;
                        grid = InquiryGrid;
                        grid.Visibility = System.Windows.Visibility.Visible;
                        Title.Text = "Запросы на отсутствия";
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

        private void GenerateReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SaveReportsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveReportsButton.Background = (Brush)bc.ConvertFrom("#2c71fd");
        }

        private void SaveReportsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveReportsButton.Background = (Brush)bc.ConvertFrom("#0049db");
        }

        private void SaveReportsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddNameReports anr = new AddNameReports();
            anr.ShowDialog();

            grid.Visibility = System.Windows.Visibility.Collapsed;
            grid = ReportsGrid;
            grid.Visibility = System.Windows.Visibility.Visible;
            Title.Text = "Сохраненные отчеты";
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
    }
}