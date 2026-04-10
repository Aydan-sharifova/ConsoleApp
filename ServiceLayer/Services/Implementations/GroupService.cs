using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly GroupRepository _groupRepository;
        private int _count;

        public GroupService()
        {
            _groupRepository = new GroupRepository();
            _count = AppDbContext<Groups>.datas.Count == 0
                ? 1
                : AppDbContext<Groups>.datas.Max(m => m.Id) + 1;
        }

        public Groups Create(Groups group)
        {
            ArgumentNullException.ThrowIfNull(group);

            group.Id = _count;
            _groupRepository.Create(group);
            _count++;
            return group;
        }

        public void Delete(int id)
        {
            Groups data = _groupRepository.GetById(id);
            _groupRepository.Delete(data);
        }

        public List<Groups> GetAll(Predicate<Groups> predicate)
        {
            return _groupRepository.GetAll(predicate);
        }

        public Groups GetById(int id)
        {
            return _groupRepository.GetById(id);
        }

        public Groups Update(int id, Groups group)
        {
            ArgumentNullException.ThrowIfNull(group);

            Groups dbGroup = _groupRepository.GetById(id);

            dbGroup.Name = group.Name;
            dbGroup.Teacher = group.Teacher;
            dbGroup.Room = group.Room;

            _groupRepository.Update(dbGroup);
            return dbGroup;
        }

        public List<Groups> GetAllByTeacher(string teacher)
        {
            return _groupRepository.GetAll(m =>
                string.Equals(m.Teacher, teacher, StringComparison.OrdinalIgnoreCase));
        }

        public List<Groups> GetAllByRoom(string room)
        {
            return _groupRepository.GetAll(m =>
                string.Equals(m.Room, room, StringComparison.OrdinalIgnoreCase));
        }
    }
}
