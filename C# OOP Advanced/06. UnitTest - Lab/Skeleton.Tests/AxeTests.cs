using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture]
class AxeTests
{
    private const int AxeAttack = 1;
    private const int AxeDurability = 1;
    private const int DummyHealth = 20;
    private const int DummyXp = 1;

    private Axe axe;
    private Dummy dummy;
    [SetUp]
    public void TestInts()
    {
        //Arange
        this.axe = new Axe(AxeAttack, AxeDurability);
        this.dummy = new Dummy(DummyHealth, DummyXp);
    }

    [Test]
    public void AxeLosesDurabilyAfterAttack()
    {
        //Act
        axe.Attack(dummy);
        //Assert
        Assert.AreEqual(0, axe.DurabilityPoints, "Axe Durability doesn't change after attack");
    }

    [Test]
    public void AttackWithBrokenAxe()
    {

        //Act
        axe.Attack(dummy);
        //Assert
        Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
    }
}