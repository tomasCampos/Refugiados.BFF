using System;
using FluentValidation;

namespace Refugiados.BFF.Models
{
    public class EmpresaModel
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoUsuario { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
