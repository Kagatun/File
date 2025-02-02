class Program
{
    static void Main(string[] args)
    {
        const string CommandDefaultAttack = "1";
        const string CommandFireBall = "2";
        const string CommandExplosion = "3";
        const string CommandHealing = "4";

        Random random = new Random();

        string defaultAttack = "Обычная атака";
        string spellFireBall = "Огненный шар";
        string spellExplosion = "Взрыв";
        string spellHealing = "Исцеление";

        int healthBoss = 2500;
        int damageBossMin = 25;
        int damageBossMax = 150;

        int healthHero = 1000;
        int manaHero = 100;
        int currentHealthHero = healthHero;
        int currentManaHero = manaHero;
        int manaConsumptionPerFireball = 20;
        int manaRecovery = 15;
        int healing = 150;
        int amountHealing = 3;
        int damageFireBall = 250;
        int damageMinExplosion = 50;
        int damageMaxExplosion = 600;
        int normalDamage = 125;

        bool hasFireballBeenApplied = false;

        Console.WriteLine($"Герой, помоги нам сразить Босса!\nКто, если не мы?\nГде, если не здесь?\nКогда, если не сейчас?\nДа помогут нам выйти из тьмы Сила, Отвага и Честь!\n");
        Console.WriteLine($"\nУ вас есть 4 заклинания, активируются вводом цифр:\n");
        Console.WriteLine($"{CommandDefaultAttack} {defaultAttack} наносит {normalDamage} урона");
        Console.WriteLine($"{CommandFireBall} {spellFireBall} наносит {damageFireBall} урона и расходует {manaConsumptionPerFireball} манны");
        Console.WriteLine($"{CommandExplosion} {spellExplosion} наносит урон от {damageMinExplosion} до {damageMaxExplosion}. Возможно применить после применения {spellFireBall}");
        Console.WriteLine($"{CommandHealing} {spellHealing} восстанавливает {healing} здоровья и {manaRecovery} манны. Можно использовать {amountHealing} раза");
        Console.WriteLine($"\nБосс всегда наносит ответный урон от {damageBossMin} до {damageBossMax}\n");

        while (currentHealthHero > 0 && healthBoss > 0)
        {
            int attackBoss = random.Next(damageBossMin, damageBossMax + 1);
            int damageExplosion = random.Next(damageMinExplosion, damageMaxExplosion + 1);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{CommandDefaultAttack} - Заклинание {defaultAttack} нанесёт ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{CommandFireBall} - Заклинание {spellFireBall}");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{CommandExplosion} - Заклинание {spellExplosion}, готовность {hasFireballBeenApplied}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{CommandHealing} - Заклинание {spellHealing} осталось {amountHealing} зарядов");

            Console.ResetColor();

            Console.WriteLine($"\nЗдоровье Героя {currentHealthHero}, манна Героя {currentManaHero} \tЗдоровье Босса {healthBoss}\n");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandDefaultAttack:
                    Console.Clear();
                    healthBoss -= normalDamage;
                    currentHealthHero -= attackBoss;
                    Console.WriteLine($"\nГерой нанес урон {normalDamage} \tБосс нанес урон {attackBoss}\n");
                    break;

                case CommandFireBall:
                    Console.Clear();

                    if (manaHero >= manaConsumptionPerFireball)
                    {
                        hasFireballBeenApplied = true;
                        currentManaHero -= manaConsumptionPerFireball;
                        currentHealthHero -= attackBoss;
                        healthBoss -= damageFireBall;
                        Console.WriteLine($"\nГерой нанес урон {damageFireBall} \tБосс нанес урон {attackBoss}\n");
                    }
                    break;

                case CommandExplosion:
                    Console.Clear();

                    if (hasFireballBeenApplied)
                    {
                        hasFireballBeenApplied = false;
                        currentHealthHero -= attackBoss;
                        healthBoss -= damageExplosion;

                        Console.WriteLine($"\nГерой нанес урон {damageExplosion} \tБосс нанес урон {attackBoss}\n");
                    }
                    else
                    {
                        currentHealthHero -= attackBoss;
                        Console.WriteLine($"\nОсечка \t\t\tБосс нанес урон {attackBoss}\n");
                    }
                    break;

                case CommandHealing:
                    Console.Clear();

                    if (amountHealing > 0)
                    {
                        amountHealing --;
                        currentHealthHero += healing;
                        currentManaHero += manaRecovery;

                        if (currentHealthHero > healthHero)
                            currentHealthHero = healthHero;

                        if (currentManaHero > manaHero)
                            currentManaHero = manaHero;

                        Console.WriteLine($"\nГерой исцелился \tБосс нанес урон {attackBoss}\n");
                        break;
                    }
                    else
                    {
                        currentHealthHero -= attackBoss;
                        Console.WriteLine($"\nОсечка \t\t\tБосс нанес урон {attackBoss}\n");
                    }
                    break;

                default:
                    Console.Clear();
                    currentHealthHero -= attackBoss;
                    Console.WriteLine($"Осечка \t\t\tБосс нанес урон {attackBoss}\n");
                    break;
            }
        }

        Console.WriteLine($"\nЗдоровье Героя {currentHealthHero} \tЗдоровье Босса {healthBoss}\n");

        if (currentHealthHero <= 0 && healthBoss <= 0)
            Console.WriteLine("НИЧЬЯ");
        else if (currentHealthHero <= 0)
            Console.WriteLine("БОСС ПОБЕДИЛ!");
        else if (healthBoss <= 0)
            Console.WriteLine("ГЕРОЙ ПОБЕДИЛ!!!");
    }
}
