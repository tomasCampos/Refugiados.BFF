using Newtonsoft.Json;

namespace Repositorio.Dtos
{
    public class EmpresaDto
    {
        [JsonProperty("codigo_usuario")]
        public string codigo_usuario { get; set; }
        
        [JsonProperty("codigo_empresa")] 
        public string codigo_empresa { get; set; }
        
        [JsonProperty("razao_social")] 
        public string razao_social { get; set; }
    }
}
