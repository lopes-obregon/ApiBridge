using ApiBridge.Models.Dto;

namespace ApiBridge.Services
{
    public class ServiceUsers
    {
        internal void Exists(SyncUserDto userDto)
        {
            throw new NotImplementedException();
        }
        // envia o usuario para o sistema de gerenciamento de asinantes de outros sistemas.
        internal async Task SendUser(SyncUserDto userDto)
        {
            string resultado = String.Empty;
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
        internal async Task UpdateUser(SyncUserDto userDto)
        {
            string resultado = String.Empty;
            HttpClient client = new HttpClient();
            //post por que o outro sistema espera um método do tipo post
            HttpResponseMessage response = await client.PostAsJsonAsync("https://gestor-mei-caminhoneiro-d1039.shrd00.internal.goskip.dev/backend/v1/sync-users", userDto);
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
    }
}
