using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Serv_RP.player;

namespace Serv_RP.Fraction.Police.Models
{
  public   class MembersModel
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public int HourInDept { get; set; }
        public bool Status { get; set; }
        public bool Online { get; set; }
        public int Time { get; set; }
    
        public int ClosedThings { get; set; }
        public int OpenedThings { get; set; }


        public MembersModel()
        {

        }

        public MembersModel(Client client,string rank)
        {
            if (client == null)
            {
                NAPI.Util.ConsoleOutput("Непридвиденная ошибка");
                return;
            }
            FullName = client.GetData(PlayerData.Nickname);
            Position = rank;
            HourInDept = 0;
            ClosedThings = 0;
            OpenedThings = 0;
            client.SetData(PlayerData.Fraction, "PD");
            client.SetData(PlayerData.FractionState, rank);
        }

    }
}
