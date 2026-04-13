using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private GroupRepository _groupRepository;
        private int _count;

        public GroupService()
        {
            _groupRepository = new GroupRepository();
            _count = GetNextId();
        }

        private int GetNextId()
        {
            List<Groups> groups = AppDbContext<Groups>.datas;

            if (groups.Count == 0)
            {
                return 1;
            }

            int maxId = groups[0].Id;

            for (int i = 1; i < groups.Count; i++)
            {
                if (groups[i].Id > maxId)
                {
                    maxId = groups[i].Id;
                }
            }

            return maxId + 1;
        }

        private bool IsAnyGroup(Groups group)
        {
            return true;
        }

        private void ValidateGroup(Groups group)
        {
            if (group == null)
            {
                throw new GroupNullException("Group cannot be null");
            }

            if (string.IsNullOrWhiteSpace(group.Name))
            {
                throw new GroupNameRequiredException("Group name is required");
            }


            if (string.IsNullOrWhiteSpace(group.Teacher))
            {
                throw new GroupTeacherRequiredException("Group teacher is required");
            }

            if (ContainsNumber(group.Teacher))
            {
                throw new GroupTeacherValidationException("Teacher name cannot contain numbers");
            }

            if (string.IsNullOrWhiteSpace(group.Room))
            {
                throw new GroupRoomRequiredException("Group room is required");
            }
        }

        private bool ContainsNumber(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public Groups Create(Groups group)
        {
            ValidateGroup(group);

            group.Id = _count;
            _groupRepository.Create(group);
            _count = _count + 1;
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

        public List<Groups> GetAll()
        {
            return _groupRepository.GetAll(IsAnyGroup);
        }

        public Groups GetById(int id)
        {
            return _groupRepository.GetById(id);
        }

        public Groups Update(int id, Groups group)
        {
            ValidateGroup(group);

            Groups dbGroup = _groupRepository.GetById(id);

            dbGroup.Name = group.Name;
            dbGroup.Teacher = group.Teacher;
            dbGroup.Room = group.Room;

            _groupRepository.Update(dbGroup);
            return dbGroup;
        }

        public List<Groups> GetAllByTeacher(string teacher)
        {
            if (teacher == null)
            {
                teacher = string.Empty;
            }

            List<Groups> groups = GetAll();
            List<Groups> result = new List<Groups>();

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Teacher != null &&
                    groups[i].Teacher.ToLower() == teacher.ToLower())
                {
                    result.Add(groups[i]);
                }
            }

            return result;
        }

        public List<Groups> GetAllByRoom(string room)
        {
            if (room == null)
            {
                room = string.Empty;
            }

            List<Groups> groups = GetAll();
            List<Groups> result = new List<Groups>();

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Room != null &&
                    groups[i].Room.ToLower() == room.ToLower())
                {
                    result.Add(groups[i]);
                }
            }

            return result;
        }

        public List<Groups> SearchByName(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            List<Groups> groups = GetAll();
            List<Groups> result = new List<Groups>();
            string searchText = text.ToLower();

            for (int i = 0; i < groups.Count; i++)
            {
                string groupName = string.Empty;

                if (groups[i].Name != null)
                {
                    groupName = groups[i].Name.ToLower();
                }

                if (groupName.Contains(searchText))
                {
                    result.Add(groups[i]);
                }
            }

            return result;
        }
    }
}
