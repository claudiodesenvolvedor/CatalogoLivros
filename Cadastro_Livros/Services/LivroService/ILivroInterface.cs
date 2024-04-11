using Cadastro_Livros.Models;

namespace Cadastro_Livros.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<ServiceResponse<ICollection<Livro>>> GetLivros();
        Task<ServiceResponse<ICollection<Livro>>> CreateLivro(Livro newLivro);
        Task<ServiceResponse<ICollection<Livro>>> GetLivroByCod(int livroId);
        Task<ServiceResponse<ICollection<Livro>>> UpdateLivro(Livro updateLivro);
        Task<ServiceResponse<ICollection<Livro>>> DeleteLivro(int livroId);

    }
}
