namespace DefaultNamespace
{
    public enum GameState
    {
        MainMenu,
        Tutorial,
        Game
    }
    public class Game
    {
        public static GameState GameState = GameState.MainMenu;
    }
}