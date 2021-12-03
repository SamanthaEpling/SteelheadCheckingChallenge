// See https://aka.ms/new-console-template for more information
using SteelheadChecking;

string filefolder = "C:/Users/Eplin/source/repos/SteelheadChecking/UserFiles/"; //Please change to appropriate filefolder to store all created files for banking system.

MainLoginMenu();

void MainLoginMenu()
{
    Console.WriteLine("Welcome to Steelhead Checking!");
    Console.WriteLine("Press '1' to login to your account, press '2' to create a new account.");
    var inputChoice = Console.ReadLine();

    if (inputChoice == "1")
    {
        Console.WriteLine("Please provide the following information to log in to your account: ");

        string username, password, username1, password1, balance = string.Empty;

        Console.WriteLine("Username: ");
        username = Console.ReadLine();
        Console.WriteLine("Password: ");
        password = Console.ReadLine();

        string filename = filefolder + "_password" + username + ".txt";
        try
        {
            using (StreamReader sr = new StreamReader(File.Open(filename, FileMode.Open)))
            {
                username1 = sr.ReadLine();
                password1 = sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();         
                balance = sr.ReadLine();
                sr.ReadLine();
                sr.Close();
            }
            if (username == username1 && password == password1)
            {
                Console.WriteLine("Login was successful.\r\n");
                CheckingMenu(username, Convert.ToDouble(balance));
            }
            else
            {
                Console.WriteLine("Login failed.\r\n");
                MainLoginMenu();
            }
        }
        finally
        {
            Console.WriteLine("Login failed.\r\n");
            MainLoginMenu();
        }   
    }

    if (inputChoice == "2")
    {
        Console.WriteLine("Please provide the following information to create your account: ");
        string createUsername, createPassword = string.Empty;
        double createBalance = 0;
        Console.Write("Username: ");
        createUsername = Console.ReadLine();
        Console.Write("Password: ");
        createPassword = Console.ReadLine();
        Console.Write("Initial account balance: ");
        createBalance = Convert.ToDouble(Console.ReadLine());

        string filename = filefolder + createUsername + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".txt";
        using (StreamWriter sw = new StreamWriter(File.Create(filename)))
        {
            sw.WriteLine(createUsername);
            sw.WriteLine(createPassword);
            sw.WriteLine(DateTime.Now);
            sw.WriteLine("n/a");
            sw.WriteLine(createBalance);
            sw.WriteLine("Initial account balance.");       
            sw.Close();
        }
        using (StreamWriter sw = new StreamWriter(File.Create(filefolder + "_password" + createUsername + ".txt")))
        {
            sw.WriteLine(createUsername);
            sw.WriteLine(createPassword);
            sw.WriteLine(DateTime.Now.ToString("ddMMyyyyHHmmss"));
        }

        Console.WriteLine("Account successfully created.\r\n");
        CheckingMenu(createUsername, createBalance);
    }
}

void CheckingMenu(string username, double balance)
{
    var account = new CheckingAccount(username, balance);

    Console.WriteLine("Please enter one of the following choices:\r\n");
    Console.WriteLine("\tb \t- -  \tView Current Balance");
    Console.WriteLine("\tv \t- -  \tView Transaction Log");
    Console.WriteLine("\td \t- -  \tRecord Deposit");
    Console.WriteLine("\tw \t- -  \tRecord Withdrawl");
    Console.WriteLine("\tr \t- -  \tLog out and return to login screen.");
    Console.WriteLine("Enter choice:");
    var input_choice = Console.ReadLine();

    if (input_choice == "b")
    {
        Console.WriteLine("Your current account balance: ${0}\r\n", balance);
        CheckingMenu(username, balance);
    }
    if (input_choice == "v")
    {
        GetAccountHistory(filefolder, username);
        CheckingMenu(username, balance);
    }
    if (input_choice == "d")
    {
        double currentamount;
        Console.WriteLine("Please enter amount to deposit: ");
        double depositamount = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Please enter notes for this transaction: ");
        string depositnote = Console.ReadLine();
        currentamount = balance + depositamount;

        account.RecordDeposit(filefolder, username, depositamount, DateTime.Now, depositnote, currentamount);
        Console.WriteLine("Your new balance is: ${0}", currentamount);
        
        CheckingMenu(username, currentamount);
    }
    if (input_choice == "w")
    {
        double currentamount;
        Console.WriteLine("Please enter amount to withdraw: ");
        double withdrawlamount = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Please enter notes for this transaction: ");
        string withdrawlnote = Console.ReadLine();
        currentamount = balance - withdrawlamount;

        account.RecordWithdrawal(filefolder, username, withdrawlamount, DateTime.Now, withdrawlnote, currentamount);
        Console.WriteLine("Your new balance is: ${0}", currentamount);

        CheckingMenu(username, currentamount);
    }
    if (input_choice == "r")
    {
        Console.WriteLine("Are you sure you wish to log out? Press 'y' to logout, or any other key to return to the Checking Menu.\r\n");
        var confirmLogout = Console.ReadLine();
        if (confirmLogout == "y")
        {
            MainLoginMenu();
        }
        else
        {
            CheckingMenu(username, balance);
        }
    }
}

void GetAccountHistory(string filefolder, string username)
{
    string[] files = System.IO.Directory.GetFiles(filefolder);
    var report = new System.Text.StringBuilder();
    report.AppendLine("Username\t\tEntryDate\t\t\tTransaction\t\tBalance\t\tNote");    
    foreach (string file in files)
    {
        if (!file.Contains("_password") && file.Contains(username)) // select all files that are not the user's password file.
        {
            using (StreamReader sr = new StreamReader(File.Open(file, FileMode.Open)))
            {
                username = sr.ReadLine();
                string password = sr.ReadLine();
                string entrydate = sr.ReadLine();
                string amount = sr.ReadLine();
                string balance = sr.ReadLine();
                string note = sr.ReadLine();
                report.AppendLine($"{username}\t\t{entrydate}\t\t\t{amount}\t\t{balance}\t\t{note}");
                sr.Close();
            }        
        }   
    }
    Console.WriteLine(report.ToString());
}


