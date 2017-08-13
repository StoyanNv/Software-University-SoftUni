namespace _03BarracksFactory.Models.Units
{
    class Gunner:Unit
    {
        private const int DefaultHealth = 50;
        private const int DefaultDamage = 10;

        public Gunner() : base(DefaultHealth, DefaultDamage)
        {
        }
    }
}
