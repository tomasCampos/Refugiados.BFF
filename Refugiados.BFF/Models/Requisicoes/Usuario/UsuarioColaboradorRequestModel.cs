using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Refugiados.BFF.Models.Requisicoes.Usuario
{
    public class UsuarioColaboradorRequestModel : IRequestModel
    {
        public string NomeColaborador { get; set; }
        public string EmailUsuario { get; set; }
        public string TefoneUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string Escolaridade { get; set; }
        public string Nacionalidade { get; set; }
        public string AreaFormacao { get; set; }
        public DateTime? DataChegadaBrasil { get; set; }
        public DateTime? DataNascimento { get; set; }
        public List<int> Idiomas { get; set; }
        public List<int> AreasTrabalho { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(EmailUsuario))
                erros.Add("O campo email do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(SenhaUsuario))
                erros.Add("O campo senha do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(NomeColaborador))
                erros.Add("O campo nome do colaborador deve ser preenchido");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }

        public ColaboradorModel CriarColaborador() 
        {
            if (Validar().Valido)
            {
                var colaborador = new ColaboradorModel
                {
                    NomeColaborador = NomeColaborador,
                    DataNascimento = DataNascimento,
                    DataChegadaBrasil = DataChegadaBrasil,
                    Escolaridade = Escolaridade,
                    Nacionalidade = Nacionalidade,
                    AreaFormacao = AreaFormacao,
                    Idiomas = new List<IdiomaModel>(),
                    AreasTrabalho = new List<AreaTrabalhoModel>()
                };

                foreach (var codigoIdioma in Idiomas)
                {
                    colaborador.Idiomas.Add(new IdiomaModel { CodigoIdioma = codigoIdioma });
                }

                foreach (var codigoAreaTrabalho in AreasTrabalho)
                {
                    colaborador.AreasTrabalho.Add(new AreaTrabalhoModel { CodigoAreaTrabalho = codigoAreaTrabalho });
                }

                return colaborador;
            }
            else
                throw new InvalidDataException("Colaborador inválido. Não pode ser instanciado");
        }
    }
}
