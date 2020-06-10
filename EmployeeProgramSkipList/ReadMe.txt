Aquired base SkipList code from https://github.com/csuttner/skiplist-example.
Then altered code to the needs of my program.

This proagram is somewhat similar to my first algorithm (HeapSort). This time I am using SkipList.
It still is a program that keeps track of the employee in a company.
This program is using a similar menu to the previous. Some functions/meanu commands from the previous program also caried over. They are:

COMMANDS - List possible commands.
LIST - List all employees in the system.
END - End the program

The commands of "ORDER was not needed in this program. "ORDER" was an operation that used heapsort to sort an array.
This program does not need that beacuse a SkipList naturely orders that Employees when they are added.
There are different functionality/menu commands in this program compared to the first one. They are below:

LIST_ALL - List all layers of the SkipList and employees on that skiplist.
ADD - The ablity to add a new employee name and ID Number.
REMOVE - Delete an employee by their ID Number
FIND - Looks through the list of employees to see if a certain ID Number is in uses.