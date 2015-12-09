using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAO
{
    class AcessoBancoDados
    {
        private MySqlConnection conn;
        private DataTable data;
        private MySqlDataAdapter da;
        //private MySqlDataReader dr;
        private MySqlCommandBuilder cb;

        private string server = "localhost";
        private string user = "root";
        private string password = "";
        private string database = "prova_aspnet";

        public void Conectar()
        {
            if (conn != null)
                conn.Close();

            string connStr = String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false", server, user, password, database);

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExecutarComandoSQL(string comandoSql)
        {
            MySqlCommand comando = new MySqlCommand(comandoSql, conn);
            comando.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable RetDataTable(string sql)
        {
            data = new DataTable();
            da = new MySqlDataAdapter(sql, conn);
            cb = new MySqlCommandBuilder(da);
            da.Fill(data);

            return data;
        }

        public MySqlDataReader RetDataReader(string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conn);
            MySqlDataReader dr = comando.ExecuteReader();
            //dr.Read();

            return dr;
        }

        public DataSet RetDataSet(string sql)
        {
            da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public bool RetDataRow(string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conn);
            bool dw = comando.ExecuteReader().HasRows;

            return dw;
        }
    }
}
