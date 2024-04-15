using Cadastro_Livros.DataContext;
using Cadastro_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro_Livros.Services.AssuntoService
{
    public class AssuntoService : IAssuntoInterface
    {
        public readonly ApplicationDbContext _context;
        public AssuntoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ICollection<Assunto>>> GetAssuntos()
        {
            ServiceResponse<ICollection<Assunto>> serviceResponse = new();

            try
            {
                serviceResponse.Dados = await _context.Assuntos
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
        
        public async Task<ServiceResponse<ICollection<Assunto>>> CreateAssunto(Assunto newAssunto)
        {
            ServiceResponse<ICollection<Assunto>> serviceResponse = new();
            // iMPLEMENTAR SE JÁ EXISTE O REGISTRO NO BANCO
            try
            {
                if (newAssunto == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    _context.Add(newAssunto);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Assuntos
                        .ToListAsync();

                    serviceResponse.Mensagem = "Dados incluídos com sucesso";
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

        public async Task<ServiceResponse<ICollection<Assunto>>> GetAssuntoByCod(int assuntoId)
        {
            ServiceResponse<ICollection<Assunto>> serviceResponse = new();

            try
            {
                var assunto =  await _context.Assuntos
                    .Where(x => x.AssuntoId == assuntoId)
                    .ToListAsync();

                if (assunto == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Dados = assunto;
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

        public async Task<ServiceResponse<ICollection<Assunto>>> UpdateAssunto(Assunto updateAssunto)
        {
            ServiceResponse<ICollection<Assunto>> serviceResponse = new();

            try
            {
                var assunto = _context.Assuntos
                    .FirstOrDefault(x => x.AssuntoId == updateAssunto.AssuntoId);

                if (assunto == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    assunto.Descricao = updateAssunto.Descricao;

                    _context.Assuntos.Update(assunto);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Assuntos
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

        public async Task<ServiceResponse<ICollection<Assunto>>> DeleteAssunto(int assuntoId)
        {
            ServiceResponse<ICollection<Assunto>> serviceResponse = new();

            try
            {
                var assunto = await _context.Assuntos
                    .FirstOrDefaultAsync(x => x.AssuntoId == assuntoId);

                if (assunto == null)
                {
                    serviceResponse.Dados = [];
                    serviceResponse.Mensagem = "Dados não encontrado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    _context.Assuntos.Remove(assunto);
                    await _context.SaveChangesAsync();

                    serviceResponse.Dados = await _context.Assuntos
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
