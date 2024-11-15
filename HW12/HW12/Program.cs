using HW12.Entity;
using HW12.Enum;
using HW12.Repository;
using HW12.Servicess;
TaskServies _taskServisces = new TaskServies();
UserService _userService = new UserService();
bool isExist = false;
while (true)
{
    Console.Clear();
    Console.WriteLine("*********Welcome ToDO List*********");
    Console.WriteLine("1.Register");
    Console.WriteLine("2.Login");
    Console.WriteLine("3.Exit");


    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Enter FullName : ");
            string fullName = Console.ReadLine();
            Console.WriteLine("Enter Username : ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter Password : ");
            string password = Console.ReadLine();

            try
            {
                _userService.Register(fullName, userName, password);
                Console.WriteLine("Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
            break;

        case "2":
            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string pass = Console.ReadLine();

            try
            {
                _userService.Login(username, pass);
                var currentUser = _userService.GetCurrentUser();
                if (currentUser == null)
                {
                    Console.WriteLine("User Not Logged");
                    Console.ReadKey();
                    break;
                }
                isExist = true;
                Console.WriteLine("Login Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
            break;

        case "3":
            return;
        default:
            Console.WriteLine("Invalid");
            Console.ReadKey();
            break;
    }

    if (isExist)
    {
        bool inMenu = true;
        while (inMenu)

        {
            try
            {
                Console.Clear();
                var currentUser = _userService.GetCurrentUser();

                
                Console.WriteLine("1. Add New Task");
                Console.WriteLine("2. Show All Task");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Remove Task");
                Console.WriteLine("5. Change Status");
                Console.WriteLine("6. Search");
                Console.WriteLine("7. LogOut");

                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {

                    case 1:

                        Console.WriteLine("Pls Enter The Title");
                        string title = Console.ReadLine();
                        Console.WriteLine("Pls Enter Time To Done");
                        DateTime timeDon = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Pls Chose Priority (1.low 2.medium 3.high )");
                        int prio = Convert.ToInt32(Console.ReadLine());
                        PriorityEnum priority = PriorityEnum.medium;
                        if (prio == 1)
                        {
                            priority = PriorityEnum.low;
                            _taskServisces.Creat(title, timeDon, priority, currentUser.Id);
                            Console.WriteLine("Done");
                            Console.ReadKey();
                        }
                        else if (prio == 2)
                        {
                            priority = PriorityEnum.medium;
                            _taskServisces.Creat(title, timeDon, priority, currentUser.Id);
                            Console.WriteLine("Done");
                            Console.ReadKey();
                        }
                        else if (prio == 3)
                        {
                            priority = PriorityEnum.high;
                            _taskServisces.Creat(title, timeDon, priority, currentUser.Id);
                            Console.WriteLine("Done");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("There is no selected priority");
                        }

                        break;
                    case 2:
                        _taskServisces.TaskList(currentUser.Id);
                        Console.ReadKey();
                        break;
                    case 3:
                        _taskServisces.TaskList(currentUser.Id);
                        Console.WriteLine("Pls Enter The Task ID");
                        int idd = Convert.ToInt32(Console.ReadLine());
                        var curTask = _taskServisces.Get(idd);
                        Console.WriteLine("Pls Enter New Tilte OR enter <N>");
                        string nTitle = Console.ReadLine();
                        if (nTitle == "N")
                        {
                            nTitle = curTask.Titel;
                        }
                        Console.WriteLine("Pls Enter New Time OR enter <N>");
                        string nTimedon = Console.ReadLine();
                        DateTime nt = curTask.TimeToDone;
                        if (nTimedon != "N")
                        {
                            nt = DateTime.Parse(nTimedon);
                        }
                        Console.WriteLine("Pls Chose New Priority (1.low 2.medium 3.high ) OR <N>");
                        string nPriority = Console.ReadLine();
                        PriorityEnum priorityEnum = curTask.Priority;
                        if (nPriority == "1")
                        {
                            priorityEnum = PriorityEnum.low;
                        }
                        else if (nPriority == "2")
                        {
                            priorityEnum = PriorityEnum.medium;
                        }
                        else if (nPriority == "3")
                        {
                            priorityEnum = PriorityEnum.high;
                        }

                        Console.WriteLine("Pls Chose New State (1.done 2.inPending 3.cancelled ) OR <N>");
                        string nState = Console.ReadLine();
                        StateEnum stateEnum = curTask.State;
                        if (nState == "1")
                        {
                            stateEnum = StateEnum.done;
                        }
                        else if (nState == "2")
                        {
                            stateEnum = StateEnum.inPending;
                        }
                        else if (nState == "3")
                        {
                            stateEnum = StateEnum.cancelled;
                        }
                        Tassk nnTask = new Tassk()
                        {
                            Titel = nTitle,
                            TimeToDone = nt,
                            State = stateEnum,
                            Priority = priorityEnum,
                            UserID = currentUser.Id,
                        };
                        _taskServisces.Edit(idd, nnTask);
                        Console.WriteLine("Done");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Pls Enter The Task ID");
                        int rID = Convert.ToInt32(Console.ReadLine());
                        _taskServisces.Remove(rID, currentUser.Id);
                        Console.WriteLine("Done");
                        Console.ReadKey();

                        break;
                    case 5:
                        Console.Clear();
                        _taskServisces.TaskList(currentUser.Id);
                        Console.WriteLine("Pls Enter The Task ID");
                        int cID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Pls Chose New State (1.done 2.inPending 3.cancelled )");
                        int cState = Convert.ToInt32(Console.ReadLine());
                        _taskServisces.ChangState(cID, cState);
                        Console.WriteLine("Done");
                        Console.ReadKey();

                        break;
                    case 6:
                        Console.WriteLine("Pls Enter The title");
                        string input = Console.ReadLine();
                        _taskServisces.Search(input);
                        Console.ReadKey();

                        break;
                    case 7:
                        currentUser = null;
                        inMenu = false;
                        isExist = false;


                        break;
                    default:

                        break;



                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                Console.ReadKey();
            }




        }
    }
}