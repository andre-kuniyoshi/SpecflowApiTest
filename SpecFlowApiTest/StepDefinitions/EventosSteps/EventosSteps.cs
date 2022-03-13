using Newtonsoft.Json;
using RestSharp;
using SpecFlowApiTest.Builders;
using SpecFlowApiTest.DTOs;
using SpecFlowApiTest.DTOs.Request;
using SpecFlowApiTest.DTOs.Response;
using SpecFlowApiTest.Support;

namespace SpecFlowApiTest.StepDefinitions.EventosSteps
{
    // TODO: Adicionar meio de analisar e validar o body campo a campo quando necessario de respostas de sucesso
    // TODO: Adicionar um meio de ler e validar o body quando vem erros de validacao 
    [Binding]
    internal class EventosSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public EventosSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        #region EventosStepsGenric

        [Given(@"que tenho um evento já agendado para o dia '([^']*)' para ser '([^']*)'")]
        public async Task GivenQueTenhoUmEventoJaAgendadoParaODiaParaSer(string dia, string acao)
        {
            var token = (string)_featureContext["token"];

            var eventoValido = new EventoDtoBuilder()
                .Titulo("Teste automatizado- evento valido")
                .Descricao($"Teste automatizado de agendamento de um evento valido para ser {acao}.")
                .DataInicio($"2022-05-{dia} 13:00")
                .DataFim($"2022-05-{dia} 13:30")
                .Build();

            var restRequest = Utils.GerarRequisicao(Method.Post, $"eventos", token, eventoValido);
            RestClient restClient = new RestClient(Configs.BaseUrl);

            var restResponse = await restClient.ExecuteAsync(restRequest);


            var bodyResponse = JsonConvert.DeserializeObject<SucessoDto<CadastroEventoRequestDto>>(restResponse.Content);

            Assert.True(bodyResponse.Sucesso);
            Assert.Equal(201, (int)restResponse.StatusCode);

            _scenarioContext["EventoId"] = bodyResponse.Resultado.Id;
        }


        #endregion

        #region Obter um evento

        [Given(@"que quero buscar um evento existente na agenda da Vaivoa")]
        public void GivenQueQueroBuscarEleNaAgendaDaVaivoa()
        {
            var eventoId = (Guid)_scenarioContext["EventoId"];

            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}/{eventoId}";

        }

        [Given(@"que quero tentar obter um evento inexistente na agenda da Vaivoa")]
        public void GivenQueQueroTentarUmEventoInexistenteNaAgendaDaVaivoa()
        {
            var eventoId = Guid.NewGuid();

            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}/{eventoId}";
        }


        #endregion

        #region Listar eventos

        [Given(@"que quero buscar os eventos entre '([^']*)' e '([^']*)'")]
        public void GivenQueQueroBuscarOsEventosEntreE(string dataInicio, string dataFim)
        {
            // TODO: Criar uma maneira de passar os query params usando metodo AddParameters do restsharp a partir de um obj de query
            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}?DataInicial={dataInicio}&DataFinal={dataFim}";
        }

        [Given(@"que quero buscar um evento com um tipo inexistente na agenda")]
        public void GivenQueQueroBuscarUmEventoComUmTipoInexistenteNaAgenda()
        {
            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}?DataInicial=&DataFinal=&TipoEventoId=fe87b1cd-b413-499e-b088-d0da60c376b5";
        }

        [Given(@"que quero buscar um evento com um tipo '([^']*)' na agenda")]
        public void GivenQueQueroBuscarUmEventoComUmTipoNaAgenda(string ehExistente)
        {
            throw new PendingStepException();
        }



        [Then(@"retorna '([^']*)' eventos")]
        public void ThenRetorna(int numEventos)
        {
            var response = (RestResponse)_scenarioContext["response"];

            var bodyJson = JsonConvert.DeserializeObject<SucessoDto<List<ListaEventoResponseDto>>>(response.Content);

            Assert.Equal(numEventos, bodyJson.Resultado.Count);
        }



        #endregion

        #region Adicionar evento

        [Given(@"que quero agendar um evento valido")]
        public void GivenQueroAgendarUmEventoValido()
        {
            var eventoValido = new EventoDtoBuilder().Build();

            _scenarioContext["DataBody"] = eventoValido;
        }

        [Given(@"que quero agendar um evento invalido")]
        public void GivenQueroAgendarUmEventoInvalido()
        {
            var eventoInvalido = new EventoDtoBuilder()
                .Titulo("Teste automatizado- evento com dados invalidos")
                .Descricao("Teste automatizado com campos nao preenchidos")
                .DataInicio("")
                .Build();
 
            _scenarioContext["DataBody"] = eventoInvalido;
        }

        [Given(@"que quero agendar um evento valido com um tipo inexistente no sistema")]
        public void GivenQueroAgendarUmEventoValidoComUmTipoInexistenteNoSistema()
        {
            var eventoTipoEventoInexistente = new EventoDtoBuilder()
                .Descricao("Teste automatizado de agendamento de um evento valido mas com um tipo de evento inexistente no sistema.")
                .TipoEventoId("21237ad0-16f3-4fb7-a84d-79ca9a6b7a3f")
                .DataInicio("2022-06-09 13:00")
                .DataFim("2022-06-09 13:30")
                .Build();

            _scenarioContext["DataBody"] = eventoTipoEventoInexistente;
        }


        [Given(@"que quero agendar um evento valido fora do horario permitido")]
        public void GivenQueroAgendarUmEventoValidoForaDoHorarioPermitido()
        {

            var eventoValidoForaHorario = new EventoDtoBuilder()
                .Descricao("Teste automatizado de agendamento de um evento valido mas com um tipo de evento inexistente no sistema.")
                .DataInicio("2022-06-25 16:00")
                .DataFim("2022-06-25 16:30")
                .Build();

            _scenarioContext["DataBody"] = eventoValidoForaHorario;
        }

        [Given(@"que ja existe um evento valido agendado no dia '([^']*)'")]
        public async Task GivenJaExisteUmEventoValidoAgendadoNoDia(string dia)
        {
            var token = (string)_featureContext["token"];

            var eventoValido = new EventoDtoBuilder()
                .Titulo($"Teste automatizado - evento valido conflitante dia {dia}")
                .Descricao("Teste automatizado de agendamento de um evento valido mas com um tipo de evento inexistente no sistema.")
                .DataInicio($"2022-07-{dia} 13:00")
                .DataFim($"2022-07-{dia} 14:00")
                .Build();

            RestClient restClient = new RestClient(Configs.BaseUrl);
            RestRequest restRequest = Utils.GerarRequisicao(Method.Post, "eventos", token, eventoValido);
            var restResponse = await restClient.ExecuteAsync(restRequest);
        }

        [Given(@"que quero agendar um evento valido no '([^']*)' das '([^']*)' até '([^']*)'")]
        public void GivenQueroAgendarUmEventoValidoNoDasAte(string dia, string horaInicio, string horaFim)
        {
            var eventoValido = new EventoDtoBuilder()
                .Titulo($"Teste automatizado - evento valido conflitante dia {dia}")
                .Descricao("Teste automatizado de agendamento de um evento valido.")
                .DataInicio($"2022-07-{dia} {horaInicio}")
                .DataFim($"2022-07-{dia} {horaFim}")
                .Build();

            _scenarioContext["DataBody"] = eventoValido;
        }
        #endregion

        #region Deletar evento

        [Given(@"quero deletar esse evento")]
        public void GivenQueroDeletarEsseEvento()
        {
            var id = (Guid)_scenarioContext["EventoId"];
            _scenarioContext["Rota"] = $"eventos/{id}";
        }

        [Given(@"quero tentar deletar esse evento passando id errado")]
        public void GivenQueroTentarDeletarEsseEventoPassandoIdErrado()
        {
            var id = "c698bda5-9dd5-4318-b68f-5cebb0fc659b";
            _scenarioContext["Rota"] = $"eventos/{id}";
        }

        [Then(@"esse evento não existe mais na agenda de eventos da vaivoa")]
        public async Task ThenEsseEventoNaoExisteMaisNaAgendaDeEventosDaVaivoa()
        {
            var token = (string)_featureContext["token"];

            var eventoId = (Guid)_scenarioContext["EventoId"];

            var restResponse = await Utils.BuscaEntidade("eventos", token, eventoId);

            var bodyResponse = JsonConvert.DeserializeObject<SucessoDto<ListaEventoResponseDto>>(restResponse.Content);

            Assert.False(bodyResponse.Sucesso);
            Assert.Equal(404, (int)restResponse.StatusCode);
        }

        [Then(@"esse evento ainda está na agenda de eventos da vaivoa")]
        public async Task ThenEsseEventoAindaEstaNaAgendaDeEventosDaVaivoa()
        {
            var token = (string)_featureContext["token"];

            var eventoId = (Guid)_scenarioContext["EventoId"];

            var restResponse = await Utils.BuscaEntidade("eventos", token, eventoId);

            var bodyResponse = JsonConvert.DeserializeObject<SucessoDto<ListaEventoResponseDto>>(restResponse.Content);

            Assert.True(bodyResponse.Sucesso);
            Assert.Equal(200, (int)restResponse.StatusCode);
            Assert.Equal(eventoId, bodyResponse.Resultado.Id);
        }

        #endregion

        #region Confirmar evento

        [Given(@"quero finalizar esse evento")]
        public void GivenQueroFinalizarEsseEvento()
        {
            var eventoId = (Guid)_scenarioContext["EventoId"];
            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}/{eventoId}/Finalizar";
            _scenarioContext["DataBody"] = new { linkGravacao = "https://finalizando.evento.com" };

        }

        [Given(@"que já foi finalizado esse evento")]
        public async Task GivenQueJaFoiFinalizadoEsseEvento()
        {
            var token = (string)_featureContext["token"];

            var linkEvento = new { linkGravacao = "https://finalizando.evento.com" };
            var idEvento = (Guid)_scenarioContext["EventoId"];
            var restRequest = Utils.GerarRequisicao(Method.Patch, $"eventos/{idEvento}/Finalizar", token, linkEvento);
            RestClient restClient = new RestClient(Configs.BaseUrl);

            var restResponse = await restClient.ExecuteAsync(restRequest);

            Assert.Equal(204, (int)restResponse.StatusCode);

            _scenarioContext["DataBody"] = new { linkGravacao = "https://finalizando.evento.com" };
            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}/{idEvento}/Finalizar";
        }

        [Given(@"quero tentar finalizr com um id inexistente")]
        public void GivenQueroTentarFinalizrComUmIdInexistente()
        {
            var eventoId = Guid.NewGuid();
            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}/{eventoId}/Finalizar";
            _scenarioContext["DataBody"] = new { linkGravacao = "https://finalizando.evento.com" };
        }

        [Given(@"quero tentar finalizr com um link invalido")]
        public void GivenQueroTentarFinalizrComUmLinkInvalido()
        {
            var eventoId = (Guid)_scenarioContext["EventoId"];
            _scenarioContext["Rota"] = $"{_scenarioContext["Rota"]}/{eventoId}/Finalizar";
            _scenarioContext["DataBody"] = new { linkGravacao = "finalizando.evento.com" };
        }


        #endregion
    }
}
