using ApiBridge.Models.Dto;
using System.Net.Http.Headers;
using String = System.String;

namespace ApiBridge.Services
{
    public class ServiceUsers
    {
        private  readonly IConfiguration _configuration;

        public ServiceUsers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal async Task<bool> DeleteUser(String externalId)
        {
            
            string urlCamioneiro = String.Empty;
            string token = String.Empty;
            string urlDelete = String.Empty;
            HttpClient client = new();
            HttpResponseMessage response;
            HttpRequestMessage httpRequestMessage;
           //verificação das configurações
            if (!String.IsNullOrEmpty(_configuration["ApiKeys:VlsolucoesiaApi"]))
                token = _configuration["ApiKeys:VlsolucoesiaApi"];
            if (!String.IsNullOrEmpty(_configuration["ApiUrls:CaminhoneiroApi"]))
                urlCamioneiro = _configuration["ApiUrls:CaminhoneiroApi"] + "subscribers";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                urlDelete = $"{urlCamioneiro}/{externalId}";
                response = await client.DeleteAsync(urlDelete);
                if (response.IsSuccessStatusCode)
                    return true;
            }catch(HttpRequestException ex)
            {
                return false;
            }
            return false;

        }

        internal void Exists(SyncUserDto userDto)
        {
            throw new NotImplementedException();
        }
        //obtem todo os usuarios do sistema de caminhoneiro
        internal async Task<bool> GetUsers(List<SyncUserDto> syncUserDtos)
        {
            string urlCamioneiro = System.String.Empty;
            string token = System.String.Empty;
            HttpClient client = new();
            List<SyncUserDto>? userDtos = new();
            if (!String.IsNullOrEmpty(_configuration["ApiUrls:CaminhoneiroApi"]) && !String.IsNullOrEmpty(_configuration["ApiKeys:VlsolucoesiaApi"]))
            { 
                urlCamioneiro = _configuration["ApiUrls:CaminhoneiroApi"] + "subscribers";
                token = _configuration["ApiKeys:VlsolucoesiaApi"];
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            userDtos = await client.GetFromJsonAsync<List<SyncUserDto>>(urlCamioneiro);
            if (userDtos is not null && userDtos.Any())
            {
                syncUserDtos.AddRange(userDtos);
                return true;
                
            }
            else
            {
                Console.WriteLine("Algo deu Errado!");
                return false;
            }
        }

        // envia o usuario para o sistema de gerenciamento de asinantes de outros sistemas.
        internal async Task SendUser(SyncUserDto userDto)
        {
            string resultado =  String.Empty;
            string urlVl =  String.Empty;
            string token =  String.Empty;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            if (!String.IsNullOrEmpty(_configuration["ApiUrls:VlApi"]) && !String.IsNullOrEmpty(_configuration["ApiKeys:VlsolucoesiaApi"]))
               { 
                urlVl = _configuration["ApiUrls:VlApi"];
                token = _configuration["ApiKeys:VlsolucoesiaApi"];
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.PostAsJsonAsync(urlVl, userDto);
            //verificação de status
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Usuário enviado com sucesso!");
                resultado = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Resultado: {resultado}");
            }
            else
            {
                Console.WriteLine($"Erro ao enviar usuário: {response.StatusCode}");
            }
        }

        internal void SyncUser(SyncUserDto userDto)
        {
            throw new NotImplementedException();
        }
        //enviar para outro o usuario atualizado; Para o sistema caminhoniro com o dados atualizados
        internal async Task<bool> UpdateUser(SyncUserDto userDto)
        {
            string resultado = System.String.Empty;
            string urlCaminhoneiro = String.Empty;
            string token = String.Empty;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            if (!String.IsNullOrEmpty(_configuration["ApiUrls:CaminhoneiroApi"]) && !String.IsNullOrEmpty(_configuration["ApiKeys:VlsolucoesiaApi"]))
            {
                urlCaminhoneiro = _configuration["ApiUrls:CaminhoneiroApi"] + "sync-users";
                token = _configuration["ApiKeys:VlsolucoesiaApi"];
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine($"url: {urlCaminhoneiro}");   
            //post por que o outro sistema espera um método do tipo post
           response = await client.PostAsJsonAsync(urlCaminhoneiro, userDto);
            //verificação de status
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Usuário enviado com sucesso!");
                resultado = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Resultado: {resultado}");
                return true;
            }
            else
            {
                string erroDetalhado = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro ao enviar usuário: {response.StatusCode}");
                Console.WriteLine($"Detalhes retornados pela API: {erroDetalhado}");
                return false;
            }

        }
    }
}
