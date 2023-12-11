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

namespace Pract_10
{
    internal class Admin : ICRUD
    {
        private string Name;
        public Admin(string login)
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
            string login = "";
            string password = "";
            int role = 0;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:\n  Логин:\n  Пароль:\n  Роль:");
            do
            {
                Console.SetCursorPosition(0, 2);
                Strelochki strelochki = new Strelochki(2, 5);
                possition = strelochki.Menu();
                Console.SetCursorPosition(90, 7);
                Console.WriteLine("                      ");
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
                    Console.SetCursorPosition(9, 3);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(9, 3);
                    login = Console.ReadLine();
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(10, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(10, 4);
                    password = Console.ReadLine();
                }
                else if (possition == 5)
                {
                    Console.SetCursorPosition(8, 5);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(8, 5);
                    try
                    {
                        role = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    User user = new User();
                    if (ID > -1) user.ID = ID;
                    else user.ID = 0;
                    user.Login = login;
                    user.Password = password;
                    if (role > -2 && role < 5) user.Role = role;
                    List<User> users = SerDeser.Deserialize<User>(file);
                    Boolean proverka = true;
                    Boolean proverka2 = true;
                    foreach (var item in users)
                    {
                        if (item.ID == ID) proverka = false;
                    }
                    foreach (var item in users)
                    {
                        if (item.Login == login) proverka2 = false;
                    }
                    if (proverka == true && proverka2 == true)
                    {
                        users.Add(user);
                        SerDeser.Serialize<List<User>>(users, file);
                        UseAdmin();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                    else if (proverka2 == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой логин уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseAdmin();
        }
        public void Read(string file, int pos)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek3();
            List<User> users = SerDeser.Deserialize<User>(file);
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID: {users[pos].ID}\n  Логин: {users[pos].Login}\n  Пароль: {users[pos].Password}\n  Роль: {users[pos].Role}");
            Strelochki strelochki = new Strelochki(2, 5);
            while (true)
            {
                int possition = strelochki.Menu();
                if (possition == (int)Knopochka.Escape)
                {
                    UseAdmin();
                }
                else if (possition == (int)Knopochka.Del)
                {
                    Delete(pos, file);
                    Console.Clear();
                    UseAdmin();
                }
                else if (possition == (int)Knopochka.F10)
                {
                    Update(pos, file);
                }
            }
        }
        public void Delete(int pos, string file)
        {
            List<User> users = SerDeser.Deserialize<User>(file);
            var sorted = users.Where(item => item != users[pos]).ToList();
            SerDeser.Serialize(sorted, file);
            return;
        }
        public void Update(int pos, string file)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek2();
            List<User> users = SerDeser.Deserialize<User>(file);
            int possition;
            int ID = users[pos].ID;
            string login = users[pos].Login;
            string password = users[pos].Password;
            int role = users[pos].Role;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID: {ID}\n  Логин: {login}\n  Пароль: {password}\n  Роль: {role}");
            do
            {
                Console.SetCursorPosition(0, 2);
                Strelochki strelochki = new Strelochki(2, 5);
                possition = strelochki.Menu();
                Console.SetCursorPosition(90, 7);
                Console.WriteLine("                      ");
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
                    Console.SetCursorPosition(9, 3);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(9, 3);
                    login = Console.ReadLine();
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(10, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(10, 4);
                    password = Console.ReadLine();
                }
                else if (possition == 5)
                {
                    Console.SetCursorPosition(8, 5);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(8, 5);
                    try
                    {
                        role = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    Boolean proverka = true;
                    Boolean proverka2 = true;
                    foreach (var item in users)
                    {
                        if (item.ID == ID && item.ID != users[pos].ID) proverka = false;
                    }
                    foreach (var item in users)
                    {
                        if (item.Login == login && item.Login != users[pos].Login) proverka2 = false;
                    }
                    if (proverka == true) users[pos].ID = ID;
                    if (proverka2 == true) users[pos].Login = login;
                    users[pos].Password = password;
                    users[pos].Role = role;
                    if (proverka == true && proverka2 == true)
                    {
                        SerDeser.Serialize<List<User>>(users, file);
                        UseAdmin();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                    else if (proverka2 == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой логин уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseAdmin();
        }
        public void Filter(string file)
        {
            List<User> users = SerDeser.Deserialize<User>(file);
            Console.Clear();
            Menushka();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID\n  Логин\n  Пароль\n  Роль");
            int possition;
            do
            {
                Strelochki strelochki = new Strelochki(2, 5);
                possition = strelochki.Menu();
                if (possition >= 0)
                {
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите значение для поиска");
                    string parametr = Console.ReadLine();
                    List<User> sorted = new List<User>();
                    if (possition == 2)
                    {
                        try
                        {
                            sorted = users.Where(item => item.ID == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 3)
                    {
                        sorted = users.Where(item => item.Login == parametr).ToList();
                    }
                    else if (possition == 4)
                    {
                        sorted = users.Where(item => item.Password == parametr).ToList();
                    }
                    else if (possition == 5)
                    {
                        try
                        {
                            sorted = users.Where(item => item.Role == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    Console.Clear();
                    Menushka();
                    MenuKnopochek1();
                    int i = OutputUsers(sorted);
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
                                break;
                            }
                            else if (possition == (int)Knopochka.F2)
                            {
                                Filter(file);
                            }
                            else if (possition >= 0)
                            {
                                int pos = 0;
                                foreach (var item in users)
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
                        UseAdmin();
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseAdmin();
        }
        private int OutputUsers(List<User> users)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("\t\tID\t\tЛогин\t\tПароль\t\tРоль");
            int i = 0;
            foreach (var user in users)
            {
                string role = "";
                if (user.Role == 0)
                {
                    role = "Administrator";
                }
                else if (user.Role == 1)
                {
                    role = "Kassir";
                }
                else if (user.Role == 2)
                {
                    role = "HR";
                }
                else if (user.Role == 3)
                {
                    role = "Sklad";
                }
                else if (user.Role == 4)
                {
                    role = "Buhgalter";
                }
                Console.WriteLine($"\t\t{user.ID}\t\t{user.Login}\t\t{user.Password}\t\t{role}");
                i++;
            }
            return i;
        }
        public void UseAdmin()
        {
            Console.Clear();
            Menushka();
            MenuKnopochek1();
            string file = "User.json";
            List<User> users = SerDeser.Deserialize<User>(file);
            Console.SetCursorPosition(0, 2);
            int i = OutputUsers(users);
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