using Cadastro_Livros.Models;
using Cadastro_Livros.Services.AssuntoService;
using Cadastro_Livros.Services.LivroService;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Cadastro_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController(ILivroInterface LivroInterface) : ControllerBase
    {
        private readonly ILivroInterface _livroInterface = LivroInterface;

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<ICollection<Livro>>>> GetLivros()
        {
            return Ok(await _livroInterface.GetLivros());
        }

        // GET: api/Livro/5
        [HttpGet("{livroId}")]
        public async Task<ActionResult<ServiceResponse<ICollection<Livro>>>> GetLivroByCod(int livroId)
        {
            return Ok(await _livroInterface.GetLivroByCod(livroId));
        }

        // POST: api/Livro
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ICollection<Livro>>>> CreateAssunto(Livro newLivro)
        {
            return Ok(await _livroInterface.CreateLivro(newLivro));

        }

        // PUT: api/Livro/5
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ICollection<Livro>>>> UpdateLivro(Livro updateLivro)
        {
            return Ok(await _livroInterface.UpdateLivro(updateLivro));
        }

        // DELETE: api/Livro/5
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<ICollection<Livro>>>> DeleteLivro(int livroId)
        {
            return Ok(await _livroInterface.DeleteLivro(livroId));
        }

    }
}
