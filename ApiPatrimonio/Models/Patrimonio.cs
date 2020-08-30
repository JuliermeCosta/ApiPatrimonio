using ApiPatrimonio.Repositorys.Base;
using System;

namespace ApiPatrimonio.Models
{
    public class Patrimonio : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int? NumeroTombo { get; set; }

        public int MarcaId { get; set; }

        public Patrimonio()
        {
        }

        public Patrimonio(string nome, string descricao, int marcaId)
        {
            Nome = nome;
            Descricao = descricao;
            NumeroTombo = new Random().Next(1, int.MaxValue);
            MarcaId = marcaId;
        }

        public Patrimonio(int id, string nome, string descricao, int marcaId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            NumeroTombo = new Random().Next(1, int.MaxValue);
            MarcaId = marcaId;
        }
    }
}
