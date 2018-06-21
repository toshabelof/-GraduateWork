using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Oracle.DataAccess.Client;
using System.IO.Ports;
using System.Threading;

namespace HRSaveTime_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BDConnect bd = new BDConnect();

        Inquiry inq = new Inquiry();
        List<int> AddIndex = new List<int>();
        List<string> DeleteIndex = new List<string>();
        SortedList<string, string> l = new SortedList<string, string>();

        String login = "";
        String password = "";
        bool Edit = false;
        bool Add = false;
        bool Del = false;
        String SPort = null;
        Thread InstanceCaller;

        public static SortedList<string, string> setting = new SortedList<string, string>();
        List<string> list = new List<string>();

        public void GetSetting()
        {
            StreamReader sr = new StreamReader("settings.txt");
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

            StreamWriter sw = new StreamWriter("settings.txt", false);
            try
            {
                foreach (string k in setting.Keys)
                {
                    sw.WriteLine(k);
                    sw.WriteLine(setting[k]);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
            }
        }

        public void GetLocationCB()
        {
            String[] mas = bd.GetBDType();

            switch (mas[0])
            {
                case "A":
                    {
                        if (WayToBDAccesse_tBox.Text != "")
                        {
                            List<string> list = new List<string>();
                            list = bd.SetLocationName(WayToBDAccesse_tBox.Text);
                            Rooms_cBox.Items.Clear();
                            foreach (string l in list)
                            {
                                Rooms_cBox.Items.Add(l);
                            }
                        }
                        break;
                    }

                case "O":
                    {
                        List<string> sss = bd.SetLocationIDNAame(mas[1], mas[2]);
                        int j = 0;
                        for (int i = 0; i < sss.Count / 2; i++)
                        {
                            List<string> list = new List<string>();
                            list = bd.SetLocationName(mas[1], mas[2]);
                            Rooms_cBox.Items.Clear();
                            foreach (string l in list)
                            {
                                Rooms_cBox.Items.Add(l);
                            }
                        }
                        break;
                    }
            }

        }

        public void GetRules()
        {
            String connect = "Data Source = localhost; User ID = " + login + "; Password = " + password;
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("select * from RULES", con);
                OracleDataReader reader = com.ExecuteReader();
                try
                {
                    int j = 0;
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataGridView1.Rows[j].Cells[i].Value = reader.GetValue(i);
                        }
                        j++;
                    }
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                }
                finally
                {
                    reader.Close();
                }
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Monitor_tBox.Text = "System: Монитор потока данных готов к работе." + Environment.NewLine;
            GetSetting();

            var result = "";
            setting.TryGetValue("BD", out result);
            if (result != null)
            {
                string[] mas = result.Split('/');

                switch (mas[0])
                {
                    case "A":
                        {
                            WayToBDAccesse_tBox.Text = mas[1];
                            string value = bd.ConnectStatus(mas[1]);
                            if (value == "OK")
                            {
                                StatusAccesse_lebel.ForeColor = Color.Green;
                                StatusAccesse_lebel.Text = "Connect";

                                StatusOracle_lebel.ForeColor = Color.Red;
                                StatusOracle_lebel.Text = "Disconnect";
                            }
                            else
                            {
                                StatusAccesse_lebel.ForeColor = Color.Red;
                                StatusAccesse_lebel.Text = "Disconnect";
                            }

                            break;
                        }

                    case "O":
                        {
                            LoginOracle_tBox.Text = mas[1];
                            login = mas[1];
                            PasswordOracle_tBox.Text = mas[2];
                            password = mas[2];
                            string value = bd.ConnectToOracle(mas[1], mas[2]);

                            if (value == "Connect")
                            {
                                StatusOracle_lebel.Text = "Connect";
                                StatusOracle_lebel.ForeColor = Color.Green;

                                GetLocationCB();
                                GetRules();

                                StatusAccesse_lebel.ForeColor = Color.Red;
                                StatusAccesse_lebel.Text = "Disconnect";
                            }
                            else
                            {
                                StatusOracle_lebel.Text = "Disconnect";
                                StatusOracle_lebel.ForeColor = Color.Red;
                            }

                            setting.TryGetValue("COMPort", out result);
                            ComPorts_cBox.Text = result;

                            ConnectTX();

                            break;
                        }
                }

            }

        }

        private void AddBDAccesse_btn_Click(object sender, EventArgs e)
        {
            string Result = bd.AddDataBase();
            if (Result != "")
            {
                WayToBDAccesse_tBox.Text = Result;
            }
        }

        private void EditRooms_Click(object sender, EventArgs e)
        {
            SettingsRoom sR = new SettingsRoom();
            sR.ShowDialog();
            GetLocationCB();
        }

        private void AddRule_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.Rows.Add();
            if (dataGridView1.Rows.Count != 1)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value) + 1;
            }
            else
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = 1;
            }
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
            dataGridView1.BeginEdit(true);

            Add = true;
            AddIndex.Add(Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value) - 1);
        }

        private void StatusUpdateBDAccesse_btn_Click(object sender, EventArgs e)
        {
            String Way = WayToBDAccesse_tBox.Text;
            string value = bd.ConnectStatus(Way);
            if (value == "OK")
            {
                StatusAccesse_lebel.ForeColor = Color.Green;
                StatusAccesse_lebel.Text = "Connect";
            }
            else
            {
                MessageBox.Show(value, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusAccesse_lebel.ForeColor = Color.Red;
                StatusAccesse_lebel.Text = "Disconnect";
            }
        }

        private void CheckIn_btn_Click(object sender, EventArgs e)
        {
            if (ComPorts_cBox.Text != "")
            {
                if (CodeIn_tBox.Text != "")
                {
                    StatusIn_label.Text = "Disconnect";
                    StatusIn_label.ForeColor = Color.Red;
                }
            }
        }

        private void CheckOut_btn_Click(object sender, EventArgs e)
        {
            if (ComPorts_cBox.Text != "")
            {
                if (CodeIn_tBox.Text != "")
                {            
                    StatusOut_label.Text = "Disconnect";
                    StatusOut_label.ForeColor = Color.Red;
                }
            }
        }

        private void SaveRooms_Click(object sender, EventArgs e)
        {
            String connect = "Data Source = localhost; User ID = " + login + "; Password = " + password;
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                OracleCommand com = new OracleCommand("Update Rooms set " +
                "INID ='" + CodeIn_tBox.Text + "', " +
                "OUTID = '" + CodeOut_tBox.Text + "' " +
                "where NAME = '" + Rooms_cBox.Text + "'",
                con);
                com.ExecuteNonQuery();
            }
        }


        private void Send_btn_Click(object sender, EventArgs e)
        {
            String value = Inquiry_tBox.Text;
            Monitor_tBox.Text += Environment.NewLine + ">> " + value + Environment.NewLine;

            String[] masS = { };
            try
            {
                masS = value.Split(' ');
                switch (masS[0])
                {
                    case "getPernr":
                        {
                            List<string> res = bd.getPernr(masS[1]);
                            if (res.Count != 0)
                            {
                                Monitor_tBox.Text += "Result: " + res[0].ToString() + " " + res[1].ToString() + " " + res[2].ToString() + res[3].ToString() + Environment.NewLine;
                            }
                            else
                                Monitor_tBox.Text += "RFID " + masS[1] + " не найден в системе." + Environment.NewLine;
                            break;
                        }

                    case "getRFID":
                        {
                            List<string> res = bd.getRFID(masS[1]);
                            if (res.Count != 0)
                            {
                                Monitor_tBox.Text += "Result: " + res[0].ToString() + " " + res[1].ToString() + " " + res[2].ToString() + res[3].ToString() + Environment.NewLine;
                            }
                            else
                                Monitor_tBox.Text += "Табельный номер " + masS[1] + " не найден в системе." + Environment.NewLine;
                            break;
                        }
                    case "getInfo":
                        {
                            List<string> res = bd.getInfo(masS[1]);
                            if (res.Count != 0)
                            {
                                Monitor_tBox.Text += "Result: " + res[0].ToString() + " " + res[1].ToString() + " " + res[2].ToString() + " " + res[3].ToString() + " " + Convert.ToDateTime(res[4].ToString()).ToString("dd.MM.yyyy") + " " + res[5].ToString() +
                                   " " + res[6].ToString() + " " + res[7].ToString() + " " + res[8].ToString() + " " + res[9].ToString() + Environment.NewLine;
                            }
                            else
                                Monitor_tBox.Text += "Табельный номер " + masS[1] + " не найден в системе." + Environment.NewLine;
                            break;
                        }
                    case "getStatus":
                        {
                            SortedList<string, string> res = bd.getINOUTROOM();
                            if (res.Count != 0)
                            {
                                Monitor_tBox.Text += Environment.NewLine;
                                foreach (string k in res.Keys)
                                {
                                    String[] mas = res[k].Split('/');
                                    Monitor_tBox.Text += k + " " + mas[0].ToString() + ": Disconnect / " + mas[1].ToString() + ": Disconnect" + Environment.NewLine;
                                }
                            }
                            else
                                Monitor_tBox.Text += "В системе нет информации о датчиках." + Environment.NewLine;
                            break;
                        }
                    case "help":
                        {

                            Monitor_tBox.Text += Environment.NewLine + "System: Доступный перечень команд:" + Environment.NewLine;
                            Monitor_tBox.Text += "1. getPernr [RFID]" + Environment.NewLine + "2. getRFID [Табельный номер]" + Environment.NewLine +
                                "3. getInfo [Табельный номер]" + Environment.NewLine + "4. getStatus" + Environment.NewLine;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Monitor_tBox.Text += "System: " + "Команда не распознана" + Environment.NewLine;
            }
        }

        private void ConnectToBDAccesse_btn_Click(object sender, EventArgs e)
        {
            String Way = WayToBDAccesse_tBox.Text;
            String value = bd.ConnectStatus(Way);
            if (value == "OK")
            {
                StatusAccesse_lebel.ForeColor = Color.Green;
                StatusAccesse_lebel.Text = "Connect";

                SetSetting("BD", "A/" + Way);

                StatusOracle_lebel.Text = "Disconnect";
                StatusOracle_lebel.ForeColor = Color.Red;

                //GetLocationCB();
            }
            else
            {
                MessageBox.Show(value, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusAccesse_lebel.ForeColor = Color.Red;
                StatusAccesse_lebel.Text = "Disconnect";
            }
        }

        private void ConnectToBDOracle_btn_Click(object sender, EventArgs e)
        {
            login = LoginOracle_tBox.Text;
            password = PasswordOracle_tBox.Text;

            String resultConnect = bd.ConnectToOracle(login, password);
            if (resultConnect == "Connect")
            {
                MessageBox.Show("HRSaveTime подключено к БД Oracle", "Успешное подключние к БД", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StatusOracle_lebel.Text = "Connect";
                StatusOracle_lebel.ForeColor = Color.Green;

                SetSetting("BD", "O/" + login + "/" + password);

                StatusAccesse_lebel.ForeColor = Color.Red;
                StatusAccesse_lebel.Text = "Disconnect";



                //GetLocationCB();
            }
            else
            {
                MessageBox.Show(resultConnect, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusOracle_lebel.Text = "Disconnect";
                StatusOracle_lebel.ForeColor = Color.Red;
            }
        }

        private void SaveRules_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            String connect = "Data Source = localhost; User ID = " + login + "; Password = " + password;
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                if (Edit)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        OracleCommand com = new OracleCommand("update RULES set " +
                            "CODE  ='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'," +
                            "DESCRIPT = '" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "', " +
                            "PROFILE = '" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "', " +
                            "INQUIRY = '" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "', " +
                            "REPORTS = '" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "', " +
                            "PGRV = '" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "', " +
                            "RULES = '" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "', " +
                            "MONITOR = '" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "', " +
                            "ABSENCE = '" + dataGridView1.Rows[i].Cells[9].Value.ToString() + "', " +
                            "TIMEPAIRS = '" + dataGridView1.Rows[i].Cells[10].Value.ToString() + "', " +
                            "PERNR = '" + dataGridView1.Rows[i].Cells[11].Value.ToString() + "', " +
                            "RFID = '" + dataGridView1.Rows[i].Cells[12].Value.ToString() + "', " +
                            "AUTH = '" + dataGridView1.Rows[i].Cells[13].Value.ToString() + "' " +
                            "where ID = " + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value),
                            con);
                        com.ExecuteNonQuery();
                    }
                }

                if (Add)
                {
                    foreach (int i in AddIndex)
                    {
                        OracleCommand com = new OracleCommand("Insert into Rules values('" +
                            dataGridView1.Rows[i].Cells[0].Value + "', '" +
                            dataGridView1.Rows[i].Cells[1].Value + "', '" +
                            dataGridView1.Rows[i].Cells[2].Value + "', '" +
                            dataGridView1.Rows[i].Cells[3].Value + "', '" +
                            dataGridView1.Rows[i].Cells[4].Value + "', '" +
                            dataGridView1.Rows[i].Cells[5].Value + "', '" +
                            dataGridView1.Rows[i].Cells[6].Value + "', '" +
                            dataGridView1.Rows[i].Cells[7].Value + "', '" +
                            dataGridView1.Rows[i].Cells[8].Value + "', '" +
                            dataGridView1.Rows[i].Cells[9].Value + "', '" +
                            dataGridView1.Rows[i].Cells[10].Value + "', '" +
                            dataGridView1.Rows[i].Cells[11].Value + "', '" +
                            dataGridView1.Rows[i].Cells[12].Value + "', '" +
                            dataGridView1.Rows[i].Cells[13].Value + "')",
                            con);
                        try
                        {
                            com.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                if (Del)
                {
                    foreach (string i in DeleteIndex)
                    {
                        OracleCommand com = new OracleCommand("Delete from Rules where " +
                        "ID = '" + i + "'",
                        con);
                        com.ExecuteNonQuery();
                    }
                }

            }
        }

        private void EditTableRules_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            dataGridView1.BeginEdit(true);

            Edit = true;
        }

        private void Rooms_cBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] mas = bd.GetBDType();
            switch (mas[0])
            {
                case "A": { break; }
                case "O":
                    {
                        List<string> res = bd.getINOUTROOM(Rooms_cBox.Text);


                        CodeIn_tBox.Text = res[0].ToString();
                        CodeOut_tBox.Text = res[1].ToString();


                        if (ComPorts_cBox.Text != "")
                        {
                            if (CodeIn_tBox.Text != "")
                            {
                                StatusIn_label.Text = "Disconnect";
                                StatusIn_label.ForeColor = Color.Red;
                                StatusOut_label.Text = "Disconnect";
                                StatusOut_label.ForeColor = Color.Red;
                            }
                        }
                        break;
                    }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 14)
            {
                Del = true;
                DeleteIndex.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void SerchComPort_btn_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            ComPorts_cBox.Items.Clear();
            foreach (string port in ports)
            {
                ComPorts_cBox.Items.Add(port);
            }

        }

        bool pause = false;
        private void ConnectToSensor_btn_Click(object sender, EventArgs e)
        {
            ConnectTX();
        }


        public delegate void InvokeDelegate(string sp);

        public void InvokeMethod(string sp)
        {
            String date = DateTime.Now.ToString("dd.MM.yyyy");
            String time = DateTime.Now.ToString("hh:mm");
            Monitor_tBox.Text += (date + " " + time + " " + sp) + Environment.NewLine;
        }

        private void geLog()
        {
            SerialPort sp = new SerialPort();
            while (true)
            {
                if (SPort != null)
                {
                    if (!pause)
                    {
                        sp.PortName = SPort;
                        sp.BaudRate = 9600;
                        sp.Open();
                        System.Threading.Thread.Sleep(500);
                        try
                        {
                            Monitor_tBox.BeginInvoke(new InvokeDelegate(InvokeMethod), sp.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        sp.Close();
                    }
                    else
                        sp.Close();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (InstanceCaller != null)
            {
                if (InstanceCaller.IsAlive != null || InstanceCaller.IsAlive != false)
                {
                    InstanceCaller.IsBackground = true;
                }
            }
        }

        public void ConnectTX()
        {
            String result = "";

            using (SerialPort sp = new SerialPort())
            {
                if (ComPorts_cBox.Text != "")
                {
                    sp.PortName = ComPorts_cBox.Text;
                    sp.BaudRate = 9600;
                    try
                    {
                        sp.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    System.Threading.Thread.Sleep(500);
                    sp.Write("1");
                    result = sp.ReadLine();
                    sp.Close();
                    if (result.Contains("1"))
                    {
                        StatusConnectToSensor_label.Text = "Connect";
                        StatusConnectToSensor_label.ForeColor = Color.Green;
                        SPort = ComPorts_cBox.Text;
                        InstanceCaller = new Thread(new ThreadStart(geLog));
                        InstanceCaller.Start();

                        SetSetting("COMPort", SPort);
                    }
                    else
                    {
                        StatusConnectToSensor_label.Text = "Disconnect";
                        StatusConnectToSensor_label.ForeColor = Color.Red;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
