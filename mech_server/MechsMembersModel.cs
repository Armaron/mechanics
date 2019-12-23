using System;
using GTANetworkAPI;
using Serv_RP.player;

namespace mech_server
{
    public class MechsMembersModel
    {
        public string FullName { get; set; }
        public string NameBuisness { get; set; }
        public string TypeCustoms { get; set; }
        public string DateHire { get; set; }

        public MechsMembersModel()
        {

        }

        public MechsMembersModel(Client client, string nameBuisness, string typeCustoms)
        {
            if (client == null)
            {
                NAPI.Util.ConsoleOutput("Непридвиденная ошибка");
                return;
            }
            FullName = client.GetData(PlayerData.Nickname);
            DateHire = DateTime.Now.ToString();
            NameBuisness = nameBuisness;
            //client.SetData(PlayerData.Fraction, "mechs "+ nameBuisness);
            client.SetSharedData(PlayerData.Fraction, "mechs");
            TypeCustoms = typeCustoms;
            client.SetSharedData("typeCustoms", TypeCustoms);
        }
    }
}
