using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TextBased_Dungeon_Game
{
    internal class DungeonGame
    {
        //DungeonGame == GameManager ,싱글톤 클래스.
        public static DungeonGame Instance;

        public SceneManager sceneManager;
        public DataManager dataManager;
        public Player player;
        public Shop shop;
        public Dungeon dungeon;
        public StringBuilder message = new StringBuilder();
        public string path;
        public DungeonGame()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            
            GameInit();
            GameStart();
        }

        public void GameInit()
        {
            path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(path).FullName;
            path = $"{path}\\.vscode";
            Console.SetWindowSize(120, 40);
            sceneManager = new SceneManager();
            dataManager = new DataManager();
            shop = new Shop();
            //Read();
        }
        public void GameStart()
        {
            int nextScene = (int)SceneType.RoadScene;
            while (true)
            {
                Console.SetWindowSize(120, 40);
                Console.Clear();
                nextScene = sceneManager.SceneList((SceneType)nextScene).DrawScene();
            }

        }

        public string PrintMessage()
        {
            string str = message.ToString();
            message.Clear();
            return str;
        }
    }
}
