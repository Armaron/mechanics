using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace mech_server
{
    public class TransComp_buis
    {
        public string Name { get; set; }
        public string NameB { get; set; }
        public string Owner { get; set; }
        public int Gain { get; set; }
        public int TrucksCount { get; set; }
        public string TypeTrans { get; set; }

        public SortedList<string, string> WorkersList { get; set; }

        public TransComp_buis()
        {

        }
        public TransComp_buis(string Name)
        {
            this.Name = Name;
            Owner = "username";
        }


        public TransComp_buis(Client client, string Name, string NameB, string Owner, int Gain, int TrucksCount, SortedList<string, string> WorkersList, string typeTrans)
        {
            this.Name = Name;
            this.NameB = NameB;
            this.Owner = Owner;
            this.Gain = Gain;
            this.TrucksCount = TrucksCount;
            this.WorkersList = WorkersList;
            this.TypeTrans = typeTrans;
            if (client != null)
            {
                client.SetData(mechanics.PlayerData.transBuisness , Name);
                client.SetSharedData(mechanics.PlayerData.transBuisness, Name);
                client.SetSharedData(mechanics.PlayerData.typeTrans, TypeTrans);
                client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "trans");
                client.SetData(Serv_RP.player.PlayerData.Fraction, "trans");

            }

        }
    }
}
