# Autobattle Test Game

- [Autobattle Test Game](#autobattle-test-game)
  - [Candidate Info](#candidate-info)
  - [Formatation and best practices.](#formatation-and-best-practices)
  - [Optimizations](#optimizations)
  - [Architecture System](#architecture-system)
  - [Bug fixed](#bug-fixed)
  - [Extra Implementation](#extra-implementation)
  - [Discussion](#discussion)

## Candidate Info
Name: Leanderson Rodrigues Ribeiro Alves

Contact: leo_ribeiro_alves@yahoo.com.br / +55 11 97974-8635

## Formatation and best practices.
1. Removed unnecessary returns.

1. Breaklines fixes.

1. Removed unnecessary files Character.cs Grid.cs in root.

1. Added access modifiers in variables, class and functions. Avoid use implicit declaration.

1. Started function name with capital case in function DrawBattlefield.

1. Variables starting with lower case. (arguments in constructor of Grid)

1. Removed $ from Write where dont need a special format.Removed this keyword in StartTurn function to variable currentBox, this keyword is necessary only if are a local and class scope variable with same name.

1. Changed all Damage/Health types to int.

## Optimizations
1. Removed \n in writeline to choose class.

1. Removed a switch statement to create PlayerCharacter, instead parse input to an int and check if is between [min, max].

1. Removed unused variables playerCharacterClass, PlayerCurrentLocation and EnemyCurrentLocation 
from Program.cs.

1. Removed if statement in StartTurn function, and randomized first player in StartGame function.

1. Removed some functions calls nested to decrease stack.

## Architecture System
1. Implemented Factory Design Pattern to Characters/Class. Encapsulated some properties from Character that makes sense to be defined only in instantiation.

1. Implemented base abstract class to Characters and speciliazed using inheritance, prefirable implement default values of stats in constructor class.

1. Changed type of AutoBattle.Type from class to namespace.

1. Moved some functions related gameplay system to a exclusive class, GameSystem.cs

1. Moved some functions related to Input player to a exclusive class, InputSystem.cs. With functions to read int in a range.

1. Moved GetRandomInt to a class Utils.cs and used where get a random.

1. Removed arguments lines and columns from function DrawBattlefield, now draw whole field.

1. Extract method Walk to execute a movement in a Grid.

1. Removed array of gridBox in Grid.cs consuming less memory, instead implemented in each Character your position by GridBox, Grid.cs is responsible to draw battlefield and store length X/Y.

1. GridBox is a class now, struct is better use when is greater thant 16 bytes and is more legible because don't need create a struct (stack) ever when is changed.

## Bug fixed
1. Draw called before movement when char walked to up.

1. When player move to down was inverted definition of flag occupied.

1. Inverted x/y correlation with line/collumns in Grid constructor.

1. If player goes to last grid and next move was to left, player was locked.

1. Character doing two moves one per axis in one turn, now it's once and prioritizing the axis closest to the opponent.

1. Fixed checking of characters in range attack.

1. TakeDamage was using own BaseDamage instead use argument amount (changed argument name to damageSuffered).

1. HandleTurn checked if character was dead comparing health with 0, now is checking property IsAlive.

## Extra Implementation
Push away opponent, implemented pushing opponent in contrary direction in near axis.

## Discussion
I thought about algorithm to allocate player/enemy in battlefield chosing a random grid and checking if is empty, for now it's a better way, but if amount players increase significantly, maybe use a algorithm with a list of empties grid is better.

If will be implemented more teams, maybe need a optimization in function `Grid.DrawBattlefield()` at a forloop nested, maybe use a Dict with Index of characters drawn.