using Newtonsoft.Json;
using RestSharp;
using SpecFlowApiTest.DTOs;
using SpecFlowApiTest.DTOs.Request;
using SpecFlowApiTest.Support;

namespace SpecFlowApiTest.Hooks
{
    [Binding]
    internal class ListarEventosHook
    {
        [BeforeFeature("AdicionaListaEventos")]
        public static async Task AdicionaListaEventos()
        {
            var token = Utils.BuscaTokenValido(nameof(Configs.TokenA));

            var listaEventosData = File.ReadAllText("../../../Data/ListaEventosData.json");
        
             var listaEventos = JsonConvert.DeserializeObject<List<CadastroEventoRequestDto>>(listaEventosData);

            RestClient restClient = new RestClient(Configs.BaseUrl);

            foreach (var evento in listaEventos)
            {
                var restRequest = Utils.GerarRequisicao(Method.Post, $"eventos", token, evento);
                
                var restResponse = await restClient.ExecuteAsync(restRequest);

                var bodyResponse = JsonConvert.DeserializeObject<SucessoDto<CadastroEventoRequestDto>>(restResponse.Content);

                Assert.True(bodyResponse.Sucesso);
                Assert.Equal(201, (int)restResponse.StatusCode);
            }
            
        }
    }
}
