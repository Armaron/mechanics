using GTANetworkAPI;
using mechanics;
using System;
using System.Collections.Generic;
using System.Text;

namespace mech_server
{
    class TransComp_Player : Script
    {

        [Command("delbt")]
        public void DelBuis(Client player)
        {
            if (player.GetSharedData("mechBuisness") != null)
            {
                TransComp.RemoveBuisnessTrans(player.GetSharedData("mechBuisness"));
            }

        }

        [RemoteEvent("Add_New_Buisness_Trans")]
        public void Add_New_BuisnessTrans(Client client, object[] args)
        {

            TransComp_buis m = TransComp.LoadBuisnessTrans(client);

            if (m == null)
            {
                TransComp.AddBuisnessTrans(client, client.GetData(Serv_RP.player.PlayerData.Nickname), args[1].ToString(), args[2].ToString(), args[3].ToString());
            }

        }

        [RemoteEvent("Load_Buisness_Trans")]
        public void Load_BuisnessTras(Client client, object[] args)
        {

            TransComp_buis m = TransComp.LoadBuisnessTrans(client);

            // NAPI.Chat.SendChatMessageToAll(NAPI.Util.ToJson(m));

            // NAPI.Chat.SendChatMessageToAll(client.GetData(Serv_RP.player.PlayerData.Nickname));
            if (m != null)
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "LoadBuisnessPageTrans", m.NameB, m.Owner, m.Gain, m.TrucksCount, m.WorkersList, m.TypeTrans);
            }
        }


        [RemoteEvent("Find_Player_To_Add_Trans")]
        public void FindPlayerToAddTrans(Client client, object[] args)
        {
            TransComp_buis m = TransComp.LoadBuisnessTrans(client);
            Client target = NAPI.Pools.GetAllPlayers().Find(pl => pl.GetData(Serv_RP.player.PlayerData.Nickname).ToString() == args[0].ToString()/* && client.GetSharedData("typeCustoms") != pl.GetSharedData("typeCustoms")*/);
            if (target != null)
            {
                // NAPI.Chat.SendChatMessageToAll(args[1].ToString());
                TransComp.AddToFractionTrans(target, m.Name, args[1].ToString());
                NAPI.ClientEvent.TriggerClientEvent(client, "hireTransWorkerOnTablet", args[0]);
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(client, "~r~Человек не найден");
            }
        }
        [RemoteEvent("Find_Player_To_Remove_Trans")]
        public void FindPlayerToRemoveTrans(Client client, object[] args)
        {
            TransComp_buis m = TransComp.LoadBuisnessTrans(client);
            Client target = NAPI.Pools.GetAllPlayers().Find(pl => pl.GetData(Serv_RP.player.PlayerData.Nickname).ToString() == args[0].ToString());
            if (target != null)
            {
                NAPI.Chat.SendChatMessageToAll(args[0].ToString());
                TransComp.RemoveFromFractionTrans(target, m.Name);
                NAPI.ClientEvent.TriggerClientEvent(client, "dissmisalTransWorkerOnTablet", args[0]);
            }

        }


        [RemoteEvent("Update_Cars_Count_Trans")]
        public void Update_Cars_Count_Trans(Client client, object[] args)
        {
            TransComp.UpdateCarsCountTrans(client, args[0].ToString(), Convert.ToInt32(args[1].ToString()));
        }

        [RemoteEvent("Update_Name_Buis_Trans")]
        public void Update_Name_Buis_Trans(Client client, object[] args)
        {
            TransComp.UpdateNameBuisTrans(client, args[0].ToString());
        }

    }
}
