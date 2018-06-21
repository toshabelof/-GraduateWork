using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace HRSaveTime_Server
{
    class BDConnect
    {
        String l = "";
        String p = "";
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

        public String ConnectToOracle(string login, string password)
        {
            String connect = "Data Source = localhost; User ID = " + login + "; Password = " + password;
            using (OracleConnection con = new OracleConnection(connect))
            {
                try
                {
                    con.Open();
                    l = login;
                    p = password;
                    con.Close();
                    return "Connect";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ex.Message.ToString();
                }
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

        public List<string> SetLocationName(string login, string password)
        {
            String connect = "Data Source = localhost; User ID = " + login + "; Password = " + password;
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("SELECT Name FROM Rooms", con);
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

        public List<string> SetLocationIDNAame(string Way)
        {
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= " + Way;
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

        public List<string> SetLocationIDNAame(string login, string password)
        {
            String connect = "Data Source = localhost; User ID = " + login + "; Password = " + password;
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("SELECT IDRooms, Name FROM Rooms", con);

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

        public String[] GetBDType()
        {
            var result = "";
            Form1.setting.TryGetValue("BD", out result);
            String[] mas = result.Split('/');
            return mas;
        }

        public List<string> getPernr(string RFID)
        {
            String connect = "Data Source = localhost; User ID = " + l + "; Password = " + p;
            var lists = new List<string>();
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("Select  PERNR.IDPERNR, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR  " +
                                                    "from PERS_INFO, RFID, PERNR " +
                                                    "where PERNR.PERSID = PERS_INFO.IDPERS and RFID.PERSID = PERS_INFO.IDPERS and RFID.IDRFID = '" + RFID + "'", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lists.Add(reader[0].ToString());
                        lists.Add(reader[1].ToString());
                        lists.Add(reader[2].ToString());
                        lists.Add(reader[3].ToString());
                    }
                }
            }
            return lists;
        }

        public List<string> getRFID(string PENR)
        {
            String connect = "Data Source = localhost; User ID = " + l + "; Password = " + p;
            var lists = new List<string>();
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("Select  RFID.IDRFID, PERS_INFO.LNAME, PERS_INFO.NAME, PERS_INFO.PATR  " +
                                                    "from PERS_INFO, RFID, PERNR " +
                                                    "where PERNR.PERSID = PERS_INFO.IDPERS and RFID.PERSID = PERS_INFO.IDPERS and PERNR.IDPERNR = '" + PENR + "'", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lists.Add(reader[0].ToString());
                        lists.Add(reader[1].ToString());
                        lists.Add(reader[2].ToString());
                        lists.Add(reader[3].ToString());
                    }
                }
            }
            return lists;
        }


        public List<string> getInfo(string PENR)
        {
            String connect = "Data Source = localhost; User ID = " + l + "; Password = " + p;
            var lists = new List<string>();
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("SELECT IDPERNR, LNAME, PERS_INFO.NAME, PATR, BIRTH, POSITION.Name, ORG_LEVEL.NAME, PGRVID, RULE, RFID.IDRFID " +
                        "FROM PERNR, PERS_INFO , POSITION, ORG_LEVEL, RFID " +
                        "WHERE PERS_INFO.IDPERS = PERNR.PERSID and POSITION.IDPOS = PERS_INFO.POSID and ORG_LEVEL.IDORG = PERS_INFO.ORGID and PERS_INFO.IDPERS = RFID.PERSID and PERNR.IDPERNR =  '" + PENR + "'", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lists.Add(reader[0].ToString());
                        lists.Add(reader[1].ToString());
                        lists.Add(reader[2].ToString());
                        lists.Add(reader[3].ToString());
                        lists.Add(reader[4].ToString());
                        lists.Add(reader[5].ToString());
                        lists.Add(reader[6].ToString());
                        lists.Add(reader[7].ToString());
                        lists.Add(reader[8].ToString());
                        lists.Add(reader[9].ToString());

                    }
                }
            }
            return lists;
        }

        public SortedList<string, string> getINOUTROOM()
        {
            String connect = "Data Source = localhost; User ID = " + l + "; Password = " + p;
            var lists = new SortedList<string, string>();
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("SELECT NAME, INID, OUTID FROM ROOMS", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lists.Add(reader[0].ToString(), reader[1].ToString() + "/" + reader[2].ToString());
                    }
                }
            }
            return lists;
        }

        public List<string> getINOUTROOM(string NameRoom)
        {
            String connect = "Data Source = localhost; User ID = " + l + "; Password = " + p;
            List<string> lists = new List<string>();
            using (OracleConnection con = new OracleConnection(connect))
            {
                con.Open();
                var cmd = new OracleCommand("SELECT INID, OUTID FROM ROOMS where Name = '" + NameRoom + "'", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lists.Add(reader[0].ToString());
                        lists.Add(reader[1].ToString());
                    }
                }
            }
            return lists;
        }

    }
}
