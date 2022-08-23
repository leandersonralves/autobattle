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

        private int distancePushAway = 2;

        private int rangeAttack = 1;

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
            if (CheckCloseTargets(battlefield))
            {
                ChooseState(battlefield);
            }
            else
            {
                Walk(battlefield);

                if (CheckCloseTargets(battlefield))
                {
                    ChooseState(battlefield);
                }
            }
        }

        private void ChooseState(Grid battlefield)
        {
            if (Utils.GetRandomInt(0, 10) > attackProbabilityOverTen)
            {
                PushTarget(battlefield);
            }
            else
            {
                Attack(Target);
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
                    currentBox.xIndex -= 1;
                    Console.WriteLine($"Player ({ PlayerIndex }) walked left.");
                    battlefield.DrawBattlefield();
                }
                else if (currentBox.xIndex < Target.currentBox.xIndex)
                {
                    currentBox.xIndex += 1;
                    Console.WriteLine($"Player ({ PlayerIndex }) walked right.");
                    battlefield.DrawBattlefield();
                }
            }
            else if (distY > 0)
            {
                if (currentBox.yIndex > Target.currentBox.yIndex)
                {
                    currentBox.yIndex -= 1;
                    Console.WriteLine($"Player ({ PlayerIndex }) walked up.");
                    battlefield.DrawBattlefield();
                }
                else if (currentBox.yIndex < Target.currentBox.yIndex)
                {
                    currentBox.yIndex += 1;
                    Console.WriteLine($"Player ({ PlayerIndex }) walked down.");
                    battlefield.DrawBattlefield();
                }
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            float verticalDistance = Math.Abs(currentBox.xIndex - Target.currentBox.xIndex);
            float horizontalDistance = Math.Abs(currentBox.yIndex - Target.currentBox.yIndex);

            return (verticalDistance == 0 && (horizontalDistance == rangeAttack)) || (horizontalDistance == 0 && (verticalDistance == rangeAttack)); // if is align in a axis and another is in distance range.
        }

        public void Attack (Character target)
        {
            int damageCalculated = (int)(Utils.GetRandomInt(0, BaseDamage + 1) * DamageMultiplier);
            Console.WriteLine($"Player ({ PlayerIndex }) is attacking the Player ({ Target.PlayerIndex }) and did { damageCalculated } damage.");
            target.TakeDamage(damageCalculated);
        }

        /// <summary>
        /// Push away a Target opponent.
        /// </summary>
        /// <param name="battlefield"></param>
        public void PushTarget (Grid battlefield)
        {
            // getting direction of enemy and multiply by distance push.
            int directionXAway = Math.Sign(Target.currentBox.xIndex - currentBox.xIndex) * distancePushAway;
            int directionYAway = Math.Sign(Target.currentBox.yIndex - currentBox.yIndex) * distancePushAway;

            Console.WriteLine($"Player ({ PlayerIndex }) is pushing away Player ({ Target.PlayerIndex }).");

            GridBox gridBox = Target.currentBox;
            gridBox.xIndex = Math.Clamp(gridBox.xIndex + directionXAway, 0, battlefield.XLength - 1);
            gridBox.yIndex = Math.Clamp(gridBox.yIndex + directionYAway, 0, battlefield.YLength - 1);
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
