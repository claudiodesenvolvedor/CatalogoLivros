using Cadastro_Livros.DataContext;
using Cadastro_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro_Livros.Services.LivroService
{
    public class LivroService(ApplicationDbContext context) : ILivroInterface
    {
        public readonly ApplicationDbContext _context = context;

        public async Task<ServiceResponse<ICollection<Livro>>> GetLivros()
        {
            ServiceResponse<ICollection<Livro>> serviceResponse = new();

            try
            {
                serviceResponse.Dados = await _context.Livros
                    .Include(ass => ass.Assuntos)
                    .Include(au => au.Autores)
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
        
        public async Task<ServiceResponse<ICollection<Livro>>> CreateLivro(Livro newLivro)
        {
            ServiceResponse<ICollection<Livro>> serviceResponse = new();
            // iMPLEMENTAR SE JÁ EXISTE O REGISTRO NO BANCO
            try
            {
                if (newLivro == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    var autores = await _context.Autores
                        .Where(au => newLivro.Autores.Contains(au))
                        .ToListAsync();

                    var assuntos = await _context.Assuntos
                        .Where(ass => newLivro.Assuntos.Contains(ass))
                        .ToListAsync();

                    newLivro.Autores = autores;
                    newLivro.Assuntos = assuntos;

                    _context.Add(newLivro);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Livros
                        .Include(ass => ass.Assuntos)
                        .Include(au => au.Autores)
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

        public async Task<ServiceResponse<ICollection<Livro>>> GetLivroByCod(int livroId)
        {
            ServiceResponse<ICollection<Livro>> serviceResponse = new();

            try
            {
                var livro = await _context.Livros
                    .Where(x => x.LivroId == livroId)
                    .Include(ass => ass.Assuntos)
                    .Include(au => au.Autores)
                    .ToListAsync();

                if (livro == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Dados = livro;
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

        public async Task<ServiceResponse<ICollection<Livro>>> UpdateLivro(Livro updateLivro)
        {
            ServiceResponse<ICollection<Livro>> serviceResponse = new();
            try
            {
                var livro = _context.Livros
                    .FirstOrDefault(x => x.LivroId == updateLivro.LivroId);

                if (livro == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    livro.Titulo = updateLivro.Titulo;
                    livro.Editora = updateLivro.Editora;
                    livro.Edicao = updateLivro.Edicao;
                    livro.AnoPublicacao = updateLivro.AnoPublicacao;
                    livro.Preco = updateLivro.Preco;

                    _context.Livros.Update(livro);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Livros
                        .Include(ass => ass.Assuntos)
                        .Include(au => au.Autores)
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

        public async Task<ServiceResponse<ICollection<Livro>>> DeleteLivro(int livroId)
        {
            ServiceResponse<ICollection<Livro>> serviceResponse = new();
            try
            {
                var livro = await _context.Livros
                    .FirstOrDefaultAsync(x => x.LivroId == livroId);

                if (livro == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    _context.Livros.Remove(livro);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Livros
                        .Include(ass => ass.Assuntos)
                        .Include(au => au.Autores)
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
