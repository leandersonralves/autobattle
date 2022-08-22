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
        public abstract int Health { get; protected set; }
        public bool IsAlive { get => Health > 0; }

        public abstract int BaseDamage { get; protected set; }
        public abstract float DamageMultiplier { get; protected set; }

        private int attackProbabilityOverTen = 7;

        private int distancePushAway = 3;

        public GridBox currentBox;
        public int PlayerIndex { get; protected set; }
        public Character Target { get; set; }

        public bool TakeDamage(int damageSuffered)
        {
            if ((Health -= damageSuffered) <= 0)
            {
                Die();
                return true;
            }
            Console.WriteLine($"{ Name } Player ({ PlayerIndex }) Health { Health }, damage suffered { damageSuffered } .");
            return false;
        }

        public void Die()
        {
            Console.WriteLine($"{ Name } Player ({ PlayerIndex }) Died!");
        }

        public void StartTurn(Grid battlefield)
        {
            bool inRangeBeforeWalk = CheckCloseTargets(battlefield);
            if (!inRangeBeforeWalk)
                Walk(battlefield);

            if (inRangeBeforeWalk || CheckCloseTargets(battlefield)) //checking if is closer only if before walk was out or range.
            {
                //TODO: implement push away ability.
                //if (Utils.GetRandomInt(0, 10) > attackProbabilityOverTen)
                {
                    Attack(Target);
                }
                //else
                //{
                //    PushTarget(battlefield);
                //}
            }
        }

        private void Walk(Grid battlefield)
        {
            float distX = Math.Abs(currentBox.xIndex - Target.currentBox.xIndex);
            float distY = Math.Abs(currentBox.yIndex - Target.currentBox.yIndex);

            //priorizing movement in axis more closer.
            if (((distX > 0) && (distX < distY)) || ((distY == 0) && (distX > 0)))
            {
                // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (currentBox.xIndex > Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player ({PlayerIndex}) walked left.");
                    battlefield.DrawBattlefield();
                }
                else if (currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player ({PlayerIndex}) walked right.");
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
                    Console.WriteLine($"Player ({PlayerIndex}) walked up.");
                    battlefield.DrawBattlefield();
                }
                else if (currentBox.yIndex < Target.currentBox.yIndex)
                {
                    currentBox.ocupied = false;
                    battlefield.grids[currentBox.Index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLength));
                    currentBox.ocupied = true;
                    battlefield.grids[currentBox.Index] = currentBox;
                    Console.WriteLine($"Player ({PlayerIndex}) walked down.");
                    battlefield.DrawBattlefield();
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
            int damageCalculated = (int)(Utils.GetRandomInt(0, BaseDamage + 1) * DamageMultiplier);
            Console.WriteLine($"Player ({ PlayerIndex }) is attacking the Player ({ Target.PlayerIndex }) and did { damageCalculated } damage.");
            target.TakeDamage(damageCalculated);
        }

        public void PushTarget (Grid battlefield)
        {
            Console.WriteLine($"Player ({ PlayerIndex }) is pushing away Player ({ Target.PlayerIndex }).");

            Target.currentBox.ocupied = false;
            battlefield.grids[Target.currentBox.Index] = Target.currentBox;

            // getting direction of enemy and multiply by distance push.
            int dirXAway = Math.Sign(currentBox.xIndex - Target.currentBox.xIndex) * distancePushAway;
            int diryAway = Math.Sign(currentBox.yIndex - Target.currentBox.yIndex) * distancePushAway;
            


            Target.currentBox.ocupied = true;
            battlefield.grids[Target.currentBox.Index] = Target.currentBox;
            battlefield.DrawBattlefield();
        }
    }

    public class Paladin : Character
    {
        public override string Name { get => "Paladin"; }
        public override int Health { get; protected set; }
        public override int BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; } = 1f;

        public Paladin (int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100;
            BaseDamage = 20;
        }
    }

    public class Warrior : Character
    {
        public override string Name { get => "Warrior"; }
        public override int Health { get; protected set; }
        public override int BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; } = 1f;

        public Warrior(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100;
            BaseDamage = 20;
        }
    }

    public class Cleric : Character
    {
        public override string Name { get => "Cleric"; }
        public override int Health { get; protected set; }
        public override int BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; } = 1f;

        public Cleric(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100;
            BaseDamage = 20;
        }
    }

    public class Archer : Character
    {
        public override string Name { get => "Archer"; }
        public override int Health { get; protected set; }
        public override int BaseDamage { get; protected set; }
        public override float DamageMultiplier { get; protected set; } = 1f;

        public Archer(int playerIndex)
        {
            PlayerIndex = playerIndex;
            Health = 100;
            BaseDamage = 20;
        }
    }
}
