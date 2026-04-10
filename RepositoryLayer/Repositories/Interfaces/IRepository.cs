using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories.Interfaces
{
  public  interface IRepository<T> where T : BaseEntity
    {
        void Create(T data);
        void Update(T data);
        void Delete(T data);
        T GetById(int id);

        List<T> GetAll(Predicate<T> predicate);

    }
}
