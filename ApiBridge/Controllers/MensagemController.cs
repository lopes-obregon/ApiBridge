using ApiBridge.Models.Dto;
using ApiBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBridge.Controllers
{
    [ApiController]
    [Route("backend/v1")]
    public class MensagemController: Controller
    {
        private readonly IConfiguration _configuration;
        public MensagemController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("mensagem")]
        public async  Task <IActionResult> SendMensage([FromBody] SyncUserDto user)
        {
            MensagemService mensagemService = new(_configuration);
            bool sucess = false;
            sucess = await mensagemService.SendMensagem(user);
            if (sucess)
                return Ok(new { messagem = $"Mensagem enviado para o Usuario {user.Name} com sucesso!" });
            else
                return BadRequest(new { messagem = $"Erro ao enviar a mensagem para o usuário {user.Name}!" });
        }
    }
}
