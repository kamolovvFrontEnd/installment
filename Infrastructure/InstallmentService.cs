namespace WebApplication1.Infrastructure;

public class InstallmentService
{
    public IResult Installments(string product, int price, string phoneNumber, int installment)
    {
        if (price <= 100)
            return Results.BadRequest("Price must be greater than 100");
        
        string[] validProducts = ["phone", "computer", "tv"];
        if (!validProducts.Contains(product))
            return Results.BadRequest("Incorrect product!"); 

        int[] validInstallments = [3, 6, 9, 12, 18, 24];
        if (!validInstallments.Contains(installment))
            return Results.BadRequest("Incorrect installment!");

        if (phoneNumber.Length is not 9)
            return Results.BadRequest("Incorrect phoneNumber! Should be 9 digits!");

        double extraFee = 0;
        string productName = product switch
        {
            "phone" => "Смартфон",
            "computer" => "Компьютер",
            "tv" => "Телевизор",
            _ => "Товар"
        };

        switch (product)
        {
            case "phone":
                if (installment == 12) extraFee = PercentOf(price, 3);
                if (installment == 18) extraFee = PercentOf(price, 6);
                if (installment == 24) extraFee = PercentOf(price, 9);
                break;

            case "computer":
                if (installment == 18) extraFee = PercentOf(price, 4);
                if (installment == 24) extraFee = PercentOf(price, 8);
                break;

            case "tv":
                if (installment == 24) extraFee = PercentOf(price, 5);
                break;
        }

        double total = price + extraFee;

        return Results.Ok( new 
        {
            message =
                $"Вы купили {productName} за {price} сомони в рассрочку на {installment} месяцев. Итоговая сумма: {total} сомони.",
            phoneNumber = $"+992 {phoneNumber}",
        });
    }
    
    public static double PercentOf(double amount, double percent)
    {
        return amount * percent / 100;
    }
}