namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSystem gameSystem = new GameSystem(5, 5);
            gameSystem.Setup();
        }
    }
}
