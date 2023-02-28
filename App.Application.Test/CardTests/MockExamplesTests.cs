using App.Application.Models;
using App.Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Test.CardTests
{
    public class MockExamplesTests
    {
        [Fact(DisplayName = "Testando save com card existente")]
        public void TestandoSaveComCardExistente()
        {
            //Arrange
            Mock<ICardRepository> mockRepository = new Mock<ICardRepository>();
            mockRepository.Setup(a => a.GetByNumber(It.IsAny<string>())).Returns(CardMock);
            CardServices service = new CardServices(mockRepository.Object);

            var myCard = new Card()
            {
                Number = "123",
                Id = 1,
                Brand = Brand.Visa,
                Status = CardStatus.Active,
                Responsible = new Responsible(1, "Deybson", "DOCUMENT")
            };

            //Act
            var saveMethod = () => service.Save(myCard);

            //Assert
            var exception = Assert.Throws<ArgumentException>(saveMethod);
            Assert.Contains("Já existe um cartão", exception.Message);
        }

        [Fact(DisplayName = "Testando save com card inexistente")]
        public void TestandoSaveComCardInExistente()
        {
            //Arrange
            Mock<ICardRepository> mockRepository = new Mock<ICardRepository>();
            mockRepository.Setup(a => a.GetByNumber(It.IsAny<string>())).Returns(null as Card);
            mockRepository.Setup(x => x.Save(It.IsAny<Card>())).Returns(7);

            CardServices service = new CardServices(mockRepository.Object);

            var myCard = new Card()
            {
                Number = "123",
                Id = 1,
                Brand = Brand.Visa,
                Status = CardStatus.Active,
                Responsible = new Responsible(1, "Deybson", "DOCUMENT")
            };

            //Act
            service.Save(myCard);

            //Assert
            Assert.Equal(7, myCard.Id);
        }


        private Card CardMock()
        {
            return new Card()
            {
                Number = "777777",
                Id = 7,
                Brand = Brand.Visa,
                Status = CardStatus.Active,
                Responsible = new Responsible(1, "Deybson", "DOCUMENT")
            };
        }
    }
}
