namespace TextBased_Dungeon_Game
{
    class SceneManager
    {
        List<Scene> _scenes = new List<Scene>();

        public SceneManager()
        {
            for (int i =0; i< (int)SceneType.EndPoint; i++)
            {
                // Enum이름으로 Type을 구분해서 List에 추가
                // Reflection 사용법 익히기
            }
            _scenes.Add(new StartScene(SceneType.StartScene));
            _scenes.Add(new StatusScene(SceneType.StatusScene));
            _scenes.Add(new InventoryScene(SceneType.InventoryScene));
            _scenes.Add(new ShopScene(SceneType.ShopScene));
            _scenes.Add(new DungeonEnterScene(SceneType.DungeonEnterScene));
            _scenes.Add(new EquipmentScene(SceneType.EquipmentScene));
            _scenes.Add(new BuyScene(SceneType.BuyScene));
            _scenes.Add(new SellScene(SceneType.SellScene));
            _scenes.Add(new DungeonClearScene(SceneType.DungeonClearScene));
            _scenes.Add(new DungeonFailScene(SceneType.DungeonFailScene));


        }

        public Scene SceneList(SceneType _type)
        {
            return _scenes[(int)_type];
        }

        
    }
    
}
