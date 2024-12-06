using Core.Entities;
using Core.Inputs;
using Infrastructure.Gateways.Queue;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController(ILogger<ContactController> logger, BaseQueue<Contato> queue) : ControllerBase
    {

        private readonly ILogger<ContactController> _logger = logger;
        private readonly BaseQueue<Contato> _queue = queue;

        [HttpPut(Name = "contatos")]
        public IActionResult Get([FromBody] ContatoInputAtualizar input)
        {
            try 
            { 
                this._queue.Publish(new Contato()
                {
                    Id = input.Id,
                    ContatoNome = input.ContatoNome,
                    ContatoTelefone = input.ContatoTelefone,
                    ContatoEmail = input.ContatoEmail
                });
                return Ok();

            } catch (Exception err) 
            {
                return BadRequest(err);
            }
        }
    }
}
