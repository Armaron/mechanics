using GTANetworkAPI;
using mech_server;
using Newtonsoft.Json;
using Serv_RP.economics;
using Serv_RP.player;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace mechanics
{
    public static class Mechanic
    {
        private static List<MechsMembersModel> mechs = new List<MechsMembersModel>();
        public static List<Mech_Buisness> mechs_buisness = new List<Mech_Buisness>();
        private static List<VehicleDetails> veh_det = new List<VehicleDetails>();


        private static SortedList<string, string> listMechs = new SortedList<string, string>();


        private static Vector3[] buyCustoms = new Vector3[11] {
            new Vector3(252.6783f, 2597.389f, 44.81868f),
            new Vector3(721.5389f, -1084.641f, 22.22401f),
            new Vector3(484.3903f, -1309.438f, 29.23346f),
            new Vector3(1153.392f, -785.2482f, 57.59873f),
            new Vector3(-1141.372f, -1991.64f, 13.16398f),
            new Vector3(112.9487f, 6619.373f, 31.83793f),
            new Vector3(2005.347f, 3791.077f, 32.18083f),
            new Vector3(548.2732f, -173.0667f, 54.48134f),
            new Vector3(-64.2979f, 77.18669f, 71.6162f),
            new Vector3(1178.3f, 2647.108f, 37.79081f),
            new Vector3(-202.6446f, -1308.662f, 31.29279f),
        };
        private static Vector3[] repairCustoms = new Vector3[11] {
            new Vector3(257.8216f, 2593.467f, 44.52076f),
            new Vector3(732.6118f, -1088.999f, 22.16901f),
            new Vector3(480.8116f, -1321.589f, 29.20394f),
            new Vector3(1149.656f, -775.9724f, 57.59866f),
            new Vector3(-1157.609f, -2021.418f, 13.13204f),
            new Vector3(110.8199f, 6627.323f, 31.78724f),
            new Vector3(2007.565f, 3798.764f, 32.18078f),
            new Vector3(538.398f, -176.5102f, 54.48743f),
            new Vector3(-66.90628f, 82.67137f, 71.54682f),
            new Vector3(1174.348f, 2640.481f, 37.76043f),
            new Vector3(-221.5106f, -1329.439f, 30.89038f),
        };
        private static Vector3[] tuningCustoms = new Vector3[5] {
            new Vector3(735.502f, -1079.062f, 22.16869f),
            new Vector3(479.0926f, -1315.811f, 29.20343f),
            new Vector3(-1159.185f, -2006.514f, 13.18026f),
            new Vector3(107.2648f, 6619.29f, 31.78725f),
            new Vector3(-207.1979f, -1324.141f, 30.89041f)
        };

        //public static List<string> nameList = new List<string>();
        public static void MechInit(Client client)
        {
            client.SetSharedData(Serv_RP.player.PlayerData.Nickname, client.GetData(Serv_RP.player.PlayerData.Nickname));
            // Mechanic_Player.LoadAllBuisnessOwner(client, null);

            //  List<Vehicle> vehicles = NAPI.Pools.GetAllVehicles();
            //  foreach (var item in vehicles)
            //  {
            //
            //      Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == item.NumberPlate);
            //      
            //      if (veh_data.Damag != "") { NAPI.ClientEvent.TriggerClientEvent(client, "SetDamag", veh_data.Damag); }
            //  }
            List<string> nameList = new List<string>();
            foreach (var item in mechs_buisness)
            {


                if (item.Owner != "username")
                {
                    nameList.Add(item.Name);
                }



                //NAPI.Util.ConsoleOutput(item.Name);
                if (item.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname))
                {

                    client.SetData("mechBuisness", item.Name);
                    client.SetSharedData("mechBuisness", item.Name);
                    client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
                    client.SetData(Serv_RP.player.PlayerData.Fraction, "mechs");
                    client.SetSharedData("typeCustoms", item.TypeCustoms);

                }
                if (item.WorkersList != null)
                {
                    foreach (var workers in item.WorkersList)
                    {
                       
                        if (workers.Key == client.GetData(Serv_RP.player.PlayerData.Nickname) && workers.Key != item.Owner)
                        {
                            client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
                            client.SetData(Serv_RP.player.PlayerData.Fraction, "mechs");
                            client.SetSharedData("typeCustoms", item.TypeCustoms);
                           
                        }
                    }
                }
            }
            NAPI.ClientEvent.TriggerClientEvent(client, "LoadBuisOwner", nameList);
        }

        public static void LoadAllBusiness(List<Mech_Buisness> mechs_buis)
        {

            if (mechs_buis.Count != 0)
            {


                //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness("buis3"));
                //  Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness("buis1"));
                mechs_buisness = mechs_buis;


            }
            else
            {

                NAPI.Util.ConsoleOutput("check");
                Mech_Buisness m1 = new Mech_Buisness("buis1");
                Mech_Buisness m2 = new Mech_Buisness("buis2");
                Mech_Buisness m3 = new Mech_Buisness("buis3");
                Mech_Buisness m4 = new Mech_Buisness("buis4");
                Mech_Buisness m5 = new Mech_Buisness("buis5");
                Mech_Buisness m6 = new Mech_Buisness("buis6");
                Mech_Buisness m7 = new Mech_Buisness("buis7");
                Mech_Buisness m8 = new Mech_Buisness("buis8");
                Mech_Buisness m9 = new Mech_Buisness("buis9");
                Mech_Buisness m10 = new Mech_Buisness("buis10");
                Mech_Buisness m11 = new Mech_Buisness("buis11");


                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m1);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m2);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m3);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m4);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m5);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m6);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m7);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m8);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m9);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m10);
                Serv_RP.database.DataBase.SaveMechBusinesOnBD(m11);


            }
            ColShape shape1;
            //ColShape shape2;
            for (int i = 0; i < buyCustoms.Length; i++)
            {
                NAPI.Marker.CreateMarker(29, buyCustoms[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(0, 204, 0), true, uint.MaxValue);
                NAPI.Marker.CreateMarker(0, repairCustoms[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(255, 176, 1), true, uint.MaxValue);
                NAPI.ColShape.CreateSphereColShape(repairCustoms[i], 10, 4294967295).SetSharedData("repapairCords", "1");
                Blip blip = NAPI.Blip.CreateBlip(buyCustoms[i], uint.MaxValue);
                shape1 = NAPI.ColShape.CreateSphereColShape(buyCustoms[i], 1, 4294967295);
                shape1.SetSharedData("nameBuis", "buis" + (i + 1));

                blip.ShortRange = true;
                blip.Name = "Автосервис";
                blip.Sprite = 402U;
            }
            for (int i = 0; i < tuningCustoms.Length; i++)
            {
                NAPI.Marker.CreateMarker(0, tuningCustoms[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(255, 176, 1), true, uint.MaxValue);
                NAPI.ColShape.CreateSphereColShape(tuningCustoms[i], 1, 4294967295).SetSharedData("customs", "1");

            }




            NAPI.Util.ConsoleOutput("Бизнесс мехов загружен");
        }

        public static void MechDiag(Client client)
        {
            NAPI.Chat.SendChatMessageToPlayer(client, "Diag activate");
        }

        public static void MechEnterVehicle(Client client, Vehicle veh)
        {

            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == veh.NumberPlate);

            if (veh_data.Damag != "") { NAPI.ClientEvent.TriggerClientEvent(client, "SetDamagGoCar", veh, veh_data.Damag); }
            NAPI.ClientEvent.TriggerClientEvent(client, "flagInCar");

        }

        public static void MechSpawn(Client client)
        {
            // List<Vehicle> vehicles = NAPI.Pools.GetAllVehicles();
            // foreach (var item in vehicles)
            // {
            //
            //
            //     Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == item.NumberPlate);
            //     NAPI.Util.ConsoleOutput(veh_data.PlateNumber);
            //     NAPI.ClientEvent.TriggerClientEvent(client, "spawnEngHealth", item, veh_data.EngHealth);
            //     NAPI.ClientEvent.TriggerClientEvent(client, "spawnBodyHealth", item, veh_data.BodyHealth);
            //     bool[] damag = NAPI.Util.FromJson<bool[]>(veh_data.Damag);
            //     if (veh_data.Damag != "") { NAPI.ClientEvent.TriggerClientEvent(client, "SetDamag", veh_data.Damag); }
            //    
            // }

        }

        public static void MechExitVehicle(Client client, Vehicle veh)
        {
            NAPI.Util.ConsoleOutput("Save vehicle records" + " " + veh.DisplayName + " " + veh.NumberPlate + " " + veh.Health);
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == veh.NumberPlate);
            VehicleDetails v = LoadServiceRecord(client, veh.NumberPlate, veh.DisplayName);
            if (v != null)
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", veh, v.CarScore, veh.NumberPlate, veh.DisplayName, veh_data.SellDate);

            }
            else
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", veh, 0, veh.NumberPlate, veh.DisplayName, veh_data.SellDate);
            }

            NAPI.Util.ConsoleOutput(veh_data.BodyHealth + " " + veh_data.EngHealth);
            NAPI.ClientEvent.TriggerClientEvent(client, "SaveDamag");
            NAPI.ClientEvent.TriggerClientEvent(client, "flagInCar");
        }

        public static void MechRepairSave(Client client, Vehicle veh)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == veh.NumberPlate);
            VehicleDetails v = LoadServiceRecord(client, veh.NumberPlate, veh.DisplayName);
            if (v != null)
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", veh, v.CarScore, veh.NumberPlate, veh.DisplayName, veh_data.SellDate);

            }
        }
        public static void MechServciceRecordInit(Vehicle veh)
        {

            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == veh.NumberPlate);


            if (veh_data.ServiceBook == null)
            {
                veh_data.SellDate = DateTime.Now.ToString();
                veh_data.CarScore = 0;
                veh_data.ServiceBook = "";
                veh_data.CurrentMods = "";
                veh_data.Damag = "";
                veh_data.BodyMax = 1000;
                veh_data.EngMax = 1000;
            }
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);

            if (veh_data.CurrentMods != "" && veh_data.CurrentMods != null && veh_data.CurrentMods != "1")
            {
                int[] mods = NAPI.Util.FromJson<int[]>(veh_data.CurrentMods);
                for (int i = 0; i < mods.Length; i++)
                {
                    if (i == 66)
                    {
                        veh.PrimaryColor = Convert.ToInt32(mods[i]);
                    }
                    else if (i == 67)
                    {
                        veh.SecondaryColor = Convert.ToInt32(mods[i]);
                    }
                    else
                    {
                        veh.SetMod(i, mods[i]);
                    }
                }
            }

            //int count = 0;
            NAPI.Util.ConsoleOutput(veh_data.Name + " " + veh_data.EngMax + " " + veh_data.BodyMax);
            AddServiceRecord(null, veh.NumberPlate, veh_data.SellDate, veh.DisplayName, veh_data.CarScore, "", veh_data.ServiceBook);
            //SaveVehicleHealth(null, veh.DisplayName, veh.NumberPlate, (int)veh_data.EngHealth);
            //SaveVehicleBodyHealth(null, veh.DisplayName, veh.NumberPlate, (int)veh_data.BodyHealth);


        }

        public static void SaveDamag(string displayName, string numberPlate, string v1, string v2)
        {
            System.Threading.Tasks.Task.Factory.StartNew((Action)(() => NAPI.Task.Run((Action)(() =>
            {
                Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == numberPlate);
                veh_data.Damag = v1 + ";" + v2;
                Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
            }), 0L)));

        }

        public static void AddToFraction(Client client, string nameBuisness, string typeCustoms)
        {
            string FullName = client.GetData(Serv_RP.player.PlayerData.Nickname);
            string DateHire = DateTime.Now.ToString();
            string NameBuisness = nameBuisness;
            //client.SetData(PlayerData.Fraction, "mechs "+ nameBuisness);
            client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
            string TypeCustoms = typeCustoms;
            client.SetSharedData("typeCustoms", TypeCustoms);
            client.SetData(Serv_RP.player.PlayerData.Fraction, "mechs");
            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Name == nameBuisness);
            //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
            if (mech_buis != null)
            {

                mech_buis.WorkersList.Add(FullName, DateHire);

                mechs_buisness.Find(pl => pl.Name == nameBuisness).WorkersList = mech_buis.WorkersList;

                Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(null, mech_buis.Name, mech_buis.NameB, mech_buis.Owner, mech_buis.Gain, mech_buis.TrucksCount, mech_buis.WorkersList, mech_buis.TypeCustoms));
            }
            NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Вас приняли в механики");
        }

        public static void AddServiceRecord(Client client, string carNumber, string sellDate, string carType, int carScore, string dateOf, string doneWorks)
        {
            bool find = false;
            //dateOf = 

            if (client == null)
            {
                veh_det.Add(new VehicleDetails(carNumber, sellDate, carType, carScore, dateOf, doneWorks));
            }
            else if (veh_det.Count != 0)
            {

                foreach (var item in veh_det)
                {
                    //if (Veh_Det.Find(pl => pl.carType == carType && pl.carNumber == carNumber) != null)
                    if (item.CarType == carType && item.CarNumber == carNumber && !find)
                    {

                        item.CarScore = carScore;


                        Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
                        veh_data.CarScore = carScore;
                        if (doneWorks != "")
                        {
                            item.DateOf = dateOf;
                            item.DoneWorks = veh_data.ServiceBook + dateOf + doneWorks + "@" + "Пробег: " + carScore + "@" + "Ремонт провёл: " + client.GetData(Serv_RP.player.PlayerData.Nickname);

                            veh_data.ServiceBook += dateOf + "@" + doneWorks + "@" + "Пробег: " + carScore + "@" + "Ремонт провёл: " + client.GetData(Serv_RP.player.PlayerData.Nickname);
                        }
                        Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
                        find = true;

                        //break;

                    }
                    else if (veh_det.Find(pl => pl.CarType == carType && pl.CarNumber == carNumber) == null)
                    {
                        veh_det.Add(new VehicleDetails(carNumber, sellDate, carType, carScore, dateOf, doneWorks));
                        //break;
                    }

                }
            }
            else
            { veh_det.Add(new VehicleDetails(carNumber, sellDate, carType, carScore, dateOf, doneWorks)); }

            //if (Veh_Det.Find(pl => pl.carType == carType && pl.carNumber == carNumber) != null)
        }
        public static void SaveMaxVehicleHealth(Client client, string carNumber, int maxCarHealth)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.EngMax = maxCarHealth;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
            //if (client != null)
            //{
            //    NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", null, veh_data.CarScore, veh_data.PlateNumber, veh_data.Name, veh_data.SellDate);
            //}
            Vehicle veh = NAPI.Pools.GetAllVehicles().Find(v => v.NumberPlate == carNumber);
            NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", veh, veh_data.CarScore, veh.NumberPlate, veh.DisplayName, veh_data.SellDate);


        }
        public static void SaveMaxVehicleBodyHealth(Client client, string carNumber, int maxCarBodyHealth)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.BodyMax = maxCarBodyHealth;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
            Vehicle veh = NAPI.Pools.GetAllVehicles().Find(v => v.NumberPlate == carNumber);
            NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", veh, veh_data.CarScore, veh.NumberPlate, veh.DisplayName, veh_data.SellDate);
        }
        public static void SaveVehicleHealth(Client client, string carType, string carNumber, int carHealth)
        {
            // NAPI.Chat.SendChatMessageToAll(carHealth.ToString());
            VehicleDetails vehdet = veh_det.Find(pl => /*pl.CarType == carType &&*/ pl.CarNumber == carNumber);
            vehdet.TotalHealth = carHealth;
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.EngHealth = carHealth;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
            //if (client != null)
            //{
            //    NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", null, veh_data.CarScore, veh_data.PlateNumber, vehdet.CarType, veh_data.SellDate);
            //}

        }
        public static void SaveMods(string carType, string carNumber, string mods)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.CurrentMods = mods;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
        }
        public static void SaveVehicleBodyHealth(Client client, string carType, string carNumber, int bodyHealth)
        {
            //  NAPI.Chat.SendChatMessageToAll(bodyHealth.ToString());
            VehicleDetails m = veh_det.Find(pl => /*pl.CarType == carType &&*/ pl.CarNumber == carNumber);
            m.BodyHealth = bodyHealth;
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.BodyHealth = bodyHealth;
            //veh_data.BodyMax = bodyHealth;

            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
            //if (client != null) 
            //{ 
            //NAPI.ClientEvent.TriggerClientEvent(client, "SaveVehicleRecord", null, veh_data.CarScore, veh_data.PlateNumber, m.CarType, veh_data.SellDate);
            //}

        }
        public static int LoadVehicleHealth(Client client, string carType, string carNumber)
        {
            VehicleDetails m = veh_det.Find(pl => pl.CarNumber == carNumber/* && pl.CarNumber == carNumber*/);
            return m.TotalHealth;


        }
        public static int LoadVehicleMaxHealth(string carNumber)
        {
            // VehicleDetails m = veh_det.Find(pl => pl.CarNumber == carNumber/* && pl.CarNumber == carNumber*/);
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            //NAPI.Chat.SendChatMessageToAll(m.TotalHealth.ToString());
            //return m.TotalHealth;
            return (int)veh_data.EngMax;

        }
        public static int LoadVehicleBodyHealth(Client client, string carType, string carNumber)
        {
            VehicleDetails m = veh_det.Find(pl => pl.CarNumber == carNumber/* && pl.CarNumber == carNumber*/);

            return m.BodyHealth;


        }
        public static int LoadVehicleMaxBodyHealth(string carNumber)
        {

            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);

            return (int)veh_data.BodyMax;


        }
        public static MechsMembersModel LoadMechs(Client client)
        {
            MechsMembersModel member = mechs.Find(pl => pl.FullName == client.GetSharedData(Serv_RP.player.PlayerData.Nickname));
            return member;
        }

        public static VehicleDetails LoadServiceRecord(Client client, string carType, string carNumber)
        {
            VehicleDetails m = veh_det.Find(pl => pl.CarNumber == carType);
            return m;
        }

        public static void RemoveFromFraction(Client client, string nameBuisness)
        {

            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Name == nameBuisness);

            if (mech_buis != null)
            {

                //foreach (var item in mechs.FindAll(pl => pl.NameBuisness == mech_buis.Name))
                //{
                mech_buis.WorkersList.Remove(client.GetData(Serv_RP.player.PlayerData.Nickname));
                mechs_buisness.Find(pl => pl.Name == nameBuisness).WorkersList.Remove(client.GetData(Serv_RP.player.PlayerData.Nickname));

                //}


                //listMechs.Remove(client.GetData(Serv_RP.player.PlayerData.Nickname);
                client.SetSharedData(Serv_RP.player.PlayerData.Fraction, null);
                client.SetData(Serv_RP.player.PlayerData.Fraction, null);
                client.SetSharedData("typeCustoms", null);
                Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(null, mech_buis.Name, mech_buis.NameB, mech_buis.Owner, mech_buis.Gain, mech_buis.TrucksCount, mech_buis.WorkersList, mech_buis.TypeCustoms));
                NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Вас исключили из механиков");
            }



        }

        public static void AddBuisness(Client client, string name, string StaticName, string typeCustoms, string nameB)
        {
            if (mechs_buisness.Find(buis => buis.Name == StaticName).Owner == "username")
            {
                mechs_buisness.Find(buis => buis.Name == StaticName).Name = StaticName;
                mechs_buisness.Find(buis => buis.Name == StaticName).NameB = nameB;
                mechs_buisness.Find(buis => buis.Name == StaticName).Owner = name;
                mechs_buisness.Find(buis => buis.Name == StaticName).TrucksCount = 0;
                mechs_buisness.Find(buis => buis.Name == StaticName).WorkersList = listMechs;
                mechs_buisness.Find(buis => buis.Name == StaticName).Gain = 1000;

                client.SetData("mechBuisness", StaticName);
                client.SetSharedData("mechBuisness", StaticName);
                client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
                client.SetData(Serv_RP.player.PlayerData.Fraction, "mechs");
                client.SetSharedData("typeCustoms", typeCustoms);

                Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(client, StaticName, nameB, name, 1000, 0, listMechs, typeCustoms));
                NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Вы приобрели бизнесс");
            }
            else
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "ClientNotify", "Этот бизнес уже принадлежит кому то");
            }
            List<string> nameList = new List<string>();
            foreach (var item in mechs_buisness)
            {
                if (item.Owner != "username")
                {
                    nameList.Add(item.Name);
                }
            }
            NAPI.ClientEvent.TriggerClientEventForAll("LoadBuisOwner", nameList);

        }
        public static void RemoveBuisness(string nameB)
        {
            mechs_buisness.Find(buis => buis.Name == nameB).Name = nameB;
            mechs_buisness.Find(buis => buis.Name == nameB).Owner = "username";
            mechs_buisness.Find(buis => buis.Name == nameB).TrucksCount = 0;
            mechs_buisness.Find(buis => buis.Name == nameB).WorkersList = null;
            mechs_buisness.Find(buis => buis.Name == nameB).Gain = 0;

            Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(nameB));
            List<string> nameList = new List<string>();
            foreach (var item in mechs_buisness)
            {
                if (item.Owner != "username")
                {
                    nameList.Add(item.Name);
                }
            }
            NAPI.ClientEvent.TriggerClientEventForAll("LoadBuisOwner", nameList);

        }

        public static void UpdateCarsCount(Client client, string nameVeh, int price)
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

            NAPI.Chat.SendChatMessageToAll(nameVeh);
            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Name == client.GetData("mechBuisness"));
            mech_buis.TrucksCount = mech_buis.TrucksCount + 1;
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

        }
        public static void UpdateNameBuis(Client client, string NewNameBuis)
        {

            NAPI.Chat.SendChatMessageToAll(NewNameBuis);
            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname));
            mech_buis.NameB = NewNameBuis;

            mechs_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname)).NameB = NewNameBuis;
            Serv_RP.database.DataBase.UpdateMechBusinesOnBD(mech_buis);

        }

        public static Mech_Buisness LoadBuisness(Client client)
        {

            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname));
            //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
            if (mech_buis != null)
            {

                return mech_buis;
            }

            return null;

        }
    }
}
