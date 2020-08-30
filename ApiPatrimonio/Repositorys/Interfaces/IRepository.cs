using ApiPatrimonio.Repositorys.Base;
using System.Collections.Generic;

namespace ApiPatrimonio.Repositorys.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        public List<T> GetAll();

        public T GetById(int id);

        public void Save(T entity);

        public void Delete(T entity);
    }
}
