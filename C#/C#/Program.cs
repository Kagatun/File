namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Как вас зовут?");
            string name = Console.ReadLine();

            Console.WriteLine("Сколько Вам лет?");
            string age = Console.ReadLine();

            Console.WriteLine("Ваш знак зодиака?");
            string zodiac = Console.ReadLine();

            Console.WriteLine("Где вы работаете?");
            string work = Console.ReadLine();

            Console.WriteLine("Вас зовут " + name + ", Вам " + age + " лет," + " Ваш знак зодиака " + zodiac + ", Вы работаете на(в) " + work + ".");
        }
    }
}
