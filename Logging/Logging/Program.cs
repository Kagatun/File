namespace Lesson
{
    public class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new FileLogger());
            Pathfinder pathfinder1 = new Pathfinder(new ConsoleLogger());
            Pathfinder pathfinder2 = new Pathfinder(new FridayFileLogger());
            Pathfinder pathfinder3 = new Pathfinder(new FridayLogger(new ConsoleLogger()));
            Pathfinder pathfinder4 = new Pathfinder(new FridayConsoleFileLogger(new List<ILogger> { new ConsoleLogger(), new FridayFileLogger() }));

            pathfinder.Find("Логирует сообщения в файл.");
            pathfinder1.Find("Логирует сообщения в консоль.");
            pathfinder2.Find("Логирует сообщения в файл, но только по пятницам.");
            pathfinder3.Find("Логирует сообщения в консоль, но только по пятницам.");
            pathfinder4.Find("Логирует сообщения в консоль, а по пятницам также в файл.");
        }
    }

    public interface ILogger
    {
        void WriteError(string text);
    }

    public class FileLogger : ILogger
    {
        private readonly string _nameFile = "log.txt";

        public void WriteError(string text) =>
            File.WriteAllText(_nameFile, text);
    }

    public class ConsoleLogger : ILogger
    {
        public void WriteError(string text) =>
            Console.WriteLine(text);
    }

    public class FridayFileLogger : ILogger
    {
        private readonly string _nameFile = "log.txt";

        public void WriteError(string text)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                File.WriteAllText(_nameFile, text);
        }
    }

    public class FridayLogger : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _nameFile = "log.txt";


        public FridayLogger(ILogger logger)
        {
            if(logger == null)
                throw new NullReferenceException();

            _logger = logger;
        }

        public void WriteError(string text)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                File.WriteAllText(_nameFile, text);
        }
    }

    public class FridayConsoleFileLogger : ILogger
    {
        private readonly List<ILogger> _loggers;
        private readonly string _nameFile = "log.txt";

        public FridayConsoleFileLogger(List<ILogger> loggers)
        {
            if (loggers == null)
                throw new NullReferenceException();

            _loggers = loggers;
        }

        public void WriteError(string text)
        {
            foreach (var logger in _loggers)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    File.WriteAllText(_nameFile, text);

                Console.WriteLine(text);
            }
        }
    }

    public class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            if (logger == null)
                throw new NullReferenceException();

            _logger = logger;
        }

        public void Find(string text) =>
            _logger.WriteError(text);
    }
}

