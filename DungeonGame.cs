using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TextBased_Dungeon_Game
{
    internal class DungeonGame
    {

        public SceneManager sceneManager;
        public static DungeonGame Instance;
        public static Warrior player;
        public static Shop shop;
        public static Dungeon dungeon;

        public static Action message;
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
            dungeon = new Dungeon();
            Read();
        }
        public void GameStart()
        {
            int nextScene = 0;
            while (true)
            {
                nextScene = sceneManager.SceneList((SceneType)nextScene).DrawScene();
            }
            
        }

        static public void PlayerSave()
        {
            IFormatter formatter = new BinaryFormatter();
            // 파일 저장을 위한 BinaryFormatter객체 생성
            using (Stream stream = new FileStream("player_info.bin", FileMode.Create, FileAccess.Write))
            {
                // 스트림을 생성 , 파일을 생성해서 쓰기모드에 들어간다.(파일을 출력할 스트림을 생성)
                formatter.Serialize(stream, player);
                // player 객체의 정보를 직렬화하여 stream에 저장한다.
            }
        }
        static public void ShopSave()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream("shop_info.bin", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, shop);
            }
        }
        public void Read()
        {
            IFormatter formatter = new BinaryFormatter();
            
            try
            {
                using (Stream stream = new FileStream("player_info.bin", FileMode.Open, FileAccess.Read))
                {
                    // 스트림을 생성, 파일을 오픈해서 읽기모드에 들어간다. (파일을 입력받는 스트림을 생성)
                    player = (Warrior)formatter.Deserialize(stream);
                    // 스트림을 역직렬화해서 player에 정보를 넣어준다.
                }
            }
            catch (FileNotFoundException)
            {
                player = new Warrior();
            }

            try
            {
                using (Stream stream = new FileStream("shop_info.bin", FileMode.Open, FileAccess.Read))
                {
                    // 스트림을 생성, 파일을 오픈해서 읽기모드에 들어간다. (파일을 입력받는 스트림을 생성)
                    shop = (Shop)formatter.Deserialize(stream);
                    // 스트림을 역직렬화해서 player에 정보를 넣어준다.
                }
            }
            catch (FileNotFoundException)
            {
                shop = new Shop();
            }

        }
        static public void PrintMessage()
        {
            if (message != null)
            {
                message();
                message = null;
            }
        }

      /*  public int EnterDungeon(int i)
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
            player.Level += 1;
            player.Attack += 1;
            player.Defense += 1;

            return (int)SceneType.DungeonClearScene;
        }*/

 
    }
}
