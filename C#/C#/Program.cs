namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            int waitingTime = 10;
            int minutesPerHour = 60;

            Console.Write("Введите колличество пациентов:");
            int numberPatients = Convert.ToInt32(Console.ReadLine());

            int totalMinutes = (numberPatients * waitingTime);
            int waitingHours = (totalMinutes / minutesPerHour);
            int waitingMinutes = (totalMinutes % minutesPerHour);

            Console.WriteLine("Вам осталось ждать до своей очереди " + waitingHours + " часов и " + waitingMinutes + " минут.");
        }
    }
}

