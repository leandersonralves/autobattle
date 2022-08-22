namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSystem gameSystem = new GameSystem(7, 5);
            gameSystem.Setup();
        }
    }
}
