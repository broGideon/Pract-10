using Pract_9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pract_10
{
    internal class Sclad : ICRUD
    {
        private string Name;
        public Sclad(string login)
        {
            List<Employee> employees = SerDeser.Deserialize<Employee>("Employee.json");
            List<User> users = SerDeser.Deserialize<User>("User.json");
            int ID = 0;
            string name = "";
            foreach (var item in users)
            {
                if (item.Login == login) ID = item.ID;
            }
            foreach (var item in employees)
            {
                if (item.User_ID == ID) name = item.FirstName;
            }
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
            string name = "";
            double cost = 0;
            int colvo = 0;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID:\n  Название:\n  Цена за штуку:\n  Количечество на складе:");
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
                    Console.SetCursorPosition(12, 3);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(12, 3);
                    name = Console.ReadLine();
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(17, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(17, 4);
                    try
                    {
                        cost = Convert.ToDouble(Console.ReadLine());
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
                        colvo = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такое вводить нельзя");
                    }
                }
                if (possition == (int)Knopochka.S)
                {
                    Product product = new Product();
                    if (ID > -1) product.ID = ID;
                    else product.ID = 0;
                    product.Name = name;
                    if (cost > 0) product.Cost = cost;
                    else product.Cost = 0;
                    if (colvo > 0) product.Colvo = colvo;
                    else product.Colvo = 0;
                    List<Product> products = SerDeser.Deserialize<Product>(file);
                    Boolean proverka = true;
                    foreach (var item in products)
                    {
                        if (item.ID == ID) proverka = false;
                    }
                    if (proverka == true)
                    {
                        products.Add(product);
                        SerDeser.Serialize<List<Product>>(products, file);
                        UseSclad();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseSclad();
        }
        public void Read(string file, int pos)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek3();
            List<Product> products = SerDeser.Deserialize<Product>(file);
            Console.SetCursorPosition(0, 2);
            try
            {
                Console.WriteLine($"  ID: {products[pos].ID}\n  Название: {products[pos].Name}\n  Цена за штуку: {products[pos].Cost}\n  Количечество на складе: {products[pos].Colvo}");
                Strelochki strelochki = new Strelochki(2, 5);
                while (true)
                {
                    int possition = strelochki.Menu();
                    if (possition == (int)Knopochka.Escape)
                    {
                        UseSclad();
                    }
                    else if (possition == (int)Knopochka.Del)
                    {
                        Delete(pos, file);
                        Console.Clear();
                        UseSclad();
                    }
                    else if (possition == (int)Knopochka.F10)
                    {
                        Update(pos, file);
                    }
                }
            }
            catch
            {
                UseSclad();
            }
        }
        public void Delete(int pos, string file)
        {
            List<Product> products = SerDeser.Deserialize<Product>(file);
            var sorted = products.Where(item => item != products[pos]).ToList();
            SerDeser.Serialize(sorted, file);
            return;
        }
        public void Update(int pos, string file)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek2();
            List<Product> products = SerDeser.Deserialize<Product>(file);
            int possition;
            int ID = products[pos].ID;
            string name = products[pos].Name;
            double cost = products[pos].Cost;
            int colvo = products[pos].Colvo;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID: {ID}\n  Название: {name}\n  Цена за штуку: {cost}\n  Количечество на складе: {colvo}");
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
                    Console.SetCursorPosition(12, 3);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(12, 3);
                    name = Console.ReadLine();
                }
                else if (possition == 4)
                {
                    Console.SetCursorPosition(17, 4);
                    Console.WriteLine("                           ");
                    Console.SetCursorPosition(17, 4);
                    try
                    {
                        cost = Convert.ToDouble(Console.ReadLine());
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
                        colvo = Convert.ToInt32(Console.ReadLine());
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
                    foreach (var item in products)
                    {
                        if (item.ID == ID && item.ID != products[pos].ID) proverka = false;
                    }
                    if (proverka == true)
                    {
                        products[pos].ID = ID;
                        products[pos].Name = name;
                        if (cost > 0) products[pos].Cost = cost;
                        if (colvo > 0) products[pos].Colvo = colvo;
                        SerDeser.Serialize<List<Product>>(products, file);
                        UseSclad();
                    }
                    else if (proverka == false)
                    {
                        Console.SetCursorPosition(90, 7);
                        Console.WriteLine("Такой ID уже занят");
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseSclad();
        }
        public void Filter(string file)
        {
            List<Product> products = SerDeser.Deserialize<Product>(file);
            Console.Clear();
            Menushka();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID\n  Название\n  Цена за штуку\n  Количечество на складе");
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
                    List<Product> sorted = new List<Product>();
                    if (possition == 2)
                    {
                        try
                        {
                            sorted = products.Where(item => item.ID == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 3)
                    {
                        sorted = products.Where(item => item.Name == parametr).ToList();
                    }
                    else if (possition == 4)
                    {
                        try
                        {
                            sorted = products.Where(item => item.Cost == Convert.ToDouble(parametr)).ToList();
                        }
                        catch { }
                    }
                    else if (possition == 5)
                    {
                        try
                        {
                            sorted = products.Where(item => item.Colvo == Convert.ToInt32(parametr)).ToList();
                        }
                        catch { }
                    }
                    Console.Clear();
                    Menushka();
                    MenuKnopochek1();
                    int i = OutputProduct(sorted);
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
                                foreach (var item in products)
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
                        UseSclad();
                    }
                }
            } while (possition != (int)Knopochka.Escape);
            UseSclad();
        }
        private int OutputProduct(List<Product> products)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("\tID\t\tНазвание\tЦена за штуку\tКол-во на складе");
            int i = 0;
            foreach (var product in products)
            {
                Console.WriteLine($"\t{product.ID}\t\t{product.Name}\t\t{product.Cost}\t\t{product.Colvo}");
                i++;
            }
            return i;
        }
        public void UseSclad()
        {
            Console.Clear();
            Menushka();
            MenuKnopochek1();
            string file = "Product.json";
            List<Product> products = SerDeser.Deserialize<Product>(file);
            Console.SetCursorPosition(0, 2);
            int i = OutputProduct(products);
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