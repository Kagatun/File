using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Order order = new Order(1, 12000);
        string secretKey = "QWERTY";

        IPaymentSystem paymentSystem1 = new PaymentSystem1(new Hasher());
        IPaymentSystem paymentSystem2 = new PaymentSystem2(new Hasher());
        IPaymentSystem paymentSystem3 = new PaymentSystem3(new Hasher(), secretKey);

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
    string ComputeMD5(string input);
    string ComputeSHA1(string input);
}

public class Order
{
    public Order(int id, int amount) => (Id, Amount) = (id, amount);

    public int Id { get; }
    public int Amount { get; }
}

public class Hasher : IHasher
{
    public string ComputeMD5(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }

    public string ComputeSHA1(string input)
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

    public PaymentSystem1(IHasher hasher) => _hasher = hasher;

    public string GetPayingLink(Order order)
    {
        string hash = _hasher.ComputeMD5(order.Id.ToString());

        return $"pay.system1.ru/order?amount={order.Amount}RUB&hash={hash}";
    }
}

public class PaymentSystem2 : IPaymentSystem
{
    private readonly IHasher _hasher;

    public PaymentSystem2(IHasher hasher) => _hasher = hasher;

    public string GetPayingLink(Order order)
    {
        string hash = _hasher.ComputeMD5(order.Id + order.Amount.ToString());

        return $"order.system2.ru/pay?hash={hash}";
    }
}

public class PaymentSystem3 : IPaymentSystem
{
    private readonly IHasher _hasher;
    private readonly string _secretKey;

    public PaymentSystem3(IHasher hasher, string secretKey)
    {
        _hasher = hasher;
        _secretKey = secretKey;
    }

    public string GetPayingLink(Order order)
    {
        string hash = _hasher.ComputeSHA1(order.Amount + order.Id + _secretKey);

        return $"system3.com/pay?amount={order.Amount}&curency=RUB&hash={hash}";
    }
}