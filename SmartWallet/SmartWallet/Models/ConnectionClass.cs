
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;


namespace SmartWallet.Models
{
    public class ConnectionClass
    {
        MySqlConnection con;
        string connectionString = "Server = 212.44.102.60; Database = ivh7_rmr; Uid = ivh7_rmr; Pwd = RazvojMobilnihResitev";
        
        //public MySqlConnectionStringBuilder Builder()
        //{
        //    return new MySqlConnectionStringBuilder
        //    {
        //        Server = "212.44.102.60",
        //        UserID = "ivh7_rmr",
        //        Password = "RazvojMobilnihResitev",
        //        Database = "ivh7_rmr",
        //        Port = 3306
                
        //    };
        //}

        

        public void OpenConnection()
        {
            con = new MySqlConnection(connectionString);
            con.Open();
        }

        public void CloseConnection()
        {
            con.Close();
        }


        public void ExecuteQueries(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, con);
            cmd.ExecuteNonQuery();
        }

        public bool ExecuteQueries2(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, con);
            if (cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }


        public MySqlDataReader DataReader(string Query_)
        {
            MySqlCommand cmd = new MySqlCommand(Query_, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

    }
}
