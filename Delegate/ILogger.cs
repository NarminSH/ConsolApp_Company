using System;
using System.Collections.Generic;
using System.Text;


public interface ILogger
{
    void Info(string message);

}

public class PasswordLogger : ILogger
{

    public void Info(string message)
    {
        Console.WriteLine($"Password logger check: {message}"); ;
    }
}

public class UsernameLogger : ILogger
{
    public void Info(string message)
    {
        Console.WriteLine($"Username logger check: {message}"); ;
    }
}

public class NameLogger : ILogger
{

    public void Info(string message)
    {
        Console.WriteLine($"Name logger check: {message}"); ;
    }
}

public class SurnameLogger : ILogger
{

    public void Info(string message)
    {
        Console.WriteLine($"Surname logger check: {message}"); ;
    }
}


public class UserExistLogger : ILogger
{

    public void Info(string message)
    {
        Console.WriteLine($"{message}"); ;
    }
}