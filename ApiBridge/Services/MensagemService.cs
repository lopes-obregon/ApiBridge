using ApiBridge.Models.Dto;

namespace ApiBridge.Services
{
    public class MensagemService
    {
        private readonly IConfiguration _configuration;
        public MensagemService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal async Task<bool> SendMensagem(SyncUserDto user)
        {
            string token = String.Empty;
            string urlCaminhoneiro = String.Empty;
            HttpClient client = new();
            HttpResponseMessage response;
            if (!String.IsNullOrEmpty(_configuration["ApiUrls:CaminhoneiroApi"]) && !String.IsNullOrEmpty(_configuration["ApiKeys:VlsolucoesiaApi"]))
            {
                urlCaminhoneiro = _configuration["ApiUrls:CaminhoneiroApi"] + "mensagem"
                token = _configuration["ApiKeys:VlsolucoesiaApi"];
            }
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                response = await client.PostAsJsonAsync(urlCaminhoneiro, user);
                if (response.IsSuccessStatusCode)
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.ToString());
                return false;
            }
            return false;
        }
    }
}
