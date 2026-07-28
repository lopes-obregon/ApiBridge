using Microsoft.AspNetCore.Mvc;

namespace ApiBridge.Controllers
{
    [ApiController]
    [Route("backend/v1")]
    public class Hello: ControllerBase
    {
        // // A rota completa fica: POST http://localhost:8080/backend/v1/hello
        [HttpGet("hello")]
        public IActionResult GetMensagem()
        {
            return Ok(new
            {
                mensagem = "Tudo Certo por aqui!",
                dataEnvio = DateTime.Now
            });
        }

    }
}
