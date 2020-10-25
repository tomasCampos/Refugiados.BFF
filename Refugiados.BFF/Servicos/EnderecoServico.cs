using Refugiados.BFF.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos
{
    public class EnderecoServico : IEnderecoServico
    {
        private readonly IEnderecoRepositorio _enderecoRepositorio;

        public EnderecoServico(IEnderecoRepositorio enderecoRepositorio)
        {
            _enderecoRepositorio = enderecoRepositorio;
        }

        public async Task<int?> CadastrarEndereco(EnderecoModel endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco.CepEndereco) || string.IsNullOrWhiteSpace(endereco.NumeroEndereco))
                return null;

            await _enderecoRepositorio.CadastrarEndereco(endereco.CidadeEndereco, endereco.BairroEndereco, endereco.RuaEndereco, endereco.NumeroEndereco, endereco.ComplementoEndereco,
                endereco.CepEndereco, endereco.EstadoEndereco);

            return 0; //Obter o endereco cadastrado pelo CEP e numero e retornar aqui este código
        }
    }

    public interface IEnderecoServico
    {
        Task<int?> CadastrarEndereco(EnderecoModel endereco);
    }
}
