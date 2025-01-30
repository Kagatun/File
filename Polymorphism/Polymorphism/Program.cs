namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentHandler = new PaymentHandler(new PaymentSystemFactory());
            var orderForm = new OrderForm();

            string systemId = orderForm.ShowForm();

            paymentHandler.ProcessPayment(systemId);
        }
    }

    public interface IPaymentSystem
    {
        void ProcessPayment();
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

    public class PaymentSystemFactory
    {
        private readonly Dictionary<string, Func<IPaymentSystem>> _paymentSystems;

        public PaymentSystemFactory()
        {
            _paymentSystems = new Dictionary<string, Func<IPaymentSystem>>
            {
                { "QIWI", () => new QIWI() },
                { "WebMoney", () => new WebMoney() },
                { "Card", () => new Card() }
            };
        }

        public IPaymentSystem CreatePaymentSystem(string systemId)
        {
            if (_paymentSystems.TryGetValue(systemId, out var paymentSystemCreator))
                return paymentSystemCreator();

            throw new ArgumentException($"Платежная система {systemId} не найдена.");
        }
    }

    public class PaymentHandler
    {
        private readonly PaymentSystemFactory _paymentSystemFactory;

        public PaymentHandler(PaymentSystemFactory paymentSystemFactory)
        {
            _paymentSystemFactory = paymentSystemFactory;
        }

        public void ProcessPayment(string systemId)
        {
            IPaymentSystem paymentSystem = _paymentSystemFactory.CreatePaymentSystem(systemId);
            paymentSystem.ProcessPayment();

            Console.WriteLine($"Вы оплатили с помощью {systemId}");
            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");
            Console.WriteLine("Какой системой вы хотите совершить оплату?");

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Неверный ввод");

            return input;
        }
    }
}