class Weapon
{
    private int _bulletsCount;

    public bool CanShoot => _bulletsCount > 0;

    public void Shoot()
    {
        if(CanShoot == false)
            throw new InvalidOperationException();

        _bulletsCount --;
    }
}
