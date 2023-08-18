using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dungeon_Game
{
    internal class DungeonGame
    {
        public SceneManager sceneManager;
        public static Warrior player = new Warrior();
        public DungeonGame()
        {
            GameInit();
            GameStart();
        }

        public void GameInit()
        {
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
