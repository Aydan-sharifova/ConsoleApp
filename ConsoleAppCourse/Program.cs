using ConsoleAppCourse.Helpers;

namespace ConsoleAppCourse
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Helper.PrintConsole(ConsoleColor.Green, "Select one option!");
           Helper.PrintConsole(ConsoleColor.Yellow,"1 - Create Group, 2 - Update group , 3 - Delete Group   , 4 - Get group  by id, 5 - Get all groups  by teacher , 6 - Get all groups by room, 7 - Get all groups   , 8 - Create Student  9 - Update Student   , 10- Get student  by id, 11 - Delete student,12 - Get students   by age, 13 - Get all students  by group id , 14- Search method for groups by name, 15 - Search method for students by name or surname");


            while (true)
            {
                string selectOpt = Console.ReadLine();
                int selectNumber;
                
                bool IsSelectOpt = int.TryParse(selectOpt, out selectNumber);

                if (IsSelectOpt) {

                    switch (selectNumber) {

                        case 1:
                            Console.WriteLine(selectNumber); break;

                    }
                }
            }
        }
    }
}
