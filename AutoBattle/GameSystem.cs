using System;
using System.Collections.Generic;
using System.Linq;
using AutoBattle.Types;
using AutoBattle.Characters;

namespace AutoBattle
{
    public class GameSystem
    {
        private Grid grid;
        private Character PlayerCharacter;
        private Character EnemyCharacter;
        private List<Character> AllPlayers = new List<Character>();

        public GameSystem (int lines, int columns)
        {
            grid = new Grid(lines, columns);
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
            PlayerCharacter = Factory.CharacterFactory.InstantiateCharacter(0, characterClass);
        }

        private void CreateEnemyCharacter()
        {
            //randomly choose the enemy class and set up vital variables
            int rand = Utils.GetRandomInt(1, 4);
            CharacterClass enemyClass = (CharacterClass)rand;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            EnemyCharacter = Factory.CharacterFactory.InstantiateCharacter(1, enemyClass);
        }

        private void StartGame()
        {
            //populates the character variables and targets
            EnemyCharacter.Target = PlayerCharacter;
            PlayerCharacter.Target = EnemyCharacter;

            //ordering turn players.
            if (Utils.GetRandomInt(0,2) > 0)
            {
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
            }
            else
            {
                AllPlayers.Add(EnemyCharacter);
                AllPlayers.Add(PlayerCharacter);
            }

            AlocatePlayers();
        }

        void StartTurn()
        {
            foreach (Character character in AllPlayers)
            {
                character.StartTurn(grid);
                //Console.WriteLine("Turn of " + character.PlayerIndex + " " + character.Name + i++);
            }

            HandleTurn();
        }

        private void HandleTurn()
        {
            if (PlayerCharacter.Health == 0)
            {
                GridBox gridBox = PlayerCharacter.currentBox;
                gridBox.ocupied = false;
                grid.grids[gridBox.Index] = gridBox;
                Console.Write(Environment.NewLine);
                Console.WriteLine("Jogo finalizado!");
                Console.WriteLine("Jogador perdeu a batalha!");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            else if (EnemyCharacter.Health == 0)
            {
                GridBox gridBox = EnemyCharacter.currentBox;
                gridBox.ocupied = false;
                grid.grids[gridBox.Index] = gridBox;
                Console.Write(Environment.NewLine);
                Console.WriteLine("Jogo finalizado!");
                Console.WriteLine("Jogador perdeu a batalha!");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...");
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
            int random = Utils.GetRandomInt(0, grid.AmountCount);
            GridBox randomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!randomLocation.ocupied)
            {
                randomLocation.ocupied = true;
                grid.grids[random] = randomLocation;
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
            int random = Utils.GetRandomInt(0, grid.AmountCount);
            GridBox randomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!randomLocation.ocupied)
            {
                randomLocation.ocupied = true;
                grid.grids[random] = randomLocation;
                EnemyCharacter.currentBox = grid.grids[random];
                grid.DrawBattlefield();
            }
            else
            {
                AlocateEnemyCharacter();
            }
        }
    }
}
