using DomainLayer.Entities;

namespace ServiceLayer.Services.Interfaces
{
    public interface IGroupService
    {
        Groups Create(Groups group);
        Groups Update(int id, Groups group);
        void Delete(int id);
        Groups GetById(int id);
        List<Groups> GetAll(Predicate<Groups> predicate);
        List<Groups> GetAllByTeacher(string teacher);
        List<Groups> GetAllByRoom(string room);
    }
}
