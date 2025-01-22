class Player
{
    public Player(Weapon weapon, Mover mover, int age, string name)
    {
        if (age <= 0)
            throw new ArgumentException(nameof(age));

        Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        Mover = mover ?? throw new ArgumentNullException(nameof(mover));
        Age = age;
        Name = name;
    }

    public Weapon Weapon { get; private set; }
    public Mover Mover { get; private set; }
    public int Age { get; private set; }
    public string Name { get; private set; }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }

    public void Attack() =>
        Weapon.Attack();

    public void Move() =>
        Mover.Move();
}

public class Weapon
{
    public Weapon(float attackCooldown, int damage)
    {
        if (attackCooldown <= 0)
            throw new ArgumentException(nameof(attackCooldown));

        if (damage <= 0)
            throw new ArgumentException(nameof(damage));

        AttackCooldown = attackCooldown;
        Damage = damage;
    }

    public float AttackCooldown { get; private set; }
    public int Damage { get; private set; }

    public void Attack()
    {
        //attack
    }
}

public class Mover
{
    public Mover(float speed, float directionX, float directionY)
    {
        if (speed <= 0)
            throw new ArgumentException(nameof(speed));

        Speed = speed;
        DirectionX = directionX;
        DirectionY = directionY;
    }

    public float Speed { get; private set; }
    public float DirectionX { get; private set; }
    public float DirectionY { get; private set; }

    public void Move()
    {
        //Do move
    }
}
