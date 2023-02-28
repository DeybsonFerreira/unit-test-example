using App.Application.Models;
using App.Application.Services;

CardServices service = new CardServices();

Responsible responsible = new Responsible(0, "Deybson", "123123123");

Card myCard = service.CreateCard(Brand.Visa, responsible);
myCard.Deposit(100);
myCard.Buy(50);
myCard.Buy(50);

string result = @$"
Saldo: {myCard.Money}:
Número: {myCard.Number} ";

Console.WriteLine(result);
