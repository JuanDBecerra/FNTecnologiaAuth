using Prueba.Domain.Interfaces;
using Prueba.Domain.Entities.Resources;
using System.Resources;

namespace Prueba.Application.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly ResourceManager _resourceManager;

        protected GenericService(IRepository<T> repository)
        {
            _repository = repository;
            _resourceManager = new ResourceManager(typeof(ValidationMessages));
        }

        public  IEnumerable<T> GetAll()
        {
            return  _repository.GetAll();
        }
        
        public  IQueryable<T> Get()
        {
            return  _repository.Get();
        }

        public  T GetById(int id)
        {
            return  _repository.GetById(id);
        }

        public  T Add(T entity)
        {
            return  _repository.Add(entity);
        }

        public  T Update(int id, T entity)
        {
            return  _repository.Update(id, entity);
        }

        public  bool Delete(int id)
        {
            return  _repository.Delete(id);
        }
    }
}
