using RAGE.Elements;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace mechanic_client
{
    class Trans_Buis
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Gain { get; set; }
        public int TrucksCount { get; set; }


        public List<WorkersTrans> WorkersListTrans { get; set; }

        public Trans_Buis()
        {

        }


        public Trans_Buis(string Name, string Owner, int Gain, int TrucksCount, List<WorkersTrans> WorkersList)
        {
            this.Name = Name;
            this.Owner = Owner;
            this.Gain = Gain;
            this.TrucksCount = TrucksCount;
            this.WorkersListTrans = WorkersList;

        }
    }
    public class WorkersTrans
    {
        public string FullName { get; set; }
        public string Date { get; set; }
        public bool Online { get; set; }

        public WorkersTrans(string fullName, string date, string typeCustoms)
        {
            FullName = fullName;
            Date = date;
            List<Player> pl = Entities.Players.All;
            foreach (var item in pl)
            {
                // RAGE.Chat.Output(item.GetSharedData(PlayerData.Nickname).ToString());
                if (item.GetSharedData("Nickname") != null)
                {
                    // RAGE.Chat.Output(item.GetSharedData("typeCustoms").ToString()+"1");
                    if (item.GetSharedData("Nickname").ToString() == fullName)
                    {
                        Online = true;
                    }
                    else
                    {
                        Online = false;
                    }
                }
            }
        }

    }
}
