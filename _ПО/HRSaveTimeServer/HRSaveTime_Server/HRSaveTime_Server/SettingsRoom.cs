using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

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
        bool Add = false;
        bool Del = false;
        List<int> editIndex = new List<int>();
        List<int> AddIndex = new List<int>();
        List<int> DeleteIndex = new List<int>();

        private void SettingsRoom_Load(object sender, EventArgs e)
        {
            String[] mas = bd.GetBDType();

            switch (mas[0])
            {
                case "A":
                    {
                        List<string> sss = bd.SetLocationIDNAame(mas[1]);
                        int j = 0;
                        for (int i = 0; i < sss.Count / 2; i++)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = sss[j];
                            dataGridView1.Rows[i].Cells[1].Value = sss[j + 1];
                            j += 2;
                        }
                        break;
                    }

                case "O":
                    {
                        List<string> sss = bd.SetLocationIDNAame(mas[1], mas[2]);
                        int j = 0;
                        for (int i = 0; i < sss.Count / 2; i++)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = sss[j];
                            dataGridView1.Rows[i].Cells[1].Value = sss[j + 1];
                            j += 2;
                        }
                        dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                        break;
                    }
            }
        }

        private void SettingsRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Edit || Add || Del)
            {
                DialogResult dr = MessageBox.Show("Вы хотите сохранить изменения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    SaveToBD();
                }
                else
                {
                    Edit = !Edit;
                    Add  = !Add;
                    Del  = !Del;
                    return;
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


            Add = true;
            AddIndex.Add(dataGridView1.RowCount-1);
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

            if (e.ColumnIndex == 3)
            {
                Del = true;
                DeleteIndex.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToBD();
            Edit = false;
            Add = false;
            Del = false;
        }

        private void SaveToBD()
        {
            String[] mas = bd.GetBDType();

            switch (mas[0])
            {
                case "A":
                    {

                        break;
                    }

                case "O":
                    {
                        String connect = "Data Source = localhost; User ID = " + mas[1] + "; Password = " + mas[2];
                        using (OracleConnection con = new OracleConnection(connect))
                        {
                            con.Open();
                            if (Edit)
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    OracleCommand com = new OracleCommand("update Rooms set " +
                                    "NAME  = '" + dataGridView1.Rows[i].Cells[1].Value + "'" +
                                    "where IDROOMS  = '" + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) + "'",
                                     con);
                                    com.ExecuteNonQuery();
                                }
                            }

                            if (Add)
                            {
                                foreach (int i in AddIndex)
                                {
                                    OracleCommand com = new OracleCommand("Insert into Rooms(IDROOMS,NAME) values('" +
                                    Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) + "', '" +
                                    dataGridView1.Rows[i].Cells[1].Value + "')",
                                    con);
                                    com.ExecuteNonQuery();
                                }
                            }

                            if (Del)
                            {
                                foreach (int i in DeleteIndex)
                                {
                                    OracleCommand com = new OracleCommand("Delete from Rooms where " +
                                    "IDROOMS = '" + i + "'",
                                    con);
                                    com.ExecuteNonQuery();
                                }
                            }
                            break;
                        }
                    }
            }
        }
    }
}
