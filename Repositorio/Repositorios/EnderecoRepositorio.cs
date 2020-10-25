using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System.Linq;
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
                cep_endereco = cep,
                estado_endereco = estado
            });            
        }

        public async Task<EnderecoDto> ObterEndereco(int codigoEndereco)
        {
            var endereco = await _dataBase.SelecionarAsync<EnderecoDto>(AppConstants.OBTER_ENDERECO_POR_CODIGO, new { codigo_endereco = codigoEndereco });
            return endereco.FirstOrDefault();
        }

        public async Task<EnderecoDto> ObterEndereco(string numero, string cep)
        {
            var endereco = await _dataBase.SelecionarAsync<EnderecoDto>(AppConstants.OBTER_ENDERECO_POR_CEP_NUMERO_E_COMPLEMENTO, new 
            {
                numero_endereco = numero, 
                cep_endereco = cep
            });
            return endereco.OrderByDescending(e => e.codigo_endereco).FirstOrDefault();
        }

        public async Task AtualizarEndereco(int codigoEndereco, string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado)
        {
            await _dataBase.ExecutarAsync(AppConstants.ALTERAR_ENDERECO, new
            {
                cidade_endereco = cidade,
                bairro_endereco = bairro,
                rua_endereco = rua,
                numero_endereco = numero,
                complemento_endereco = complemento,
                cep_endereco = cep,
                estado_endereco = estado,
                codigo_endereco = codigoEndereco
            });
        }
    }

    public interface IEnderecoRepositorio
    {
        Task CadastrarEndereco(string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado);
        Task<EnderecoDto> ObterEndereco(int codigoEndereco);
        Task<EnderecoDto> ObterEndereco(string numero, string cep);
        Task AtualizarEndereco(int codigoEndereco, string cidade, string bairro, string rua, string numero, string complemento, string cep, string estado);
    }
}
