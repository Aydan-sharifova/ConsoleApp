using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories.Implementations
{
    public class StudentRepository : IRepository<Student>
    {
        public void Create(Student data)
        {
            if (data == null)
            {
                throw new StudentNullException("Student cannot be null");
            }

            AppDbContext<Student>.datas.Add(data);
        }

        public void Update(Student data)
        {
            if (data == null)
            {
                throw new StudentNullException("Student cannot be null");
            }

            for (int i = 0; i < AppDbContext<Student>.datas.Count; i++)
            {
                if (AppDbContext<Student>.datas[i].Id == data.Id)
                {
                    AppDbContext<Student>.datas[i].Name = data.Name;
                    AppDbContext<Student>.datas[i].Surname = data.Surname;
                    AppDbContext<Student>.datas[i].Age = data.Age;
                    AppDbContext<Student>.datas[i].GroupId = data.GroupId;
                    AppDbContext<Student>.datas[i].Group = data.Group;
                    return;
                }
            }

            throw new StudentNotFoundException("Student not found");
        }

        public void Delete(Student data)
        {
            if (data == null)
            {
                throw new StudentNullException("Student cannot be null");
            }

            int index = -1;

            for (int i = 0; i < AppDbContext<Student>.datas.Count; i++)
            {
                if (AppDbContext<Student>.datas[i].Id == data.Id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new StudentNotFoundException("Student not found");
            }

            AppDbContext<Student>.datas.RemoveAt(index);
        }

        public Student GetById(int id)
        {
            for (int i = 0; i < AppDbContext<Student>.datas.Count; i++)
            {
                if (AppDbContext<Student>.datas[i].Id == id)
                {
                    return AppDbContext<Student>.datas[i];
                }
            }

            throw new StudentNotFoundException("Student not found");
        }

        public List<Student> GetAll(Predicate<Student> predicate)
        {
            if (predicate == null)
            {
                return new List<Student>(AppDbContext<Student>.datas);
            }

            return AppDbContext<Student>.datas.FindAll(predicate);
        }

        public void RemoveAll()
        {
            AppDbContext<Student>.datas.Clear();
        }
    }
}
