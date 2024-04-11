
using System.Text.Json.Serialization;

namespace Cadastro_Livros.Models
{
    public class Assunto
    {
        public Assunto()
        {
            Livros = [];
        }

        public int AssuntoId { get; set; }
        public string? Descricao { get; set; }
        [JsonIgnore]
        public virtual ICollection<Livro> Livros { get; set; }

    }
}
