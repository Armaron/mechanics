using GTANetworkAPI;
using mech_server;
using Newtonsoft.Json;
using Serv_RP.economics;
using Serv_RP.player;
using System;
using System.Collections.Generic;

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

        public static void MechInit(Client client)
        {

           
            for (int i = 0; i < buyCustoms.Length; i++)
            {
                NAPI.Marker.CreateMarker(29, buyCustoms[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(0, 204, 0), true, uint.MaxValue);
                NAPI.Marker.CreateMarker(0, repairCustoms[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(255, 176, 1), true, uint.MaxValue);
                Blip blip = NAPI.Blip.CreateBlip(buyCustoms[i], uint.MaxValue);
                blip.ShortRange = true;
                blip.Name = "Автосервис";
                blip.Sprite = 402U;
            }
            for (int i = 0; i < tuningCustoms.Length; i++)
            {
                NAPI.Marker.CreateMarker(0, tuningCustoms[i], new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f, new Color(255, 176, 1), true, uint.MaxValue);

            }
            List<Vehicle> vehicles = NAPI.Pools.GetAllVehicles();
            foreach (var item in vehicles)
            {

                Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == item.NumberPlate);
                
                if (veh_data.Damag != "") { NAPI.ClientEvent.TriggerClientEvent(client, "SetDamag", veh_data.Damag); }
            }

            foreach (var item in mechs_buisness)
            {
                //NAPI.Util.ConsoleOutput(item.Name);
                if (item.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname))
                {
                   
                    client.SetData("mechBuisness", item.Name);
                    client.SetSharedData("mechBuisness", item.Name);
                    client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
                    client.SetSharedData("typeCustoms", item.TypeCustoms);
                }
                if (item.WorkersList != null)
                {
                    foreach (var workers in item.WorkersList)
                    {
                       // mechs.Add(new MechsMembersModel(null, item.Name, item.TypeCustoms));
                        if (workers.Key == client.GetData(Serv_RP.player.PlayerData.Nickname))
                        {
                            client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
                            client.SetSharedData("typeCustoms", item.TypeCustoms);
                           // mechs.Add(new MechsMembersModel(client, item.Name, item.TypeCustoms));
                        }
                    }
                }
            }
        }

        public static void LoadAllBusiness(List<Mech_Buisness> mechs_buis)
        {

            if (mechs_buis.Count != 0)
            {
                
               
              //Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness("buis11"));
               mechs_buisness = mechs_buis;
                
                //foreach (var item in mechs_buisness)
                //{
                //    NAPI.Util.ConsoleOutput(item.WorkersList.Count.ToString());
                //}
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
        }

        public static void MechSpawn(Client client)
        {
            List<Vehicle> vehicles = NAPI.Pools.GetAllVehicles();
            foreach (var item in vehicles)
            {


                Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == item.NumberPlate);
                NAPI.Util.ConsoleOutput(veh_data.PlateNumber);
                NAPI.ClientEvent.TriggerClientEvent(client, "spawnEngHealth", item, veh_data.EngHealth);
                NAPI.ClientEvent.TriggerClientEvent(client, "spawnBodyHealth", item, veh_data.BodyHealth);
                bool[] damag = NAPI.Util.FromJson<bool[]>(veh_data.Damag);
                if (veh_data.Damag != "") { NAPI.ClientEvent.TriggerClientEvent(client, "SetDamag", veh_data.Damag); }
               
            }
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
            AddServiceRecord(null, veh.NumberPlate, veh_data.SellDate, veh.DisplayName, veh_data.CarScore, "", veh_data.ServiceBook);
            SaveVehicleHealth(null, veh.DisplayName, veh.NumberPlate, (int)veh_data.EngHealth);
            SaveVehicleBodyHealth(null, veh.DisplayName, veh.NumberPlate, (int)veh_data.BodyHealth);
            // NAPI.Util.ConsoleOutput(veh_data.BodyMax.ToString());
            // NAPI.Util.ConsoleOutput(veh_data.EngMax.ToString());


        }

        public static void SaveDamag(string displayName, string numberPlate, string v1, string v2)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == numberPlate);
            veh_data.Damag = v1 + ";" + v2;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
        }

        public static void AddToFraction(Client client, string nameBuisness, string typeCustoms)
        {
            // mechs.Add(new MechsMembersModel(client, nameBuisness, typeCustoms));
            string FullName = client.GetData(Serv_RP.player.PlayerData.Nickname);
            string DateHire = DateTime.Now.ToString();
            string NameBuisness = nameBuisness;
            //client.SetData(PlayerData.Fraction, "mechs "+ nameBuisness);
            client.SetSharedData(Serv_RP.player.PlayerData.Fraction, "mechs");
            string TypeCustoms = typeCustoms;
            client.SetSharedData("typeCustoms", TypeCustoms);
            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Name == nameBuisness);
            //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
            if (mech_buis != null)
            {
                // NAPI.Chat.SendChatMessageToAll("ada");
                //SortedList<string, string> mechlist = new SortedList<string, string>();
                //foreach (var item in mechs.FindAll(pl => pl.NameBuisness == mech_buis.Name))
                //{
                // mech_buis.WorkersList.Add
                mech_buis.WorkersList.Add(FullName, DateHire);
                // mechs_buisness.Find(pl => pl.Name == nameBuisness).WorkersList.Add(FullName, DateHire);
                //}
                // mechs_buisness.Add(mech_buis);      
                mechs_buisness.Find(pl => pl.Name == nameBuisness).WorkersList = mech_buis.WorkersList;
                // NAPI.Chat.SendChatMessageToAll(mech_buis.Name + " " + mech_buis.Owner);
                Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(null, mech_buis.Name, mech_buis.Owner, mech_buis.Gain, mech_buis.TrucksCount, mech_buis.WorkersList, mech_buis.TypeCustoms));
            }

        }

        public static void AddServiceRecord(Client client, string carNumber, string sellDate, string carType, int carScore, string dateOf, string doneWorks)
        {
            bool find = false;
            //NAPI.Chat.SendChatMessageToAll(Veh_Det.Count.ToString());
            if (veh_det.Count != 0)
            {

                foreach (var item in veh_det)
                {
                    //if (Veh_Det.Find(pl => pl.carType == carType && pl.carNumber == carNumber) != null)
                    if (item.CarType == carType && item.CarNumber == carNumber && !find)
                    {
                        //NAPI.Chat.SendChatMessageToAll(Veh_Det.FindIndex());

                        //Veh_Det.Insert(Veh_Det.IndexOf(item), new VehicleDetails(carNumber, sellDate, carType, carScore, dateOf, doneWorks));
                        item.CarScore = carScore;
                        item.DateOf = dateOf;

                        Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
                        item.DoneWorks = veh_data.ServiceBook + dateOf + doneWorks;
                        veh_data.CarScore = carScore;
                        veh_data.ServiceBook += dateOf + "@" + doneWorks;
                        Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
                        find = true;

                        break;

                    }
                    else if (veh_det.Find(pl => pl.CarType == carType && pl.CarNumber == carNumber) == null)
                    {
                        veh_det.Add(new VehicleDetails(carNumber, sellDate, carType, carScore, dateOf, doneWorks));
                        break;
                    }

                }
            }
            else
            { veh_det.Add(new VehicleDetails(carNumber, sellDate, carType, carScore, dateOf, doneWorks)); }

            //if (Veh_Det.Find(pl => pl.carType == carType && pl.carNumber == carNumber) != null)
        }
        public static void SaveMaxVehicleHealth(string carNumber, int maxCarHealth)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.EngMax = maxCarHealth;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
        }
        public static void SaveMaxVehicleBodyHealth(string carNumber, int maxCarBodyHealth)
        {
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.BodyMax = maxCarBodyHealth;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);
        }
        public static void SaveVehicleHealth(Client client, string carType, string carNumber, int carHealth)
        {
           // NAPI.Chat.SendChatMessageToAll(carHealth.ToString());
            VehicleDetails vehdet = veh_det.Find(pl => /*pl.CarType == carType &&*/ pl.CarNumber == carNumber);
            vehdet.TotalHealth = carHealth;
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            veh_data.EngHealth = carHealth;
            Serv_RP.database.DataBase.SaveStatVehicleOnBD(veh_data);

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

        }
        public static int LoadVehicleHealth(Client client, string carType, string carNumber)
        {
            VehicleDetails m = veh_det.Find(pl => pl.CarNumber == carNumber/* && pl.CarNumber == carNumber*/);
           // Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            //NAPI.Chat.SendChatMessageToAll(m.TotalHealth.ToString());
            return m.TotalHealth;
           // return (int)veh_data.EngMax;

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
            //Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            //NAPI.Chat.SendChatMessageToAll(m.BodyHealth.ToString()+"body");
            return m.BodyHealth;
           

        }
        public static int LoadVehicleMaxBodyHealth(string carNumber)
        {
           // VehicleDetails m = veh_det.Find(pl => pl.CarNumber == carNumber/* && pl.CarNumber == carNumber*/);
            Serv_RP.vehicles.VehicleModel veh_data = Serv_RP.vehicles.VehicleList.vehicles.Find(vehicle => vehicle.PlateNumber == carNumber);
            //NAPI.Chat.SendChatMessageToAll(m.BodyHealth.ToString()+"body");
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
                  client.SetSharedData("typeCustoms", null);
                Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(null, mech_buis.Name, mech_buis.Owner, mech_buis.Gain, mech_buis.TrucksCount, mech_buis.WorkersList, mech_buis.TypeCustoms));
            }

           //MechsMembersModel mechMember = mechs.Find(pl => pl.FullName == client.GetData(Serv_RP.player.PlayerData.Nickname));
           //if (mechMember != null)
           //{
           //    mechs.Remove(mechMember);
           //    listMechs.Remove(mechMember.FullName);
           //    client.SetSharedData(Serv_RP.player.PlayerData.Fraction, null);
           //    client.SetSharedData("typeCustoms", null);
           //}
            
        }

        public static void AddBuisness(Client client, string name, string nameB, string typeCustoms)
        {
            mechs_buisness.Find(buis => buis.Name == nameB).Name = nameB;
            mechs_buisness.Find(buis => buis.Name == nameB).Owner = name;
            mechs_buisness.Find(buis => buis.Name == nameB).TrucksCount = 0;
            mechs_buisness.Find(buis => buis.Name == nameB).WorkersList = listMechs;
            mechs_buisness.Find(buis => buis.Name == nameB).Gain = 1000;

            Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(client, nameB, name, 1000, 0, listMechs, typeCustoms));
          
        }
        public static void RemoveBuisness(string nameB)
        {
            mechs_buisness.Find(buis => buis.Name == nameB).Name = nameB;
            mechs_buisness.Find(buis => buis.Name == nameB).Owner = "username";
            mechs_buisness.Find(buis => buis.Name == nameB).TrucksCount = 0;
            mechs_buisness.Find(buis => buis.Name == nameB).WorkersList = null;
            mechs_buisness.Find(buis => buis.Name == nameB).Gain = 0;

            Serv_RP.database.DataBase.UpdateMechBusinesOnBD(new Mech_Buisness(nameB));

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
            // Mech_Buisness mech_buis = Mechs_Buisness.Find(pl => pl.Name == client.GetData("mechBuisness"));
            //
            // //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
            // List<string> mechlist = new List<string>();
            // foreach (var item in Mechs.FindAll(pl => pl.nameBuisness == mech_buis.Name))
            // {
            //     //Client target = NAPI.Pools.GetAllPlayers().Find(pl => pl.GetData(PlayerData.Nickname) == item.FullName);
            //     client.SetData(PlayerData.Fraction, "mechs " + NewNameBuis);
            //     item.nameBuisness = NewNameBuis;
            //     mechlist.Add(item.FullName);
            // }
            //
            // Mechs_Buisness.Add(new Mech_Buisness(client, NewNameBuis, mech_buis.Owner, mech_buis.Gain, mech_buis.TrucksCount, mechlist, mech_buis.typeCustoms));
            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Name == client.GetData("mechBuisness"));

            foreach (var item in mechs.FindAll(pl => pl.NameBuisness == client.GetData("mechBuisness")))
            {
                item.NameBuisness = NewNameBuis;
            }


            foreach (var item in mechs_buisness)
            {
                if (item.Name == client.GetData("mechBuisness"))
                {
                    client.SetData("mechBuisness", NewNameBuis);
                    item.Name = NewNameBuis;
                    foreach (var itemMechs in item.WorkersList)
                    {
                        Client target = NAPI.Pools.GetAllPlayers().Find(pl => pl.GetData(Serv_RP.player.PlayerData.Nickname) == itemMechs);
                        target.SetData(Serv_RP.player.PlayerData.Fraction, "mechs " + NewNameBuis);

                    }

                }
            }

        }

        public static Mech_Buisness LoadBuisness(Client client)
        {

            Mech_Buisness mech_buis = mechs_buisness.Find(pl => pl.Owner == client.GetData(Serv_RP.player.PlayerData.Nickname));
            //Mechs = Mechs.FindAll(pl => pl.nameBuisness == m.Name);
            if (mech_buis != null)
            {
                // if (mechs != null) {
                // SortedList<string, string> mechlist = new SortedList<string, string>();
                // foreach (var item in mechs.FindAll(pl => pl.NameBuisness == mech_buis.Name))
                // {
                //     mechlist.Add(item.FullName, item.DateHire);
                // }

                //mech_buis.WorkersList = mechlist;
                // }
                foreach (var item in mech_buis.WorkersList)
                {
                    NAPI.Chat.SendChatMessageToAll(item.Key+"dsd");
                }
                return mech_buis;
            }
           
            return null;

        }
    }
}
