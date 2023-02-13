using SolartTestTask.Contexts;
using SolartTestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolartTestTask.Controllers
{
    internal class UserController
    {
        public static void GetAll()
        {
            Console.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                Console.WriteLine("Список всех дней рождений");

                foreach (var user in users)
                {
                    Console.WriteLine($"[{user.UserID}] {user.UserName} {user.birthDay}");
                }
            }
            Console.ReadKey();
        }

        public static void AddPerson()
        {

            try
            {
                Console.Clear();
                using (ApplicationContext db = new ApplicationContext())
                {
                    Console.WriteLine("Введите ФИО");
                    string name = Console.ReadLine();
                    var user = db.Users.Select(person => person).Where(person => person.UserName == name).FirstOrDefault();
                    if (user != null)
                    {
                        throw new Exception("Такой пользователь уже существует!");
                    }
                    Console.WriteLine("Введите дату рождения");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    db.Users.Add(new User(){ UserName = name, birthDay = date, nearestbirthDay = new DateTime(DateTime.Now.Year, date.Month, date.Day)});
                    db.SaveChanges();
                    Console.WriteLine("Пользователь был успешно добавлен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        public static void DeletePerson()
        {
            try
            {
                Console.Clear();
                using (ApplicationContext db = new ApplicationContext())
                {
                    Console.WriteLine("Введите имя пользователя");
                    string name = Console.ReadLine();

                    var user = db.Users.Select(person => person).Where(person => person.UserName == name).ToArray();

                    if (user != null)
                        db.Users.RemoveRange(user);
                    else
                        throw new Exception("Данный пользователь не найден");

                    db.SaveChanges();
                    Console.WriteLine("Пользователь был успешно удалён");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static void UpdatePerson()
        {
            try
            {
                Console.Clear();
                using (ApplicationContext db = new ApplicationContext())
                {
                    Console.WriteLine("Введите имя пользователя");
                    string name = Console.ReadLine();
                    var user = db.Users.Select(person => person).Where(person => person.UserName == name).ToArray().FirstOrDefault();
                    if (user != null)
                    {
                        Console.WriteLine("Введите новое имя пользователя(Пустая строка что бы оставить без изменений)");
                        string newname = Console.ReadLine();

                        Console.WriteLine("Введите новую дату рождения((Пустая строка что бы оставить без изменений))");
                        string newbirthday = Console.ReadLine();

                        user.UserName = newname == "" ? user.UserName : newname;
                        user.birthDay = newbirthday == "" ? user.birthDay : DateTime.Parse(newbirthday);

                        db.SaveChanges();
                    }
                    else
                        throw new Exception("Данный пользователь не найден");
                    Console.WriteLine("Пользователь был успешно обновлён");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static void GetNearestBirthdays()
        {
            try
            {
                Console.Clear();
                using (ApplicationContext db = new ApplicationContext())
                {
                    var users = db.Users.Select(person => person).ToList();
                    User[] use = users.ToArray();

                    for (int i = 0; i < use.Length; i++)
                        if ((DateTime.Now - use[i].nearestbirthDay).Days > 30)
                            users.Remove(use[i]);
                    Console.WriteLine("Ближайшие дни рождения:");
                    if (users != null)
                    {
                        foreach (var user in users)
                        {
                            Console.WriteLine($"[{user.UserID}] {user.UserName} {user.birthDay}");
                        }
                    }
                    else
                        throw new Exception("Ближайших дней рождения не найдено");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static void SelectAction()
        {
            Console.Clear();
            Console.WriteLine("Выберите действие: " +
                "\n 1 - Просмотреть все дни рождения" +
                "\n 2 - Просмотреть ближайшие дни рождения" +
                "\n 3 - Добавить данные о пользователе" +
                "\n 4 - Обновить данные о пользователе" +
                "\n 5 - Удалить данные о пользователе" +
                "\n 6 - Выход");
            int num = int.Parse(Console.ReadLine());

            switch (num)
            {
                case 1:
                    GetAll();
                    break;
                case 2:
                    GetNearestBirthdays();
                    break;
                case 3:
                    AddPerson();
                    break;
                case 4:
                    UpdatePerson();
                    break;
                case 5:
                    DeletePerson();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ошибка! Такого действия не существует");
                    break;
            }
            SelectAction();
        }
    }
}
