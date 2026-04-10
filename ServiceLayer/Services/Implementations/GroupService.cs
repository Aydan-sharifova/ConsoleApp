using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ServiceLayer.Services.Implementations
{
    public class GroupService : IGroupService
    {
      private GroupRepository _groupRepository;
      private  int _count = 1;
        public Groups Create(Groups group)
        {
            group.Id = _count;
            _groupRepository.Create(group);
            _count++;
            return group;
        }

        public Group Create(Group group)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Groups> GetAll(Predicate<Groups> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll(Predicate<Group> predicate)
        {
            throw new NotImplementedException();
        }

        public Groups GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Groups Update(int id, Groups group)
        {
            throw new NotImplementedException();
        }

        public Group Update(int id, Group group)
        {
            throw new NotImplementedException();
        }

        Group IGroupService.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
