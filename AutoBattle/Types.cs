using AutoBattle.Characters;

namespace AutoBattle.Types
{
    public struct CharacterClassSpecific
    {
        CharacterClass CharacterClass;
        float hpModifier;
        float ClassDamage;
        CharacterSkills[] skills;
    }

    public struct GridBox
    {
        public int xIndex;
        public int yIndex;
        public bool ocupied;
        public int Index;

        public GridBox(int x, int y, bool ocupied, int index)
        {
            xIndex = x;
            yIndex = y;
            this.ocupied = ocupied;
            this.Index = index;
        }
    }

    public struct CharacterSkills
    {
        string Name;
        float damage;
        float damageMultiplier;
    }
}
