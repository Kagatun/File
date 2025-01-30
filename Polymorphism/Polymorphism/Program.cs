namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentSystemFactories = new Dictionary<string, IPaymentSystemFactory>
            {
                { "QIWI", new QIWIPaymentSystemFactory() },
                { "WebMoney", new WebMoneyPaymentSystemFactory() },
                { "Card", new CardPaymentSystemFactory() }
            };

            var orderForm = new OrderForm(paymentSystemFactories.Keys);

            string systemId = orderForm.ShowForm();

            if (paymentSystemFactories.TryGetValue(systemId, out var paymentSystemFactory))
            {
                var paymentHandler = new PaymentHandler(paymentSystemFactory);
                paymentHandler.ProcessPayment();
            }
            else
            {
                throw new ArgumentException("Платежная система не найдена.");
            }
        }
    }

    public interface IPaymentSystem
    {
        void ProcessPayment();
    }

    public interface IPaymentSystemFactory
    {
        IPaymentSystem CreatePaymentSystem();
    }

    public class QIWI : IPaymentSystem
    {
        public void ProcessPayment()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
            Console.WriteLine("Проверка платежа через QIWI...");
        }
    }

    public class WebMoney : IPaymentSystem
    {
        public void ProcessPayment()
        {
            Console.WriteLine("Вызов API WebMoney...");
            Console.WriteLine("Проверка платежа через WebMoney...");
        }
    }

    public class Card : IPaymentSystem
    {
        public void ProcessPayment()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            Console.WriteLine("Проверка платежа через Card...");
        }
    }

    public class QIWIPaymentSystemFactory : IPaymentSystemFactory
    {
        public IPaymentSystem CreatePaymentSystem() =>
             new QIWI();
    }

    public class WebMoneyPaymentSystemFactory : IPaymentSystemFactory
    {
        public IPaymentSystem CreatePaymentSystem() =>
             new WebMoney();
    }

    public class CardPaymentSystemFactory : IPaymentSystemFactory
    {
        public IPaymentSystem CreatePaymentSystem() =>
             new Card();
    }

    public class PaymentHandler
    {
        private readonly IPaymentSystem _paymentSystem;

        public PaymentHandler(IPaymentSystemFactory paymentSystemFactory)
        {
            _paymentSystem = paymentSystemFactory.CreatePaymentSystem();
        }

        public void ProcessPayment()
        {
            _paymentSystem.ProcessPayment();
            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    public class OrderForm
    {
        private readonly IEnumerable<string> _availablePaymentSystems;

        public OrderForm(IEnumerable<string> availablePaymentSystems)
        {
            _availablePaymentSystems = availablePaymentSystems;
        }

        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: " + string.Join(", ", _availablePaymentSystems));
            Console.WriteLine("Какой системой вы хотите совершить оплату?");

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Неверный ввод");

            return input;
        }
    }
}