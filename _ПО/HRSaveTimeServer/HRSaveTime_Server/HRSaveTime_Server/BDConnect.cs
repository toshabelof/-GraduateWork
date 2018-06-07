using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HRSaveTime_Server
{
    class BDConnect
    {
        public String ConnectStatus(string Name)
        {
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= " + Name;
            using (var con = new OleDbConnection(connect))
            {
                try
                {
                    con.Open();
                    con.Close();
                    return "OK";
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }

        public String AddDataBase()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Microsoft Access (*.mdb) | *.mdb";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName.ToString();
                }
                else
                    return "";
            }
        }

        public List<string> SetLocationName(string Name)
        {
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= " + Name;
            using (var con = new OleDbConnection(connect))
            {
                con.Open();
                var cmd = new OleDbCommand("SELECT Name FROM Location", con);
                using (var reader = cmd.ExecuteReader())
                {
                    var lists = new List<string>();

                    while (reader.Read())
                    {
                        lists.Add(reader.GetString(0));
                    }
                    con.Close();
                    return lists;
                }
            }
        }

        public List<string> SetLocationIDNAame(string Name)
        {
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= " + Name;
            using (var con = new OleDbConnection(connect))
            {
                con.Open();
                var cmd = new OleDbCommand("SELECT ID_Room, Name FROM Location", con);

                using (var reader = cmd.ExecuteReader())
                {
                    var lists = new List<string>();

                    while (reader.Read())
                    {
                        lists.Add(reader[0].ToString());
                        lists.Add(reader[1].ToString());
                    }
                    con.Close();

                    return lists;
                }
            }
        }

        public void SaveEditBDLocation(string NameBD, string Name, string ID)
        {
            OleDbConnection bd = new OleDbConnection();
            bd.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + NameBD;

            bd.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = bd;
            command.CommandText = String.Format("UPDATE Location SET Name='{0}' WHERE ID_Room={1}", Name, ID);
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();
            bd.Close();

        }

        public void SaveNewBDLocation(string NameBD, string Name, string ID)
        {
            OleDbConnection bd = new OleDbConnection();
            bd.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + NameBD;

            bd.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = bd;
            command.CommandText = String.Format("INSERT INTO Location(ID_Room, Name) VALUES ('{0}','{1}')", ID, Name);
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();
            bd.Close();

        }
    }
}
