using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Refugiados.BFF.Models.Requisicoes.Endereco;

namespace Refugiados.BFF.Models.Requisicoes.Usuario
{
    public class UsuarioEmpresaRequestModel : IRequestModel
    {
        public string RazaoSocial { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string TelefoneUsuario { get; set; }
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime? DataFundacao { get; set; }
        public int? NumeroFuncionarios { get; set; }
        public List<int> AreasTrabalho { get; set; }
        public CadastrarAtualizarEnderecoRequestModel Endereco { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(EmailUsuario))
                erros.Add("O campo email do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(SenhaUsuario))
                erros.Add("O campo senha do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(RazaoSocial))
                erros.Add("O campo razão social deve ser preenchido");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }

        public EmpresaModel CriarEmpresa()
        {
            if (Validar().Valido)
            {
                var empresa = new EmpresaModel
                {
                    RazaoSocial = RazaoSocial,
                    CNPJ = CNPJ,
                    NomeFantasia = NomeFantasia,
                    DataFundacao = DataFundacao,
                    NumeroFuncionarios = NumeroFuncionarios,
                    AreasTrabalho = new List<AreaTrabalhoModel>(),
                    Endereco = new EnderecoModel 
                    {
                        BairroEndereco = this.Endereco.BairroEndereco,
                        CepEndereco = this.Endereco.CepEndereco,
                        CidadeEndereco = this.Endereco.CidadeEndereco,
                        ComplementoEndereco = this.Endereco.ComplementoEndereco,
                        EstadoEndereco = this.Endereco.EstadoEndereco,
                        NumeroEndereco = this.Endereco.NumeroEndereco,
                        RuaEndereco = this.Endereco.RuaEndereco
                    }
                };

                foreach (var codigoAreaTrabalho in AreasTrabalho)
                {
                    empresa.AreasTrabalho.Add(new AreaTrabalhoModel { CodigoAreaTrabalho = codigoAreaTrabalho });
                }

                return empresa;
            }
            else
                throw new InvalidDataException("Colaborador inválido. Não pode ser instanciado");
        }
    }
}
