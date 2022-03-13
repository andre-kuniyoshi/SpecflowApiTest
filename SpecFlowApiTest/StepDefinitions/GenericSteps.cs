using RestSharp;
using SpecFlowApiTest.Support;

namespace SpecFlowApiTest.StepDefinitions
{
    [Binding]
    public sealed class GenericSteps
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public GenericSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [Given(@"que estou autenticado no sistema")]
        public void GivenQueEstouAutenticadoNoSistema()
        {
            _featureContext["token"] = Utils.BuscaTokenValido();
        }

        [Given(@"a rota do endpoint é '([^']*)' e o método http é '([^']*)'")]
        public void GivenAUrlDoEndpointEEOMetodoHttpE(string rota, string metodo)
        {
            Method tipoMetodo = Utils.ConverterParaMetodoHttp(metodo);

            _scenarioContext["HttpMethod"] = tipoMetodo;
            _scenarioContext["Rota"] = rota;
        }

        [When(@"faco a requisição")]
        public async Task WhenFazerARequisicao()
        {
            var token = (string)_featureContext["token"];

            var rota = (string)_scenarioContext["Rota"];
            var method = (Method)_scenarioContext["HttpMethod"];

            object json = new { };
            if (method == Method.Post || method == Method.Put || method == Method.Patch)
            {
                json = _scenarioContext["DataBody"];
            }
            //var json = _scenarioContext["DataBody"];

            RestRequest restRequest = Utils.GerarRequisicao(method, rota, token, json);          

            RestClient restClient = new RestClient(Configs.BaseUrl);
            var restResponse = await restClient.ExecuteAsync(restRequest);
            _scenarioContext["response"] = restResponse;

        }

        [Then(@"retorna uma resposta com o status igual a '([^']*)'")]
        public void ThenRetornaUmaRespostaComOStatusIgual(string status)
        {
            var expectedResponseStatusCode = (int)Utils.ConverterParaStatusCode(status);

            var responseData = (RestResponse)_scenarioContext["response"];
            var statusCodeResponse = (int)responseData.StatusCode;

            Assert.Equal(statusCodeResponse, expectedResponseStatusCode);
        }

        [Then(@"com o campo sucesso do body da resposta igual a '([^']*)'")]
        public void ThenComOCampoSucessoDoBodyDaRespostaIgualA(string ehSucesso)
        {
            var responseData = (RestResponse)_scenarioContext["response"];
            var responseBody = responseData.Content;

            Assert.Contains($"\"sucesso\":{ehSucesso}", responseBody);
        }
    }
}
