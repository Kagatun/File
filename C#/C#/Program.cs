class Program
{
    static void Main(string[] args)
    {
        string name = "Ivanov";
        string surname = "Ivan";

        Console.WriteLine(surname + " " + name);

        string correctName = surname;
        surname = name;
        name = correctName;

        Console.WriteLine(surname + " " + name);
    }
}