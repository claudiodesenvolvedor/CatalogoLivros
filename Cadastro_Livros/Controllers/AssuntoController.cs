using Microsoft.AspNetCore.Mvc;
using Cadastro_Livros.Models;
using Cadastro_Livros.Services.AssuntoService;
using NuGet.Protocol;

namespace Cadastro_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoInterface _assuntoInterface;

        public AssuntoController(IAssuntoInterface AssuntoInterface)
        {
            _assuntoInterface = AssuntoInterface;
        }

        // GET: api/Assuntos
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<ICollection<Assunto>>>> GetAssuntos()
        {
            return Ok(await _assuntoInterface.GetAssuntos());
        }

        // GET: api/Assunto/5
        [HttpGet("{assuntoId}")]
        public async Task<ActionResult<ServiceResponse<ICollection<Assunto>>>> GetAssuntoByCod(int assuntoId)
        {
            return Ok(await _assuntoInterface.GetAssuntoByCod(assuntoId));
        }

        // POST: api/Assunto
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ICollection<Assunto>>>> CreateAssunto(Assunto newAssunto)
        {
            return Ok(await _assuntoInterface.CreateAssunto(newAssunto));

        }

        // PUT: api/Assunto/5
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ICollection<Assunto>>>> UpdateAssunto(Assunto updateAssunto)
        {
            return Ok(await _assuntoInterface.UpdateAssunto(updateAssunto));
        }


        // DELETE: api/Assunto/5
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<ICollection<Assunto>>>> DeleteAssunto(int assuntoId)
        {
            return Ok(await _assuntoInterface.DeleteAssunto(assuntoId));
        }
    }
}
