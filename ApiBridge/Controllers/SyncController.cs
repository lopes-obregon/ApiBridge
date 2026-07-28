using Microsoft.AspNetCore.Mvc;
using ApiBridge.Models.Dto;
using ApiBridge.Services;

namespace ApiBridge.Controllers
{
    [ApiController]
    [Route("backend/v1")] // Define o prefixo da URL

    public class SyncController : ControllerBase
    {
        // A rota completa fica: POST http://localhost:8080/backend/v1/users
        [HttpPost("users")] // Define o endpoint para sincronizar o usuário
        public IActionResult SyncUser([FromBody] SyncUserDto userDto)
        {
            // Aqui você pode implementar a lógica para sincronizar o usuário com base nos dados recebidos
            Console.WriteLine($"[POCKETBASE SYNC] ID: {userDto.External_id} | Nome: {userDto.Name} | Email: {userDto.Email}");
            // Por exemplo, você pode salvar os dados em um banco de dados ou chamar outro serviço
            // Retorna uma resposta de sucesso (200 OK) com uma mensagem
            ServiceUsers serviceUsers = new ServiceUsers();
            //serviceUsers.Exists(userDto);
            //serviceUsers.SyncUser(userDto);
           serviceUsers.SendUser(userDto);
            return Ok(new { sucess = true, message = "Usuário sincronizado com sucesso!" });
        }
        [HttpPut("users")]
        public IActionResult AtualizarUsuario([FromBody] SyncUserDto userDto)
        {
            ServiceUsers service = new();
            if (userDto is null || String.IsNullOrEmpty(userDto.Name))
                return BadRequest(new { mensagem = "Dados invalidos" });
            else
            {
                service.UpdateUser(userDto);
            }
            return Ok(new { sucess = true, message = "Usuario atualizado com sucesso!" });
        }
    }
}


