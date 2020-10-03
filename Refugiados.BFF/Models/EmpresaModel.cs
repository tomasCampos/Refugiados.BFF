using System;

namespace Refugiados.BFF.Models
{
    public class EmpresaModel
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoUsuario { get; set; }
        public string RazaoSocial { get; set; }
        public string EmailContato { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime? DataFundacao { get; set; }
        public int? NumeroFuncionarios { get; set; }
    }
}
