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
                }
                finally
                {
                    reader.Close();
                }
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Monitor_tBox.Text = ">> System: Монитор потока данных готов к работе." + Environment.NewLine;
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

                                //GetLocationCB();
                                //GetRules();

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

        }

        private void CheckOut_btn_Click(object sender, EventArgs e)
        {

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
            Monitor_tBox.Text += ">>" + value + Environment.NewLine;

            String[] masS;
            try
            {
                masS = value.Split(' ');
                String Result = inq.SendInq(masS[0], masS[1]);
                Monitor_tBox.Text += ">> Result: " + Result + Environment.NewLine;
            }
            catch (Exception ex)
            {
                Monitor_tBox.Text += ">> Result: " + "Команда не распознана" + Environment.NewLine;
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
                        "IDRULES = '" + i + "'",
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
                        String connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                        using (OracleConnection con = new OracleConnection(connect))
                        {
                            con.Open();
                            OracleCommand com = new OracleCommand("Select INID, OUTID from Rooms where Name = '" + Rooms_cBox.Text + "'", con);
                            using (var reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    CodeIn_tBox.Text = reader[0].ToString();
                                    CodeOut_tBox.Text = reader[1].ToString();
                                }
                            }

                            break;
                        }
                    }
            }

            if (CodeIn_tBox.Text != "")
            {
                StatusIn_label.Text = "Disconnect";
                StatusIn_label.ForeColor = Color.Red;
                StatusOut_label.Text = "Disconnect";
                StatusOut_label.ForeColor = Color.Red;
            }

            if (CodeOut_tBox.Text != "")
            {
                StatusOut_label.Text = "Disconnect";
                StatusOut_label.ForeColor = Color.Red;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 17)
            {
                Del = true;
                DeleteIndex.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void SerchComPort_btn_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                ComPorts_cBox.Items.Add(port);
            }

        }

        private void ConnectToSensor_btn_Click(object sender, EventArgs e)
        {
            String result = "";
            using (SerialPort sp = new SerialPort())
            {
                sp.PortName = ComPorts_cBox.Text;
                sp.BaudRate = 9600;
                sp.Open();
                System.Threading.Thread.Sleep(1000); 
                sp.Write("1");
                result = sp.ReadLine();
            }

            if (result.Contains("1"))
            {
                StatusConnectToSensor_label.Text = "Connect";
                StatusConnectToSensor_label.ForeColor = Color.Green;
            }
            else
            {
                StatusConnectToSensor_label.Text = "Disconnect";
                StatusConnectToSensor_label.ForeColor = Color.Red;
            }
        }
    }
}
