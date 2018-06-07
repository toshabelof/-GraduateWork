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
                    for (int i = 0; i < list.Count; i = +2)
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
            if (textBox5.Text != "")
            {
                List<string> mas = new List<string>();
                mas = con.SetLocationName(textBox5.Text);
                comboBox1.Items.Clear();
                foreach (string m in mas)
                {
                    comboBox1.Items.Add(m);
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GetSetting();

            var result = "";
            setting.TryGetValue("BD", out result);
            if (result != null)
            { 
                textBox5.Text = result;
                string value = con.ConnectStatus(result);
                if (value == "OK")
                {
                    label9.ForeColor = Color.Green;
                    label9.Text = "Подключено";

                    GetLocationCB();
                }
                else
                {
                    MessageBox.Show(value, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    label9.ForeColor = Color.Red;
                    label9.Text = "Ошибка";
                    return;
                }
            }

            string[] val = sp.SearchPorts();
            foreach (string v in val)
            {
                comboBox2.Items.Add(v);
                comboBox3.Items.Add(v);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                string Value = con.ConnectStatus(textBox5.Text);
                if (Value == "OK")
                {
                    label9.ForeColor = Color.Green;
                    label9.Text = "Подключено";

                    SetSetting("BD", textBox5.Text);

                    GetLocationCB();
                }
                else
                {
                    label9.ForeColor = Color.Red;
                    label9.Text = "Ошибка";
                    MessageBox.Show(Value, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Необходимо добавить базу данных в систему.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string Result = con.AddDataBase();
            if (Result != "")
            {
                textBox5.Text = Result;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SettingsRoom sR = new SettingsRoom();
            sR.ShowDialog();
            GetLocationCB();
        }

    }
}
