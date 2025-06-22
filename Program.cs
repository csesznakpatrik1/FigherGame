using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FighterGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // players setup
              Console.WriteLine("Player 1");
              Player player1 = SelectPlayer();
              Console.WriteLine("Player 2");
              Player player2 = SelectPlayer();

              Console.Clear();
            /*
              // game
              while (player1.HP > 0 && player2.HP > 0)
              {

                  //shows the stats at the beginning of every round
                  ShowStats(player1, player2);
                  // gets the round winner in the guessing game, the round winner can attack
                  int winner = NextPlayer();


                  //attack mechanism
                  if (winner == 0) 
                  {
                      HandlePlayerMove(player1, player2);

                  }
                  else if (winner == 1)
                  {
                      HandlePlayerMove(player2, player1);
                  }

                  //wait for a bit after the round
                  System.Threading.Thread.Sleep(3000);
                  Console.Clear();

              } */

            HandlePlayerMove(player1,player2);
        }


        static void HandlePlayerMove(Player current, Player target)
        {
            Console.WriteLine("Válaszd ki mit szeretnél csinálni: ");

            //getavailablemoves call, majd ezzel torteno move vegrehajtasa!!!!
            List<Attacks> currentMoves = current.GetAvailableMoves();
            int input;
            do
            {
                for (int i = 0; i < currentMoves.Count; i++)
                {
                    Console.WriteLine($"({i + 1}): {currentMoves[i]}");

                }
                int.TryParse(Console.ReadLine(), out input);

            } while (input < 1 || input > 3);


        }


        static void ShowStats(Player player1, Player player2)
        {
            Console.WriteLine($"1-es Játékos statisztikái: \n \n Neve:{player1.GetType().Name} \n HP:{player1.HP} \n Damage:{player1.Damage} \n Stamina:{player1.Stamina} \n");
            Console.WriteLine($"2-es Játékos statisztikái: \n \n Neve:{player2.GetType().Name}\n HP:{player2.HP} \n Damage:{player2.Damage} \n Stamina:{player2.Stamina} \n");
        }
        
        static int NextPlayer()
        {
            Random random = new Random();
            int GuessNumber = random.Next(0, 10);
            Console.WriteLine("1. Játékos tippeljen 1-10 között!");
            int.TryParse(Console.ReadLine(), out int player1GuessNumber);

            Console.WriteLine("2. Játékos tippeljen 1-10 között!");
            int.TryParse(Console.ReadLine(), out int player2GuessNumber);

            player1GuessNumber -= GuessNumber;
            player1GuessNumber = Math.Abs(player1GuessNumber);

            player2GuessNumber -= GuessNumber;
            player2GuessNumber = Math.Abs(player2GuessNumber);

            if (player1GuessNumber < player2GuessNumber)
            {
                Console.WriteLine("Az első játékos nyert!");
                return 0;
            }
            else
            {
                Console.WriteLine("A második játékos nyert!");
                return 1;
            }

        }
        static Player SelectPlayer()
        {
            int selectedPlayerNum;
            do
            {

                Console.WriteLine("Kérem válaszd ki a karaktered: \n (1) Tank \n (2) GlassCannon \n (3) Scout");
                int.TryParse(Console.ReadLine(), out selectedPlayerNum);
            } while (selectedPlayerNum > 3 || selectedPlayerNum < 1);


            switch (selectedPlayerNum)
            {
                case 1: return new Tank();
                case 2: return new GlassCannon();
                case 3: return new Scout();
                    default: throw new Exception("nem megfelelo a mukodes");
            }
            
            
        }
    }
}
