using Refugiados.BFF.Models;
using Repositorio.Repositorios;
using System;
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

        public async Task<int> CadastrarEndereco(EnderecoModel endereco)
        {
            var chaveIdentificacaoEndereco = Guid.NewGuid().ToString();

            if (endereco == null)
            {
                await _enderecoRepositorio.CadastrarEndereco(null, null, null, null, null, null, null, chaveIdentificacaoEndereco);
            }
            else
            {
                await _enderecoRepositorio.CadastrarEndereco(endereco.CidadeEndereco, endereco.BairroEndereco, endereco.RuaEndereco, endereco.NumeroEndereco, endereco.ComplementoEndereco,
                    endereco.CepEndereco, endereco.EstadoEndereco, chaveIdentificacaoEndereco);
            }

            var enderecoCadastrado = await ObterEndereco(chaveIdentificacaoEndereco);

            return enderecoCadastrado.CodigoEndereco;
        }

        public async Task<EnderecoModel> ObterEndereco(int codigoEndereco)
        {
            var endereco = await _enderecoRepositorio.ObterEndereco(codigoEndereco);

            if (endereco == null)
                return null;

            return new EnderecoModel
            {
                BairroEndereco = endereco.bairro_endereco,
                CepEndereco = endereco.cep_endereco,
                CidadeEndereco = endereco.cidade_endereco,
                CodigoEndereco = endereco.codigo_endereco,
                ComplementoEndereco = endereco.complemento_endereco,
                EstadoEndereco = endereco.estado_endereco,
                NumeroEndereco = endereco.numero_endereco,
                RuaEndereco = endereco.rua_endereco
            };
        }

        public async Task<EnderecoModel> ObterEndereco(string chaveIdentificacaoEndereco)
        {
            var endereco = await _enderecoRepositorio.ObterEndereco(chaveIdentificacaoEndereco);

            if (endereco == null)
                return null;

            return new EnderecoModel
            {
                BairroEndereco = endereco.bairro_endereco,
                CepEndereco = endereco.cep_endereco,
                CidadeEndereco = endereco.cidade_endereco,
                CodigoEndereco = endereco.codigo_endereco,
                ComplementoEndereco = endereco.complemento_endereco,
                EstadoEndereco = endereco.estado_endereco,
                NumeroEndereco = endereco.numero_endereco,
                RuaEndereco = endereco.rua_endereco
            };
        }

        public async Task AtualizarEndereco(int codigoEndereco, string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado)
        {
            var endereco = await ObterEndereco(codigoEndereco);

            endereco.CidadeEndereco = string.IsNullOrWhiteSpace(cidade) ? endereco.CidadeEndereco : cidade;
            endereco.BairroEndereco = string.IsNullOrWhiteSpace(bairro) ? endereco.BairroEndereco : bairro;
            endereco.RuaEndereco = string.IsNullOrWhiteSpace(rua) ? endereco.RuaEndereco : rua;
            endereco.NumeroEndereco = string.IsNullOrWhiteSpace(numero) ? endereco.NumeroEndereco : numero;
            endereco.ComplementoEndereco = string.IsNullOrWhiteSpace(complemento) ? endereco.ComplementoEndereco : complemento;
            endereco.CepEndereco = string.IsNullOrWhiteSpace(cep) ? endereco.CepEndereco : cep;
            endereco.EstadoEndereco = string.IsNullOrWhiteSpace(estado) ? endereco.EstadoEndereco : estado;

            await _enderecoRepositorio.AtualizarEndereco(codigoEndereco, endereco.CidadeEndereco, endereco.BairroEndereco, endereco.RuaEndereco, endereco.NumeroEndereco, 
                endereco.ComplementoEndereco, endereco.CepEndereco, endereco.EstadoEndereco);
        }
    }

    public interface IEnderecoServico
    {
        Task<int> CadastrarEndereco(EnderecoModel endereco);
        Task<EnderecoModel> ObterEndereco(int codigoEndereco);
        Task<EnderecoModel> ObterEndereco(string chaveIdentificacaoEndereco);
        Task AtualizarEndereco(int codigoEndereco, string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado);
    }
}
