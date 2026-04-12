using DomainLayer.Entities;
using RepositoryLayer.Repositories.Interfaces;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;

namespace RepositoryLayer.Repositories.Implementations
{
    public class GroupRepository : IRepository<Groups>
    {
        public void Create(Groups data)
        {
            if (data == null)
            {
                throw new GroupNullException("Group cannot be null");
            }

            AppDbContext<Groups>.datas.Add(data);
        }

        public void Delete(Groups data)
        {
            if (data == null)
            {
                throw new GroupNullException("Group cannot be null");
            }

            int index = -1;

            for (int i = 0; i < AppDbContext<Groups>.datas.Count; i++)
            {
                if (AppDbContext<Groups>.datas[i].Id == data.Id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new GroupNotFoundException("Group not found");
            }

            AppDbContext<Groups>.datas.RemoveAt(index);
        }

        public List<Groups> GetAll(Predicate<Groups> predicate)
        {
            if (predicate == null)
            {
                return new List<Groups>(AppDbContext<Groups>.datas);
            }

            return AppDbContext<Groups>.datas.FindAll(predicate);
        }

        public Groups GetById(int id)
        {
            for (int i = 0; i < AppDbContext<Groups>.datas.Count; i++)
            {
                if (AppDbContext<Groups>.datas[i].Id == id)
                {
                    return AppDbContext<Groups>.datas[i];
                }
            }

            throw new GroupNotFoundException("Group not found");
        }

        public void Update(Groups data)
        {
            if (data == null)
            {
                throw new GroupNullException("Group cannot be null");
            }

            for (int i = 0; i < AppDbContext<Groups>.datas.Count; i++)
            {
                if (AppDbContext<Groups>.datas[i].Id == data.Id)
                {
                    AppDbContext<Groups>.datas[i].Name = data.Name;
                    AppDbContext<Groups>.datas[i].Teacher = data.Teacher;
                    AppDbContext<Groups>.datas[i].Room = data.Room;
                    return;
                }
            }

            throw new GroupNotFoundException("Group not found");
        }
    }
}
