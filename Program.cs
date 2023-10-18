namespace RefactorIfElse;

internal class Program
{
    static void Main(string[] args)
    {
        const int quantity = 60;
        const int price = 100;


        Console.WriteLine("Normal If-else Version");
        var discountAmt = GetDiscount(quantity, price);
        Console.WriteLine($"Discount value of quantity {quantity} with price {price} is {discountAmt}");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Using Chain of Responsibility");
        ChainOfResponsibility firstCondition = new FirstCondition();
        ChainOfResponsibility secondCondition = new SecondCondition();
        ChainOfResponsibility thirdCondition = new ThirdCondition();
        ChainOfResponsibility finalCondition = new FinalCondition();

        firstCondition.SetNext(secondCondition);
        secondCondition.SetNext(thirdCondition);
        thirdCondition.SetNext(finalCondition);

        var discountAmt2 = firstCondition.HandleRequest(quantity, price);
        Console.WriteLine($"Discount value of quantity {quantity}  with price  {price} is {discountAmt2}");

        Console.Read();
    }

    static int GetDiscount(int quantity, int price)
    {
        var finalPrice = 0;
        if (quantity is > 10 and <= 50)
        {
            finalPrice = price * 10 / 100;
        }

        else if (quantity is > 50 and <= 100)
        {
            finalPrice = price * 20 / 100;
        }
        else if (quantity > 100)
        {
            finalPrice = price * 30 / 100;
        }
        else
        {
            finalPrice = price * 5 / 100;
        }
        return finalPrice;
    }
}

public abstract class ChainOfResponsibility
{
    public ChainOfResponsibility Next = null!;

    public void SetNext(ChainOfResponsibility next)
    {
        Next = next;
    }

    public virtual int HandleRequest(int quantity, int price)
    {
        return Next.HandleRequest(quantity, price);
    }
}

public class FirstCondition : ChainOfResponsibility
{
    public override int HandleRequest(int quantity, int price)
    {
        if (quantity is > 10 and <= 50)
        {
            return price * 10 / 100;
        }
        return base.HandleRequest(quantity, price);
    }
}

public class SecondCondition : ChainOfResponsibility
{
    public override int HandleRequest(int quantity, int price)
    {
        if (quantity is > 50 and <= 100)
        {
            return price * 20 / 100;
        }
        return base.HandleRequest(quantity, price);
    }
}

public class ThirdCondition : ChainOfResponsibility
{
    public override int HandleRequest(int quantity, int price)
    {
        if (quantity > 100)
        {
            return price * 30 / 100;
        }
        return base.HandleRequest(quantity, price);
    }
}

public class FinalCondition : ChainOfResponsibility
{
    public override int HandleRequest(int quantity, int price)
    {
        return price * 5 / 100;
    }
}
