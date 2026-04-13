using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;

namespace ConsoleAppCourse
{
    internal class Program
    {
        private const ConsoleColor GroupOptionColor = ConsoleColor.Cyan;
        private const ConsoleColor StudentOptionColor = ConsoleColor.Blue;
        private const ConsoleColor SearchOptionColor = ConsoleColor.Yellow;
        private const ConsoleColor DeleteOptionColor = ConsoleColor.DarkRed;

        static void Main(string[] args)
        {
            GroupService groupService = new GroupService();
            StudentService studentService = new StudentService();

            ShowMenu();

            while (true)
            {
                int selectNumber = InputInt("Select option number:", ConsoleColor.White);

                switch (selectNumber)
                {
                    case 1:
                        CreateGroup(groupService);
                        goto case 100;

                    case 2:
                        UpdateGroup(groupService);
                        goto case 100;

                    case 3:
                        DeleteGroup(groupService);
                        goto case 100;

                    case 4:
                        GetGroupById(groupService);
                        goto case 100;

                    case 5:
                        GetAllGroupsByTeacher(groupService);
                        goto case 100;

                    case 6:
                        GetAllGroupsByRoom(groupService);
                        goto case 100;

                    case 7:
                        GetAllGroups(groupService);
                        goto case 100;

                    case 8:
                        CreateStudent(studentService, groupService);
                        goto case 100;

                    case 9:
                        UpdateStudent(studentService, groupService);
                        goto case 100;

                    case 10:
                        GetStudentById(studentService);
                        goto case 100;

                    case 11:
                        DeleteStudent(studentService);
                        goto case 100;

                    case 12:
                        GetStudentsByAge(studentService);
                        goto case 100;

                    case 13:
                        GetStudentsByGroupId(studentService);
                        goto case 100;

                    case 14:
                        SearchGroupsByName(groupService);
                        goto case 100;

                    case 15:
                        SearchStudentsByNameOrSurname(studentService);
                        goto case 100;

                    case 16:
                        RemoveAllStudents(studentService);
                        goto case 100;

                    case 17:
                        GetAllStudents(studentService);
                        goto case 100;

                    case 100:
                        Console.WriteLine();
                        ShowMenu();
                        break;

                    default:
                        WriteColor(ConsoleColor.Red, "Invalid option.");
                        goto case 100;
                }
            }
        }

        private static void CreateGroup(GroupService groupService)
        {
            while (true)
            {
                Groups group = new Groups
                {
                    Name = InputRequiredText("Enter group name:", "Group name is required", GroupOptionColor),
                    Teacher = InputNameWithoutNumber("Enter teacher name:", "Group teacher is required", "Teacher name cannot contain numbers", GroupOptionColor),
                    Room = InputRequiredText("Enter room:", "Group room is required", GroupOptionColor)
                };

                try
                {
                    Groups createdGroup = groupService.Create(group);
                    WriteColor(ConsoleColor.Green, "Group created successfully.");
                    PrintSingleGroup(createdGroup);
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void UpdateGroup(GroupService groupService)
        {
            while (true)
            {
                int id = InputPositiveInt("Enter group id:", "Group id must be greater than 0", GroupOptionColor);

                Groups currentGroup;
                try
                {
                    currentGroup = groupService.GetById(id);
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                    continue;
                }

                Groups group = new Groups
                {
                    Name = InputOptionalRequiredText("Change group name? (y/n):", "Enter new group name:", currentGroup.Name, "Group name is required", GroupOptionColor),
                    Teacher = InputOptionalNameWithoutNumber("Change teacher name? (y/n):", "Enter new teacher name:", currentGroup.Teacher, "Group teacher is required", "Teacher name cannot contain numbers", GroupOptionColor),
                    Room = InputOptionalRequiredText("Change room? (y/n):", "Enter new room:", currentGroup.Room, "Group room is required", GroupOptionColor)
                };

                try
                {
                    Groups updatedGroup = groupService.Update(id, group);
                    WriteColor(ConsoleColor.Green, "Group updated successfully.");
                    PrintSingleGroup(updatedGroup);
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void DeleteGroup(GroupService groupService)
        {
            while (true)
            {
                int id = InputPositiveInt("Enter group id:", "Group id must be greater than 0", DeleteOptionColor);

                try
                {
                    groupService.Delete(id);
                    WriteColor(ConsoleColor.Green, "Group deleted successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void GetGroupById(GroupService groupService)
        {
            while (true)
            {
                int id = InputPositiveInt("Enter group id:", "Group id must be greater than 0", GroupOptionColor);

                try
                {
                    Groups group = groupService.GetById(id);
                    PrintSingleGroup(group);
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void GetAllGroupsByTeacher(GroupService groupService)
        {
            string teacher = InputRequiredText("Enter teacher name:", "Teacher name is required", GroupOptionColor);
            List<Groups> groups = groupService.GetAllByTeacher(teacher);
            PrintManyGroups(groups);
        }

        private static void GetAllGroupsByRoom(GroupService groupService)
        {
            string room = InputRequiredText("Enter room:", "Room is required", GroupOptionColor);
            List<Groups> groups = groupService.GetAllByRoom(room);
            PrintManyGroups(groups);
        }

        private static void GetAllGroups(GroupService groupService)
        {
            List<Groups> groups = groupService.GetAll();
            PrintManyGroups(groups);
        }

        private static void CreateStudent(StudentService studentService, GroupService groupService)
        {
            while (true)
            {
                Student student = new Student
                {
                    Name = InputNameWithoutNumber("Enter student name:", "Student name is required", "Student name cannot contain numbers", StudentOptionColor),
                    Surname = InputNameWithoutNumber("Enter student surname:", "Student surname is required", "Student surname cannot contain numbers", StudentOptionColor),
                    Age = InputAge("Enter student age:", StudentOptionColor),
                    GroupId = InputExistingGroupId("Enter student group id:", StudentOptionColor, groupService)
                };

                try
                {
                    Student createdStudent = studentService.Create(student);
                    WriteColor(ConsoleColor.Green, "Student created successfully.");
                    PrintSingleStudent(createdStudent);
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void UpdateStudent(StudentService studentService, GroupService groupService)
        {
            while (true)
            {
                int id = InputPositiveInt("Enter student id:", "Student id must be greater than 0", StudentOptionColor);

                Student currentStudent;
                try
                {
                    currentStudent = studentService.GetById(id);
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                    continue;
                }

                Student student = new Student
                {
                    Name = InputOptionalNameWithoutNumber("Change student name? (y/n):", "Enter new student name:", currentStudent.Name, "Student name is required", "Student name cannot contain numbers", StudentOptionColor),
                    Surname = InputOptionalNameWithoutNumber("Change student surname? (y/n):", "Enter new student surname:", currentStudent.Surname, "Student surname is required", "Student surname cannot contain numbers", StudentOptionColor),
                    Age = InputOptionalAge("Change student age? (y/n):", "Enter new student age:", currentStudent.Age, StudentOptionColor),
                    GroupId = InputOptionalExistingGroupId("Change student group id? (y/n):", "Enter new student group id:", currentStudent.GroupId, StudentOptionColor, groupService)
                };

                try
                {
                    Student updatedStudent = studentService.Update(id, student);
                    WriteColor(ConsoleColor.Green, "Student updated successfully.");
                    PrintSingleStudent(updatedStudent);
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void GetStudentById(StudentService studentService)
        {
            while (true)
            {
                int id = InputPositiveInt("Enter student id:", "Student id must be greater than 0", StudentOptionColor);

                try
                {
                    Student student = studentService.GetById(id);
                    PrintSingleStudent(student);
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void DeleteStudent(StudentService studentService)
        {
            while (true)
            {
                int id = InputPositiveInt("Enter student id:", "Student id must be greater than 0", DeleteOptionColor);

                try
                {
                    studentService.Delete(id);
                    WriteColor(ConsoleColor.Green, "Student deleted successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static void GetStudentsByAge(StudentService studentService)
        {
            int age = InputAge("Enter age:", StudentOptionColor);
            List<Student> students = studentService.GetAllByAge(age);
            PrintManyStudents(students);
        }

        private static void GetStudentsByGroupId(StudentService studentService)
        {
            int groupId = InputPositiveInt("Enter group id:", "Group id must be greater than 0", StudentOptionColor);
            List<Student> students = studentService.GetAllByGroupId(groupId);
            PrintManyStudents(students);
        }

        private static void SearchGroupsByName(GroupService groupService)
        {
            string text = InputRequiredText("Enter group name text:", "Search text is required", SearchOptionColor);
            List<Groups> groups = groupService.SearchByName(text);
            PrintManyGroups(groups);
        }

        private static void SearchStudentsByNameOrSurname(StudentService studentService)
        {
            string text = InputRequiredText("Enter student name or surname text:", "Search text is required", SearchOptionColor);
            List<Student> students = studentService.SearchByNameOrSurname(text);
            PrintManyStudents(students);
        }

        private static void RemoveAllStudents(StudentService studentService)
        {
            bool isSure = InputYesOrNo("Do you want to remove all students? (y/n):", DeleteOptionColor);

            if (!isSure)
            {
                WriteColor(ConsoleColor.Yellow, "Operation cancelled.");
                return;
            }

            studentService.RemoveAll();
            WriteColor(ConsoleColor.Green, "All students removed successfully.");
        }

        private static void GetAllStudents(StudentService studentService)
        {
            List<Student> students = studentService.GetAll();
            PrintManyStudents(students);
        }

        private static void PrintSingleGroup(Groups group)
        {
            WriteColor(GroupOptionColor, $"Id: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
        }

        private static void PrintManyGroups(List<Groups> groups)
        {
            if (groups.Count == 0)
            {
                WriteColor(ConsoleColor.Yellow, "No groups found.");
                return;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                PrintSingleGroup(groups[i]);
            }
        }

        private static void PrintSingleStudent(Student student)
        {
            string groupName = "-";

            if (student.Group != null && !string.IsNullOrWhiteSpace(student.Group.Name))
            {
                groupName = student.Group.Name;
            }

            WriteColor(StudentOptionColor, $"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age}, GroupId: {student.GroupId}, GroupName: {groupName}");
        }

        private static void PrintManyStudents(List<Student> students)
        {
            if (students.Count == 0)
            {
                WriteColor(ConsoleColor.Yellow, "No students found.");
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                PrintSingleStudent(students[i]);
            }
        }

        private static void ShowMenu()
        {
            WriteColor(ConsoleColor.Green, "Select one option!");
            WriteColor(GroupOptionColor, "1 - Create Group");
            WriteColor(GroupOptionColor, "2 - Update Group");
            WriteColor(DeleteOptionColor, "3 - Delete Group");
            WriteColor(GroupOptionColor, "4 - Get group by id");
            WriteColor(GroupOptionColor, "5 - Get all groups by teacher");
            WriteColor(GroupOptionColor, "6 - Get all groups by room");
            WriteColor(GroupOptionColor, "7 - Get all groups");
            WriteColor(StudentOptionColor, "8 - Create Student");
            WriteColor(StudentOptionColor, "9 - Update Student");
            WriteColor(StudentOptionColor, "10 - Get student by id");
            WriteColor(DeleteOptionColor, "11 - Delete student");
            WriteColor(StudentOptionColor, "12 - Get students by age");
            WriteColor(StudentOptionColor, "13 - Get all students by group id");
            WriteColor(SearchOptionColor, "14 - Search groups by name");
            WriteColor(SearchOptionColor, "15 - Search students by name or surname");
            WriteColor(DeleteOptionColor, "16 - Remove all students");
            WriteColor(StudentOptionColor, "17 - Get all students");
        }

        private static string InputText(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message + " ");
            Console.ResetColor();

            string? value = Console.ReadLine();
            if (value == null)
            {
                return string.Empty;
            }

            return value.Trim();
        }

        private static int InputInt(string message, ConsoleColor color)
        {
            while (true)
            {
                string value = InputText(message, color);
                int number;

                if (value.Contains(" "))
                {
                    WriteColor(ConsoleColor.Red, "Please enter only numbers without spaces.");
                    continue;
                }

                if (int.TryParse(value, out number))
                {
                    return number;
                }

                WriteColor(ConsoleColor.Red, "Please enter only numbers.");
            }
        }

        private static int InputPositiveInt(string message, string errorMessage, ConsoleColor color)
        {
            while (true)
            {
                int value = InputInt(message, color);

                if (value > 0)
                {
                    return value;
                }

                WriteColor(ConsoleColor.Red, errorMessage);
            }
        }

        private static int InputAge(string message, ConsoleColor color)
        {
            while (true)
            {
                int age = InputInt(message, color);

                if (age >= 18)
                {
                    return age;
                }

                WriteColor(ConsoleColor.Red, "Student age cannot be less than 18");
            }
        }

        private static string InputRequiredText(string message, string errorMessage, ConsoleColor color)
        {
            while (true)
            {
                string value = InputText(message, color);

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                WriteColor(ConsoleColor.Red, errorMessage);
            }
        }

        private static string InputNameWithoutNumber(string message, string requiredError, string numberError, ConsoleColor color)
        {
            while (true)
            {
                string value = InputRequiredText(message, requiredError, color);

                if (ContainsNumber(value))
                {
                    WriteColor(ConsoleColor.Red, numberError);
                    continue;
                }

                return value;
            }
        }

        private static int InputExistingGroupId(string message, ConsoleColor color, GroupService groupService)
        {
            while (true)
            {
                int groupId = InputPositiveInt(message, "Group id must be greater than 0", color);

                try
                {
                    groupService.GetById(groupId);
                    return groupId;
                }
                catch (Exception ex)
                {
                    WriteColor(ConsoleColor.Red, ex.Message);
                }
            }
        }

        private static string InputOptionalRequiredText(string askMessage, string inputMessage, string currentValue, string requiredError, ConsoleColor color)
        {
            bool shouldChange = InputYesOrNo(askMessage, color);

            if (!shouldChange)
            {
                return currentValue;
            }

            return InputRequiredText(inputMessage, requiredError, color);
        }

        private static string InputOptionalNameWithoutNumber(string askMessage, string inputMessage, string currentValue, string requiredError, string numberError, ConsoleColor color)
        {
            bool shouldChange = InputYesOrNo(askMessage, color);

            if (!shouldChange)
            {
                return currentValue;
            }

            return InputNameWithoutNumber(inputMessage, requiredError, numberError, color);
        }

        private static int InputOptionalAge(string askMessage, string inputMessage, int currentValue, ConsoleColor color)
        {
            bool shouldChange = InputYesOrNo(askMessage, color);

            if (!shouldChange)
            {
                return currentValue;
            }

            return InputAge(inputMessage, color);
        }

        private static int InputOptionalExistingGroupId(string askMessage, string inputMessage, int currentValue, ConsoleColor color, GroupService groupService)
        {
            bool shouldChange = InputYesOrNo(askMessage, color);

            if (!shouldChange)
            {
                return currentValue;
            }

            return InputExistingGroupId(inputMessage, color, groupService);
        }

        private static bool InputYesOrNo(string message, ConsoleColor color)
        {
            while (true)
            {
                string answer = InputText(message, color).ToLower();

                if (answer == "y" || answer == "yes")
                {
                    return true;
                }

                if (answer == "n" || answer == "no")
                {
                    return false;
                }

                WriteColor(ConsoleColor.Red, "Please enter y or n.");
            }
        }

        private static bool ContainsNumber(string text)
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

        private static void WriteColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
