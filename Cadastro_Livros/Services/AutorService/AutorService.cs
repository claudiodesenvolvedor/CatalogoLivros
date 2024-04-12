using Cadastro_Livros.DataContext;
using Cadastro_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro_Livros.Services.AutorService
{
    public class AutorService : IAutorInterface
    {
        public readonly ApplicationDbContext _context;
        public AutorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ICollection<Autor>>> GetAutores()
        {
            ServiceResponse<ICollection<Autor>> serviceResponse = new();

            try
            {
                serviceResponse.Dados = await _context.Autores
                    .ToListAsync();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Nenhum dado encontrado";
                    serviceResponse.Sucesso = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Dados = [];
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<Autor>>> CreateAutor(Autor newAutor)
        {
            ServiceResponse<ICollection<Autor>> serviceResponse = new();
            // iMPLEMENTAR SE JÁ EXISTE O REGISTRO NO BANCO
            try
            {
                if (newAutor == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    _context.Add(newAutor);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Autores
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Dados = [];
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<Autor>>> GetAutorByCod(int autorId)
        {
            ServiceResponse<ICollection<Autor>> serviceResponse = new();

            try
            {
                var autor = await _context.Autores
                    .Where(x => x.AutorId == autorId)
                    .ToListAsync();

                if (autor == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Dados = autor;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Dados = [];
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ICollection<Autor>>> UpdateAutor(Autor updateAutor)
        {
            ServiceResponse<ICollection<Autor>> serviceResponse = new();

            try
            {
                var autor = _context.Autores
                    .FirstOrDefault(x => x.AutorId == updateAutor.AutorId);

                if (autor == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    autor.Nome = updateAutor.Nome;

                    _context.Autores.Update(autor);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Autores
                        .ToListAsync();
                    serviceResponse.Mensagem = "Dados alterados com sucesso";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Dados = [];
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async  Task<ServiceResponse<ICollection<Autor>>> DeleteAutor(int autorId)
        {
            ServiceResponse<ICollection<Autor>> serviceResponse = new();

            try
            {
                var autor = await _context.Autores
                    .FirstOrDefaultAsync(x => x.AutorId == autorId);

                if (autor == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    _context.Autores.Remove(autor);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Autores
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Dados = [];
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

    }
}
