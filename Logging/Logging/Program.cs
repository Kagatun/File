﻿namespace Lesson
{
    public class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new FileLogger());
            Pathfinder pathfinder1 = new Pathfinder(new ConsoleLogger());
            Pathfinder pathfinder2 = new Pathfinder(new CompositeLogger(new List<ILogger> { new FileLogger() }, DayOfWeek.Friday));
            Pathfinder pathfinder3 = new Pathfinder(new CompositeLogger(new List<ILogger> { new ConsoleLogger() }, DayOfWeek.Friday));
            Pathfinder pathfinder4 = new Pathfinder(new CompositeLogger(new List<ILogger> { new ConsoleLogger(), new FileLogger() }, DayOfWeek.Friday));

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

    public class CompositeLogger : ILogger
    {
        private readonly List<ILogger> _loggers;
        private readonly DayOfWeek _dayOfWeek;

        public CompositeLogger(List<ILogger> loggers, DayOfWeek dayOfWeek)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
            _dayOfWeek = dayOfWeek;
        }

        public void WriteError(string text)
        {
            foreach (var logger in _loggers)
                if (logger is ConsoleLogger)
                    logger.WriteError(text);

            if (DateTime.Now.DayOfWeek == _dayOfWeek)
            {
                foreach (ILogger logger in _loggers)
                    if (logger is FileLogger)
                        logger.WriteError(text);
            }
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