using RestSharp;
using System.Net;

namespace SpecFlowApiTest.Support
{
    internal static class Utils
    {
        public static Method ConverterParaMetodoHttp(string metodo)
        {
            switch (metodo.ToUpper())
            {
                case "POST":
                    return Method.Post;

                case "GET":
                    return Method.Get;
                    
                case "PUT":
                    return Method.Put;
                    
                case "DELETE":
                    return Method.Delete;
                    
                case "PATCH":
                    return Method.Patch;
                    
                default:
                    throw new ArgumentException("Método de requisição não foi informado corretamente!");
            }
        }

        public static HttpStatusCode ConverterParaStatusCode(string resStatus) 
        {
            switch (resStatus.ToUpper())
            {
                case "OK":
                    return HttpStatusCode.OK;

                case "CREATED":
                    return HttpStatusCode.Created;

                case "NOTFOUND":
                    return HttpStatusCode.NotFound;

                case "BADREQUEST":
                    return HttpStatusCode.BadRequest;

                case "FORBIDDEN":
                    return HttpStatusCode.Forbidden;

                case "NOCONTENT":
                    return HttpStatusCode.NoContent;

                default:
                    throw new ArgumentException("O status code da resposta não foi informado corretamente!");
            }
        }

        public static RestRequest GerarRequisicao(Method metodo, string rota, string token, object? jsonBody = null)
        {

            RestRequest restRequest = new RestRequest(rota, metodo);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {token}");


            if (restRequest.Method == Method.Post || restRequest.Method == Method.Put || restRequest.Method == Method.Patch)
            {
                restRequest.RequestFormat = DataFormat.Json;

                if(jsonBody != null)
                    restRequest.AddJsonBody(jsonBody);
            }

            return restRequest;
        }

        public static async Task<RestResponse> BuscaEntidade(string rota, string token, Guid id)
        {
            var rotaComId = $"{rota}/{id}";
            var request = GerarRequisicao(Method.Get, rotaComId, token);
            var client = new RestClient(Configs.BaseUrl);

            return await client.ExecuteAsync(request);
        }

        public static string BuscaTokenValido()
        {
            var token = Configs.token;

            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Precisa chumbar um token valido no arquivo de SpecFlowApiTest.Support.Configs");
            }

            return token;
        }
    }
}
