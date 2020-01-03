using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using mech_server;
using Newtonsoft.Json;
using Serv_RP.player;


namespace mechanics
{
    public class Mechanic_Player : Script
    {


         [Command("delb")]
         public void DelBuis(Client player)
         {
            if (player.GetSharedData("mechBuisness") != null)
            {
                Mechanic.RemoveBuisness(player.GetSharedData("mechBuisness"));
            }
             
         }

        [RemoteEvent("Sync_Event_Tow")]
        public void Event_Sync(Client client, object[] args)
        {

            Vehicle veh1 = (Vehicle)args[0];
            Vehicle veh2 = (Vehicle)args[1];

            veh1.SetSharedData("targetVehicle", "tow");
            veh2.SetSharedData("vehicle", "tow");
            NAPI.ClientEvent.TriggerClientEventForAll("Sync_Event_Tow_cl");
            veh1.ResetSharedData("targetVehicle");
            veh2.ResetSharedData("vehicle");

        }

        [RemoteEvent("Sync_Event_Detach")]

        public void Event_Sync_Detch(Client client, object[] args)
        {
            Vehicle veh1 = (Vehicle)args[0];
            Vehicle veh2 = (Vehicle)args[1];
            veh1.SetSharedData("currentlyTowedVehicle", "tow");
            veh2.SetSharedData("vehicle", "tow");
            NAPI.ClientEvent.TriggerClientEventForAll("Sync_Event_Detach_cl");
            veh1.ResetSharedData("currentlyTowedVehicle");
            veh2.ResetSharedData("vehicle");
            // veh1.ResetSharedData("tow");
        }

        //[RemoteEvent("Sync_Window")]
        //public void SyncWindow(Client client, object[] args)
        //{
        //    Vehicle veh = (Vehicle)args[1];
        //    veh.SetSharedData("targetVehicleFixWindows", "fixWindows");
        //    int window = Convert.ToInt32(args[0].ToString());
        //    NAPI.ClientEvent.TriggerClientEventForAll("syncFixWindows", window);
        //    veh.ResetSharedData("targetVehicleFixWindows");
        //}

        [RemoteEvent("Sync_Window")]
        public void SyncWindow(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            //veh.SetSharedData("targetVehicleFixWindows", "fixWindows");
            int window = Convert.ToInt32(args[1].ToString());
            NAPI.ClientEvent.TriggerClientEventForAll("syncFixWindows",veh, window);
           // veh.ResetSharedData("targetVehicleFixWindows");
        }

        //[RemoteEvent("Sync_BodyShell")]
        //public void SyncBodyShell(Client client, object[] args)
        //{
        //    Vehicle veh = (Vehicle)args[0];
        //    veh.SetSharedData("targetVehicleFixBodyShell", "fixBodyShell");
        //    NAPI.ClientEvent.TriggerClientEventForAll("syncFixBodyShell");
        //    veh.ResetSharedData("targetVehicleFixBodyShell");
        //}

        [RemoteEvent("Sync_BodyShell")]
        public void SyncBodyShell(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
           // veh.SetSharedData("targetVehicleFixBodyShell", "fixBodyShell");
            NAPI.ClientEvent.TriggerClientEventForAll("syncFixBodyShell", veh);
            //veh.ResetSharedData("targetVehicleFixBodyShell");
        }

        //[RemoteEvent("Sync_Wheel")]
        //public void SyncWheel(Client client, object[] args)
        //{
        //    Vehicle veh = (Vehicle)args[1];
        //    veh.SetSharedData("targetVehicleFixWheel", "fixWheel");
        //    int wheel = Convert.ToInt32(args[0].ToString());
        //    NAPI.ClientEvent.TriggerClientEventForAll("syncFixWheels", wheel);
        //    veh.ResetSharedData("targetVehicleFixWheel");
        //}

        [RemoteEvent("Sync_Wheel")]
        public void SyncWheel(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            //veh.SetSharedData("targetVehicleFixWheel", "fixWheel");
            int wheel = Convert.ToInt32(args[1].ToString());
            NAPI.ClientEvent.TriggerClientEventForAll("syncFixWheels",veh, wheel);
            //veh.ResetSharedData("targetVehicleFixWheel");
        }

        //[RemoteEvent("Sync_Body")]
        //public void SyncBody(Client client, object[] args)
        //{
        //    Vehicle veh = (Vehicle)args[0];
        //    int bodyH = Convert.ToInt32(args[1].ToString());
        //    veh.SetSharedData("targetVehicleFixBody", "fixBody");
        //    NAPI.ClientEvent.TriggerClientEventForAll("syncFixBody", bodyH);
        //    veh.ResetSharedData("targetVehicleFixBody");
        //}

        [RemoteEvent("Sync_Body")]
        public void SyncBody(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            int bodyH = Convert.ToInt32(args[1].ToString());
            //veh.SetSharedData("targetVehicleFixBody", "fixBody");
            NAPI.ClientEvent.TriggerClientEventForAll("syncFixBody",veh, bodyH);
            //veh.ResetSharedData("targetVehicleFixBody");
        }

        //[RemoteEvent("Sync_Eng")]
        //public void SyncEng(Client client, object[] args)
        //{
        //    Vehicle veh = (Vehicle)args[0];
        //    int EngH = Convert.ToInt32(args[1].ToString());
        //    veh.SetSharedData("targetVehicleFixEng", "fixEng");
        //    NAPI.ClientEvent.TriggerClientEventForAll("SyncFixEng", EngH);
        //    veh.ResetSharedData("targetVehicleFixEng");
        //}

        [RemoteEvent("Sync_Eng")]
        public void SyncEng(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            int EngH = Convert.ToInt32(args[1].ToString());
           
            NAPI.ClientEvent.TriggerClientEventForAll("syncFixEng",veh, EngH);

        }

        [RemoteEvent("Add_New_Buisness")]
        public void Add_New_Buisness(Client client, object[] args)
        {
           
            Mech_Buisness m = Mechanic.LoadBuisness(client);
            
            if (m == null)
            {
                Mechanic.AddBuisness(client, client.GetData(Serv_RP.player.PlayerData.Nickname), args[1].ToString(), args[2].ToString(), args[3].ToString());
            }

        }


        [RemoteEvent("Add_Service_Records")]
        public void Add_Service_Records(Client client, object[] args)
        {
            Mechanic.AddServiceRecord(client, args[0].ToString(), args[1].ToString(), args[2].ToString(), Convert.ToInt32(args[3].ToString()), args[4].ToString(), args[5].ToString());

        }

        [RemoteEvent("Load_Buisness")]
        public void Load_Buisness(Client client, object[] args)
        {
            
            Mech_Buisness m = Mechanic.LoadBuisness(client);
          
           // NAPI.Chat.SendChatMessageToAll(NAPI.Util.ToJson(m));
          
           // NAPI.Chat.SendChatMessageToAll(client.GetData(Serv_RP.player.PlayerData.Nickname));
            if (m != null)
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "LoadBuisnessPage", m.NameB, m.Owner, m.Gain, m.TrucksCount, m.WorkersList, m.TypeCustoms);
            }
        }


        [Command("service", "name", SensitiveInfo = true, GreedyArg = true)]
        public void service(Client player, string name)
        {

            VehicleDetails v = Mechanic.LoadServiceRecord(player, name, "1231231");

            // NAPI.Chat.SendChatMessageToAll(v.TotalHealth.ToString());

            NAPI.ClientEvent.TriggerClientEvent(player, "LoadVehicleRecord", v.CarNumber, v.SellDate, v.CarType, v.CarScore, v.DateOf, v.DoneWorks);
        }


        [RemoteEvent("ServiceBook")]
        public void ServiceBook(Client client, object[] args)
        {

            Vehicle veh = (Vehicle)args[0];
            VehicleDetails v = Mechanic.LoadServiceRecord(client, veh.NumberPlate, "1231231");

            // NAPI.Chat.SendChatMessageToAll(v.TotalHealth.ToString());

            NAPI.ClientEvent.TriggerClientEvent(client, "LoadVehicleRecord", v.CarNumber, v.SellDate, v.CarType, v.CarScore, v.DateOf, v.DoneWorks);
        }




        [RemoteEvent("Find_Player_To_Add")]
        public void FindPlayerToAdd(Client client, object[] args)
        {
            Mech_Buisness mechBuis = Mechanic.LoadBuisness(client);
            Client target = NAPI.Pools.GetAllPlayers().Find(pl => pl.GetData(Serv_RP.player.PlayerData.Nickname).ToString() == args[0].ToString()/* && client.GetSharedData("typeCustoms") != pl.GetSharedData("typeCustoms")*/);
            if (target != null)
            {
                // NAPI.Chat.SendChatMessageToAll(args[1].ToString());
                Mechanic.AddToFraction(target, mechBuis.Name, args[1].ToString());
                NAPI.ClientEvent.TriggerClientEvent(client, "hireMechanicalOnTablet", args[0]);
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(client, "~r~Человек не найден");
            }
        }
        [RemoteEvent("Find_Player_To_Remove")]
        public void FindPlayerToRemove(Client client, object[] args)
        {
            Mech_Buisness mechBuis = Mechanic.LoadBuisness(client);
            Client target = NAPI.Pools.GetAllPlayers().Find(pl => pl.GetData(Serv_RP.player.PlayerData.Nickname).ToString() == args[0].ToString());
            if (target != null)
            {
                NAPI.Chat.SendChatMessageToAll(args[0].ToString());
                Mechanic.RemoveFromFraction(target, mechBuis.Name);
                NAPI.ClientEvent.TriggerClientEvent(client, "dissmisalMechanicalOnTablet", args[0]);
            }

        }

        [RemoteEvent("Update_Cars_Count")]
        public void Update_Cars_Count(Client client, object[] args)
        {
            Mechanic.UpdateCarsCount(client, args[0].ToString(), Convert.ToInt32(args[1].ToString()));
        }

        [RemoteEvent("Save_Car_Health")]
        public void SaveCarHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            Mechanic.SaveVehicleHealth(client, veh.DisplayName, veh.NumberPlate, Convert.ToInt32(args[1]));
        }
        [RemoteEvent("Save_Car_Max_Health")]
        public void SaveMaxCarHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            Mechanic.SaveMaxVehicleHealth(client, veh.NumberPlate, Convert.ToInt32(args[1]));
        }
        [RemoteEvent("Load_Car_Health")]
        public void LoadCarHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            
            int carHealth = Mechanic.LoadVehicleHealth(client, veh.DisplayName, veh.NumberPlate);
            NAPI.ClientEvent.TriggerClientEvent(client, "Load_Vehicle_Health", carHealth);
        }
        [RemoteEvent("Load_Car_Max_Health")]
        public void LoadCarMaxHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];

            int carMaxHealth = Mechanic.LoadVehicleMaxHealth(veh.NumberPlate);
            NAPI.ClientEvent.TriggerClientEvent(client, "Load_Vehicle_Max_Health", carMaxHealth);
        }

        [RemoteEvent("Save_Car_Body_Health")]
        public void SaveCarBodyHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            Mechanic.SaveVehicleBodyHealth(client, veh.DisplayName, veh.NumberPlate, Convert.ToInt32(args[1]));
        }
        [RemoteEvent("Save_Car_Max_Body_Health")]
        public void SaveCarMaxBodyHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            Mechanic.SaveMaxVehicleBodyHealth(client, veh.NumberPlate, Convert.ToInt32(args[1]));
        }
        [RemoteEvent("Load_Car_Body_Health")]
        public void LoadCarBodyHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];

            int carBodyHealth = Mechanic.LoadVehicleBodyHealth(client, veh.DisplayName, veh.NumberPlate);
            NAPI.ClientEvent.TriggerClientEvent(client, "Load_Vehicle_Body_Health", carBodyHealth);
        }
        [RemoteEvent("Load_Car_Max_Body_Health")]
        public void LoadCarMaxBodyHealth(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];

            int carMaxBodyHealth = Mechanic.LoadVehicleMaxBodyHealth(veh.NumberPlate);
            NAPI.ClientEvent.TriggerClientEvent(client, "Load_Vehicle_Max_Body_Health", carMaxBodyHealth);
        }
        [RemoteEvent("Update_Name_Buis")]
        public void Update_Name_Buis(Client client, object[] args)
        {
            Mechanic.UpdateNameBuis(client, args[0].ToString());
        }


        [RemoteEvent("custom")]
        public void Сustom(Client client, object[] args)
        {

          
            Vehicle veh = (Vehicle)args[2];
            veh.SetMod(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
        }

        [RemoteEvent("SaveAllMods")]
        public void SaveAllMods(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[1];
            Mechanic.SaveMods(veh.DisplayName, veh.NumberPlate, args[0].ToString());
            //int[] mods = NAPI.Util.FromJson<int[]>(args[0]);

        }
        [RemoteEvent("SaveDamag")]
        public void SaveDamag(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            Mechanic.SaveDamag(veh.DisplayName, veh.NumberPlate, args[1].ToString(), args[2].ToString());
            //int[] mods = NAPI.Util.FromJson<int[]>(args[0]);

        }

        [RemoteEvent("CancelAllMods")]
        public void CancelAllMods(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[1];
            int[] oldMods = NAPI.Util.FromJson<int[]>(args[0]);
            for (int i = 0; i < oldMods.Length; i++)
            {
                if (i == 66)
                {
                    veh.PrimaryColor = Convert.ToInt32(oldMods[i]);
                }
                else if (i == 67)
                {
                    veh.SecondaryColor = Convert.ToInt32(oldMods[i]);
                }
                else
                {
                    veh.SetMod(i, oldMods[i]);

                }
            }
        }

        [RemoteEvent("customColor1")]
        public void СustomColor1(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[2];
            veh.PrimaryColor = Convert.ToInt32(Convert.ToInt32(args[1]));
        }
        [RemoteEvent("customColor2")]
        public void СustomColor2(Client client, object[] args)
        {
            Vehicle veh = (Vehicle)args[2];
            veh.SecondaryColor = Convert.ToInt32(Convert.ToInt32(args[1]));
        }

        
    }
}
