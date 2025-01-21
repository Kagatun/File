using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Order order = new Order(1, 12000);
        string secretKey = "секретный ключ от системы";

        IPaymentSystem paymentSystem1 = new PaymentSystem1(new HasherMD5());
        IPaymentSystem paymentSystem2 = new PaymentSystem2(new HasherMD5());
        IPaymentSystem paymentSystem3 = new PaymentSystem3(new HasherSHA1(), secretKey);

        List<IPaymentSystem> paymentSystems = new List<IPaymentSystem> { paymentSystem1, paymentSystem2, paymentSystem3 };

        foreach (var paymentSystem in paymentSystems)
            Console.WriteLine(paymentSystem.GetPayingLink(order));
    }
}

public interface IPaymentSystem
{
    string GetPayingLink(Order order);
}

public interface IHasher
{
    string ComputeHash(string input);
}

public class Order
{
    public Order(int id, int amount)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Id = id;
        Amount = amount;
    }

    public int Id { get; }
    public int Amount { get; }
}

public class HasherMD5 : IHasher
{
    public string ComputeHash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}

public class HasherSHA1 : IHasher
{
    public string ComputeHash(string input)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha1.ComputeHash(inputBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}

public class PaymentSystem1 : IPaymentSystem
{
    private readonly IHasher _hasher;

    public PaymentSystem1(IHasher hasher)
    {
        _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
    }

    public string GetPayingLink(Order order)
    {
        string hash = _hasher.ComputeHash(order.Id.ToString());

        return $"pay.system1.ru/order?amount={order.Amount}RUB&hash={hash}";
    }
}

public class PaymentSystem2 : IPaymentSystem
{
    private readonly IHasher _hasher;

    public PaymentSystem2(IHasher hasher)
    {
        _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
    }

    public string GetPayingLink(Order order)
    {
        string hash = _hasher.ComputeHash(order.Id + order.Amount.ToString());

        return $"order.system2.ru/pay?hash={hash}";
    }
}

public class PaymentSystem3 : IPaymentSystem
{
    private readonly IHasher _hasher;
    private readonly string _secretKey;

    public PaymentSystem3(IHasher hasher, string secretKey)
    {
        _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        _secretKey = secretKey;
    }

    public string GetPayingLink(Order order)
    {
        string hash = _hasher.ComputeHash(order.Amount + order.Id + _secretKey);

        return $"system3.com/pay?amount={order.Amount}&currency=RUB&hash={hash}";
    }
}