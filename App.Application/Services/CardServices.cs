using App.Application.Models;

namespace App.Application.Services
{
    public class CardServices
    {
        private ICardRepository _cardRepository;
        public CardServices(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public CardServices()
        {

        }
        public Card CreateCard(Brand brand, Responsible responsible)
        {
            ValidResponsibleCard(responsible);
            string cardNumber = GenerateCardNumber();

            Card cardCreated = new Card()
            {
                Number = cardNumber,
                Brand = brand,
                Responsible = responsible,
                Status = CardStatus.Active,
            };
            return cardCreated;
        }
        public string GenerateCardNumber()
        {
            Random random = new Random();
            int number = random.Next(9999999);
            int digit = random.Next(9);
            return $"{number}-{digit}";
        }

        public void ValidResponsibleCard(Responsible responsible)
        {
            if (responsible == null)
            {
                throw new ArgumentException("Respons�vel obrigat�rio");
            }
            if (string.IsNullOrEmpty(responsible.Name))
            {
                throw new ArgumentException("Campo obrigat�rio: Nome do respons�vel");
            }
            if (string.IsNullOrEmpty(responsible.Document))
            {
                throw new ArgumentException("Campo obrigat�rio: Documento do respons�vel");
            }
        }

        public void Save(Card card)
        {
            //ICardRepository _cardRepository = new CardRepository();
            var cardExistent = _cardRepository.GetByNumber(card.Number);
            if (cardExistent != null)
                throw new ArgumentException("J� existe um cart�o com este n�mero");

            var idSaved = _cardRepository.Save(card);
            card.Id = idSaved;
        }
    }
}


public interface ICardRepository
{
    Card GetByNumber(string number);
    int Save(Card mycard);
}

public class CardRepository : ICardRepository
{
    public Card GetByNumber(string number)
    {
        //exemplo:: buscar do banco::
        return null;
    }

    public int Save(Card mycard)
    {
        //saved
        return 1;
    }
}
