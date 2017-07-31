using System;
using System.Collections.Generic;

public class CoffeeMachine
{
    public List<CoffeeType> sold;
    private int coins;

    public CoffeeMachine()
    {
        this.sold = new List<CoffeeType>();
    }
    public IEnumerable<CoffeeType> CoffeesSold
    {
        get => sold;
    }
    public void BuyCoffee(string size, string type)
    {
        var price = (CoffeePrice)Enum.Parse(typeof(CoffeePrice), size);
        var coffeeType = (CoffeeType)Enum.Parse(typeof(CoffeeType), type);
        if (this.coins >= (int)price)
        {
            sold.Add(coffeeType);
            coins = 0;
        }
    }
    public void InsertCoin(string coin)
    {
        var enterCoins = (Coin)Enum.Parse(typeof(Coin), coin);
        coins += (int)enterCoins;
    }
}