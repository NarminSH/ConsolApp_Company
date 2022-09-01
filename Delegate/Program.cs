using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;


public delegate void CheckError(string message);
public delegate string Check(string name, string surname);



public class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Please enter company name");
        string companyName = Console.ReadLine();
        menu:
        Console.WriteLine("1 - Register a User \n2 - Login a user\n" +
            "3 - See all users in a Company(GetAll)\n4 - Get one user from company(GetById)" +
            "\n5 - Update User's data(UpdateById)\n6 - Delete User from Company(DeleteById)\n7 - Exit");
        string choice = Console.ReadLine();

        Console.WriteLine(choice);
        switch (choice)
            {
            case "1":
                Console.WriteLine("case is register");
                Register(companyName);
                goto menu;
            case "2":
                Console.WriteLine("case is login");
                Login();
                goto menu;
            case "3":
                Console.WriteLine("case is all users");
                GetAllUsers();
                goto menu;
            case "4":
                Console.WriteLine("case is getbyid");
                GetOneUser();
                goto menu;
            case "5":
                Console.WriteLine("case is update");
                update:
                Console.WriteLine("Choose update option:\na. Update name\nb. Update surname\nc. Update username\nd. Update email");
                string updateChoice = Console.ReadLine();
                switch (updateChoice)
                {
                    case "a":
                        UpdateUser("Name");
                        break;
                    case "b":
                        UpdateUser("Surname");
                        break;
                    case "c":
                        UpdateUser("Username");
                        break;
                    case "d":
                        UpdateUser("Email");
                        break;
                    default:
                        Console.WriteLine("Please choose from below options");
                        goto update;
                }
                goto menu;
            case "6":
                Console.WriteLine("case is delete");
                DeleteUser();
                goto menu;
            case "7":
                Console.WriteLine("Exiting...");
                break;

            }

    }
    public static void Register(string companyName)
    {
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
        Console.WriteLine("Please enter your password.Password must start with a capital letter and contain letters, numbers and symbols");
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
        if (!Global.existingCompanies.Contains(companyName.ToUpper()))
        {
            Global.existingCompanies.Add(companyName.ToUpper());
        }

        Global.existingUserIds.Add(user.Id);
        Global.existingUsers.Add(user);
        Global.existingUsernames.Add(user.Username.ToUpper());
        foreach (var item in Global.existingUsers)
        {
            Console.WriteLine("user's name is " + item.Name);
        }

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
    public static void Login()
    {
        Console.WriteLine("Please enter your username");
        string username = Console.ReadLine();
        if (Global.existingUsernames.Contains(username.ToUpper()))
        {
            Console.WriteLine("You are successfully logged in");
        }
    }
    public static void GetAllUsers()
    {
        foreach (var item in Global.existingUsers)
        {
            Console.WriteLine($"User id: {item.Id}, Username: {item.Username}, Name: {item.Name}, Surname: {item.Surname}");
        }
        
    }
    public static void GetOneUser()
    {
        Console.WriteLine("Please enter the user id");
        Console.WriteLine("Existing user ids are: ");
        foreach (int id in Global.existingUserIds)
        {
            Console.WriteLine(id);
        }
        int enteredId = Convert.ToInt32(Console.ReadLine());
        IEnumerable<User> results = Global.existingUsers.Where(user => user.Id == enteredId);
        foreach (var user in results)
        {
            Console.WriteLine($"Name:{user.Name}, Surname: {user.Surname}, Username: {user.Username}, Email: {user.Email}");
        }
    }
    public static void UpdateUser(string choice)
    {
        Console.WriteLine("Enter user id that you wanna update");
        int enteredId = Convert.ToInt32(Console.ReadLine());
        if (!Global.existingUserIds.Contains(enteredId))
        {
            Console.WriteLine("User with this id does not exist");
        }
        IEnumerable<User> user = Global.existingUsers.Where(user => user.Id == enteredId);
        if (user.Count() > 0)
        {
            Console.WriteLine("Found user! What is the new value?");
            string newValue = Console.ReadLine();
            if (newValue.Length < 2)
            {
                Console.WriteLine("Must be at least 2 letters");
            }
            else
                
            {
                if (choice == "Name")
                {
                    foreach (var u in user)
                    {
                        u.Name = newValue;
                    }
                }
                else if (choice == "Surname")
                {
                    foreach (var u in user)
                    {
                        u.Surname = newValue;
                    }
                }
                else if (choice == "Email")
                {
                    foreach (var u in user)
                    {
                        u.Email = newValue;
                    }
                }
                else if (choice == "Username")
                {
                    foreach (var u in user)
                    {
                        u.Username = newValue;
                    }
                }

            }

        }

        
    }
    public static void DeleteUser()
    {
        Console.WriteLine("Please enter user id that you wanna delete");
        int enteredId = Convert.ToInt32(Console.ReadLine());
        if (!Global.existingUserIds.Contains(enteredId))
        {
            Console.WriteLine("User with this email does not exist");
        }else Global.existingUsers.RemoveAll(user => user.Id == enteredId);


    }

}



