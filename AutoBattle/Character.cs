using System;
using AutoBattle.Types;

namespace AutoBattle.Characters
{
    public enum CharacterClass : uint
    {
        Paladin = 1,
        Warrior = 2,
        Cleric = 3,
        Archer = 4
    }

    public abstract class Character
    {
        public abstract string Name { get; }
        public abstract float Health { get; protected set; }
        public abstract float BaseDamage { get; protected set; }
        public abstract float DamageMultiplier { get; protected set; }
        public GridBox currentBox;
        public int PlayerIndex { get; protected set; }
        public Character Target { get; set; }

        public bool TakeDamage(float amount)
        {
            if((Health -= BaseDamage) <= 0)
            {
                Console.WriteLine($"{ Name } Player ({ PlayerIndex }) Died!");
                Die();
                return true;
            }
            Console.WriteLine($"{ Name } P({ PlayerIndex }) Health {Health}");
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield)
        {
            if (CheckCloseTargets(battlefield)) 
            {
                Attack(Target);
            }
            else
            {
                float distX = Math.Abs(currentBox.xIndex - Target.currentBox.xIndex);
                float distY = Math.Abs(currentBox.yIndex - Target.currentBox.yIndex);

                //priorizing movement in axis more closer.
                if ((distX != 0) && (distX < distY))
                {
                    // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                    if (currentBox.xIndex > Target.currentBox.xIndex)
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked left\n");
                        battlefield.DrawBattlefield();
                    }
                    else if (currentBox.xIndex < Target.currentBox.xIndex)
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked right\n");
                        battlefield.DrawBattlefield();
                    }
                }
                else if (distY != 0)
                {
                    if (currentBox.yIndex > Target.currentBox.yIndex)
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLength));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked up\n");
                        battlefield.DrawBattlefield();
                    }
                    else if (currentBox.yIndex < Target.currentBox.yIndex)
                    {
                        currentBox.ocupied = false;
                        battlefield.grids[currentBox.Index] = currentBox;
                        currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLength));
                        currentBox.ocupied = true;
                        battlefield.grids[currentBox.Index] = currentBox;
                        Console.WriteLine($"Player {PlayerIndex} walked down\n");
                        battlefield.DrawBattlefield();
                    }
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLength).ocupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLength).ocupied);

            return left || right || up || down;
        }

        public void Attack (Character target)
        {
            Random rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
        }
    }

    public class Paladin : Character
    {
        public override string Name { get => "Paladin"; }
        public override float Health { get; protected set; }
        public override float BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; }

        public Paladin (int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100f;
            BaseDamage = 20f;
        }
    }

    public class Warrior : Character
    {
        public override string Name { get => "Warrior"; }
        public override float Health { get; protected set; }
        public override float BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; }

        public Warrior(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100f;
            BaseDamage = 20f;
        }
    }

    public class Cleric : Character
    {
        public override string Name { get => "Cleric"; }
        public override float Health { get; protected set; }
        public override float BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; }

        public Cleric(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100f;
            BaseDamage = 20f;
        }
    }

    public class Archer : Character
    {
        public override string Name { get => "Archer"; }
        public override float Health { get; protected set; }
        public override float BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; }

        public Archer(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100f;
            BaseDamage = 20f;
        }
    }
}
