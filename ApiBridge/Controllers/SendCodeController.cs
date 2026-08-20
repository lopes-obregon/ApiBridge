using ApiBridge.Models.Dto;
using ApiBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBridge.Controllers
{
    [ApiController] // Indica que esta classe é um controlador de API
    [Route("backend/v1")] // Define o prefixo da URL
    public class SendCodeController: ControllerBase
    {
        private readonly SendCodeService _sendCodeService;
        public SendCodeController(SendCodeService sendCodeService)
        {
          
            _sendCodeService = sendCodeService;
        }
        [HttpPost]
        [Route("send-code")] // Define o endpoint para enviar o código
        public async Task<IActionResult> SendCodeToEmailAsync([FromBody] SyncUserDto user)
        {
            bool sucess = false;
            sucess =  await _sendCodeService.SendCodToEmail(user); //aguarde o resultado
            if (sucess)
                return Ok(new { message = $"Código enviado para o email: {user.Email}" });
            else
                return BadRequest(new { sucess = sucess, message = "Algum Erro Enesperado!" });
        }
    }
}
