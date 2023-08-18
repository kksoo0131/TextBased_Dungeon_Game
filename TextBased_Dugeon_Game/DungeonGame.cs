using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBased_Dugeon_Game
{
    internal class DungeonGame
    {
        SceneManager sceneManager;
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
