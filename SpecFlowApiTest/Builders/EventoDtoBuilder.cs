using SpecFlowApiTest.DTOs.Request;
using SpecFlowApiTest.Support;

namespace SpecFlowApiTest.Builders
{
    internal class EventoDtoBuilder
    {
        private readonly CadastroEventoRequestDto _eventoRequestDto;
        public EventoDtoBuilder()
        {
            _eventoRequestDto = new CadastroEventoRequestDto()
            {
                Titulo = $"Teste automatizado - evento valido",
                Descricao = "Teste automatizado de agendamento de um evento valido para servir de evento conflitante para outro evento.",
                TipoEventoId = Configs.TipoEventoTechTalkId,
                Apresentador = "Astro automatizado",
                Link = "https://google.com.br",
                Inicio = $"2022-07-25 13:00",
                Fim = $"2022-07-25 14:00"
            };
        }

        public EventoDtoBuilder Titulo(string titulo)
        {
            _eventoRequestDto.Titulo = titulo;
            return this;
        }
        public EventoDtoBuilder Descricao(string descricao)
        {
            _eventoRequestDto.Descricao = descricao;
            return this;
        }
        
        public EventoDtoBuilder Apresentador(string apresentador)
        {
            _eventoRequestDto.Apresentador = apresentador;
            return this;
        }
        public EventoDtoBuilder Link(string link)
        {
            _eventoRequestDto.Link = link;
            return this;
        }

        public EventoDtoBuilder DataInicio(string dataInicio)
        {
            _eventoRequestDto.Inicio = dataInicio;
            return this;
        }

        public EventoDtoBuilder DataFim(string dataFim)
        {
            _eventoRequestDto.Fim = dataFim;
            return this;
        }

        public EventoDtoBuilder TipoEventoId(string tipoEventoId)
        {
            _eventoRequestDto.TipoEventoId = tipoEventoId;
            return this;
        }

        public CadastroEventoRequestDto Build()
        {
            return _eventoRequestDto;
        }
    }
}
