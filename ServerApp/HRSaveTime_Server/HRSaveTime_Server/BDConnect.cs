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

        public List<string> SetLocation(string Name)
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
    }
}
