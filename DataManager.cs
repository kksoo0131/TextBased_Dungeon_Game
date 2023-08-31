using Newtonsoft.Json;

namespace TextBased_Dungeon_Game
{
    class DataManager
    {
        public void PlayerSave()
        {
            Player player = DungeonGame.Instance.player;

            string json = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText(@"PlayerData.json", json);

            Thread.Sleep(1000);
        }

        public void PlayerLoad()
        {
            string json = File.ReadAllText(@"PlayerData.json");
            DungeonGame.Instance.player.Inventory.Clear();
            DungeonGame.Instance.player = JsonConvert.DeserializeObject<Player>(json);

            Thread.Sleep(1000);
        }

        //public void InventorySave()
        //{
        //    List<Item> inventory = DungeonGame.player.Inventory.itemlist;

        //    string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
        //    File.WriteAllText(@"InventoryData.json", json);
        //}

        //public void InventoryLoad()
        //{
        //    string json = File.ReadAllText(@"InventoryData.json");
        //    DungeonGame.player.Inventory.itemlist = JsonConvert.DeserializeObject<List<Item>>(json);
        //}
    }
}
