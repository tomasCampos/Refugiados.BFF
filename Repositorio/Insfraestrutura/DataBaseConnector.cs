using MySqlConnector;
using System.Collections.Generic;
using Dapper;
using Repositorio.CrossCutting;
using System.Threading.Tasks;

namespace Repositorio.Insfraestrutura
{
    public class DataBaseConnector
    {
        private readonly MySqlConnection _conn;

        public DataBaseConnector()
        {
            _conn = new MySqlConnection(AppConstants.CONNECTION_STRING);
        }

        public async Task<IEnumerable<T>> SelecionarAsync<T>(string sql)
        {
            await _conn.OpenAsync();
            
            var result = await _conn.QueryAsync<T>(sql);

            await _conn.CloseAsync();

            return result;
        }

        public async Task<IEnumerable<T>> SelecionarAsync<T>(string sql, object parametros)
        {
            await _conn.OpenAsync();

            var result = await _conn.QueryAsync<T>(sql, parametros);
            
            await _conn.CloseAsync();

            return result;
        }

        public async Task<int> ExecutarAsync(string sql)
        {
            await _conn.OpenAsync();

            var numeroDeLinhasAfetadas = await _conn.ExecuteAsync(sql);

            await _conn.CloseAsync();

            return numeroDeLinhasAfetadas;
        }

        public async Task<int> ExecutarAsync(string sql, object parametros)
        {
            await _conn.OpenAsync();
            
            var numeroDeLinhasAfetadas = await _conn.ExecuteAsync(sql, parametros);
            
            await _conn.CloseAsync();

            return numeroDeLinhasAfetadas;
        }
    }
}
