namespace TextBased_Dungeon_Game
{
    internal class DungeonGame
    {

        public SceneManager sceneManager;
        public static DungeonGame Instance;
        public static Warrior player = new Warrior();
        public static Shop shop = new Shop();
        public DungeonGame()
        {
            Instance = this;
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

        public int EnterDungeon(int i)
        {
            int dungeonDefense;
            Random dice = new Random();
            Warrior player = DungeonGame.player;

            player.PrevGold = player.Gold;
            player.PrevHealth = player.Health;

            switch (i)
            {
                case 1:
                    dungeonDefense = 5;
                    break;
                case 2:
                    dungeonDefense = 11;
                    break;
                case 3:
                    dungeonDefense = 17;
                    break;
                default:
                    return 0;
            }

            int benefit = player.Defense - dungeonDefense;
            int hpReduction = dice.Next(20 - benefit, 36 - benefit);

            if (benefit < 0 && dice.Next(0, 100) < 40)
            {
                player.Health -= hpReduction/2;
                // 실패 씬 리턴
                return (int)SceneType.DungeonFailScene;
            }

            
            int reward = 0;

            switch (i)
            {
                case 1:
                    reward += 1000;
                    break;
                case 2:
                    reward += 1700;
                    break;
                case 3:
                    reward += 2500;
                    break;
                default:
                    return 0;
            }

            int attack = player.Attack;
            int rewardRate = dice.Next(attack, attack * 2 + 1) / 100 + 1;
            player.Gold += (int)reward* rewardRate;
            player.Health -= hpReduction;

            return (int)SceneType.DungeonClearScene;
        }

 
    }
}
