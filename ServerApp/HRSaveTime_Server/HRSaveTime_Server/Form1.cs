using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace HRSaveTime_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewRoom ANR = new AddNewRoom();
            ANR.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BDConnect con = new BDConnect();
            String Value = con.ConnectStatus("ClientBD.mdb");
            if (Value == "OK")
            {
                label9.ForeColor = Color.Green;
                label9.Text = "Подключено";
            }
            else
            {
                label9.ForeColor = Color.Red;
                label9.Text = "Ошибка";
                MessageBox.Show("Ошибка", Value);
            }
        }
    }
}
