using System;
using System.Text.Json.Serialization;

namespace Repositorio.Dtos
{
    public class EmpresaDto
    {
        public DateTime data_alteracao;

        public DateTime data_criacao;

        public string email_usuario;

        [JsonPropertyName("codigo_usuario")]
        public int codigo_usuario { get; set; }
        
        [JsonPropertyName("codigo_empresa")] 
        public int codigo_empresa { get; set; }
        
        [JsonPropertyName("razao_social")] 
        public string razao_social { get; set; }
        [JsonPropertyName("cnpj")]
        public string cnpj { get; set; }
        [JsonPropertyName("nome_fantasia")]
        public string nome_fantasia { get; set; }
        [JsonPropertyName("data_fundacao")]
        public DateTime? data_fundacao { get; set; }
        [JsonPropertyName("numero_funcionarios")]
        public int? numero_funcionarios { get; set; }
    }
}
