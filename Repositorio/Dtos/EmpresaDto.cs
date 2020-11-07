using System;
using System.Text.Json.Serialization;

namespace Repositorio.Dtos
{
    public class EmpresaDto
    {
        public DateTime data_alteracao { get; set; }
        public DateTime data_criacao { get; set; }
        public string email_usuario { get; set; }
        public string telefone_usuario { get; set; }
        public bool entrevistado { get; set; }        
        public int codigo_usuario { get; set; }                
        public int codigo_empresa { get; set; }                
        public string razao_social { get; set; }        
        public string cnpj { get; set; }        
        public string nome_fantasia { get; set; }        
        public DateTime? data_fundacao { get; set; }        
        public int? numero_funcionarios { get; set; }
        public int codigo_endereco { get; set; }
        public string cidade_endereco { get; set; }
        public string bairro_endereco { get; set; }
        public string rua_endereco { get; set; }
        public string numero_endereco { get; set; }
        public string complemento_endereco { get; set; }
        public string cep_endereco { get; set; }
        public string estado_endereco { get; set; }
    }
}
