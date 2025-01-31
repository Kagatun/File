class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите имя:");
        string name = Console.ReadLine();

        Console.WriteLine("Введите число сколько раз нужно повторить имя?");
        int numberRepetitions = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < numberRepetitions; i++)
            Console.WriteLine(name);
    }
}
