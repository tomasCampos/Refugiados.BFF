using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using Dapper;
using System.Linq;

namespace Repositorio.Insfraestrutura
{
    public class DataBaseConnector
    {
        private const string CONNECTION_STRING = "server=us-cdbr-east-02.cleardb.com;user=bf7492f9f13e58;password=e5fc5916;database=heroku_93ac2d8811d872a";
        private readonly MySqlConnection _conn;

        public DataBaseConnector()
        {
            _conn = new MySqlConnection(CONNECTION_STRING);
        }

        public IEnumerable<T> Listar<T>(string sql)
        {
            _conn.Open();
            var result = _conn.Query<T>(sql).ToList();
            _conn.Close();

            return result;
        }
    }
}
