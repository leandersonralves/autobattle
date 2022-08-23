using System;
using AutoBattle.Types;
using AutoBattle.Characters;

namespace AutoBattle
{
    public class GameSystem
    {
        private Grid grid;

        private Character PlayerCharacter;

        private Character EnemyCharacter;

        private Character[] AllPlayers = new Character[0];

        public void StartNewGame(int lines, int columns)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("Autobattle Game Test");
            int classChoice = GetPlayerChoice();

            Console.Write(Environment.NewLine);
            CreatePlayerCharacter(classChoice);
            CreateEnemyCharacter();
            PrepareBattlefield(lines, columns);

            Console.ReadKey(); // waiting a input to start turns.
            StartTurn();
        }

        /// <summary>
        /// Wait a Input from Player which Class he want.
        /// </summary>
        /// <returns>Player Input</returns>
        private int GetPlayerChoice()
        {
            Console.WriteLine("-------------------------------------");

            //asks for the player to choose between for possible classes via console.
            Console.WriteLine(Environment.NewLine + 
                              "Choose Between One of this Classes:" +
                              Environment.NewLine +
                              "[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");

            //store the player choice in a variable
            int choose = InputSystem.ReadInt(1, 4,
                Environment.NewLine + 
                "Choose not recognized, choose Between:" +
                Environment.NewLine + 
                "[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer"
            );

            Console.WriteLine("-------------------------------------");

            return choose;
        }

        private void CreatePlayerCharacter(int classIndex)
        {
            CharacterClass characterClass = (CharacterClass)classIndex;
            Console.WriteLine($"Player Class Choice: { characterClass }");
            PlayerCharacter = Factory.CharacterFactory.InstantiateCharacter(0, characterClass);
        }

        private void CreateEnemyCharacter()
        {
            //randomly choose the enemy class and set up vital variables
            int rand = Utils.GetRandomInt(1, 4);
            CharacterClass enemyClass = (CharacterClass)rand;
            Console.WriteLine($"Enemy Class Choice: { enemyClass }");
            EnemyCharacter = Factory.CharacterFactory.InstantiateCharacter(1, enemyClass);
        }

        /// <summary>
        /// Create battlefield with a specified size.
        /// </summary>
        /// <param name="lines">Amount lines.</param>
        /// <param name="columns">Amount columns</param>
        private void PrepareBattlefield(int lines, int columns)
        {
            //populates the character variables and targets
            EnemyCharacter.Target = PlayerCharacter;
            PlayerCharacter.Target = EnemyCharacter;

            AllPlayers = new Character[2];
            //ordering turn players randomized.
            if (Utils.GetRandomInt(0,2) > 0)
            {
                AllPlayers[0] = PlayerCharacter;
                AllPlayers[1] = EnemyCharacter;
            }
            else
            {
                AllPlayers[1] = PlayerCharacter;
                AllPlayers[0] = EnemyCharacter;
            }

            grid = new Grid(lines, columns, AllPlayers);

            AlocatePlayers();
        }

        private void AlocatePlayers()
        {
            int playerRandomX = Utils.GetRandomInt(0, grid.XLength);
            int playerRandomY = Utils.GetRandomInt(0, grid.YLength);

            PlayerCharacter.currentBox = new GridBox(playerRandomX, playerRandomY);

            int enemyRandomX = Utils.GetRandomInt(0, grid.XLength);
            int enemyRandomY = Utils.GetRandomInt(0, grid.YLength);

            //colision random avoid.
            while (enemyRandomX == playerRandomX)
            {
                enemyRandomX = Utils.GetRandomInt(0, grid.XLength);
            }

            //colision random avoid.
            while (enemyRandomY == playerRandomY)
            {
                enemyRandomY = Utils.GetRandomInt(0, grid.YLength);
            }

            EnemyCharacter.currentBox = new GridBox(enemyRandomX, enemyRandomY);

            grid.DrawBattlefield();
        }

        void StartTurn()
        {
            foreach (Character character in AllPlayers)
            {
                if (character.IsAlive) // Checking if player is alive, to avoid attack at falling death.
                {
                    character.StartTurn(grid);
                    //Console.WriteLine("Turn of " + character.PlayerIndex + " " + character.Name + i++);
                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Press any key to start new phase.");
                    Console.Write(Environment.NewLine);
                    Console.ReadKey();
                }
            }

            HandleTurn();
        }

        private void HandleTurn()
        {
            if (!PlayerCharacter.IsAlive)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("Game Over!");
                Console.WriteLine("Player Lost battle!");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            else if (!EnemyCharacter.IsAlive)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("Player Win!");
                Console.WriteLine("Player slaughter Enemy!");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...");
                Console.Write(Environment.NewLine);
                Console.ReadKey();
                StartTurn();
            }
        }
    }
}
