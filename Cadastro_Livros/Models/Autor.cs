
using System.Text.Json.Serialization;

namespace Cadastro_Livros.Models
{
    public class Autor
    {
        public Autor()
        {
            Livros = [];
        }

        public int AutorId { get; set; }
        public string? Nome { get; set; }
        [JsonIgnore]
        public virtual ICollection<Livro> Livros { get; set; }
    }
}
