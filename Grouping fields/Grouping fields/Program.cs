class Player
{
    public Player(Weapon weapon, Mover mover, int age, string name)
    {
        if (age <= 0)
            throw new ArgumentOutOfRangeException(nameof(age));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name));

        Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        Mover = mover ?? throw new ArgumentNullException(nameof(mover));
        Age = age;
        Name = name;
    }

    public Weapon Weapon { get; }
    public Mover Mover { get; }
    public int Age { get; }
    public string Name { get; }

    public void Attack() =>
        Weapon.Attack();

    public bool IsReloading() =>
        Weapon.IsReloading();

    public void Move() =>
        Mover.Move();
}

public class Weapon
{
    public Weapon(float attackCooldown, int damage)
    {
        if (attackCooldown <= 0)
            throw new ArgumentOutOfRangeException(nameof(attackCooldown));

        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        AttackCooldown = attackCooldown;
        Damage = damage;
    }

    public float AttackCooldown { get; }
    public int Damage { get; }

    public void Attack()
    {
        //attack
    }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
}

public class Mover
{
    private float _directionX;
    private float _directionY;

    public Mover(float speed, float directionX, float directionY)
    {
        if (speed <= 0)
            throw new ArgumentOutOfRangeException(nameof(speed));

        Speed = speed;
        _directionX = directionX;
        _directionY = directionY;

        NormalizeDirection();
    }

    public float Speed { get; }

    public void Move()
    {
        //Do move
    }

    private void NormalizeDirection()
    {
        float length = MathF.Sqrt(_directionX * _directionX + _directionY * _directionY);

        if (length == 0)
            throw new InvalidOperationException();

        _directionX /= length;
        _directionY /= length;
    }
}
