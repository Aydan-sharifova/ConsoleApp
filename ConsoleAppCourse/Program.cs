using ConsoleAppCourse.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;

namespace ConsoleAppCourse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GroupService groupService = new GroupService();
            StudentService studentService = new StudentService();

            ShowMenu();

            while (true)
            {
                int selectNumber = ReadInt("Select option number:");

                try
                {
                    switch (selectNumber)
                    {
                        case 1:
                            CreateGroup(groupService);
                            break;

                        case 2:
                            UpdateGroup(groupService);
                            break;

                        case 3:
                            DeleteGroup(groupService);
                            break;

                        case 4:
                            GetGroupById(groupService);
                            break;

                        case 5:
                            GetAllGroupsByTeacher(groupService);
                            break;

                        case 6:
                            GetAllGroupsByRoom(groupService);
                            break;

                        case 7:
                            GetAllGroups(groupService);
                            break;

                        case 8:
                            CreateStudent(studentService);
                            break;

                        case 9:
                            UpdateStudent(studentService);
                            break;

                        case 10:
                            GetStudentById(studentService);
                            break;

                        case 11:
                            DeleteStudent(studentService);
                            break;

                        case 12:
                            GetStudentsByAge(studentService);
                            break;

                        case 13:
                            GetStudentsByGroupId(studentService);
                            break;

                        case 14:
                            SearchGroupsByName(groupService);
                            break;

                        case 15:
                            SearchStudentsByNameOrSurname(studentService);
                            break;

                        default:
                            Helper.PrintConsole(ConsoleColor.Red, "Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Helper.PrintConsole(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void CreateGroup(GroupService groupService)
        {
            string name = ReadText("Enter group name:");
            string teacher = ReadText("Enter teacher name:");
            string room = ReadText("Enter room:");

            Groups group = new Groups
            {
                Name = name,
                Teacher = teacher,
                Room = room
            };

            Groups createdGroup = groupService.Create(group);
            PrintSingleGroup(createdGroup);
        }

        private static void UpdateGroup(GroupService groupService)
        {
            int id = ReadInt("Enter group id:");
            string name = ReadText("Enter new group name:");
            string teacher = ReadText("Enter new teacher name:");
            string room = ReadText("Enter new room:");

            Groups group = new Groups
            {
                Name = name,
                Teacher = teacher,
                Room = room
            };

            Groups updatedGroup = groupService.Update(id, group);
            PrintSingleGroup(updatedGroup);
        }

        private static void DeleteGroup(GroupService groupService)
        {
            int id = ReadInt("Enter group id:");

            groupService.Delete(id);
            Helper.PrintConsole(ConsoleColor.Green, "Group deleted successfully.");
        }

        private static void GetGroupById(GroupService groupService)
        {
            int id = ReadInt("Enter group id:");

            Groups group = groupService.GetById(id);
            PrintSingleGroup(group);
        }

        private static void GetAllGroupsByTeacher(GroupService groupService)
        {
            string teacher = ReadText("Enter teacher name:");

            List<Groups> groups = groupService.GetAllByTeacher(teacher);
            PrintManyGroups(groups);
        }

        private static void GetAllGroupsByRoom(GroupService groupService)
        {
            string room = ReadText("Enter room:");

            List<Groups> groups = groupService.GetAllByRoom(room);
            PrintManyGroups(groups);
        }

        private static void GetAllGroups(GroupService groupService)
        {
            List<Groups> groups = groupService.GetAll();
            PrintManyGroups(groups);
        }

        private static void CreateStudent(StudentService studentService)
        {
            string name = ReadText("Enter student name:");
            string surname = ReadText("Enter student surname:");
            int age = ReadInt("Enter student age:");
            int groupId = ReadInt("Enter student group id:");

            Student student = new Student
            {
                Name = name,
                Surname = surname,
                Age = age,
                GroupId = groupId
            };

            Student createdStudent = studentService.Create(student);
            PrintSingleStudent(createdStudent);
        }

        private static void UpdateStudent(StudentService studentService)
        {
            int id = ReadInt("Enter student id:");
            string name = ReadText("Enter new student name:");
            string surname = ReadText("Enter new student surname:");
            int age = ReadInt("Enter new student age:");
            int groupId = ReadInt("Enter new student group id:");

            Student student = new Student
            {
                Name = name,
                Surname = surname,
                Age = age,
                GroupId = groupId
            };

            Student updatedStudent = studentService.Update(id, student);
            PrintSingleStudent(updatedStudent);
        }

        private static void GetStudentById(StudentService studentService)
        {
            int id = ReadInt("Enter student id:");

            Student student = studentService.GetById(id);
            PrintSingleStudent(student);
        }

        private static void DeleteStudent(StudentService studentService)
        {
            int id = ReadInt("Enter student id:");

            studentService.Delete(id);
            Helper.PrintConsole(ConsoleColor.Green, "Student deleted successfully.");
        }

        private static void GetStudentsByAge(StudentService studentService)
        {
            int age = ReadInt("Enter age:");
            List<Student> students = studentService.GetAllByAge(age);
            PrintManyStudents(students);
        }

        private static void GetStudentsByGroupId(StudentService studentService)
        {
            int groupId = ReadInt("Enter group id:");
            List<Student> students = studentService.GetAllByGroupId(groupId);
            PrintManyStudents(students);
        }

        private static void SearchGroupsByName(GroupService groupService)
        {
            string text = ReadText("Enter group name text:");
            List<Groups> groups = groupService.SearchByName(text);
            PrintManyGroups(groups);
        }

        private static void SearchStudentsByNameOrSurname(StudentService studentService)
        {
            string text = ReadText("Enter student name or surname text:");
            List<Student> students = studentService.SearchByNameOrSurname(text);
            PrintManyStudents(students);
        }

        private static void PrintSingleGroup(Groups group)
        {
            Helper.PrintConsole(ConsoleColor.Green, $"Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
        }

        private static void PrintManyGroups(List<Groups> groups)
        {
            if (groups.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No groups found.");
                return;
            }

            foreach (Groups group in groups)
            {
                PrintSingleGroup(group);
            }
        }

        private static void PrintSingleStudent(Student student)
        {
            Helper.PrintConsole(ConsoleColor.Green, $"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age}, GroupId: {student.GroupId}");
        }

        private static void PrintManyStudents(List<Student> students)
        {
            if (students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No students found.");
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                PrintSingleStudent(students[i]);
            }
        }

        private static void ShowMenu()
        {
            Helper.PrintConsole(ConsoleColor.Green, "Select one option!");
            Helper.PrintConsole(ConsoleColor.Yellow, "1 - Create Group, 2 - Update group , 3 - Delete Group   , 4 - Get group  by id, 5 - Get all groups  by teacher , 6 - Get all groups by room, 7 - Get all groups   , 8 - Create Student  9 - Update Student   , 10- Get student  by id, 11 - Delete student,12 - Get students   by age, 13 - Get all students  by group id , 14- Search method for groups by name, 15 - Search method for students by name or surname");
        }

        private static string ReadText(string message)
        {
            Helper.PrintConsole(ConsoleColor.Cyan, message);
            string? value = Console.ReadLine();

            if (value == null)
            {
                return string.Empty;
            }

            return value;
        }

        private static int ReadInt(string message)
        {
            while (true)
            {
                Helper.PrintConsole(ConsoleColor.Cyan, message);
                string? value = Console.ReadLine();
                int result;

                if (int.TryParse(value, out result))
                {
                    return result;
                }

                Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid number.");
            }
        }
    }
}
