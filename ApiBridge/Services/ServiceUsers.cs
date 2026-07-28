using ApiBridge.Models.Dto;

namespace ApiBridge.Services
{
    public class ServiceUsers
    {
        internal void Exists(SyncUserDto userDto)
        {
            throw new NotImplementedException();
        }

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
    }
}
