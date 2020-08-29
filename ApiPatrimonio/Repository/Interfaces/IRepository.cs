using System.Collections.Generic;

namespace ApiPatrimonio.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();

        public T GetById(int id);

        public void Save(T entity);

        public void Delete(int id);
    }
}
