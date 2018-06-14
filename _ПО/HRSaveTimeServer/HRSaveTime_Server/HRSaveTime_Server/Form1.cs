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

namespace HRSaveTime_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BDConnect con = new BDConnect();
        SerialPorts sp = new SerialPorts();
        Inquiry inq = new Inquiry();

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
            if (WayToBDAccesse_tBox.Text != "")
            {
                List<string> mas = new List<string>();
                mas = con.SetLocationName(WayToBDAccesse_tBox.Text);
                Rooms_cBox.Items.Clear();
                foreach (string m in mas)
                {
                    Rooms_cBox.Items.Add(m);
                }
            }
        }

        public void GetRules(string login, string password)
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
                            string value = con.ConnectStatus(mas[1]);
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
                            PasswordOracle_tBox.Text = mas[2];
                            string value = con.ConnectToOracle(mas[1], mas[2]);

                            if (value == "Connect")
                            {
                                StatusOracle_lebel.Text = "Connect";
                                StatusOracle_lebel.ForeColor = Color.Green;

                                //GetLocationCB();
                                GetRules(mas[1], mas[2]);

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

            string[] val = sp.SearchPorts();
            foreach (string v in val)
            {
                ComIn_cBox.Items.Add(v);
                ComOut_cBox.Items.Add(v);
            }
        }

        private void AddBDAccesse_btn_Click(object sender, EventArgs e)
        {
            string Result = con.AddDataBase();
            if (Result != "")
            {
                WayToBDAccesse_tBox.Text = Result;
            }
        }

        private void EditRooms_Click(object sender, EventArgs e)
        {
            SettingsRoom sR = new SettingsRoom();
            sR.ShowDialog();
            //GetLocationCB();
        }

        private void AddRule_btn_Click(object sender, EventArgs e)
        {

        }

        private void StatusUpdateBDAccesse_btn_Click(object sender, EventArgs e)
        {
            String Way = WayToBDAccesse_tBox.Text;
            string value = con.ConnectStatus(Way);
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
            String value = con.ConnectStatus(Way);
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
            String login = LoginOracle_tBox.Text;
            String password = PasswordOracle_tBox.Text;

            String resultConnect = con.ConnectToOracle(login, password);
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




    }
}
