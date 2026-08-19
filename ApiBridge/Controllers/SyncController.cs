using Microsoft.AspNetCore.Mvc;
using ApiBridge.Models.Dto;
using ApiBridge.Services;
using System.Linq.Expressions;

namespace ApiBridge.Controllers
{
    [ApiController]
    [Route("backend/v1")] // Define o prefixo da URL

    public class SyncController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SyncController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // A rota completa fica: POST http://localhost:8080/backend/v1/users
        [HttpPost("users")] // Define o endpoint para sincronizar o usuário
        public IActionResult SyncUser([FromBody] SyncUserDto userDto)
        {
            // Aqui você pode implementar a lógica para sincronizar o usuário com base nos dados recebidos
            Console.WriteLine($"[POCKETBASE SYNC] ID: {userDto.External_id} | Nome: {userDto.Name} | Email: {userDto.Email}");
            // Por exemplo, você pode salvar os dados em um banco de dados ou chamar outro serviço
            // Retorna uma resposta de sucesso (200 OK) com uma mensagem
            ServiceUsers serviceUsers = new ServiceUsers(_configuration);
            //serviceUsers.Exists(userDto);
            //serviceUsers.SyncUser(userDto);
           serviceUsers.SendUser(userDto);
            return Ok(new { sucess = true, message = "Usuário sincronizado com sucesso!" });
        }
        [HttpPut("users")]
        public async Task<IActionResult> AtualizarUsuarioAsync([FromBody] SyncUserDto userDto)
        {
            ServiceUsers service = new(_configuration);
            bool sucess = false;
            if (userDto is null || String.IsNullOrEmpty(userDto.Name))
                return BadRequest(new { mensagem = "Dados invalidos" });
            else
            {
                //adicionando o parametro para segurança
               sucess =  await service.UpdateUser(userDto);

            }
            if (sucess)
                return Ok(new { sucess = true, message = "Usuario atualizado com sucesso!" });
            else
                return BadRequest(new { sucess = false, message = $"Erro ao atualizar o usuario {userDto.Name} | ID: {userDto.External_id}" });
        }
        //tipo get ok.
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            //get vem do sistema principal para coletar os dados dos usuarios
            ServiceUsers serviceUsers = new(_configuration);
            List<SyncUserDto> syncUserDtos = new();
            bool sucess = false;
             sucess = await serviceUsers.GetUsers(syncUserDtos);
            /* foreach (var usr in syncUserDtos)
            {
                Console.WriteLine($"Id:{usr.External_id} | Nome: {usr.Name}");
            }*/
            if (sucess)
                return Ok(new { sucess = true, message = "Usuários sincrocizados!", users = syncUserDtos });
            else
                return BadRequest(new { sucess = false, message = "Algo deu errado!", usrs = syncUserDtos });
        }
        // em pausa para ajustar o restante com a camada de segurança.
        [HttpDelete("users/{external_id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute(Name = "external_id")] String externalId)
        {
            ServiceUsers serviceUsers = new(_configuration);
            if (String.IsNullOrEmpty(externalId))
                return BadRequest(new { message = "Id Obrigatório!" });
            //Console.WriteLine($"Entrou aqui com usuario {externalId}");
            bool sucess =  await serviceUsers.DeleteUser(externalId);
            if (sucess)
                return Ok(new { sucess = true, message = $"Usuário {externalId} Deletado com sucesso!" });
            else
                return BadRequest(new { sucess = false, message = $"Usuário {externalId} Erro ao deletar!" });
        }
    }
}


