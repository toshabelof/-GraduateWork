using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace HRSaveTime_Server
{
    class BDConnect
    {
        public String ConnectStatus(String Name)
        {
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source=.\\" + Name;
            OleDbConnection con = new OleDbConnection(connect);
            try
            {
                con.Open();
                con.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
