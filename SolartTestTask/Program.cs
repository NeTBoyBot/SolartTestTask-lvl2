using SolartTestTask.Contexts;
using SolartTestTask.Controllers;
using SolartTestTask.Models;
using System;

public class Program
{
    public static void Main()
    {
        UserController.GetAllAndNearestBirthdays();
        UserController.SelectAction();
    }
}