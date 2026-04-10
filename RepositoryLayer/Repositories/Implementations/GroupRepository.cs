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
            if (data is null) throw new NotFoundException("Data isn't found");
            AppDbContext<Groups>.datas.Add(data);
        }

        public void Delete(Groups data)
        {
            if (data is null) throw new NotFoundException("Data isn't found");

            Groups? existingData = AppDbContext<Groups>.datas.FirstOrDefault(m => m.Id == data.Id);
            if (existingData is null) throw new NotFoundException("Group not found with this id");

            AppDbContext<Groups>.datas.Remove(existingData);
        }

        public List<Groups> GetAll(Predicate<Groups> predicate)
        {
            if (predicate is null)
            {
                return AppDbContext<Groups>.datas.ToList();
            }

            return AppDbContext<Groups>.datas.FindAll(predicate);
        }

        public Groups GetById(int id)
        {
            Groups? data = AppDbContext<Groups>.datas.FirstOrDefault(m => m.Id == id);
            if (data is null) throw new NotFoundException("Group not found with this id");

            return data;
        }

        public void Update(Groups data)
        {
            if (data is null) throw new NotFoundException("Data isn't found");

            Groups? existingData = AppDbContext<Groups>.datas.FirstOrDefault(m => m.Id == data.Id);
            if (existingData is null) throw new NotFoundException("Group not found with this id");

            existingData.Name = data.Name;
            existingData.Teacher = data.Teacher;
            existingData.Room = data.Room;
        }
    }
}
