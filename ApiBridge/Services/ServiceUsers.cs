using ApiBridge.Models.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiBridge.Services
{
    public class ServiceUsers
    {
        internal async Task<bool> DeleteUser(SyncUserDto syncUserDto)
        {
            string urlCamioneiro = "https://gestor-mei-caminhoneiro-d1039.shrd00.internal.goskip.dev/backend/v1/subscribers";
            HttpClient client = new();
            HttpRequestMessage httpRequestMessage = new(HttpMethod.Delete, urlCamioneiro)
            {
                Content = JsonContent.Create(syncUserDto)
            };
            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
            return response.IsSuccessStatusCode;

        }

        internal void Exists(SyncUserDto userDto)
        {
            throw new NotImplementedException();
        }

        internal async Task<bool> GetUsers(List<SyncUserDto> syncUserDtos)
        {
            string urlCamioneiro = "https://gestor-mei-caminhoneiro-d1039.shrd00.internal.goskip.dev/backend/v1/subscribers";
            HttpClient client = new();
            List<SyncUserDto>? userDtos = new();
            
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
            string resultado =  System.String.Empty;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("https://gestao-de-empresa-de-sistemas-e4fd0.shrd00.internal.goskip.dev/backend/v1/sync-users", userDto);
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
        //enviar para outro o usuario atualizado
        internal async Task<bool> UpdateUser(SyncUserDto userDto)
        {
            string resultado = System.String.Empty;
            HttpClient client = new HttpClient();
            //post por que o outro sistema espera um método do tipo post
            HttpResponseMessage response = await client.PostAsJsonAsync("https://gestor-mei-caminhoneiro-d1039.shrd00.internal.goskip.dev/backend/v1/sync-users", userDto);
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
                Console.WriteLine($"Erro ao enviar usuário: {response.StatusCode}");
                return false;
            }

        }
    }
}
