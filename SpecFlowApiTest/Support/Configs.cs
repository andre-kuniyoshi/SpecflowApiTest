
namespace SpecFlowApiTest.Support
{
    internal static class Configs
    {
        public static string TokenA { get; private set; } = "";
        public static string TokenB { get; private set; } = "";
        public static string BaseUrl => "https://localhost:7193/api/";
        public static string TipoEventoTechTalkId => "5299245b-5798-4b50-938c-15a2d0d9b78c";
        public static string TipoEventoMeetUpId => "5299245B-5798-4B50-938C-15A2D0D9B78C";

        public static string TokenFactory(string tokenNome)
        {
            switch (tokenNome)
            {
                case nameof(TokenA):
                    return TokenA;
                
                case nameof(TokenB):
                    return TokenB;
                
                default:
                    throw new ArgumentException("Nome do token não existe.");
            }
        }

    }
}
