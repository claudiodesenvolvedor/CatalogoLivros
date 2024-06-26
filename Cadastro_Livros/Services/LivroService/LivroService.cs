﻿using Cadastro_Livros.DataContext;
using Cadastro_Livros.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ExceptionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute;

namespace Cadastro_Livros.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        public readonly ApplicationDbContext _context;
        public LivroService(ApplicationDbContext context)
        {
            _context = context; 
        }
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
                if (newLivro.Titulo.Length >= 40 ||
                    newLivro.Editora.Length >= 40 ||
                    newLivro.Preco.ToString().Length >= 5) {

                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Número de caracteres ultrapassado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                if (newLivro == null || newLivro.Titulo == "" || newLivro.Editora == ""
                    || newLivro.Edicao <= 0 || newLivro.AnoPublicacao == ""
                    || newLivro.Preco.ToString() == "")
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Favor preencher todos os campos";
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

                    serviceResponse.Mensagem = "Dados incluídos com sucesso!";
                }

                return serviceResponse;
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

                if (livro.Count() == 0)
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

        public async Task<ServiceResponse<ICollection<Livro>>> GetLivrosByAutor(int autorId)
        {
            ServiceResponse<ICollection<Livro>> serviceResponse = new();
            try
            {
                var autor = new Autor();
                
                autor = await _context.Autores
                    .Where(au => au.AutorId == autorId)
                    .FirstOrDefaultAsync();

                var livros = await _context.Livros
                        .Where(l => l.Autores.Contains(autor))
                        .Include(au => au.Autores)
                        .Include(ass => ass.Assuntos)
                        .ToListAsync();

                if (livros.Count == 0)
                {
                    serviceResponse.Dados = livros;
                    serviceResponse.Mensagem = "Não existe livros para esse autor";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Dados = livros;
                }
            }
            catch (Exception ex)
            {
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

                if (updateLivro.Titulo.Length >= 40 ||
                    updateLivro.Editora.Length >= 40 ||
                    updateLivro.Preco.ToString().Length >= 5)
                {

                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Número de caracteres ultrapassado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                if (updateLivro == null || updateLivro.Titulo == "" || updateLivro.Editora == ""
                    || updateLivro.Edicao <= 0 || updateLivro.AnoPublicacao == ""
                    || updateLivro.Preco.ToString() == "")
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Favor preencher todos os campos";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    livro.Titulo = updateLivro.Titulo;
                    livro.Editora = updateLivro.Editora;
                    livro.Edicao = updateLivro.Edicao;
                    livro.AnoPublicacao = updateLivro.AnoPublicacao;
                    livro.Preco = updateLivro.Preco;
                    livro.Autores = updateLivro.Autores;
                    livro.Assuntos = updateLivro.Assuntos;

                    var autores = await _context.Autores
                        .Where(au => updateLivro.Autores.Contains(au))
                        .ToListAsync();

                    var assuntos = await _context.Assuntos
                        .Where(ass => updateLivro.Assuntos.Contains(ass))
                        .ToListAsync();

                    livro.Autores = autores;
                    livro.Assuntos = assuntos;


                    _context.Livros.Update(livro);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Livros
                        .Include(ass => ass.Assuntos)
                        .Include(au => au.Autores)
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

                    serviceResponse.Mensagem = "Dados excluídos com sucesso.";
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
