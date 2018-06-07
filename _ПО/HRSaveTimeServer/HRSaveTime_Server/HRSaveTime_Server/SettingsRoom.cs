using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;

namespace HRSaveTime_Server
{
    public partial class SettingsRoom : Form
    {
        public SettingsRoom()
        {
            InitializeComponent();
        }

        BDConnect bd = new BDConnect();
        bool Edit = false;
        bool newRow = false;
        List<int> editIndex = new List<int>();
        List<int> newIndex = new List<int>();

        private void SettingsRoom_Load(object sender, EventArgs e)
        {
            var result = "";
            Form1.setting.TryGetValue("BD", out result);

            List<string> sss = bd.SetLocationIDNAame(result);

            int j = 0;
            for (int i = 0; i < sss.Count / 2; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = sss[j];
                dataGridView1.Rows[i].Cells[1].Value = sss[j + 1];
                j += 2;
            }

        }

        private void SettingsRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = "";
            Form1.setting.TryGetValue("BD", out result);

            if (Edit || newRow)
            {
                DialogResult dr = MessageBox.Show("Вы хотите сохранить изменения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {

                    if (Edit)
                    {
                        foreach (int i in editIndex)
                        {
                            bd.SaveEditBDLocation(result, dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString());
                        }
                    }

                    if (newRow)
                    {
                        foreach (int i in newIndex)
                        {
                            bd.SaveNewBDLocation(result, dataGridView1.Rows[i - 1].Cells[1].Value.ToString(), dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                        }
                    }

                    
                }
                else if (dr == DialogResult.No)
                {
                    this.Close();
                }
            }

        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].ReadOnly = false;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = Convert.ToInt32(dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[0].Value) + 1;
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1];
            dataGridView1.BeginEdit(true);

           
            newRow = true;
            newIndex.Add(dataGridView1.RowCount);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[1];
                dataGridView1.BeginEdit(true);

                Edit = true;
                editIndex.Add(e.RowIndex);
            }
        }
    }
}
