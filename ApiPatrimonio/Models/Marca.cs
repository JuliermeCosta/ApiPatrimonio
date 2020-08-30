using ApiPatrimonio.Repositorys.Base;

namespace ApiPatrimonio.Models
{
    public class Marca : Entity
    {
        public string Nome { get; set; }

        public Marca()
        {
        }

        public Marca(string nome)
        {
            Nome = nome;
        }

        public Marca(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
