namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            int waitingTime = 10;
            int totalMinutes;
            int minutesPerHour = 60;
            int hoursOfWaiting;
            int minutesOfWaiting;
            int numberOfPatients;

            Console.Write("Введите колличество пациентов:");
            numberOfPatients = Convert.ToInt32(Console.ReadLine());

            totalMinutes = (numberOfPatients * waitingTime);
            hoursOfWaiting = (totalMinutes / minutesPerHour);
            minutesOfWaiting = (totalMinutes % minutesPerHour);

            Console.WriteLine("Вам осталось ждать до своей очереди " + hoursOfWaiting + " часов и " + minutesOfWaiting + " минут.");
        }
    }
}

