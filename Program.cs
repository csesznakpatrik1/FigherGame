using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
                  int winner = NextPlayer(player1, player2);


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
            // get the inputs
            List<Attacks> currentMoves;
            Dictionary<Attacks, int> currentAttackStaminaCost;
            int input;

            (input,currentMoves, currentAttackStaminaCost ) = GetInput(current);

            //Handle the player moves
            switch(currentMoves[input - 1])
            {
                //default moves
                case Attacks.WaitRound: current.SkipRound(currentAttackStaminaCost[Attacks.WaitRound]); break;
                case Attacks.Basic: current.DoDamage(target); break;

                // Class specific moves
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
            Console.WriteLine("Válaszd ki mit szeretnél csinálni:\n");
            // get the inputs from the winner player
            List<Attacks> currentMoves = current.GetAvailableMoves();
            Dictionary<Attacks, int> currentAttackStaminaCost = current.attackStaminaCost();
            int input;

            Attacks chosenAttack;
            // do-while handler
            bool isValid = false;

            do
            {
                //write out the current player's moves
                for (int i = 0; i < currentMoves.Count(); i++)
                {
                    Console.WriteLine($"{currentMoves[i]} Attack | Stamina Cost: {currentAttackStaminaCost[currentMoves[i]]}");
                }

                //input check
                int.TryParse(Console.ReadLine(), out input);
                if (input < 1 || input > currentAttackStaminaCost.Count())
                {
                    continue;
                }
                else
                {
                    chosenAttack = currentMoves[input - 1];
                }

                //stamina check
                if (currentAttackStaminaCost[chosenAttack] <= current.Stamina)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Erre nincs elég staminád!");
                }



            } while (!isValid);

            return (input,currentMoves, currentAttackStaminaCost);
        }

        // I don't have to explain this
        static void ShowStats(Player player1, Player player2)
        {
            Console.WriteLine($"1-es Játékos statisztikái: \n \n Neve:{player1.GetType().Name} \n HP:{player1.HP} \n Damage:{player1.Damage} \n Stamina:{player1.Stamina} \n");
            Console.WriteLine($"2-es Játékos statisztikái: \n \n Neve:{player2.GetType().Name}\n HP:{player2.HP} \n Damage:{player2.Damage} \n Stamina:{player2.Stamina} \n");
        }
        
        // get's the winner player from the luck game!!!
        static int NextPlayer(Player player1, Player player2)
        {
            Random random = new Random();
            int GuessNumber = random.Next(1, 11);
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
                Console.Clear();
                ShowStats(player1, player2);
                Console.WriteLine("Az első játékos nyert! \n");
                System.Threading.Thread.Sleep(500);
                return 0;
            }
            else
            {
                Console.Clear();
                ShowStats(player1, player2);
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
