using static Pract_10.Knopochka;

namespace Pract_9
{
    internal class Strelochki
    {
        private int MaxStrelochka;
        private int MinStrelochka;
        public Strelochki(int min, int max)
        {
            MaxStrelochka = max;
            MinStrelochka = min;
        }
        public int Menu()
        {
            int position = MinStrelochka;
            ConsoleKeyInfo key;
            do
            {
                Console.SetCursorPosition(0, position);
                Console.Write("->");
                key = Console.ReadKey(true);
                Console.SetCursorPosition(0, position);
                Console.Write("  ");
                position = key.Key == ConsoleKey.UpArrow && position != MinStrelochka ? position -= 1 : key.Key == ConsoleKey.DownArrow && position != MaxStrelochka ? position += 1 : position;

            } while (key.Key != ConsoleKey.F1 && key.Key != ConsoleKey.F2 && key.Key != ConsoleKey.F10 && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Delete && key.Key != ConsoleKey.S);
            if (key.Key == ConsoleKey.F1)
            {
                return (int) F1;
            }
            else if (key.Key == ConsoleKey.F2)
            {
                return (int) F2;
            }
            else if (key.Key == ConsoleKey.F10)
            {
                return (int) F10;
            }
            else if (key.Key == ConsoleKey.Delete)
            {
                return (int) Del;
            }
            else if (key.Key == ConsoleKey.S)
            {
                return (int)S;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return position;
            }
            else
            {
                return (int) Escape;
            }
        }
    }
}
