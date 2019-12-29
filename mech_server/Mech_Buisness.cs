using GTANetworkAPI;
using Serv_RP.player;
using System.Collections.Generic;


namespace mech_server
{
   public class Mech_Buisness
    {
       
        public string Name { get; set; }
        public string NameB { get; set; }
        public string Owner { get; set; }
        public int Gain { get; set; }
        public int TrucksCount { get; set; }
        public string TypeCustoms { get; set; }

        public SortedList<string, string> WorkersList { get; set; }

        public Mech_Buisness()
        {

        }
        public Mech_Buisness(string Name)
        {
            this.Name = Name;
            Owner = "username";
        }


        public Mech_Buisness(Client client, string Name, string NameB, string Owner, int Gain, int TrucksCount, SortedList<string, string> WorkersList, string typeCustoms)
        {
            this.Name = Name;
            this.NameB = NameB;
            this.Owner = Owner;
            this.Gain = Gain;
            this.TrucksCount = TrucksCount;
            this.WorkersList = WorkersList;
            TypeCustoms = typeCustoms;
            if(client != null) { 
            client.SetData("mechBuisness", Name);
            client.SetSharedData("mechBuisness", Name);
            client.SetSharedData("typeCustoms", TypeCustoms);
            client.SetSharedData(PlayerData.Fraction, "mechs");
            client.SetData(PlayerData.Fraction, "mechs");

            }

        }
    }
}
