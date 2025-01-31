class Program
{
    static void Main(string[] args)
    {
        string name = "Ivanov";
        string surname = "Ivan";
        string firstCup = "Tea";
        string secondCup = "Coffee";

        Console.WriteLine(surname + " " + name + "\n" + firstCup + " " + secondCup);

        string correctName = "Ivan";
        string tempCup = "Coffee";

        surname = name;
        name = correctName;
        secondCup = firstCup;
        firstCup = tempCup;

        Console.WriteLine(surname + " " + name + "\n" + firstCup + " " + secondCup);
    }
}