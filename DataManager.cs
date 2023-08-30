using Newtonsoft.Json;

namespace TextBased_Dungeon_Game
{
    class DataManager
    {
        public void PlayerSave()
        {
            Warrior player = DungeonGame.player;

            string json = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText(@"PlayerData.json", json);

            Thread.Sleep(1000);
        }

        public void PlayerLoad()
        {
            string json = File.ReadAllText(@"PlayerData.json");
            DungeonGame.player.Inventory.Clear();
            DungeonGame.player = JsonConvert.DeserializeObject<Warrior>(json);

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
