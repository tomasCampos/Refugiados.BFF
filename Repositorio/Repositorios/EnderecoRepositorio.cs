using Repositorio.CrossCutting;
using Repositorio.Insfraestrutura;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public EnderecoRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task CadastrarEndereco(string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado)
        {
            await _dataBase.ExecutarAsync(AppConstants.CADASTRAR_ENDERECO, new 
            {
                cidade_endereco = cidade,
                bairro_endereco = bairro,
                rua_endereco = rua,
                numero_endereco = numero,
                complemento_endereco = complemento,
                cep_enderco = cep,
                estado_endereco = estado
            });            
        }
    }

    public interface IEnderecoRepositorio
    {
        Task CadastrarEndereco(string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado);
    }
}
