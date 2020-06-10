// Aquired base SkipList code from https://github.com/csuttner/skiplist-example
// Then altered code to the needs of my program
using System;

namespace EmployeeProgramSkipList
{
    class MainClass
    {
        private static int maxLevel;
        private static SLNode header;
        private static SLNode sentinel;

        public static void Main(string[] args)
        {

            maxLevel = 5; // this sets the max leave of the skip list

            // SL will contain values between 1000 and 9999
            // Warning - nothing has been done to ensure this
            header = new SLNode(1000, "Start of list", maxLevel);
            sentinel = new SLNode(9999, "End of List", maxLevel);

            for (int i = 0; i < maxLevel; i++)
            {
                header.forward[i] = sentinel;
            }

            // Data //
            Insert(1015, "Office_Lady");
            Insert(1050, "Desk_Gentleman");
            Insert(1020, "New_Girl");
            Insert(1001, "Boss_Man");
            Insert(1070, "Boss_Woman");
            Insert(1060, "Office_Clown");
            Insert(1040, "Cat_Man");

            // UI //
            String userResponse = "";

            Console.WriteLine("Type COMMANDS to get a list of commands.");

            while (userResponse != "end")
            {

                Console.Write("//");
                userResponse = Console.ReadLine().ToLower();

                switch (userResponse)
                {

                    case "?":
                    case "help":
                    case "commands":
                        Console.WriteLine("List of Commands:");
                        Console.WriteLine("");
                        Console.WriteLine("COMMANDS");
                        Console.WriteLine("     List commands availible.");
                        Console.WriteLine("LIST");
                        Console.WriteLine("     List all employees in the system.");
                        Console.WriteLine("LIST_ALL");
                        Console.WriteLine("     List all layers of the SkipList.");
                        Console.WriteLine("ADD");
                        Console.WriteLine("     Add a new employee and ID Number");
                        Console.WriteLine("REMOVE");
                        Console.WriteLine("     Delete an employee by their ID Number");
                        Console.WriteLine("FIND");
                        Console.WriteLine("     Looks through the list of employees to see if a certain ID Number is in uses.");
                        Console.WriteLine("END");
                        Console.WriteLine("     End program.");
                        break;

                    case "list":
                        PrintList();
                        break;

                    case "list_all":
                        DisplayAllList();
                        break;

                    case "add":
                        Console.WriteLine("What is the new employee's ID? (must between 1000 and 9999 in addtion to not already being in uses.)");
                        Console.Write("//");
                        int newID = int.Parse(Console.ReadLine());

                        while (newID < 1000 || newID > 10000)
                        {
                            Console.WriteLine("Employee's ID {0} is out of range!!", newID);
                            Console.WriteLine("Try another ID number.");
                            Console.Write("//");
                            newID = int.Parse(Console.ReadLine());
                        }

                        while (Search(newID) == true) { 
                            Console.WriteLine("Employee with the ID {0} already exist!!!", newID);
                            Console.WriteLine("Try another ID number.");
                            Console.Write("//");
                            newID = int.Parse(Console.ReadLine());
                        }
                        
                        Console.WriteLine("New Employee's name?");
                        Console.Write("//");
                        String newName = Console.ReadLine();
                        Insert(newID, newName);
                        Console.WriteLine("New Employee added.");
                        PrintList();
                        break;

                    case "remove":
                        PrintList();
                        Console.WriteLine("What is the Employee's ID that is to be deleted?");
                        int deleteID = int.Parse(Console.ReadLine());
                        if (Search(deleteID)) {
                            Delete(deleteID);
                            Console.WriteLine("Employee with the ID of {0} removed.", deleteID);
                        }
                        else
                            Console.WriteLine("Employee with the ID of {0} could not be found.", deleteID);
                        break;

                    case "find":
                        Console.WriteLine("Type in Employee's ID");
                        Console.Write("//");
                        int searchID = int.Parse(Console.ReadLine());
                        while (searchID < 1000 || searchID > 10000)
                        {
                            Console.WriteLine("Employee ID {0} is out of range!!", searchID);
                            Console.WriteLine("Try another ID number.");
                            Console.Write("//");
                            newID = int.Parse(Console.ReadLine());
                        }
                        if (Search(searchID))
                            Console.WriteLine("An Employee with the ID {0} exists", searchID);
                        else
                            Console.WriteLine("Did not find an Employee with the ID {0}", searchID);
                        break;

                    case "end":
                        break;

                    default:
                        Console.WriteLine("Command Unkown");
                        Console.WriteLine("");
                        break;
                }
            }

            Console.WriteLine("Program END");
        }

        class SLNode
        {
            public SLNode[] forward;
            public int key;
            public string name;
            public int height;

            public SLNode(int key, string name, int height)
            {
                this.key = key;
                this.name = name;
                this.height = height;
                forward = new SLNode[height];
            }

        }

        public static bool Search(int searchKey)
        {
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                while (cur.forward[i].key < searchKey)
                {
                    cur = cur.forward[i];
                }
            }

            cur = cur.forward[0];

            if (cur.key == searchKey)
                return true;
            else
                return false;
        }

        public static void Insert(int searchKey, string name)
        {
            SLNode sLNode = new SLNode(searchKey, name, GenerateLevel());
            SLNode[] update = new SLNode[maxLevel];
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {

                while (cur.forward[i].key < searchKey)
                {
                    cur = cur.forward[i];
                }

                update[i] = cur;
            }

            // stitch in
            for (int i = 0; i < sLNode.height; i++)
            {
                sLNode.forward[i] = update[i].forward[i];
                update[i].forward[i] = sLNode;
            }
        }

        public static void Delete(int target)
        {
            SLNode targetNode;
            SLNode cur = header;

            for (int i = maxLevel - 1; i >= 0; i--)
            {
                while (cur.forward[i].key < target)
                {
                    cur = cur.forward[i];
                }

                // splice out
                if (cur.forward[i].key == target)
                {
                    targetNode = cur.forward[i];
                    cur.forward[i] = targetNode.forward[i];
                }
            }
        }

        public static int GenerateLevel()
        {
            Random random = new Random();
            int newlevel = 1;

            while (random.Next(2) != 0 && newlevel < maxLevel)
            {
                newlevel += 1;
            }

            return newlevel;
        }

        public static void PrintList()
        {
            SLNode node = header.forward[0];
            Console.WriteLine("****** EmployeeList ******");
            Console.WriteLine("ID   Names");

            for (int i = 0; i <= maxLevel - 1; i++)
            {
                while (node.forward[i] != null)
                {
                    Console.WriteLine("{0} {1}", node.key, node.name);
                    node = node.forward[i];
                }
            }
            Console.WriteLine(" ");
        }

        public static void DisplayAllList()
        {

            Console.WriteLine("****** SkipList ******");

            for (int i = 0; i <= maxLevel - 1; i++)
            {
                SLNode node = header.forward[i];
                Console.WriteLine("Level  {0}", i);
                Console.WriteLine("ID   Names");
                while (node.forward[i] != null)
                {
                    Console.WriteLine("{0} {1}", node.key, node.name);
                    node = node.forward[i];
                }
                Console.WriteLine(" ");
            }
        }

    }
}