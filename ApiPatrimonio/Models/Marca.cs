using System;

namespace ApiPatrimonio.Models
{
    public class Marca
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime UltimaModificacao { get; set; }

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
