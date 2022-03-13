namespace SpecFlowApiTest.DTOs.Response
{
    public class ListaEventoResponseDto
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public Guid? TipoEventoId { get; set; }
        public string? Apresentador { get; set; }
        public string? Inicio { get; set; }
        public string? Fim { get; set; }
    }
}
