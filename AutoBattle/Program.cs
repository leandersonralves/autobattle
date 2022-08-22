namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSystem gameSystem = new GameSystem(7, 7);
            gameSystem.Setup();
        }
    }
}
