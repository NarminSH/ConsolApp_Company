using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


public class User
{
    public User(string name, string surname, string username, string email, string password)
    {
        Counter++; 
        Id = Counter; 
        Name = name;
        Surname = surname;
        Username = username;
        Email = email;
        Password = password;
    }

    public static int Counter = 0;
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

