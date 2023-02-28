using App.Application.Models;
using App.Application.Services;

namespace App.Application.Test.CardTests
{
    public class CreateCardTests
    {
        [Fact(DisplayName = "Testando cartão com número")]
        public void DeveCriarCartaoComNumero()
        {
            //Arrange
            CardServices services = new CardServices();
            Responsible responsible = new Responsible(1, "Deybson", "123124");

            //Act
            var card = services.CreateCard(Models.Brand.Mastercard, responsible);

            //Assert xunit
            Assert.NotNull(card.Number);
            Assert.NotEqual("", card.Number);
        }

        [Fact(DisplayName = "Testando cartão com pessoa sem doc")]
        public void DeveRetornarErroPessoaSemDoc()
        {
            //Arrange
            CardServices services = new CardServices();
            Responsible responsible = new Responsible(1, "Deybson", "");

            //Act
            Func<Card> card = () => services.CreateCard(Models.Brand.Mastercard, responsible);

            //Assert
            var exception = Assert.Throws<ArgumentException>(card);
            Assert.Contains("campo obrigatório", exception.Message.ToLower());
        }

        [Fact(DisplayName = "Testando Depósito")]
        public void TestandoDeposito()
        {
            //Arrange
            CardServices service = new CardServices();
            Responsible responsible = new Responsible(0, "Deybson", "123123123");

            //Act
            Card myCard = service.CreateCard(Brand.Visa, responsible);
            myCard.Deposit(1000);

            //Assert
            Assert.Equal(1000, myCard.Money);
        }

        [Fact(DisplayName = "Testando Compra")]
        public void TestandoCompra()
        {
            //Arrange
            CardServices service = new CardServices();
            Responsible responsible = new Responsible(0, "Deybson", "123123123");

            //Act
            Card myCard = service.CreateCard(Brand.Visa, responsible);
            myCard.Deposit(1000);
            myCard.Buy(100);
            myCard.Buy(100);


            //Assert
            Assert.Equal(800, myCard.Money);
        }

        [Theory(DisplayName = "Testando Documentos válidos")]
        [InlineData("1")]
        [InlineData("123")]
        [InlineData("123456")]
        public void TestandoDocumentosValidos(string document)
        {
            try
            {
                //Arrange
                CardServices service = new CardServices();
                Responsible responsible = new Responsible(0, "Deybson", document);

                //act
                service.ValidResponsibleCard(responsible);

            }
            catch (Exception error)
            {
                //Assert
                Assert.Fail(error.Message);
            }
        }

        [Theory(DisplayName = "Testando Documentos inválidos")]
        [InlineData("")]
        [InlineData(null)]
        public void TestandoDocumentosNaoValidos(string document)
        {
            try
            {
                //Arrange
                CardServices service = new CardServices();
                Responsible responsible = new Responsible(0, "Deybson", document);

                //act
                service.ValidResponsibleCard(responsible);
                Assert.Fail("Não esperado");

            }
            catch (Exception error)
            {
                Assert.Contains("Campo obrigatório", error.Message);
            }
        }


    }
}
