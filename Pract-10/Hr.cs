using Pract_10;
using Pract_9;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pract_10.Knopochka;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Boolean = System.Boolean;

namespace Pract_10
{
    internal class HR : ICRUD
    {
        private string Name;
        public HR(string login)
        {
            List<Employee> employees = SerDeser.Deserialize<Employee>("Employee.json");
            List<User> users = SerDeser.Deserialize<User>("User.json");
            int ID = 0;
            string name = "";
            foreach (var item in users)
            {
                if (item.Login == login) ID = item.ID;
            }
            try
            {
                foreach (var item in employees)
                {
                    if (item.User_ID == ID) name = item.FirstName;
                }
            }
            catch { }
            if (name == "") name = login;
            Name = name;
        }
        private void Menushka()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"\t\t\t\t\tДобро пожаловать, {Name}\n------------------------------------------------------------------------------------------------------------------------");
            return;
        }
        private void MenuKnopochek1()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("F1 - Создать запись");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("F2 - Найти запись");
            return;
        }
        private void MenuKnopochek2()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("S - Сохранить запись");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("Escape - Вернуться обратно");
            return;
        }
        private void MenuKnopochek3()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("F10 - Изменить запись");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("Del - Удалить запись");
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("Escape - Вернуться обратно");
            return;
        }
        public void Create(string file)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek2();
            int possition;
            int ID = 0;
            string surname = "";
            string firstName = "";
            string middleName = "";
            DateOnly date = new DateOnly();
            int pasport = 0;
            string post = "";
            double salary = -1;
            int user_ID = -1;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:\n  Фамилия:\n  Имя:\n  Отчество:\n  Дата рождения:\n  Паспорт:\n  Должность:\n  Зарплата:\n  Аккаунт сотрудника: ");
            do
            {
                Strelochki strelochki = new Strelochki(2, 10);
                possition = strelochki.Menu();
                Console.SetCursorPosition(90, 7);
                Console.WriteLine("                    ");
                if (possition == 2)
                {
                    Console.SetCursorPosition(6, 2);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(6, 2);
                    try
                    {
                        ID = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 3)
                {
                    Console.SetCursorPosition(11, 3);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(11, 3);
                    surname = Console.ReadLine();
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(7, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(7, 4);
                    firstName = Console.ReadLine();
                }
                else if (possition == 5)
                {
                    Console.SetCursorPosition(12, 5);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(12, 5);
                    middleName = Console.ReadLine();
                }
                else if (possition == 6)
                {
                    Console.SetCursorPosition(17, 6);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(17, 6);
                    try
                    {
                        date = DateOnly.FromDateTime(Convert.ToDateTime(Console.ReadLine()));
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 7)
                {
                    Console.SetCursorPosition(11, 7);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(11, 7);
                    try
                    {
                        pasport = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 8)
                {
                    Console.SetCursorPosition(11, 10);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(11, 10);
                    post = Console.ReadLine();
                }
                else if (possition == 9)
                {
                    Console.SetCursorPosition(12, 9);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(12, 9);
                    try
                    {
                        salary = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 10)
                {
                    Console.SetCursorPosition(22, 10);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(22, 10);
                    try
                    {
                        user_ID = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    Employee employee = new Employee();
                    if (ID > -1) employee.ID = ID;
                    employee.Surname = surname;
                    employee.FirstName = firstName;
                    employee.MiddleName = middleName;
                    employee.Date = date;
                    if (pasport > 0) employee.Pasport = pasport;
                    else employee.Pasport = 0;
                    employee.Post = post;
                    if (salary > -1) employee.Salary = salary;
                    else employee.Salary = 0;
                    if (user_ID > -1) employee.User_ID = user_ID;
                    else employee.User_ID = -1;

                    List<Employee> employees = SerDeser.Deserialize<Employee>(file);
                    Boolean proverka = true;
                    Boolean proverka2 = true;
                    try
                    {
                    foreach (var item in employees)
                    {
                        if (item.User_ID == user_ID && user_ID != -1) proverka2 = false;
                    }
                    foreach (var item in employees)
                    {
                        if (item.ID == ID) proverka = false;
                    }
                    }
                    catch { }
                    if (proverka == true && proverka2 == true)
                    {
                        employees.Add(employee);
                        SerDeser.Serialize<List<Employee>>(employees, file);
                        UseHR();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                    else if (proverka2 == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой аккаунт уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseHR();
        }
        public void Read(string file, int pos)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek3();
            List<Employee> employees = SerDeser.Deserialize<Employee>(file);
            Console.SetCursorPosition(0, 2);
            try
            {
                Console.WriteLine($"  ID: {employees[pos].ID}\n  Фамилия: {employees[pos].MiddleName}\n  Имя: {employees[pos].Surname}\n  Отчество: {employees[pos].MiddleName}\n  Дата рождения: {employees[pos].Date}\n  Паспорт: {employees[pos].Pasport}\n  Должность: {employees[pos].Post}\n  Зарплата: {employees[pos].Salary}\n  Аккаунт сотрудника: {employees[pos].User_ID}");
                Strelochki strelochki = new Strelochki(2, 10);
                while (true)
                {
                    int possition = strelochki.Menu();
                    if (possition == (int)Knopochka.Escape)
                    {
                        UseHR();
                    }
                    else if (possition == (int)Knopochka.Del)
                    {
                        Delete(pos, file);
                        Console.Clear();
                        UseHR();
                    }
                    else if (possition == (int)Knopochka.F10)
                    {
                        Update(pos, file);
                    }
                }
            }
            catch
            {
                UseHR();
            }
        }
        public void Delete(int pos, string file)
        {
            List<Employee> employees = SerDeser.Deserialize<Employee>(file);
            var sorted = employees.Where(item => item != employees[pos]).ToList();
            SerDeser.Serialize(sorted, file);
            return;
        }
        public void Update(int pos, string file)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek2();
            List<Employee> employees = SerDeser.Deserialize<Employee>(file);
            int possition;
            int ID = employees[pos].ID;
            int pasport = employees[pos].Pasport;
            double salary = employees[pos].Salary;
            int user_ID = employees[pos].User_ID;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID: {ID}\n  Фамилия: {employees[pos].Surname}\n  Имя: {employees[pos].FirstName}\n  Отчество: {employees[pos].MiddleName}\n  Дата рождения: {employees[pos].Date}\n  Паспорт: {pasport}\n  Должность: {employees[pos].Post}\n  Зарплата: {salary}\n  Аккаунт сотрудника: {user_ID}");
            do
            {
                Strelochki strelochki = new Strelochki(2, 10);
                possition = strelochki.Menu();
                Console.SetCursorPosition(88, 7);
                Console.WriteLine("                    ");
                if (possition == 2)
                {
                    Console.SetCursorPosition(6, 2);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(6, 2);
                    try
                    {
                        ID = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(88, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 3)
                {
                    Console.SetCursorPosition(11, 3);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(11, 3);
                    employees[pos].Surname = Console.ReadLine();
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(7, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(7, 4);
                    employees[pos].FirstName = Console.ReadLine();
                }
                else if (possition == 5)
                {
                    Console.SetCursorPosition(12, 5);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(12, 5);
                    employees[pos].MiddleName = Console.ReadLine();
                }
                else if (possition == 6)
                {
                    Console.SetCursorPosition(17, 6);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(17, 6);
                    try
                    {
                        employees[pos].Date = DateOnly.FromDateTime(Convert.ToDateTime(Console.ReadLine()));
                    }
                    catch
                    {
                        Console.SetCursorPosition(88, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 7)
                {
                    Console.SetCursorPosition(11, 7);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(11, 7);
                    try
                    {
                        pasport = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(88, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 8)
                {
                    Console.SetCursorPosition(11, 10);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(11, 10);
                    employees[pos].Post = Console.ReadLine();
                }
                else if (possition == 9)
                {
                    Console.SetCursorPosition(12, 9);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(12, 9);
                    try
                    {
                        salary = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(88, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 10)
                {
                    Console.SetCursorPosition(22, 10);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(22, 10);
                    try
                    {
                        user_ID = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(88, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    Boolean proverka = true;
                    Boolean proverka2 = true;
                    if (user_ID < -1) user_ID = -1;
                    if (ID < 0) ID = 0;
                    foreach (var item in employees)
                    {
                        if (item.ID == ID && item.ID != employees[pos].ID) proverka = false;
                    }
                    foreach (var item in employees)
                    {
                        if (item.User_ID == user_ID && user_ID != -1 && item.User_ID != employees[pos].User_ID) proverka2 = false;
                    }
                    if (proverka == true && proverka2 == true)
                    {
                        employees[pos].ID = ID;
                        if (pasport > 0) employees[pos].Pasport = pasport;
                        if (salary > -1) employees[pos].Salary = salary;
                        employees[pos].User_ID = user_ID;
                        SerDeser.Serialize<List<Employee>>(employees, file);
                        UseHR();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                    else if (proverka2 == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой аккаунт уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseHR();
        }
        public void Filter(string file)
        {
            List<Employee> employees = SerDeser.Deserialize<Employee>(file);
            Console.Clear();
            Menushka();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID\n  Фамилия\n  Имя\n  Отчество\n  Дата рождения\n  Паспорт\n  Должность\n  Зарплата\n  Аккаунт сотрудника");
            int possition;
            do
            {
                Strelochki strelochki = new Strelochki(2, 10);
                possition = strelochki.Menu();
                if (possition >= 0)
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Введите значение для поиска");
                    string parametr = Console.ReadLine();
                    List<Employee> sorted = new List<Employee>();
                    if (possition == 2)
                    {
                        try
                        {
                            sorted = employees.Where(item => item.ID == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 3)
                    {
                        sorted = employees.Where(item => item.Surname == parametr).ToList();
                    }
                    else if (possition == 4)
                    {
                        sorted = employees.Where(item => item.FirstName == parametr).ToList();
                    }
                    else if (possition == 5)
                    {
                        try
                        {
                            sorted = employees.Where(item => item.MiddleName == parametr).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 6)
                    {
                        try
                        {
                            sorted = employees.Where(item => item.Date == DateOnly.FromDateTime(Convert.ToDateTime(parametr))).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 7)
                    {
                        try
                        {
                            sorted = employees.Where(item => item.Pasport == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 8)
                    {
                        sorted = employees.Where(item => item.Post == parametr).ToList();
                    }
                    else if (possition == 9)
                    {
                        try
                        {
                            sorted = employees.Where(item => item.Salary == Convert.ToDouble(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 10)
                    {
                        try
                        {
                            sorted = employees.Where(item => item.User_ID == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    Console.Clear();
                    Menushka();
                    MenuKnopochek1();
                    int i = OutputEmployee(sorted);
                    if (i != 0)
                    {
                        do
                        {
                            Strelochki strelochki2 = new Strelochki(3, i + 2);
                            possition = strelochki2.Menu();
                            if (possition == (int)Knopochka.F1)
                            {
                                Create(file);
                            }
                            else if (possition == (int)Knopochka.Escape)
                            {
                                UseHR();
                            }
                            else if (possition == (int)Knopochka.F2)
                            {
                                Filter(file);
                            }
                            else if (possition >= 0)
                            {
                                int pos = 0;
                                foreach (var item in employees)
                                {
                                    if (item.ID == sorted[possition - 3].ID)
                                    {
                                        break;
                                    }
                                    pos++;
                                }
                                Read(file, pos);
                            }
                        } while (possition != (int)Knopochka.Escape);
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 4);
                        Console.WriteLine("Туть ничего нет");
                        Console.ReadKey(true);
                        UseHR();
                    }
                }
            } while (possition != (int)Knopochka.Escape);
        }
        private int OutputEmployee(List<Employee> employees)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"\tID\t\tФамилия\t\tИмя\t\tОтчество\t\tДолжность");
            int i = 0;
            try
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine($"\t{employee.ID}\t\t{employee.Surname}\t\t{employee.FirstName}\t\t{employee.MiddleName}\t\t{employee.Post}");
                    i++;
                }
            }
            catch { }
            return i;
        }
        public void UseHR()
        {
            Console.Clear();
            Menushka();
            MenuKnopochek1();
            string file = "Employee.json";
            List<Employee> employees = SerDeser.Deserialize<Employee>(file);
            Console.SetCursorPosition(0, 2);
            int i = OutputEmployee(employees);
            if (i == 0) i = 1;
            int possition;
            do
            {
                Strelochki strelochki = new Strelochki(3, i + 2);
                possition = strelochki.Menu();
                if (possition == (int)Knopochka.F1)
                {
                    Create(file);
                }
                else if (possition == (int)Knopochka.F2)
                {
                    Filter(file);
                }
                else if (possition == (int)Knopochka.Escape)
                {
                    Autorization.Vxod();
                }
                else if (possition >= 0)
                {
                    Read(file, possition - 3);
                }
            } while (possition != (int)Knopochka.Escape);
        }
    }
}