using System;

namespace ApiPatrimonio.Repositorys.Base
{
    /// <summary>
    /// As Models usadas pelos repositórios devem ser herdeiras dessa classe
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }

        public virtual DateTime DataUltimaModificacao { get; set; }

        public virtual DateTime DataCriacao { get; set; }
    }
}
