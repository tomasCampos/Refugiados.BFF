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
    }
}
