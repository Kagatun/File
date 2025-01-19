namespace Lesson
{
    public class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new FileLogger());
            Pathfinder pathfinder1 = new Pathfinder(new ConsoleLogger());
            Pathfinder pathfinder2 = new Pathfinder(new FridayLogger(new FileLogger()));
            Pathfinder pathfinder3 = new Pathfinder(new FridayLogger(new ConsoleLogger()));
            Pathfinder pathfinder4 = new Pathfinder(new FridayConsoleFileLogger(new ConsoleLogger(), new FileLogger()));

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
            File.AppendAllText(_nameFile, text + Environment.NewLine);
    }

    public class ConsoleLogger : ILogger
    {
        public void WriteError(string text) =>
            Console.WriteLine(text);
    }

    public class FridayLogger : ILogger
    {
        private readonly ILogger _logger;

        public FridayLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void WriteError(string text)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _logger.WriteError(text);
        }
    }

    public class FridayConsoleFileLogger : ILogger
    {
        private readonly ILogger _consoleLogger;
        private readonly ILogger _fileLogger;

        public FridayConsoleFileLogger(ILogger consoleLogger, ILogger fileLogger)
        {
            _consoleLogger = consoleLogger ?? throw new ArgumentNullException(nameof(consoleLogger));
            _fileLogger = fileLogger ?? throw new ArgumentNullException(nameof(fileLogger));
        }

        public void WriteError(string text)
        {
            _consoleLogger.WriteError(text);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _fileLogger.WriteError(text);
        }
    }

    public class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Find(string text) =>
            _logger.WriteError(text);
    }
}