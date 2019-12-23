using RAGE.Elements;
using Serv_RP.player;
using System;
using System.Collections.Generic;
using System.Text;

namespace mechanic_client
{
    public class Mech_Org
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Gain { get; set; }
        public int TrucksCount { get; set; }
        

        public List<Workers> WorkersList { get; set; }

        public Mech_Org()
        {

        }


        public Mech_Org(string Name, string Owner, int Gain, int TrucksCount, List<Workers> WorkersList)
        {
            this.Name = Name;
            this.Owner = Owner;
            this.Gain = Gain;
            this.TrucksCount = TrucksCount;
            this.WorkersList = WorkersList;
           
        }
    }
    public class Workers
    {
        public string FullName { get; set; }
        public string Date { get; set; }
        public bool Online { get; set; }

        public Workers(string fullName, string date)
        {
            FullName = fullName;
            Date = date;
           // List<Player> pl = Entities.Players.All;
          //  if (pl.Find(x => x.GetSharedData(PlayerData.Nickname).ToString() == fullName) != null)
          //  {
                Online = true;
           //}
           //else
           //{
           //    Online = false;
           //}

        }
    }
}

