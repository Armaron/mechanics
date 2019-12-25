using RAGE;
using RAGE.Elements;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace cs_packages.client
{
    class GlobalServInfo : Events.Script
    {
        public static List<Player> AllPlayer = new List<Player>();

        public GlobalServInfo()
        {
            Events.Add("GetAllList", GetAllList);
            Events.Add("PlayerAdd", PlayerAdd);
            Events.Add("PlayerRemove", PlayerRemove);
            Events.Add("PlayerTalk", PlayerTalk);
            Events.Add("PlayerStopTalk", PlayerStopTalk);
            Events.Add("GetAllList", GetAllList);

        }
        public void GetAllList(object[] args)
        {
            if (JsonConvert.DeserializeObject<List<RAGE.Elements.Player>>(args[0].ToString()) != null)
                AllPlayer = JsonConvert.DeserializeObject<List<RAGE.Elements.Player>>(args[0].ToString());
        }

        public void PlayerAdd(object[] args)
        {
            AllPlayer.Add((Player)args[0]);
        }
        public void PlayerRemove(object[] args)
        {
            AllPlayer.Remove((Player)args[0]);
        }
        public void PlayerTalk(object[] args)
        {
            ((Player)args[0]).SetData<bool>("Voice", true);
        }
        public void PlayerStopTalk(object[] args)
        {
            ((Player)args[0]).SetData<bool>("Voice", false);
        }
    }
}
