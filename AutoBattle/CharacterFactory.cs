
using AutoBattle.Characters;

namespace AutoBattle.Factory
{
    public class CharacterFactory
    {
        public static Character InstantiateCharacter (int indexPlayer, Characters.CharacterClass charClass)
        {
            switch(charClass)
            {
                case CharacterClass.Archer:
                    return new Archer(indexPlayer);

                case CharacterClass.Cleric:
                    return new Cleric(indexPlayer);

                case CharacterClass.Paladin:
                    return new Paladin(indexPlayer);

                case CharacterClass.Warrior:
                    return new Warrior(indexPlayer);
                default:
                    throw new System.NotImplementedException($"Character Class { charClass } not implemented.");
            }
        }
    }
}
