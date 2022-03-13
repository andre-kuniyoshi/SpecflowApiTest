namespace SpecFlowApiTest.DTOs.Response
{
    public class CadastroEventoResponseDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid TipoEventoId { get; set; }
        public string Apresentador { get; set; }
        public string Link { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public bool Finalizado { get; set; }
    }
}
