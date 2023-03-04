using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class DeckPokemon : DeckModel
    {
        public int Pontos { get; set; }
        public DeckPokemon(string dono)
        {
            Proprietario = dono;
            CriarDeck();
        }

        protected override void CriarDeck()
        {
            var rnd = new Random();

            for (int cartas = 0; cartas < 15; cartas++)
            {
                int elemento = rnd.Next(1, 4);
                int dano = rnd.Next(1, 5);
                Card carta = new Card { Elemento = (Elemento)elemento, Dano = dano };
                Deck.Add(carta);
            }
        }

        public override void CriarMao()
        {
            Console.WriteLine($"Criando a mão do {Proprietario} com 10 cartas !");

            for (int i = 0; i < 10; i++)
            {
                Card carta = Deck.Take(1).First();
                Deck.Remove(carta);
                Mao.Add(carta);
            }
        }

        public void MostrarCartas()
        {
            int numero = 1;
            Console.WriteLine($"Mostrando o deck do {Proprietario}");
            foreach (var carta in Deck)
            {
                Console.WriteLine($"{numero}: {carta.Elemento} {carta.Dano}");
                numero++;
            }
        }

        public void MostrarMao()
        {
            int numero = 1;
            Console.WriteLine($"Mostrando a mao do {Proprietario}");
            foreach (var carta in Mao)
            {
                Console.WriteLine($"{numero}: {carta.Elemento} {carta.Dano}");
                numero++;
            }
        }


    }
}
