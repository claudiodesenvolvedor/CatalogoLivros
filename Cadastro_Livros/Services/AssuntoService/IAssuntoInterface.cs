using Cadastro_Livros.Models;

namespace Cadastro_Livros.Services.AssuntoService
{
    public interface IAssuntoInterface
    {
        Task<ServiceResponse<ICollection<Assunto>>> GetAssuntos();
        Task<ServiceResponse<ICollection<Assunto>>> CreateAssunto(Assunto newAssunto);
        Task<ServiceResponse<ICollection<Assunto>>> GetAssuntoByCod(int assuntoId);
        Task<ServiceResponse<ICollection<Assunto>>> UpdateAssunto(Assunto updateAssunto);
        Task<ServiceResponse<ICollection<Assunto>>> DeleteAssunto(int assuntoId);
    }
}
