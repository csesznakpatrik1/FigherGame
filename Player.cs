using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FighterGame
{
   public enum Attacks
    {
        Basic,
        Crit,
        Heal,
        Double

    }

    abstract class  Player
    {
        
        private int hp;
        private int damage;
        private int stamina;
        private readonly int maxStamina;
        private readonly int maxHp;
        public abstract List<Attacks> GetAvailableMoves();

        //get set
        public int Stamina
        {
            get { return stamina; }
            protected set { stamina = Mathf.Clamp(value, 0, MaxStamina); }
        }
        public int Damage
        {
            get { return damage; }
            protected set { damage = value; }
        }



        public int HP
        {
            get { return hp; }
            protected set
            {
                if (value < 0)
                {
                    hp = 0;
                }
                else
                {
                    hp = Mathf.Clamp(value, 0, MaxHP);
                }
            }
        }

        public int MaxHP
        {
            get { return maxHp; }
        }
        public int MaxStamina
        {
            get { return maxStamina; }
        }


        public Player(int maxHp, int damage, int maxStamina)
        {
            this.maxHp = maxHp;
            this.hp = maxHp;

            this.damage = damage;

            this.maxStamina = maxStamina;
            this.stamina = maxStamina;
        }
       

        public void TakeDamage(int damage)
        {
            HP -= damage;
        }
        public void DoDamage(Player player)
        {
            if (Stamina > maxStamina * 0.1)
            {
            player.TakeDamage(Damage);
            Stamina -= (int)(Stamina * 0.1);

            }
        }


    }

    class Tank : Player
    {
        private readonly double healPercent = 0.1;
        public Tank() : base(maxHp: 300,damage: 20,maxStamina: 120) { }
        public void Heal()
        {
            Random rand = new Random();
            int randomHeal = rand.Next(0, 2);

            if (randomHeal == 0)
            {
                Console.WriteLine("Nem sikerült a heal");
            }
            else if (randomHeal == 1)
            {
                Console.WriteLine("Sikerült a heal");

                HP += (int)(MaxHP * healPercent);
                Stamina -= (int)(MaxStamina * 0.15);
            }

        }

        public override List<Attacks> GetAvailableMoves()
        {
            return new List<Attacks> {Attacks.Basic, Attacks.Heal };
        }


    }

    class GlassCannon : Player
    {
        private readonly int critMultiplier = 2;
        public override List<Attacks> GetAvailableMoves()
        {
            return new List<Attacks> { Attacks.Basic, Attacks.Crit };
        }
        public void CritAttack(Player player)
        {
            Random random = new Random();
            int critChance = random.Next(0, 2);

            if (critChance == 0)
            {
                Console.WriteLine("Nem sikerült a támadás!");
            }
            else if (critChance == 1)
            {
                Console.WriteLine("Sikerült a támadás");

                player.TakeDamage(Damage * critMultiplier);
                Stamina -= (int)(MaxStamina * 0.15);
            }

        }

 
        public GlassCannon() :base (maxHp: 50,damage: 120,maxStamina: 60) { }


    }


    class Scout : Player
    {
        private readonly int multipleAttack = 2;
        public override List<Attacks> GetAvailableMoves()
        {
            return new List<Attacks> { Attacks.Basic, Attacks.Double };
        }
        public void DoubleAttack(Player player)
        {


            Random rand = new Random();
            int doubleAttackChance = rand.Next(0, 2);

            if (doubleAttackChance == 0)
            {
                Console.WriteLine("Nem sikerült a dupla támadás");
            }
            else if (doubleAttackChance == 1)
            {
                Console.WriteLine("Sikerült a dupla támadás");

                for (int i = 0; i < multipleAttack; i++)
                {
                    DoDamage(player);
                }
            }

            
        }



        public Scout()  :base(maxHp: 70,damage: 30,maxStamina: 200) { }
 

    }

    class Mathf
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min) { return min; } else if (value > max) { return max; }
            return value;
        }
    }
}
