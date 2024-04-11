using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cadastro_Livros.DataContext;
using Cadastro_Livros.Models;
using Cadastro_Livros.Services.AutorService;


namespace Cadastro_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorInterface _autorInterface;

        public AutorController(IAutorInterface autorInterface)
        {
            _autorInterface = autorInterface;
        }

        // GET: api/GetAutores
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<ICollection<Autor>>>> GetAutores()
        {
            return Ok(await _autorInterface.GetAutores());
        }

        // GET: api/Autor/5
        [HttpGet("{autorId}")]
        public async Task<ActionResult<ServiceResponse<ICollection<Autor>>>> GetAutorByCod(int autorId)
        {
            return Ok(await _autorInterface.GetAutorByCod(autorId));
        }

        // POST: api/Autor
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ICollection<Autor>>>> CreateAutor(Autor newAutor)
        {
            return Ok(await _autorInterface.CreateAutor(newAutor));
            
        }

        // PUT: api/Autor/
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ICollection<Autor>>>> UpdateAutor(Autor updateAutor)
        {
            return Ok(await _autorInterface.UpdateAutor(updateAutor));
        }

        // DELETE: api/Autor/5
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<ICollection<Autor>>>> DeleteAutor(int codau)
        {
            return Ok(await _autorInterface.DeleteAutor(codau));
        }

    }
}
