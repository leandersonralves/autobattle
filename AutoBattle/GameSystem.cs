using System;
using System.Collections.Generic;
using System.Linq;
using AutoBattle.Types;

namespace AutoBattle
{
    public class GameSystem
    {
        private Grid grid;
        private CharacterClass playerCharacterClass;
        private GridBox PlayerCurrentLocation;
        private GridBox EnemyCurrentLocation;
        private Character PlayerCharacter;
        private Character EnemyCharacter;
        private List<Character> AllPlayers = new List<Character>();
        private int numberOfPossibleTiles = 0;
        private bool firstTurn = true;

        public GameSystem (int height, int width)
        {
            grid = new Grid(width, height);
            numberOfPossibleTiles = grid.grids.Count;
            firstTurn = true;
        }

        public void Setup()
        {
            int choice = GetPlayerChoice();
            CreatePlayerCharacter(choice);
            CreateEnemyCharacter();
            StartGame();
            StartTurn();
        }

        private int GetPlayerChoice()
        {
            //asks for the player to choose between for possible classes via console.
            Console.WriteLine("Choose Between One of this Classes:");
            Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");

            //store the player choice in a variable
            return InputSystem.ReadInt(1, 4, "Type which class: [1] Paladin, [2] Warrior, [3] Cleric, [4] Archer...");
        }

        private void CreatePlayerCharacter(int classIndex)
        {
            CharacterClass characterClass = (CharacterClass)classIndex;
            Console.WriteLine($"Player Class Choice: {characterClass}");
            PlayerCharacter = new Character(characterClass);
            PlayerCharacter.Health = 100;
            PlayerCharacter.BaseDamage = 20;
            PlayerCharacter.PlayerIndex = 0;
        }

        private void CreateEnemyCharacter()
        {
            //randomly choose the enemy class and set up vital variables
            int rand = Utils.GetRandomInt(1, 4);
            CharacterClass enemyClass = (CharacterClass)rand;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            EnemyCharacter = new Character(enemyClass);
            EnemyCharacter.Health = 100;
            PlayerCharacter.BaseDamage = 20;
            PlayerCharacter.PlayerIndex = 1;
        }

        private void StartGame()
        {
            //populates the character variables and targets
            EnemyCharacter.Target = PlayerCharacter;
            PlayerCharacter.Target = EnemyCharacter;
            AllPlayers.Add(PlayerCharacter);
            AllPlayers.Add(EnemyCharacter);
            AlocatePlayers();
        }

        void StartTurn()
        {
            if (firstTurn)
            {
                //AllPlayers.Sort();  
            }
            firstTurn = true;

            foreach (Character character in AllPlayers)
            {
                character.StartTurn(grid);
            }

            HandleTurn();
        }

        private void HandleTurn()
        {
            if (PlayerCharacter.Health == 0)
            {
                //TODO: to include lost game.
            }
            else if (EnemyCharacter.Health == 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                //TODO: include win game.

                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");
                Console.Write(Environment.NewLine + Environment.NewLine);

                ConsoleKeyInfo key = Console.ReadKey();
                StartTurn();
            }
        }

        private void AlocatePlayers()
        {
            AlocatePlayerCharacter();
        }

        private void AlocatePlayerCharacter()
        {
            int random = Utils.GetRandomInt(0, numberOfPossibleTiles);
            GridBox RandomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                GridBox PlayerCurrentLocation = RandomLocation;
                RandomLocation.ocupied = true;
                grid.grids[random] = RandomLocation;
                PlayerCharacter.currentBox = grid.grids[random];
                AlocateEnemyCharacter();
            }
            else
            {
                AlocatePlayerCharacter();
            }
        }

        private void AlocateEnemyCharacter()
        {
            int random = Utils.GetRandomInt(0, numberOfPossibleTiles);
            GridBox RandomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                EnemyCurrentLocation = RandomLocation;
                RandomLocation.ocupied = true;
                grid.grids[random] = RandomLocation;
                EnemyCharacter.currentBox = grid.grids[random];
                grid.DrawBattlefield(5, 5);
            }
            else
            {
                AlocateEnemyCharacter();
            }
        }
    }
}
