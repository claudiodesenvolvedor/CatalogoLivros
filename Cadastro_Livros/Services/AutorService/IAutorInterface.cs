using Cadastro_Livros.Models;

namespace Cadastro_Livros.Services.AutorService
{
    public interface IAutorInterface
    {
        Task<ServiceResponse<ICollection<Autor>>> GetAutores();
        Task<ServiceResponse<ICollection<Autor>>> CreateAutor(Autor newAutor);
        Task<ServiceResponse<ICollection<Autor>>> GetAutorByCod(int autorId);
        Task<ServiceResponse<ICollection<Autor>>> UpdateAutor(Autor updateAutor);
        Task<ServiceResponse<ICollection<Autor>>> DeleteAutor(int autorId);

    }
}
