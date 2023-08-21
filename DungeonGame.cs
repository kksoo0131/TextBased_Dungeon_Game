namespace TextBased_Dungeon_Game
{
    internal class DungeonGame
    {
        public SceneManager sceneManager;
        public static Warrior player = new Warrior();
        public static Shop shop = new Shop();
        public DungeonGame()
        {
            GameInit();
            GameStart();
        }

        public void GameInit()
        {
            Console.SetWindowSize(100, 40);
            sceneManager = new SceneManager();
        }
        public void GameStart()
        {
            int nextScene = 0;
            while (true)
            {
                nextScene = sceneManager.SceneList((SceneType)nextScene).DrawScene();
            }
            
        }

 
    }
}
