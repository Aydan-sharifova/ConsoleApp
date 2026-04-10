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

            Helper.PrintConsole(ConsoleColor.Green, "Select one option!");
            Helper.PrintConsole(ConsoleColor.Yellow, "1 - Create Group, 2 - Update group , 3 - Delete Group   , 4 - Get group  by id, 5 - Get all groups  by teacher , 6 - Get all groups by room, 7 - Get all groups   , 8 - Create Student  9 - Update Student   , 10- Get student  by id, 11 - Delete student,12 - Get students   by age, 13 - Get all students  by group id , 14- Search method for groups by name, 15 - Search method for students by name or surname");

            while (true)
            {
                string? selectOpt = Console.ReadLine();
                int selectNumber;

                bool IsSelectOpt = int.TryParse(selectOpt, out selectNumber);

                if (IsSelectOpt)
                {
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
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid number.");
                }
            }
        }

        private static void CreateGroup(GroupService groupService)
        {
            Helper.PrintConsole(ConsoleColor.Cyan, "Enter group name:");
            string name = Console.ReadLine() ?? string.Empty;

            Helper.PrintConsole(ConsoleColor.Cyan, "Enter teacher name:");
            string teacher = Console.ReadLine() ?? string.Empty;

            Helper.PrintConsole(ConsoleColor.Cyan, "Enter room:");
            string room = Console.ReadLine() ?? string.Empty;

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
            Helper.PrintConsole(ConsoleColor.Cyan, "Enter group id:");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Helper.PrintConsole(ConsoleColor.Cyan, "Enter new group name:");
            string name = Console.ReadLine() ?? string.Empty;

            Helper.PrintConsole(ConsoleColor.Cyan, "Enter new teacher name:");
            string teacher = Console.ReadLine() ?? string.Empty;

            Helper.PrintConsole(ConsoleColor.Cyan, "Enter new room:");
            string room = Console.ReadLine() ?? string.Empty;

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
            Helper.PrintConsole(ConsoleColor.Cyan, "Enter group id:");
            int id = int.Parse(Console.ReadLine() ?? "0");

            groupService.Delete(id);
            Helper.PrintConsole(ConsoleColor.Green, "Group deleted successfully.");
        }

        private static void GetGroupById(GroupService groupService)
        {
            Helper.PrintConsole(ConsoleColor.Cyan, "Enter group id:");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Groups group = groupService.GetById(id);
            PrintSingleGroup(group);
        }

        private static void GetAllGroupsByTeacher(GroupService groupService)
        {
            Helper.PrintConsole(ConsoleColor.Cyan, "Enter teacher name:");
            string teacher = Console.ReadLine() ?? string.Empty;

            List<Groups> groups = groupService.GetAllByTeacher(teacher);
            PrintManyGroups(groups);
        }

        private static void GetAllGroupsByRoom(GroupService groupService)
        {
            Helper.PrintConsole(ConsoleColor.Cyan, "Enter room:");
            string room = Console.ReadLine() ?? string.Empty;

            List<Groups> groups = groupService.GetAllByRoom(room);
            PrintManyGroups(groups);
        }

        private static void GetAllGroups(GroupService groupService)
        {
            List<Groups> groups = groupService.GetAll(m => true);
            PrintManyGroups(groups);
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
    }
}
