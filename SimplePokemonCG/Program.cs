using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePokemonCG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Card Game de Pokemon !");
            Console.WriteLine("Voce vai jogar contra o Igordao !");

            string nomeDoJogador = CriarJogador();

            DeckPokemon jogador = PrepararJogador(nomeDoJogador);
            DeckPokemon inimigo = PrepararJogador("Igordão");

            Console.WriteLine("Vamos começar a porrada ! ENTER PARA CONTINUAR");
            Console.ReadLine();
            Console.Clear();
            jogador.MostrarMao();

            do
            {
                FazerUmRound(jogador, inimigo);
                Console.ReadLine();
                Console.Clear();
                jogador.MostrarMao();
            } while (inimigo.Mao.Count >= 1 && jogador.Mao.Count >= 1);

            Console.WriteLine($"{inimigo.Mao.Count} X {jogador.Mao.Count}");

            if (jogador.Mao.Count > inimigo.Mao.Count)
                Console.WriteLine("Parabens voce venceu !");
            else
                Console.WriteLine("PERDEU ! VAZA DAQUI SEU FRACASSADO !");


            Console.ReadLine();
        }

        private static void FazerUmRound(DeckPokemon jogador, DeckPokemon inimigo)
        {

            Card cartaJogador = EscolherCarta(jogador);

            var rnd = new Random();
            int index = rnd.Next(inimigo.Mao.Count);
            Card cartaInimigo = inimigo.Mao[index];

            Console.WriteLine($"O inimigo escolheu {cartaInimigo.Elemento} {cartaInimigo.Dano}");

            bool venceu = Lutinha(cartaJogador, cartaInimigo);

            if (venceu)
            {
                Console.WriteLine("Parabens ! A carta do inimigo foi DESTRUIDA ! ENTER PARA CONTINUAR");
                inimigo.Mao.Remove(cartaInimigo);
                cartaJogador.Dano -= 1;
            }
            else
            {
                Console.WriteLine("Se tomou no cú ! Sua carta foi destruida ! ENTER PARA CONTINUAR");
                jogador.Mao.Remove(cartaJogador);
                cartaInimigo.Dano -= 1;
            }
        }

        private static bool Lutinha(Card cartaJogador, Card cartaInimigo)
        {
            int danoOriginal = cartaJogador.Dano;

            switch (cartaJogador.Elemento)
            {
                case Elemento.Fogo:
                    if (cartaInimigo.Elemento == Elemento.Agua)
                        cartaJogador.Dano /= 2;
                    else if (cartaInimigo.Elemento == Elemento.Terra)
                        cartaJogador.Dano *= 2;
                    break;
                case Elemento.Terra:
                    if (cartaInimigo.Elemento == Elemento.Fogo)
                        cartaJogador.Dano /= 2;
                    else if (cartaInimigo.Elemento == Elemento.Ar)
                        cartaJogador.Dano *= 2;
                    break;
                case Elemento.Agua:
                    if (cartaInimigo.Elemento == Elemento.Ar)
                        cartaJogador.Dano /= 2;
                    else if (cartaInimigo.Elemento == Elemento.Fogo)
                        cartaJogador.Dano *= 2;
                    break;
                case Elemento.Ar:
                    if (cartaInimigo.Elemento == Elemento.Terra)
                        cartaJogador.Dano /= 2;
                    else if (cartaInimigo.Elemento == Elemento.Agua)
                        cartaJogador.Dano *= 2;
                    break;
                default:
                    break;
            }

            Console.WriteLine($"Por counter elemental, seu dano contra a carta do inimigo é {cartaJogador.Dano} !");

            if (cartaJogador.Dano > cartaInimigo.Dano)
            {
                cartaJogador.Dano = danoOriginal;
                return true;
            }
            else if (cartaJogador.Dano == cartaInimigo.Dano){
                Console.WriteLine("Empate ! Nesse caso, o vencedor sera definido pelo elemento !");
                cartaJogador.Dano = danoOriginal;

                switch (cartaJogador.Elemento)
                {
                    case Elemento.Fogo:
                        if (cartaInimigo.Elemento == Elemento.Terra)
                            return true;
                        else return false;
                    case Elemento.Terra:
                        if (cartaInimigo.Elemento == Elemento.Ar)
                            return true;
                        else return false;
                    case Elemento.Agua:
                        if (cartaInimigo.Elemento == Elemento.Fogo)
                            return true;
                        else return false;
                    case Elemento.Ar:
                        if (cartaInimigo.Elemento == Elemento.Agua)
                            return true;
                        else return false;
                    default:
                        return false;
                }
            }
            else
            {
                cartaJogador.Dano = danoOriginal;
                return false;
            }
        }

        static string CriarJogador()
        {
            Console.WriteLine("Qual o seu nome :");
            string resposta = Console.ReadLine();

            return resposta;
        }

        static DeckPokemon PrepararJogador(string nome)
        {
            DeckPokemon deck = new DeckPokemon(nome);
            deck.Embaralhar();
            deck.CriarMao();

            return deck;
        }

        static Card EscolherCarta(DeckPokemon jogador)
        {
            bool numeroValido = false;
            int numero = 0;
            do
            {
                Console.WriteLine("Qual carta da mão voce escolhe ? <1 a 10>");
                string output = Console.ReadLine();
                numeroValido = int.TryParse(output, out numero);
            } while (!numeroValido);

            if (numero > jogador.Mao.Count() || numero <= 0)
            {
                Console.WriteLine("Esta de gracinha ? A carta escolhida sera a primeira !");
                numero = 1;
            }

            var cartaEscolhida = jogador.Mao.ElementAt(numero - 1);
            Console.WriteLine($"Escolheu a carta {cartaEscolhida.Elemento} {cartaEscolhida.Dano}");

            return cartaEscolhida;
        }
    }
}
