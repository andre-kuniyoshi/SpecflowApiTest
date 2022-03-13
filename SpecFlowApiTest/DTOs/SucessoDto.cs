
namespace SpecFlowApiTest.DTOs
{
    internal class SucessoDto<T> where T : class
    {
        public bool Sucesso { get; set; }
        public T Resultado { get; set; }
    }
}
