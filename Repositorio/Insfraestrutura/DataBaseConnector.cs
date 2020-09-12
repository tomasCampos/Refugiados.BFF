using MySqlConnector;
using System.Collections.Generic;
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

        public IEnumerable<T> Selecionar<T>(string sql)
        {
            _conn.Open();
            var result = _conn.Query<T>(sql).ToList();
            _conn.Close();

            return result;
        }

        public IEnumerable<T> Selecionar<T>(string sql, object parametros)
        {
            _conn.Open();
            var result = _conn.Query<T>(sql, parametros).ToList();
            _conn.Close();

            return result;
        }

        public int Executar(string sql)
        {
            _conn.Open();
            var numeroDeLinhasAfetadas = _conn.Execute(sql);
            _conn.Close();

            return numeroDeLinhasAfetadas;
        }

        public int Executar(string sql, object parametros)
        {
            _conn.Open();
            var numeroDeLinhasAfetadas = _conn.Execute(sql, parametros);
            _conn.Close();

            return numeroDeLinhasAfetadas;
        }
    }
}
