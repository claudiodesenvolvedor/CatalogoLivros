
namespace Cadastro_Livros.Models
{
    public class Livro
    {
        public Livro()
        {
            Assuntos = [];
            Autores = [];
        }

        public int LivroId { get; set; }
        public string? Titulo { get; set; }
        public string? Editora { get; set; }
        public int Edicao { get; set; }
        public string? AnoPublicacao { get; set; }
        public decimal Preco { get; set; }

        public virtual ICollection<Autor> Autores { get; set; }
        
        public virtual ICollection<Assunto> Assuntos { get; set; }

    }
}
