using Pract_9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Boolean = System.Boolean;

namespace Pract_10
{
    internal class Accountant
    {
        private string Name;
        public Accountant(string login)
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
            double sum = 0;
            DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(DateTime.Now));
            Boolean operation = false;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID:\n  Сумма:\n  Время записи:\n  Прибавка?:");
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
                    try
                    {
                        sum = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(16, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(16, 4);
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
                else if (possition == 5)
                {
                    Console.SetCursorPosition(13, 5);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(13, 5);
                    try
                    {
                        operation = Convert.ToBoolean(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    Buh buh = new Buh();
                    if (ID > -1) buh.ID = ID;
                    else buh.ID = 0;
                    buh.Date = date;
                    if (sum > 0) buh.Sum = sum;
                    else buh.Sum = 0;
                    buh.Operation = operation;
                    List<Buh> buhs = SerDeser.Deserialize<Buh>(file);
                    Boolean proverka = true;
                    try
                    {
                        foreach (var item in buhs)
                        {
                            if (item.ID == ID) proverka = false;
                        }
                    }
                    catch { }
                    if (proverka == true)
                    {
                        buhs.Add(buh);
                        SerDeser.Serialize<List<Buh>>(buhs, file);
                        UseAccountant();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseAccountant();
        }
        public void Read(string file, int pos)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek3();
            List<Buh> buhs = SerDeser.Deserialize<Buh>(file);
            Console.SetCursorPosition(0, 2);
            try
            {
                Console.WriteLine($"  ID: {buhs[pos].ID}\n  Сумма: {buhs[pos].Sum}\n  Время записи: {buhs[pos].Date}\n  Прибавка?: {buhs[pos].Operation}");
                Strelochki strelochki = new Strelochki(2, 5);
                while (true)
                {
                    int possition = strelochki.Menu();
                    if (possition == (int)Knopochka.Escape)
                    {
                        UseAccountant();
                    }
                    else if (possition == (int)Knopochka.Del)
                    {
                        Delete(pos, file);
                        Console.Clear();
                        UseAccountant();
                    }
                    else if (possition == (int)Knopochka.F10)
                    {
                        Update(pos, file);
                    }
                }
            }
            catch
            {
                UseAccountant();
            }
        }
        public void Delete(int pos, string file)
        {
            List<Buh> buhs = SerDeser.Deserialize<Buh>(file);
            var sorted = buhs.Where(item => item != buhs[pos]).ToList();
            SerDeser.Serialize(sorted, file);
            return;
        }
        public void Update(int pos, string file)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek2();
            List<Buh> buhs = SerDeser.Deserialize<Buh>(file);
            int possition;
            int ID = buhs[pos].ID;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID: {ID}\n  Сумма: {buhs[pos].Sum}\n  Время записи: {buhs[pos].Date}\n  Прибавка?: {buhs[pos].Operation}");
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
                    try
                    {
                        buhs[pos].Sum = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(16, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(16, 4);
                    try
                    {
                        buhs[pos].Date = DateOnly.FromDateTime(Convert.ToDateTime(Console.ReadLine()));
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                else if (possition == 5)
                {
                    Console.SetCursorPosition(26, 5);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(26, 5);
                    try
                    {
                        buhs[pos].Operation = Convert.ToBoolean(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    if (ID < 0) ID = 0;
                    Boolean proverka = true;
                    foreach (var item in buhs)
                    {
                        if (item.ID == ID && item.ID != buhs[pos].ID) proverka = false;
                    }
                    if (proverka == true && buhs[pos].Sum > 0)
                    {
                        buhs[pos].ID = ID;
                        SerDeser.Serialize<List<Buh>>(buhs, file);
                        UseAccountant();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseAccountant();
        }
        public void Filter(string file)
        {
            List<Buh> buhs = SerDeser.Deserialize<Buh>(file);
            Console.Clear();
            Menushka();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID\n  Сумма\n  Время записи\n  Прибавка?");
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
                    List<Buh> sorted = new List<Buh>();
                    if (possition == 2)
                    {
                        try
                        {
                            sorted = buhs.Where(item => item.ID == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 3)
                    {
                        sorted = buhs.Where(item => item.Sum == Convert.ToDouble(parametr)).ToList();
                    }
                    else if (possition == 4)
                    {
                        try
                        {
                            sorted = buhs.Where(item => item.Date == DateOnly.FromDateTime(Convert.ToDateTime(parametr))).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 5)
                    {
                        try
                        {
                            sorted = buhs.Where(item => item.Operation == Convert.ToBoolean(parametr)).ToList();
                        }
                        catch { }
                    }
                    Console.Clear();
                    Menushka();
                    MenuKnopochek1();
                    int i = OutputBuh(sorted);
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
                                foreach (var item in buhs)
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
                        UseAccountant();
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseAccountant();
        }
        private int OutputBuh(List<Buh> buhs)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("\tID\t\tСумма\t\tВремя записи\t\tПрибавка?");
            int i = 0;
            double sum = 0;
            foreach (var item in buhs)
            {
                Console.WriteLine($"\t{item.ID}\t\t{item.Sum}\t\t{item.Date}\t\t{item.Operation}");
                if (item.Operation == true) sum += item.Sum;
                else sum -= item.Sum;
                i++;
            }
            Console.WriteLine($"------------------------------------------------------------------------------------------\n\t\t\t\t\tИтоговая сумма: {sum}");
            return i;
        }
        public void UseAccountant()
        {
            Console.Clear();
            Menushka();
            MenuKnopochek1();
            string file = "Accountant.json";
            List<Buh> buhs = SerDeser.Deserialize<Buh>(file);
            Console.SetCursorPosition(0, 2);
            int i = OutputBuh(buhs);
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
