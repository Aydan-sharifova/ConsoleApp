using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private StudentRepository _studentRepository;
        private GroupService _groupService;
        private int _count;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
            _groupService = new GroupService();
            _count = GetNextId();
        }

        private int GetNextId()
        {
            List<Student> students = AppDbContext<Student>.datas;

            if (students.Count == 0)
            {
                return 1;
            }

            int maxId = students[0].Id;

            for (int i = 1; i < students.Count; i++)
            {
                if (students[i].Id > maxId)
                {
                    maxId = students[i].Id;
                }
            }

            return maxId + 1;
        }

        private bool IsAnyStudent(Student student)
        {
            return true;
        }

        private void ValidateStudent(Student student)
        {
            if (student == null)
            {
                throw new StudentNullException("Student cannot be null");
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                throw new StudentNameRequiredException("Student name is required");
            }

            if (ContainsNumber(student.Name))
            {
                throw new StudentNameValidationException("Student name cannot contain numbers");
            }

            if (string.IsNullOrWhiteSpace(student.Surname))
            {
                throw new StudentSurnameRequiredException("Student surname is required");
            }

            if (ContainsNumber(student.Surname))
            {
                throw new StudentSurnameValidationException("Student surname cannot contain numbers");
            }

            if (student.Age < 18)
            {
                throw new StudentAgeValidationException("Student age cannot be less than 18");
            }

            if (student.GroupId <= 0)
            {
                throw new StudentGroupIdValidationException("Group id must be greater than 0");
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

        public Student Create(Student student)
        {
            ValidateStudent(student);

            Groups group = _groupService.GetById(student.GroupId);

            student.Id = _count;
            student.Group = group;
            _studentRepository.Create(student);
            _count = _count + 1;
            return student;
        }

        public Student Update(int id, Student student)
        {
            ValidateStudent(student);

            Student dbStudent = _studentRepository.GetById(id);
            Groups group = _groupService.GetById(student.GroupId);

            dbStudent.Name = student.Name;
            dbStudent.Surname = student.Surname;
            dbStudent.Age = student.Age;
            dbStudent.GroupId = student.GroupId;
            dbStudent.Group = group;

            _studentRepository.Update(dbStudent);
            return dbStudent;
        }

        public void Delete(int id)
        {
            Student student = _studentRepository.GetById(id);
            _studentRepository.Delete(student);
        }

        public void RemoveAll()
        {
            _studentRepository.RemoveAll();
        }

        public Student GetById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll(IsAnyStudent);
        }

        public List<Student> GetAllByAge(int age)
        {
            List<Student> students = GetAll();
            List<Student> result = new List<Student>();

            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].Age == age)
                {
                    result.Add(students[i]);
                }
            }

            return result;
        }

        public List<Student> GetAllByGroupId(int groupId)
        {
            List<Student> students = GetAll();
            List<Student> result = new List<Student>();

            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].GroupId == groupId)
                {
                    result.Add(students[i]);
                }
            }

            return result;
        }

        public List<Student> SearchByNameOrSurname(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            List<Student> students = GetAll();
            List<Student> result = new List<Student>();
            string searchText = text.ToLower();

            for (int i = 0; i < students.Count; i++)
            {
                string studentName = string.Empty;
                string studentSurname = string.Empty;

                if (students[i].Name != null)
                {
                    studentName = students[i].Name.ToLower();
                }

                if (students[i].Surname != null)
                {
                    studentSurname = students[i].Surname.ToLower();
                }

                if (studentName.Contains(searchText) || studentSurname.Contains(searchText))
                {
                    result.Add(students[i]);
                }
            }

            return result;
        }
    }
}
