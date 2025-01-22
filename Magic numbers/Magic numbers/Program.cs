class Weapon
{
    private int _bulletsCount;

    public bool CanShoot => _bulletsCount > 0;

    public Weapon(int bulletsCount)
    {
        if(bulletsCount < 0)
            throw new ArgumentException(nameof(bulletsCount));

        _bulletsCount = bulletsCount;
    }

    public void Shoot()
    {
        if(CanShoot == false)
            throw new InvalidOperationException();

        _bulletsCount --;
    }
}
