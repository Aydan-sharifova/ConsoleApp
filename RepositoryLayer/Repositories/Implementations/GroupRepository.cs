using DomainLayer.Entities;
using RepositoryLayer.Repositories.Interfaces;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Data;

namespace RepositoryLayer.Repositories.Implementations
{
    public class GroupRepository : IRepository<Groups>
    {
        public void Create(Groups data)
        {
            try
            {
                if (data is null) throw new NotFoundException("Data isn't found");

                AppDbContext<Groups>.datas.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(Groups data)
        {
            throw new NotImplementedException();
        }

        public List<Groups> GetAll(Predicate<Groups> predicate)
        {
            throw new NotImplementedException();
        }

        public Groups GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Groups data)
        {
            throw new NotImplementedException();
        }
    }
}
