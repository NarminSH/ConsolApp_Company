using System;
using System.Text.RegularExpressions;


public delegate void CheckError(string message);
public delegate string Check(string name, string surname);

public class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("1 - Register a User \n2 - Login a user\n" +
            "3 - See all users in a Company(GetAll)\n4 - Get one user from company(GetById)" +
            "\n5 - Update User's data(UpdateById)\n6 - Delete User from Company(DeleteById)\n7 - Exit");
        string choice = Console.ReadLine();
        Console.WriteLine(choice);
        switch (choice)
        {
            case "1":
                Console.WriteLine("case is register");
                Register();
                break;
            case "2":
                Console.WriteLine("case is login");
                break ;

        }
        

    }
    public static void Register()
    {
        Console.WriteLine("Please enter company name");
        string companyName = Console.ReadLine();
        name:
        Console.WriteLine("Please enter your name");
        string name = Console.ReadLine();
        if (name.Length < 2)
        {
            NameLogger nameLogger = new NameLogger();
            CheckError p1 = new CheckError(nameLogger.Info);
            p1.Invoke("Not valid Name");
            goto name;
        }
        surname:
        Console.WriteLine("Please enter your surname");
        string surname = Console.ReadLine();
        if (surname.Length < 2)
        {
            SurnameLogger surnameLogger = new SurnameLogger();
            CheckError p1 = new CheckError(surnameLogger.Info);
            p1("Not valid Surname");
            goto surname;
        }
        password:
        Console.WriteLine("Please enter your password. Password must contain letters, numbers and symbols");
        string password = Console.ReadLine();
        Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        Match match = regex.Match(password);
        if (!match.Success)
        {
            PasswordLogger passwordLogger = new PasswordLogger();
            CheckError p1 = new CheckError(passwordLogger.Info);
            p1.Invoke("Not valid Password");
            goto password;
        }
        Check del1 = new Check(CreateUsername);
        string username = del1.Invoke(name, surname);
        Console.WriteLine(username);
        del1 += CreateEmail;
        string email = del1.Invoke(name, surname);
        Console.WriteLine(email);
        User user = new User(name, surname, username, email, password);

    }
    public static string CreateUsername(string name, string surname)
    {
        string username = $"{name}.{surname}";
        return username;
    }
    public static string CreateEmail(string name, string surname)
    {
        string email = $"{name}.{surname}@gmail.com";
        return email;
    }

}



