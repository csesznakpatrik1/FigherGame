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
                Console.WriteLine("Nyomj bármilyen gombot a folytatáshoz...");
                Console.ReadKey();
                Console.Clear();
                  
              } 

        }


        static void HandlePlayerMove(Player current, Player target)
        {
            List<Attacks> currentMoves;
            Dictionary<Attacks, int> currentAttackStaminaCost;
            int input;
            (input,currentMoves, currentAttackStaminaCost ) = GetInput(current);

            switch(currentMoves[input - 1])
            {
                case Attacks.Basic: current.DoDamage(target); break;
                case Attacks.Crit: 
                    if (current is GlassCannon gc)
                    {   
                        
                        gc.CritAttack(target);
                    }
                    break;
                case Attacks.Heal: 
                    if (current is Tank tank)
                    {
                        tank.Heal();
                    }
                    break;
                case Attacks.Double: 
                    if (current is Scout scout)
                    {
                        scout.DoubleAttack(target);
                    }
                    break;
                default: break;

            }

        }

        static (int, List<Attacks>, Dictionary<Attacks, int>) GetInput(Player current)
        {
            Console.WriteLine("Válaszd ki mit szeretnél csinálni: ");

            List<Attacks> currentMoves = current.GetAvailableMoves();
            Dictionary<Attacks, int> currentAttackStaminaCost = current.attackStaminaCost();
            int input;
            do
            {
                for (int i = 0; i < currentMoves.Count; i++)
                {
                    if (currentMoves[i] != Attacks.WaitRound)
                    {

                        Console.WriteLine($"({i + 1}): {currentMoves[i]} attack, Stamina Cost: {currentAttackStaminaCost[currentMoves[i]]}");
                    }
                    else
                    {
                        Console.WriteLine($"({i + 1}): {currentMoves[i]} Stamina Regenerate: {currentAttackStaminaCost[currentMoves[i]]}");

                    }
                }
                bool x = int.TryParse(Console.ReadLine(), out input);
                if (!x) Console.WriteLine("számot kérek");

            } while (input < 1 || input > currentMoves.Count());

            return (input,currentMoves, currentAttackStaminaCost);
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
                Console.WriteLine("Az első játékos nyert! \n");
                System.Threading.Thread.Sleep(500);
                return 0;
            }
            else
            {
                Console.WriteLine("A második játékos nyert! \n");
                System.Threading.Thread.Sleep(500);
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
