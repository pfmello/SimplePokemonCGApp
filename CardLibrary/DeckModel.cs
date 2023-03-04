using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
     public abstract class DeckModel
    {
        protected List<Card> Deck { get; set; } = new List<Card>();

        public List<Card> Mao { get; set; } = new List<Card>();

        protected string Proprietario { get; set; }


        protected abstract void CriarDeck();

        public abstract void CriarMao();

        public virtual void Embaralhar()
        {
            var random = new Random();
            Deck = Deck.OrderBy(x => random.Next()).ToList();
        }
    }
}
