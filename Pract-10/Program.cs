using Pract_10;
using Pract_9;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string file = "User.json";
        List<User> users = SerDeser.Deserialize<User>(file);
        Boolean proverka = false;
        foreach (User user in users)
        {
            if (user.Role == 0) proverka = true;
        }
        int ID = 0;
        foreach (var user in users)
        {
            if (ID != user.ID) break;
            else ID++;
        }
        if (proverka == false)
        {
            User user = new User();
            user.ID = ID;
            user.Login = "admin";
            user.Password = "admin";
            user.Role = 0;
            users.Add(user);
            SerDeser.Serialize<List<User>>(users, file);
        }
        Autorization.Vxod();
    }
}


static public class Autorization
{
    static public void Vxod()
    {
        Console.Clear();
        Console.WriteLine("\t\t\t\t\tДобро пожаловать в магазин Монголия!\n------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("  Логин:\n  Пароль:\n  Авторизоваться");
        Strelochki strelochki = new Strelochki(2, 4);
        int possition = 0;
        string login = "";
        int posLogin = 9;
        int posPassword = 10;
        string password = "";
        do
        {
            possition = strelochki.Menu();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("                             ");
            if (possition == 2)
            {
                ConsoleKeyInfo key;
                do
                {
                    Console.SetCursorPosition(posLogin, 2);
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Backspace && posLogin != 9)
                    {
                        int login1 = login.Length - 1;
                        login = login.Remove(login1);
                        posLogin--;
                        Console.SetCursorPosition(posLogin, 2);
                        Console.WriteLine(" ");
                    }
                    else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow && key.Key != ConsoleKey.DownArrow && key.Key != ConsoleKey.UpArrow && key.Key != ConsoleKey.Delete && key.Key != ConsoleKey.Tab && key.Key != ConsoleKey.F1 && key.Key != ConsoleKey.F2 && key.Key != ConsoleKey.F3 && key.Key != ConsoleKey.F4 && key.Key != ConsoleKey.F5 && key.Key != ConsoleKey.F6 && key.Key != ConsoleKey.F7 && key.Key != ConsoleKey.F8 && key.Key != ConsoleKey.F9 && key.Key != ConsoleKey.F10 && key.Key != ConsoleKey.F11 && key.Key != ConsoleKey.F12)
                    {
                        Console.WriteLine(key.KeyChar);
                        login = login + key.KeyChar;
                        posLogin++;
                    }
                } while (key.Key != ConsoleKey.Enter);
            }
            if (possition == 3)
            {
                ConsoleKeyInfo key;
                do
                {
                    Console.SetCursorPosition(posPassword, 3);
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Backspace && posPassword != 10)
                    {
                        int password1 = password.Length - 1;
                        password = password.Remove(password1);
                        posPassword--;
                        Console.SetCursorPosition(posPassword, 3);
                        Console.WriteLine(" ");

                    }
                    else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow && key.Key != ConsoleKey.DownArrow && key.Key != ConsoleKey.UpArrow && key.Key != ConsoleKey.Delete && key.Key != ConsoleKey.Tab && key.Key != ConsoleKey.F1 && key.Key != ConsoleKey.F2 && key.Key != ConsoleKey.F3 && key.Key != ConsoleKey.F4 && key.Key != ConsoleKey.F5 && key.Key != ConsoleKey.F6 && key.Key != ConsoleKey.F7 && key.Key != ConsoleKey.F8 && key.Key != ConsoleKey.F9 && key.Key != ConsoleKey.F10 && key.Key != ConsoleKey.F11 && key.Key != ConsoleKey.F12)
                    {
                        password = password + key.KeyChar;
                        Console.WriteLine("*");
                        posPassword++;
                    }
                } while (key.Key != ConsoleKey.Enter);
            }
            if (possition == 4)
            {
                List<User> users = SerDeser.Deserialize<User>("User.json");
                foreach (User user in users)
                {
                    if (user.Login == login && user.Password == password)
                    {
                        if (user.Role == 0)
                        {
                            Admin admin = new Admin(login);
                            admin.UseAdmin();
                        }
                        else if (user.Role == 1)
                        {
                            Kassa kassa = new Kassa(login); 
                            kassa.UseKassa();
                        }
                        else if (user.Role == 2)
                        {
                            HR hr = new HR(login);
                            hr.UseHR();
                        }
                        else if (user.Role == 3)
                        {
                            Sclad sclad = new Sclad(login);
                            sclad.UseSclad();
                        }
                        else if (user.Role == 4)
                        {
                            Accountant accountant = new Accountant(login);
                            accountant.UseAccountant();
                        }
                    }
                }
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Неверный логин или пароль");
                Console.SetCursorPosition(9, 2);
                Console.WriteLine("                                                           "); 
                Console.SetCursorPosition(10, 3);
                Console.WriteLine("                                                           ");
                login = "";
                posLogin = 9;
                posPassword = 10;
                password = "";
            }
        } while (possition != (int)Knopochka.Escape);
        Process.GetCurrentProcess().Kill();
    }
}
