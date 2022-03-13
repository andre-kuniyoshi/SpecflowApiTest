
namespace SpecFlowApiTest.DTOs.Request
{
    internal class EditarEventoRequestDto
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Link { get; set; }
        public string? TipoEventoId { get; set; }
        public string? Apresentador { get; set; }
        public string? Inicio { get; set; }
        public string? Fim { get; set; }
    }
}
