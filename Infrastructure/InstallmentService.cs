using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication1.Infrastructure;

public class InstallmentService
{
    public IResult Installments(string product, int price, string phoneNumber, int installment)
    {
        string[] validProducts = ["phone", "computer", "tv"];
        if (!validProducts.Contains(product))
            return Results.NotFound("Product not found"); 

        int[] validInstallments = [3, 6, 9, 12, 18, 24];
        if (!validInstallments.Contains(installment))
            return Results.BadRequest("Incorrect installment"); 

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
                extraFee = installment switch
                {
                    12 => PercentOf(price, 3),
                    18 => PercentOf(price, 6),
                    24 => PercentOf(price, 9),
                    _ => extraFee
                };
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

        return Results.Ok(
                $"Вы купили {productName} за {price} сомони в рассрочку на {installment} месяцев. Итоговая сумма: {total} сомони.");
    }
    
    public static double PercentOf(double amount, double percent)
    {
        return amount * percent / 100;
    }
}