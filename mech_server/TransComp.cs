using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTANetworkAPI;
using mech_server;
using Serv_RP.economics;
using Serv_RP.player;


namespace mechanics
{
    public static class TransComp
    {
        public static List<TransComp_buis> transcomp_buisness = new List<TransComp_buis>();
        private static SortedList<string, string> listTrans = new SortedList<string, string>();


        private static Vector3[] buyTrans = new Vector3[10] {
            new Vector3(-1165.825f, -2210.055f, 13.195f),
            new Vector3(-774.3486, -2633.117f, 13.9285f),
            new Vector3(-263.5008f, -2486.728f, 7.296107f),
            new Vector3(-501.591f, -2857.049f, 7.2959f),
            new Vector3(1383.333f, -2078.762f, 51.99854f),
            new Vector3(913.8523f, -1273.083f, 27.09614f),
            new Vector3(1612.523f, 3776.409f, 34.6917f),
            new Vector3(346.7017f, 3407.186f, 36.5326f),
            new Vector3(175.6526f, 6400.84f, 31.34312f),
            new Vector3(-250.3742f, 6234.498f, 31.48951f),
        };

        public static void TransInit(Client client)
        {
            List<string> nameList = new List<string>();
            foreach (var item in transcomp_buisness)
            {


                if (item.Owner != "username")
                {
                    nameList.Add(item.Name);
                }



                //NAPI.Util.ConsoleOutput(item.Name);
                if (item.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname))
                {

                    client.SetData(mechanics.PlayerData.transBuisness, item.Name);
                    client.SetSharedData(mechanics.PlayerData.transBuisness, item.Name);
                    client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "trans");
                    client.SetData(Serv_RP.player.PlayerData.Fraction, "trans");
                    client.SetSharedData(mechanics.PlayerData.typeTrans, item.TypeTrans);

                }
                if (item.WorkersList != null)
                {
                    foreach (var workers in item.WorkersList)
                    {

                        if (workers.Key == client.GetData(Serv_RP.player.PlayerData.Nickname) && workers.Key != item.Owner)
                        {
                            client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "trans");
                            client.SetData(Serv_RP.player.PlayerData.Fraction, "trans");
                            client.SetSharedData(mechanics.PlayerData.typeTrans, item.TypeTrans);

                        }
                    }
                }
            }
            client.SetSharedData(Serv_RP.player.PlayerData.Nickname, client.GetData(Serv_RP.player.PlayerData.Nickname));
           // NAPI.ClientEvent.TriggerClientEvent(client, "LoadBuisOwner", nameList);
        }


        public static void LoadAllBusinessTransCo(List<TransComp_buis> trans_buis)
        {

            if (transcomp_buisness.Count != 0)
            {


                //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness("buis10"));
                
                transcomp_buisness = trans_buis;
                foreach (var item in trans_buis)
                {
                    NAPI.Util.ConsoleOutput(item.Owner);
                }


            }
            else
            {

               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis1"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis2"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis3"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis4"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis5"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis6"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis7"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis8"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis9"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis10"));
               // Serv_RP.database.DataBase.SaveMechBusinesOnBD(new Mech_Buisness("buis11"));


            }
            ColShape shape1;
            //ColShape shape2;
            for (int i = 0; i < buyTrans.Length; i++)
            {
                NAPI.Marker.CreateMarker(29, buyTrans[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(0, 204, 0), true, uint.MaxValue);

                //Blip blip = NAPI.Blip.CreateBlip(buyCustoms[i], uint.MaxValue);
                shape1 = NAPI.ColShape.CreateSphereColShape(buyTrans[i], 1, 4294967295);
                shape1.SetSharedData("nameBuis", "buis" + (i + 1));

                // blip.ShortRange = true;
                // blip.Name = "Автосервис";
                // blip.Sprite = 402U;
            }
            int count = 0;
            foreach (var item in transcomp_buisness)
            {
                if (item.NameB != null)
                {
                    BlipList.AddBlip(BlipList.TYPE_INFRASTRUCTURE, 402, buyTrans[count], "Транс. Комп.: " + item.NameB, 1f, 71, (int)byte.MaxValue, 0.0f, true, 0.0f, uint.MaxValue);
                }
                else
                {
                    BlipList.AddBlip(BlipList.TYPE_INFRASTRUCTURE, 402, buyTrans[count], "Транс. Комп.", 1f, 25, (int)byte.MaxValue, 0.0f, true, 0.0f, uint.MaxValue);
                }
                count++;
            }




            NAPI.Util.ConsoleOutput("Бизнесс мехов загружен");
        }


        public static void AddToFractionTrans(Client client, string nameBuisness, string typeTrans)

        {
            Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                string FullName = client.GetData(Serv_RP.player.PlayerData.Nickname);
                string DateHire = Serv_RP.server_state.EventTickOnServer.Day + "." + Serv_RP.server_state.EventTickOnServer.Mounth + "." + Serv_RP.server_state.EventTickOnServer.Year;
                string NameBuisness = nameBuisness;
                //client.SetData(PlayerData.Fraction, "mechs "+ nameBuisness);
                client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "trans");
                string TypeCustoms = typeTrans;
                client.SetSharedData(mechanics.PlayerData.typeTrans, TypeCustoms);
                client.SetData(Serv_RP.player.PlayerData.Fraction, "trans");
                TransComp_buis trans_comp = transcomp_buisness.Find(pl => pl.Name == nameBuisness);
                //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
                if (trans_comp != null)
                {

                    trans_comp.WorkersList.Add(FullName, DateHire);

                    transcomp_buisness.Find(pl => pl.Name == nameBuisness).WorkersList = trans_comp.WorkersList;

                    //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(null, trans_comp.Name, trans_comp.NameB, trans_comp.Owner, trans_comp.Gain, trans_comp.TrucksCount, trans_comp.WorkersList, trans_comp.TypeCustoms));
                }
                NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Вас приняли в транспортную компанию");
            }), 0L)));
        }

        public static void RemoveFromFractionTrans(Client client, string nameBuisness)
        {
            Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                TransComp_buis trans_comp = transcomp_buisness.Find(pl => pl.Name == nameBuisness);

                if (trans_comp != null)
                {

                    //foreach (var item in mechs.FindAll(pl => pl.NameBuisness == mech_buis.Name))
                    //{
                    trans_comp.WorkersList.Remove(client.GetData(Serv_RP.player.PlayerData.Nickname));
                    transcomp_buisness.Find(pl => pl.Name == nameBuisness).WorkersList.Remove(client.GetData(Serv_RP.player.PlayerData.Nickname));

                    //}


                    //listMechs.Remove(client.GetData(Serv_RP.player.PlayerData.Nickname);
                    client.SetSharedData(Serv_RP.player.PlayerData.Fraction, null);
                    client.SetData(Serv_RP.player.PlayerData.Fraction, null);
                    client.SetSharedData(mechanics.PlayerData.typeTrans, null);
                    //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(null, trans_comp.Name, trans_comp.NameB, trans_comp.Owner, trans_comp.Gain, trans_comp.TrucksCount, trans_comp.WorkersList, trans_comp.TypeCustoms));
                    NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Вас исключили из механиков");
                }
            }), 0L)));

        }

        public static void AddBuisnessTrans(Client client, string name, string StaticName, string typeTrans, string nameB)
        {
            Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                if (transcomp_buisness.Find(buis => buis.Name == StaticName).Owner == "username")
                {
                    transcomp_buisness.Find(buis => buis.Name == StaticName).Name = StaticName;
                    transcomp_buisness.Find(buis => buis.Name == StaticName).NameB = nameB;
                    transcomp_buisness.Find(buis => buis.Name == StaticName).Owner = name;
                    transcomp_buisness.Find(buis => buis.Name == StaticName).TypeTrans = typeTrans;
                    transcomp_buisness.Find(buis => buis.Name == StaticName).TrucksCount = 0;
                    transcomp_buisness.Find(buis => buis.Name == StaticName).WorkersList = listTrans;
                    transcomp_buisness.Find(buis => buis.Name == StaticName).Gain = 1000;

                    client.SetData(mechanics.PlayerData.transBuisness, StaticName);
                    client.SetSharedData(mechanics.PlayerData.transBuisness, StaticName);
                    client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "trans");
                    client.SetData(Serv_RP.player.PlayerData.Fraction, "trans");
                    client.SetSharedData(mechanics.PlayerData.typeTrans, typeTrans);

                    //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(client, StaticName, nameB, name, 1000, 0, listTrans, typeCustoms));
                    NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Вы приобрели бизнесс");
                }
                else
                {
                    NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Этот бизнес уже принадлежит кому то");
                }
                List<string> nameList = new List<string>();
                foreach (var item in transcomp_buisness)
                {
                    if (item.Owner != "username")
                    {
                        nameList.Add(item.Name);
                    }
                }
                //NAPI.ClientEvent.TriggerClientEventForAll("LoadBuisOwner", nameList);
                //Serv_RP.economics.BlipList.AddBlip()
            }), 0L)));
        }


        public static void RemoveBuisnessTrans(string nameB)
        {
            Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                List<Client> clients = NAPI.Pools.GetAllPlayers();
                foreach (var client in clients)
                {
                    NAPI.Chat.SendChatMessageToAll(transcomp_buisness.Find(buis => buis.Name == nameB).TypeTrans);
                    if (client.GetSharedData(mechanics.PlayerData.typeTrans) == transcomp_buisness.Find(buis => buis.Name == nameB).TypeTrans)
                    {
                        if (client.GetData(mechanics.PlayerData.transBuisness) != null)
                        {
                            client.SetData(mechanics.PlayerData.transBuisness, null);
                            client.SetSharedData(mechanics.PlayerData.transBuisness, null);
                        }
                        client.SetSharedData(Serv_RP.player.PlayerData.Fraction, null);
                        client.SetData(Serv_RP.player.PlayerData.Fraction, null);
                        client.SetSharedData(mechanics.PlayerData.typeTrans, null);
                        NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Бизнес продан");
                    }
                }

                transcomp_buisness.Find(buis => buis.Name == nameB).Name = nameB;
                transcomp_buisness.Find(buis => buis.Name == nameB).Owner = "username";
                transcomp_buisness.Find(buis => buis.Name == nameB).TrucksCount = 0;
                transcomp_buisness.Find(buis => buis.Name == nameB).WorkersList = null;
                transcomp_buisness.Find(buis => buis.Name == nameB).Gain = 0;
                transcomp_buisness.Find(buis => buis.Name == nameB).TypeTrans = "";

                //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(nameB));

                List<string> nameList = new List<string>();
                foreach (var item in transcomp_buisness)
                {
                    if (item.Owner != "username")
                    {
                        nameList.Add(item.Name);
                    }
                }
                //NAPI.ClientEvent.TriggerClientEventForAll("LoadBuisOwner", nameList);
            }), 0L)));
        }


        public static void UpdateCarsCountTrans(Client client, string nameVeh, int price)
        {
            Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                float heading = client.Heading;
                Vector3 pos = client.Position;
                heading *= (float)(Math.PI / 180);
                pos.X += (float)(3.0f * Math.Sin(-heading));
                pos.Y += (float)(3.0f * Math.Cos(-heading)); ;
                string nameVehSpawn = "";
                switch (nameVeh)
                {
                    case "Towtruck Large":
                        nameVehSpawn = "towtruck";
                        break;
                    case "Vapid Tow Truck":
                        nameVehSpawn = "towtruck2";
                        break;
                    case "MTL Flatbed":
                        nameVehSpawn = "flatbed";
                        break;
                }

                //NAPI.Chat.SendChatMessageToAll(nameVeh);
                TransComp_buis transcomp_buis = transcomp_buisness.Find(pl => pl.Name == client.GetData(mechanics.PlayerData.transBuisness));
                transcomp_buis.TrucksCount = transcomp_buis.TrucksCount + 1;
                Serv_RP.vehicles.VehicleModel vehicleModel = new Serv_RP.vehicles.VehicleModel();
                vehicleModel.OwnerID = client.GetData(Serv_RP.player.PlayerData.PLAYER_ID);
                vehicleModel.Name = nameVehSpawn;
                vehicleModel.BodyHealth = 1000;
                vehicleModel.EngHealth = 1000;
                Serv_RP.database.DataBase.CarNumber++;
                vehicleModel.PlateNumber = Serv_RP.database.DataBase.CarNumber.ToString();
                vehicleModel.Color1 = 0;
                vehicleModel.Color2 = 0;
                vehicleModel.FuelLevel = 100;
                vehicleModel.pos = pos;
                vehicleModel.rot = client.Rotation;
                vehicleModel.Parked = "";
                vehicleModel.KeyID = ++Serv_RP.database.DataBase.LastKey;

                GameObjectModel GOM = GameObjects.ObjectsList.Find(gom => gom.categoryS == "Key" && gom.hunger == vehicleModel.KeyID);
                if (GOM == null)
                {
                    GOM = new GameObjectModel("Key", "Ключ " + vehicleModel.PlateNumber + " " + vehicleModel.KeyID.ToString(), 0, 0.01, false, "0", hunger: vehicleModel.KeyID);

                    GameObjects.ObjectsList.Add(GOM);
                }
                ((Inventory)client.GetData(Serv_RP.player.PlayerData.PLAYER_INVENTORY)).SetToInvetoty(GOM.Name, 1, "Автодиллер");
                Serv_RP.vehicles.VehicleData.CreateVehicle(vehicleModel, false);
                Serv_RP.database.DataBase.SaveServerState();
            }), 0L)));
        }

        public static void UpdateNameBuisTrans(Client client, string NewNameBuis)
        {
            Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                NAPI.Chat.SendChatMessageToAll(NewNameBuis);
                TransComp_buis transcomp_buis = transcomp_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname));
                transcomp_buis.NameB = NewNameBuis;

                transcomp_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname)).NameB = NewNameBuis;
                //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(transcomp_buis);
            }), 0L)));
        }

        public static TransComp_buis LoadBuisnessTrans(Client client)
        {

            TransComp_buis transcomp_buis = transcomp_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname));
            //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
            if (transcomp_buis != null)
            {

                return transcomp_buis;
            }

            return null;

        }

    }
}
