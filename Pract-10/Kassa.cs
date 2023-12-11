using Pract_9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_10
{
    internal class Kassa
    {
        private List<SelectedProduct> selectProducts = new List<SelectedProduct>();
        private double Sum;
        private string Name;
        public Kassa(string login)
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
        private void MenuKnopochek()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("+ - Добавить товар");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("- - Убрать товар");
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("Esc - Вернуться обратно");
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
        private void InputProduct(string fileProduct)
        {
            List<Product> products = SerDeser.Deserialize<Product>(fileProduct);
            foreach (var product in products)
            {
                SelectedProduct selectedProduct = new SelectedProduct(product.ID, product.Name, product.Cost, product.Colvo, 0);
                selectProducts.Add(selectedProduct);
            }
        }
        private int OutputProduct()
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("\tID\t\tНазвание\tЦена за штуку\tКоличество");
            int i = 0;
            double sum = 0;
            if (selectProducts.Count != 0)
            {
                foreach (var item in selectProducts)
                {
                    Console.WriteLine($"\t{item.ID}\t\t{item.Name}\t\t{item.Cost}\t\t{item.ViborColvo}");
                    i++;
                    sum = sum + item.Cost * item.ViborColvo;
                }
            }
            Console.WriteLine($"------------------------------------------------------------------------------------------\n\t\t\t\t\tИтоговая сумма: {sum}");
            Sum = sum;
            return i;
        }
        public void UseKassa()
        {
            Console.Clear();
            Menushka();
            MenuKnopochek2();
            string fileProduct = "Product.json";
            string fileSelectedProduct = "SelectedProduct.json";
            string fileBuh = "Accountant.json";
            Console.SetCursorPosition(0, 2);
            if (selectProducts.Count == 0) InputProduct(fileProduct);
            int i = OutputProduct();
            if (i == 0) i = 1;
            int possition;
            do
            {
                Strelochki strelochki = new Strelochki(3, i + 2);
                possition = strelochki.Menu();
                if (possition == (int)Knopochka.Escape)
                {
                    Autorization.Vxod();
                }
                else if (possition >= 0)
                {
                    Read(possition - 3, fileSelectedProduct);
                }
                else if (possition == (int)Knopochka.S)
                {
                    Complete(fileSelectedProduct, fileProduct, fileBuh);
                    Autorization.Vxod();
                }
            } while (possition != (int)Knopochka.Escape);
        }
        private void Read(int pos, string file)
        {
            Console.Clear();
            Menushka();
            MenuKnopochek();
            Console.SetCursorPosition(0, 2);
            try
            {
                Console.WriteLine($"  ID: {selectProducts[pos].ID}\n  Название: {selectProducts[pos].Name}\n  Цена за штуку: {selectProducts[pos].Cost}\n  Количество: {selectProducts[pos].ViborColvo}");
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.OemPlus && selectProducts[pos].Colvo != selectProducts[pos].ViborColvo)
                    {
                        selectProducts[pos].ViborColvo += 1;
                        Console.SetCursorPosition(14, 5);
                        Console.WriteLine("       ");
                        Console.SetCursorPosition(14, 5);
                        Console.WriteLine(selectProducts[pos].ViborColvo);
                    }
                    if (key.Key == ConsoleKey.OemMinus && 0 != selectProducts[pos].ViborColvo)
                    {
                        selectProducts[pos].ViborColvo -= 1;
                        Console.SetCursorPosition(14, 5);
                        Console.WriteLine("       ");
                        Console.SetCursorPosition(14, 5);
                        Console.WriteLine(selectProducts[pos].ViborColvo);
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        UseKassa();
                    }
                } while (key.Key != ConsoleKey.Escape);
            }
            catch
            {
                UseKassa();
            }
        }
        private void Complete(string fileSelectedProduct, string fileProduct, string fileBuh)
        {
            List<SelectedProduct> selectProductss = SerDeser.Deserialize<SelectedProduct>(fileSelectedProduct);
            foreach (var item in selectProducts)
            {
                selectProductss.Add(item);
            }
            SerDeser.Serialize<List<SelectedProduct>>(selectProductss, fileSelectedProduct);
            List<Product> products = SerDeser.Deserialize<Product>(fileProduct);
            int i = 0;
            foreach (var item in products)
            {
                item.Colvo -= selectProducts[i].ViborColvo;
                i++;
            }
            SerDeser.Serialize<List<Product>>(products, fileProduct);
            List<Buh> buhs = SerDeser.Deserialize<Buh>(fileBuh);
            int ID = 0;
            foreach (var item in buhs)
            {
                if (ID != item.ID) break;
                else ID++;
            }
            Buh buh = new Buh();
            buh.ID = ID;
            buh.Sum = Sum;
            buh.Date = DateOnly.FromDateTime(DateTime.Now);
            buh.Operation = true;
            buhs.Add(buh);
            SerDeser.Serialize<List<Buh>>(buhs, fileBuh);
            return;
        }
    }
}


