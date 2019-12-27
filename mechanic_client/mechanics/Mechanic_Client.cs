using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using static RAGE.Events;

namespace mechanic_client
{
    public class Mechanic_Client : Script
    {
        public static bool[] Wheel { get; set; } = new bool[10];
        public static bool[] Window { get; set; } = new bool[4];
        public static bool ActiveDiag = false;
        public static bool ActiveRepairKit = false;
        public static bool ActiveRepairKitCitizen = false;
        private static HtmlWindow buyBuisness;
        private static HtmlWindow serviceBook;
        public static List<Current_Mods> Mods = new List<Current_Mods>();
        public static int[] Currmod = new int[76];
        public static bool Open = false;

        public static bool Cursor = false;
        //private static bool show = false;
        private static bool buy = false;
        public static int countBodyRepair = 0;
        public static int countEngRepair = 0;
        public RAGE.Elements.Marker markMech1;
        public RAGE.Elements.Marker markMech2;
        public RAGE.Elements.Marker markMech3;

        public Mechanic_Client()
        {
            Events.OnPlayerChat += OnPlayerChat;

            Events.Add("Sync_Event_Tow_cl", SyncTow);
            Events.Add("Sync_Event_Detach_cl", SyncDetch);
            Events.Add("LoadVehicleRecord", LoadVehicleRecord);
            Events.Add("closeServiceBook", CloseServiceBook);
           // Events.Add("pushBuyCustoms", OpenBuyBuis);
            Events.Add("buyCustomsExit", CloseBuyBuis);
            Events.Add("buyCustomsButton", BuyCustomsButton);
            Events.Add("SaveVehicleRecord", SaveVehicleRecord);
            Events.Add("mechanicalExit", PressExit);
            Events.Add("putMechanical", PressArrow);
            Events.Add("removeMechanical", PressCancel);
            Events.Add("mechanicalLookCar", ResetCamera);
            Events.Add("mechanicalBuyButton", BuyButton);
            Events.Add("syncFixWindows", SyncFixWindows);
            Events.Add("syncFixWheels", SyncFixWheels);
            Events.Add("syncFixBody", SyncFixBody);
            Events.Add("spawnEngHealth", spawnEngHealth);
            Events.Add("spawnBodyHealth", spawnBodyHealth);
            Events.Add("syncFixEng", SyncFixEng);
            Events.Add("syncFixBodyShell", SyncFixBodyShell);
            Events.Add("ActiveDiagMech", ActiveDiagMech);
            Events.Add("HasRepaikit", HasRepaikit);
           // Events.Add("GetCountRepair", GetCountRepair);
            Events.Add("SetDamag", SetDamag);
            Events.Add("ClientNotify", ClientNotify);
            Events.Add("SaveDamag", SaveDamag);
            Events.Add("SetDamagGoCar", SetDamagGoCar);
            Events.Add("LoadBuisOwner", LoadBuisOwner);

            // Events.Add("init", init);
        }

        public static List<string> nameList = new List<string>();
        private void LoadBuisOwner(object[] args)
        {
            nameList = JsonConvert.DeserializeObject<List<string>>(args[0].ToString());
        }

        private void ClientNotify(object[] args)
        {
            Notify(args[0].ToString());
        }

        private void SaveDamag(object[] args)
        {
            int veh = GetVehicle(5.0f);
            for (int i = 0; i < 8; i++)
            {
                Wheel[i] = RAGE.Game.Vehicle.IsVehicleTyreBurst(veh, i, false);
            }
            Wheel[8] = RAGE.Game.Vehicle.IsVehicleTyreBurst(veh, 45, false);
            Wheel[9] = RAGE.Game.Vehicle.IsVehicleTyreBurst(veh, 47, false);
          
            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 0))
            {
               
                Window[0] = false;
            }
            else
            {
                
                Window[0] = true;
            }

            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 1))
            {
               
                Window[1] = false;
            }
            else
            {
                Window[1] = true;
            }
            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 2))
            {
                Window[2] = false;
            }
            else
            {
             
                Window[2] = true;
            }

            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 3))
            {
               
                Window[3] = false;
            }
            else
            {
               
                Window[3] = true;
            }
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            Events.CallRemote("SaveDamag", veh_obj, Wheel, Window);
        }

        private void SetDamag(object[] args)
        {
            //Vehicle v = (Vehicle)args[0];
            List<Vehicle> vehicles =  Entities.Vehicles.All;
            foreach (var item in vehicles)
            {

                
            string[] damag = args[0].ToString().Split(";");

            //NAPI.Util.ConsoleOutput(c);
            bool[] d1 = JsonConvert.DeserializeObject<bool[]>(damag[0]);
            bool[] d2 = JsonConvert.DeserializeObject<bool[]>(damag[1]);
            for (int i = 0; i < d1.Length; i++)
            {
                if (d1[i])
                {
                    RAGE.Game.Vehicle.SetVehicleTyreBurst(item.Handle, i, false, 100.0f);
                }
            }
            for (int i = 0; i < d2.Length; i++)
            {
                if (d2[i])
                {
                    RAGE.Game.Vehicle.SmashVehicleWindow(item.Handle, i);
                }
            }
            }
        }
        private void SetDamagGoCar(object[] args)
        {

            Vehicle v = (Vehicle)args[0];
            string[] damag = args[1].ToString().Split(";");

                //NAPI.Util.ConsoleOutput(c);
                bool[] d1 = JsonConvert.DeserializeObject<bool[]>(damag[0]);
                bool[] d2 = JsonConvert.DeserializeObject<bool[]>(damag[1]);
                for (int i = 0; i < d1.Length; i++)
                {
                    if (d1[i])
                    {
                    //Events.CallRemote("Sync_Wheel", v, i);
                    RAGE.Game.Vehicle.SetVehicleTyreBurst(v.Handle, i, false, 100.0f);
                    }
                }
                for (int i = 0; i < d2.Length; i++)
                {
                    if (d2[i])
                    {
                    //Events.CallRemote("Sync_Window", v, i);
                    RAGE.Game.Vehicle.SmashVehicleWindow(v.Handle, i);
                    }
                }
            
        }

        private void spawnEngHealth(object[] args)
        {
            Vehicle v = (Vehicle)args[0];
            RAGE.Game.Vehicle.SetVehicleEngineHealth(v.Handle, (float)args[1]);
        }
       //private void GetCountRepair(object[] args)
       //{
       //    countEngRepair = (int)args[0];
       //    countBodyRepair = (int)args[1];
       //    Chat.Output(countEngRepair + " " + countBodyRepair);
       //}
        private void spawnBodyHealth(object[] args)
        {
            Vehicle v = (Vehicle)args[0];
            v.SetBodyHealth((float)args[1]);
        }


        private void SyncFixBodyShell(object[] args)
        {
            //List<Vehicle> vehicles = Entities.Vehicles.All;
            //for (int i = 0; i < vehicles.Count; i++)
            //{
            //    if (vehicles != null)
            //    {
            //        if (vehicles[i].GetSharedData("targetVehicleFixBodyShell") != null)
            //        {
            //            if (vehicles[i].GetSharedData("targetVehicleFixBodyShell").ToString() == "fixBodyShell")
            //            {
            //                RAGE.Game.Vehicle.SetVehicleFixed(vehicles[i].Handle);
            //            }
            //        }
            //    }
            //}
            Vehicle veh = (Vehicle)args[0];
            veh.SetFixed();
        }

        private void SyncFixWindows(object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            int window_sync = Convert.ToInt32(args[1].ToString());
            veh.FixWindow(window_sync);
            // List<Vehicle> vehicles = Entities.Vehicles.All;
            // for (int i = 0; i < vehicles.Count; i++)
            // {
            //     if (vehicles != null)
            //     {
            //         if (vehicles[i].GetSharedData("targetVehicleFixWindows") != null)
            //         {
            //             if (vehicles[i].GetSharedData("targetVehicleFixWindows").ToString() == "fixWindows")
            //             {
            //
            //                 RAGE.Game.Vehicle.FixVehicleWindow(vehicles[i].Handle, window_sync);
            //             }
            //         }
            //     }
            // }
        }

        private void SyncFixBody(object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            int body_sync = Convert.ToInt32(args[1].ToString());
            Chat.Output(veh.ToString() + " " + body_sync.ToString());
            veh.SetBodyHealth(body_sync);
           //List<Vehicle> vehicles = Entities.Vehicles.All;
           //for (int i = 0; i < vehicles.Count; i++)
           //{
           //    if (vehicles != null)
           //    {
           //        if (vehicles[i].GetSharedData("targetVehicleFixBody") != null)
           //        {
           //            if (vehicles[i].GetSharedData("targetVehicleFixBody").ToString() == "fixBody")
           //            {
           //
           //                RAGE.Game.Vehicle.SetVehicleBodyHealth(vehicles[i].Handle, body_sync);
           //            }
           //        }
           //    }
           //}
        }

        private void SyncFixEng(object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            int eng_sync = Convert.ToInt32(args[1].ToString());
            Chat.Output(veh.ToString() + " " + eng_sync.ToString());
            veh.SetEngineHealth(eng_sync);
           //List<Vehicle> vehicles = Entities.Vehicles.All;
           //for (int i = 0; i < vehicles.Count; i++)
           //{
           //    if (vehicles != null)
           //    {
           //        if (vehicles[i].GetSharedData("targetVehicleFixEng") != null)
           //        {
           //            if (vehicles[i].GetSharedData("targetVehicleFixEng").ToString() == "fixEng")
           //            {
           //
           //                RAGE.Game.Vehicle.SetVehicleEngineHealth(vehicles[i].Handle, eng_sync);
           //            }
           //        }
           //    }
           //}
        }

        private void SyncFixWheels(object[] args)
        {
            Vehicle veh = (Vehicle)args[0];
            int wheel_sync = Convert.ToInt32(args[1].ToString());
            veh.SetTyreFixed(wheel_sync);
           //List<Vehicle> vehicles = Entities.Vehicles.All;
           //for (int i = 0; i < vehicles.Count; i++)
           //{
           //    if (vehicles != null)
           //    {
           //        if (vehicles[i].GetSharedData("targetVehicleFixWheel") != null)
           //        {
           //            if (vehicles[i].GetSharedData("targetVehicleFixWheel").ToString() == "fixWheel")
           //            {
           //
           //                RAGE.Game.Vehicle.SetVehicleTyreFixed(vehicles[i].Handle, wheel_sync);
           //            }
           //        }
           //    }
           //}
        }
        private void CloseServiceBook(object[] args)
        {
            serviceBook.Active = false;
            serviceBook.Destroy();
            serviceBook = null;
            RAGE.Ui.Cursor.Visible = false;
        }

        private void BuyCustomsButton(object[] args)
        {

            if (Player.LocalPlayer.GetSharedData("typeCustoms") == null)
            {
                
                if (args[0].ToString() != "")
                {
                   //if (args[0].ToString() == "cash")
                   //{
                   //    Notify("~c~Вы заплатили наличными ~g~" + args[2].ToString() + " $");
                   //    //buy = true;
                   //}
                   //else
                   //{
                   //    Notify("~c~Вы заплатили картой ~g~" + args[2].ToString() + " $");
                   //    //buy = true;
                   //}
                    AddBuis(args[1].ToString());
                }
                else
                {
                    Notify("Введите название бизнеса");
                }
            }
            else
            {
                Notify("Вы не можете купить бизнесс");
            }
        }



        private void LoadVehicleRecord(object[] args)
        {
            // Chat.Output(args[5].ToString());
            string date = args[4].ToString();
            date = date.Replace("@", "");
            VehicleDetailsCl vehdet = new VehicleDetailsCl(args[0].ToString(), args[1].ToString(), args[2].ToString(), (int)args[3], date, args[5].ToString());
            serviceBook = new HtmlWindow("package://auth/assets/service-book.html");
            string json = JsonConvert.SerializeObject(vehdet);
            serviceBook.ExecuteJs($"pushServiceBook('{json}')");
            RAGE.Ui.Cursor.Visible = true;
            //Events.CallRemote("Load_Car_Health", args[0].ToString(), args[0].ToString());

        }

        public static void SaveVehicleRecord(object[] args)
        {
          
            //Chat.Output(args[2].ToString() + " " + args[3].ToString());
            Events.CallRemote("Add_Service_Records", args[2].ToString(), args[4].ToString(), args[3].ToString(), Ticks_Mechs.CarScore + (int)args[1], Ticks_Mechs.Data, Ticks_Mechs.Text);
            Ticks_Mechs.Data = "";
            Ticks_Mechs.Text = "";
            Ticks_Mechs.CarScore = 0;
            Ticks_Mechs.MileageKM = 0;
            Ticks_Mechs.Mileage = 0;
        }



        public static void OpenBuyBuis(string nameB)
        {
            if(nameList.Count != 0) {
            bool accetpBuy = true;
            //Events.CallRemote("Load_All_Buisness");
            foreach (var item in nameList)
            {
                if(item == nameB)
                {
                    accetpBuy = false;
                    break;
                }
            }
            if (accetpBuy) { 
            buyBuisness = new HtmlWindow("package://auth/assets/buyCustoms.html");
            buyBuisness.ExecuteJs($"pushBuyCustoms('{1000}')");
            buyBuisness.Active = true;
            RAGE.Ui.Cursor.Visible = true;
            }
                else
                {
                    Notify("Это бизнес уже куплен");
                }
            }
        }
        public static void CloseBuyBuis(object[] args)
        {

            buyBuisness.Destroy();
            RAGE.Ui.Cursor.Visible = false;
        }

        //test
        private void OnPlayerChat(string text, Events.CancelEventArgs cancel)
        {
            if (text == ".b")
            {
                burstWheel();
                int veh = GetVehicle(5.0f);
                RAGE.Game.Vehicle.SetVehicleEngineHealth(veh, 400);
                RAGE.Game.Vehicle.SetVehicleBodyHealth(veh, 400);

            }
            if (text == ".g")
            {

                PlaceVehOnGround();

            }
            if (text == ".tow")
            {
                Tow();
            }
            if (text == ".weapon")
            {

                RAGE.Game.Weapon.GiveWeaponToPed(Player.LocalPlayer.Handle, RAGE.Game.Misc.GetHashKey("WEAPON_PISTOL"), 250, false, true);
            }
            if (text == ".bdoor")
            {
                RAGE.Game.Vehicle.SetVehicleDoorBroken(GetVehicle(5.0f), 0, false);
                RAGE.Game.Vehicle.SetVehicleDoorBroken(GetVehicle(5.0f), 1, false);
                RAGE.Game.Vehicle.SetVehicleDoorBroken(GetVehicle(5.0f), 2, false);
                RAGE.Game.Vehicle.SetVehicleDoorBroken(GetVehicle(5.0f), 3, false);
                RAGE.Game.Vehicle.SetVehicleDoorBroken(GetVehicle(5.0f), 4, false);
                RAGE.Game.Vehicle.SetVehicleDoorBroken(GetVehicle(5.0f), 5, false);
            }
            if (text == ".re")
            {
                if (!ActiveRepairKit)
                {
                    ActiveRepairKit = true;
                }
                else
                {
                    ActiveRepairKit = false;
                }
            }
            if (text == ".d")
            {
                if (!ActiveDiag)
                {
                    ActiveDiag = true;
                }
                else
                {
                    ActiveDiag = false;
                }

            }
        }

        public static void OpenServiceBook()
        {
            int veh = GetVehicle(5.0f);
           
            string plate = RAGE.Game.Vehicle.GetVehicleNumberPlateText(veh);
            plate = plate.Replace(" ", "");
            Events.CallRemote("ServiceBook", plate);

        }

        //получение машины в радиусе
        public static int GetVehicle(float radius)
        {
            Vector3 pos = Player.LocalPlayer.Position;
            if (Player.LocalPlayer.IsSittingInAnyVehicle())
            {
                return Player.LocalPlayer.GetVehicleIsIn(true);
            }
            else
            {
                int veh = RAGE.Game.Vehicle.GetClosestVehicle(pos.X + 0.0001f, pos.Y + 0.0001f, pos.Z + 0.0001f, radius + 0.0001f, 0, 8192 + 4096 + 4 + 2 + 1);
                if (!RAGE.Game.Entity.IsEntityAVehicle(veh))
                {
                    veh = RAGE.Game.Vehicle.GetClosestVehicle(pos.X + 0.0001f, pos.Y + 0.0001f, pos.Z + 0.0001f, radius + 0.0001f, 0, 4 + 2 + 1);
                   // return veh;
                }
                //else
                //{
                    return veh;
               // }


            }

        }

        static string[] windowParts = new string[4]
       {
        "window_lf",
        "window_rf",
        "window_lr",
        "window_rr",
       };

        public static void FixBodyShell()
        {
            int veh = GetVehicle(5.0f);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            RAGE.Game.Vehicle.SetVehicleFixed(veh);
            Events.CallRemote("Sync_BodyShell", veh_obj);

        }

        static string[] wheelParts = new string[4]
        {
        "wheel_lf",
        "wheel_rf",
        "wheel_lr",
        "wheel_rr",
        };

        public static void ActiveDiagMech(object[] args)
        {
            if (!ActiveDiag)
            {
                ActiveDiag = true;
                ActiveRepairKit = true;
            }
            else
            {
                ActiveDiag = false;
                ActiveRepairKit = false;
            }
        }

        public static void HasRepaikit(object[] args)
        {
            if (!ActiveRepairKit)
            {
                ActiveRepairKitCitizen = true;
                Notify("Рем. Комплект активирован");
            }
            else
            {
                ActiveRepairKitCitizen = false;
                Notify("Рем. Комплект деактивирвоан");
            }
        }

        //получение статуса 10 колёс
        public static bool[] GetStatusWheel()
        {
            int veh = GetVehicle(5.0f);
            for (int i = 0; i < 8; i++)
            {
                Wheel[i] = RAGE.Game.Vehicle.IsVehicleTyreBurst(veh, i, false);
            }
            Wheel[8] = RAGE.Game.Vehicle.IsVehicleTyreBurst(veh, 45, false);
            Wheel[9] = RAGE.Game.Vehicle.IsVehicleTyreBurst(veh, 47, false);

            if (Wheel[0])
            {
                VehicleBone("wheel_lf", "~y~Колесо. ~r~Не исправно. ~r~[Alt+E] ~s~~y~Починить~y~");
            }
            else
            {
                VehicleBone("wheel_lf", "~y~Колесо~y~. ~g~Исправно");
            }
            if (Wheel[1])
            {
                VehicleBone("wheel_rf", "~y~Колесо. ~r~Не исправно. ~r~[Alt+E] ~s~~y~Починить~y~");
            }
            else
            {
                VehicleBone("wheel_rf", "~y~Колесо~y~. ~g~Исправно");
            }
            if (Wheel[4])
            {
                VehicleBone("wheel_lr", "~y~Колесо. ~r~Не исправно. ~r~[Alt+E] ~s~~y~Починить~y~");
            }
            else
            {
                VehicleBone("wheel_lr", "~y~Колесо~y~. ~g~Исправно");
            }
            if (Wheel[5])
            {
                VehicleBone("wheel_rr", "~y~Колесо. ~r~Не исправно. ~r~[Alt+E] ~s~~y~Починить~y~");
            }
            else
            {
                VehicleBone("wheel_rr", "~y~Колесо~y~. ~g~Исправно");
            }
            if (Wheel[8])
            {
                VehicleBone("wheel_lm", "~y~Колесо. ~r~Не исправно. ~r~[Alt+E] ~s~~y~Починить~y~");
            }
            else
            {
                VehicleBone("wheel_lm", "~y~Колесо~y~. ~g~Исправно");
            }
            if (Wheel[9])
            {
                VehicleBone("wheel_rm", "~y~Колесо. ~r~Не исправно. ~r~[Alt+E] ~s~~y~Починить~y~");
            }
            else
            {
                VehicleBone("wheel_rm", "~y~Колесо~y~. ~g~Исправно");
            }


            return Wheel;
        }
        public static float GetEngineHealth()
        {
            int veh = GetVehicle(5.0f);
            float eHeatlh = RAGE.Game.Vehicle.GetVehicleEngineHealth(veh);
            //if (eHeatlh < 1000)
            //{
            //    VehicleBone("engine", "~y~Двигатель. ~r~1000/" + eHeatlh.ToString() + ". ~r~[Alt+Z] ~s~~y~Починить~y~");
            //}
            //else
            //{
            //    VehicleBone("engine", "~y~Двигатель~y~. ~g~Исправно");
            //}

            if (eHeatlh >= 800)
            {
                VehicleBone("engine", "~y~Двигатель~y~. ~g~Исправно" + eHeatlh.ToString());
            }
            else if (eHeatlh < 800 && eHeatlh > 400)
            {
                VehicleBone("engine", "~y~Двигатель~y~. ~y~Повереждён ~r~[Alt+Z] ~s~ ~y~Починить~y~" + eHeatlh.ToString());
            }
            else if (eHeatlh <= 400)
            {
                VehicleBone("engine", "~y~Двигатель~y~. ~r~Неисправен ~r~[Alt+Z] ~s~ ~y~Починить~y~" + eHeatlh.ToString());
            }
            return eHeatlh;
        }




        public static void FixWheel()
        {
            int veh = GetVehicle(5.0f);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            Vector3 pos = Player.LocalPlayer.Position;
            for (int i = 0; i < wheelParts.Length; i++)
            {
                int index = RAGE.Game.Entity.GetEntityBoneIndexByName(GetVehicle(5.0f), wheelParts[i]);
                Vector3 coords = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index);

                if (RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords.X, coords.Y, coords.Z) <= 1.5f)
                {

                    //  Chat.Output(bone);
                    if (wheelParts[i] == "wheel_lf")
                    {
                        RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 0);
                        Events.CallRemote("Sync_Wheel", veh_obj, 0);
                        Wheel[0] = false;
                    }

                    if (wheelParts[i] == "wheel_rf")
                    {
                        RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 1);
                        Events.CallRemote("Sync_Wheel",  veh_obj, 1);
                        Wheel[1] = false;

                    }
                    if (wheelParts[i] == "wheel_lr")
                    {
                        RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 4);
                        Events.CallRemote("Sync_Wheel",  veh_obj, 4);
                        Wheel[4] = false;
                    }

                    if (wheelParts[i] == "wheel_rr")
                    {
                        RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 5);
                        Events.CallRemote("Sync_Wheel", veh_obj, 5);
                        Wheel[5] = false;
                    }

                    //  switch (wheelParts[i])
                    //  {
                    //      case "wheel_lf":
                    //          RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 0);
                    //          break;
                    //      case "wheel_rf":
                    //          RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 1);
                    //          break;
                    //      case "wheel_lr":
                    //          RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 4);
                    //          break;
                    //      case "wheel_rr":
                    //          RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 5);
                    //          break;
                    //      case "wheel_lm":
                    //          RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 8);
                    //          break;
                    //      case "wheel_rm":
                    //          RAGE.Game.Vehicle.SetVehicleTyreFixed(veh, 9);
                    //          break;
                    //  }
                }

                Events.CallRemote("SaveDamag", veh_obj, Wheel, Window);
            }
        }

        //получение статуса 4 окон
        public static bool[] GetWindow()
        {
            int veh = GetVehicle(5.0f);
            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 0))
            {
                VehicleBone("window_lf", "~y~Окно~y~. ~g~Исправно");
                Window[0] = false;
            }
            else
            {
                VehicleBone("window_lf", "~y~Окно. ~r~Не исправно. ~r~[Alt+W] ~s~~y~Починить~y~");
                Window[0] = true;
            }

            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 1))
            {
                VehicleBone("window_rf", "~y~Окно~y~. ~g~Исправно");
                Window[1] = false;
            }
            else
            {
                VehicleBone("window_rf", "~y~Окно. ~r~Не исправно. ~r~[Alt+W]~s~~y~Починить~y~");
                Window[1] = true;
            }
            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 2))
            {
                VehicleBone("window_lr", "~y~Окно~y~. ~g~Исправно");
                Window[2] = false;
            }
            else
            {
                VehicleBone("window_lr", "~y~Окно. ~r~Не исправно. ~r~[Alt+W]~s~~y~Починить~y~");
                Window[2] = true;
            }

            if (RAGE.Game.Vehicle.IsVehicleWindowIntact(veh, 3))
            {
                VehicleBone("window_rr", "~y~Окно~y~. ~g~Исправно");
                Window[3] = false;
            }
            else
            {
                VehicleBone("window_rr", "~y~Окно. ~r~Не исправно. ~r~[Alt+W]~s~~y~Починить~y~");
                Window[3] = true;
            }
            return Window;
        }

        public static void FixWindow()
        {
            int veh = GetVehicle(5.0f);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            Vector3 pos = Player.LocalPlayer.Position;
            for (int i = 0; i < windowParts.Length; i++)
            {
                int index = RAGE.Game.Entity.GetEntityBoneIndexByName(GetVehicle(5.0f), windowParts[i]);
                Vector3 coords = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index);
                if (RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords.X, coords.Y, coords.Z) <= 2.0f)
                {
                    if (windowParts[i] == "window_lf")
                    {
                        RAGE.Game.Vehicle.FixVehicleWindow(veh, 0);
                        Events.CallRemote("Sync_Window", veh_obj, 0);
                        Window[0] = false;
                    }
                    if (windowParts[i] == "window_rf")
                    {
                        RAGE.Game.Vehicle.FixVehicleWindow(veh, 1);
                        Events.CallRemote("Sync_Window", veh_obj, 1);
                        Window[1] = false;
                    }
                    if (windowParts[i] == "window_lr")
                    {
                        RAGE.Game.Vehicle.FixVehicleWindow(veh, 2);
                        Events.CallRemote("Sync_Window",  veh_obj, 2);
                        Window[2] = false;
                    }
                    if (windowParts[i] == "window_rr")
                    {
                        RAGE.Game.Vehicle.FixVehicleWindow(veh, 3);
                        Events.CallRemote("Sync_Window", veh_obj, 3);
                        Window[3] = false;
                    }

                    //Events.CallRemote("Sync_Window", Window, Entities.Vehicles.GetAtHandle(veh));
                    //  switch (windowParts[i])
                    //  {
                    //      case "window_lf":
                    //          RAGE.Game.Vehicle.FixVehicleWindow(veh, 0);
                    //          break;
                    //      case "window_rf":
                    //          RAGE.Game.Vehicle.FixVehicleWindow(veh, 1);
                    //          break;
                    //      case "window_lr":
                    //          RAGE.Game.Vehicle.FixVehicleWindow(veh, 2);
                    //          break;
                    //      case "window_rr":
                    //          RAGE.Game.Vehicle.FixVehicleWindow(veh, 3);
                    //          break;
                    //  }
                }


            }
            Events.CallRemote("SaveDamag", veh_obj, Wheel, Window);
        }

        public static bool[] GetDoors()
        {
            bool[] Door = new bool[0];
            int veh = GetVehicle(5.0f);
            uint ModelVeh = RAGE.Game.Entity.GetEntityModel(veh);
            if (RAGE.Game.Vehicle.IsThisModelACar(ModelVeh))
            {
                Door = new bool[RAGE.Game.Vehicle.GetNumberOfVehicleDoors(veh)];
                for (int i = 0; i < RAGE.Game.Vehicle.GetNumberOfVehicleDoors(veh); i++)
                {
                    Door[i] = RAGE.Game.Vehicle.IsVehicleDoorDamaged(veh, i);

                    if (i == 1)
                    {
                        VehicleBone("door_dside_f", "~y~Дверь~y~. ~g~Исправно");
                    }
                    if (i == 2)
                    {
                        VehicleBone("door_pside_f", "~y~Дверь~y~. ~g~Исправно");
                    }
                    if (i == 3)
                    {
                        VehicleBone("door_dside_r", "~y~Дверь~y~. ~g~Исправно");
                    }
                    if (i == 4)
                    {
                        VehicleBone("door_pside_r", "~y~Дверь~y~. ~g~Исправно");
                    }
                }
                return Door;
            }
            return Door;
        }

        public static float GetBodyHealth()
        {
            int veh = GetVehicle(5.0f);
            float bodyHealth = RAGE.Game.Vehicle.GetVehicleBodyHealth(veh);
            // if (bodyHealth < 1000)
            // {
            //     VehicleBone("bodyshell", "~y~Корпус. ~r~1000/" + bodyHealth.ToString() + ". ~r~[Alt+B] ~s~~y~Починить~y~");
            // }
            // else
            // {
            //     VehicleBone("bodyshell", "~y~Корпус~y~. ~g~Исправно");
            // }
            if (bodyHealth >= 800)
            {
                VehicleBone("bodyshell", "~y~Корпус~y~. ~g~Исправно"+ bodyHealth.ToString());
            }
            else if (bodyHealth < 800 && bodyHealth > 400)
            {
                VehicleBone("bodyshell", "~y~Корпус~y~. ~y~Повереждён ~r~[Alt+Y] ~s~ ~y~Починить~y~" + bodyHealth.ToString());
            }
            else if (bodyHealth <= 400)
            {
                VehicleBone("bodyshell", "~y~Корпус~y~. ~r~Неисправен ~r~[Alt+Y] ~s~ ~y~Починить~y~" + bodyHealth.ToString());
            }
            return bodyHealth;
        }
        public static void FixBody(int health, int curHealth, int TotalBodyHealth, int totalMaxBodyHealth)
        {
            int veh = GetVehicle(5.0f);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            float bodyHealth = RAGE.Game.Vehicle.GetVehicleBodyHealth(veh);
            Vector3 pos = Player.LocalPlayer.Position;
            int index = RAGE.Game.Entity.GetEntityBoneIndexByName(GetVehicle(5.0f), "bodyshell");
            Vector3 coords = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index);
            Chat.Output(curHealth.ToString());
            if (RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords.X, coords.Y, coords.Z) <= 2.0f)
            {
                if (health == 500 && curHealth < 500)
                {
                    RAGE.Game.Vehicle.SetVehicleBodyHealth(veh, health);
                    Events.CallRemote("Sync_Body", veh_obj, health);
                    Events.CallRemote("Save_Car_Body_Health", veh_obj, health);
                    Notify("Ремонтный набор применён");
                }
                if (health == 1000 && TotalBodyHealth != -1000 && curHealth <= 500)
                {

                    
                    int health_veh = totalMaxBodyHealth;
                    int percent = (health_veh / 100) * 5;

                    health_veh = health_veh - percent;
                    Chat.Output(health_veh.ToString());
                    RAGE.Game.Vehicle.SetVehicleBodyHealth(veh, health_veh);
                    Events.CallRemote("Sync_Body", veh_obj, health_veh);
                    Events.CallRemote("Save_Car_Body_Health", veh_obj, health_veh);
                    Events.CallRemote("Save_Car_Max_Body_Health", veh_obj, health_veh);
                    Notify("Капитальный ремонт корпуса проведён");
                }
                else if (TotalBodyHealth != -1000 && curHealth > 500)
                {
                    //health = health - (countEngRepair * 5);
                    RAGE.Game.Vehicle.SetVehicleBodyHealth(veh, totalMaxBodyHealth);
                    Events.CallRemote("Sync_Body", veh_obj, totalMaxBodyHealth);
                    Events.CallRemote("Save_Car_Body_Health", veh_obj, totalMaxBodyHealth);
                    Notify("Ремонт корпуса проведён");
                }

            }
        }



        public static void FixEngine(int health, int curHealth, int TotalHealth, int totalMaxHelth)
        {
            int veh = GetVehicle(5.0f);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);

            Vector3 pos = Player.LocalPlayer.Position;
            int index = RAGE.Game.Entity.GetEntityBoneIndexByName(GetVehicle(5.0f), "engine");
            Vector3 coords = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index);
            Chat.Output(curHealth.ToString()+"тек");
            if (RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords.X, coords.Y, coords.Z) <= 2.0f)
            {
                if (health == 500 && curHealth < 500)
                {
                    uint vehicleModel = RAGE.Game.Entity.GetEntityModel(veh);
                    float MaxVehicle = RAGE.Game.Vehicle.GetVehicleModelMaxSpeed(vehicleModel);
                    float maxSpeed = (0.001f * 500 + 0.001f * 500) * MaxVehicle * 0.15f;
                    RAGE.Game.Entity.SetEntityMaxSpeed(veh, maxSpeed);
                    RAGE.Game.Vehicle.SetVehicleEngineHealth(veh, health);
                    Events.CallRemote("Sync_Eng", veh_obj, health);
                    Events.CallRemote("Save_Car_Health", veh_obj, health);
                    Notify("Ремонтный набор применён");

                }
                if (health == 1000 && TotalHealth != -1000 && curHealth <= 500)
                {

                     Chat.Output( totalMaxHelth.ToString()+"кап");
                     int health_veh = totalMaxHelth;
                     int percent = (health_veh / 100) * 5;

                      health_veh = health_veh - percent;
                      Chat.Output(health_veh.ToString());
                      RAGE.Game.Vehicle.SetVehicleEngineHealth(veh, health_veh);
                      Events.CallRemote("Sync_Eng", veh_obj, health_veh);
                      Events.CallRemote("Save_Car_Health", veh_obj, health_veh);
                      Events.CallRemote("Save_Car_Max_Health", veh_obj, health_veh);
                    
                    Notify("Капитальный ремонт двигателя проведён");
                }
                else if (TotalHealth != -1000 && curHealth > 500)
                {
                    Chat.Output(totalMaxHelth.ToString() + "об");
                    RAGE.Game.Vehicle.SetVehicleEngineHealth(veh, totalMaxHelth);
                    Events.CallRemote("Sync_Eng", veh_obj, totalMaxHelth);
                    Events.CallRemote("Save_Car_Health", veh_obj, totalMaxHelth);
                    Notify("Ремонт двигателя проведён");
                }
               

            }
        }

        public static void Notif(string text)
        {
            RAGE.Game.Ui.BeginTextCommandDisplayHelp("STRING");
            RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);
            RAGE.Game.Ui.EndTextCommandDisplayHelp(0, false, false, -1);
        }
        public static void Notify(string text)
        {
            RAGE.Game.Ui.SetNotificationTextEntry("STRING");
            RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);
            RAGE.Game.Ui.DrawNotification(true, false);
        }

        public static void PlaceVehOnGround()
        {
            int veh = GetVehicle(5.0f);
            if (RAGE.Game.Entity.IsEntityAVehicle(veh))
            {
                RAGE.Game.Vehicle.SetVehicleOnGroundProperly(veh, 1);
            }
        }

        public static void VehicleBone(string bone, string text)
        {
            int veh = GetVehicle(5.0f);
            int index = RAGE.Game.Entity.GetEntityBoneIndexByName(GetVehicle(5.0f), bone);
            Vector3 coords = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index);
            if (bone == "bodyshell")
            {
                Text3d(coords.X, coords.Y, coords.Z + 1, text);
            }
            else
            {
                Text3d(coords.X, coords.Y, coords.Z, text);
            }

        }

        public static void AddBuis(string nameBuis)
        {
            Events.CallRemote("Add_New_Buisness", "", Ticks_Mechs.NameCustoms, Ticks_Mechs.TypeCustoms);
        }
        //test
        public static void burstWheel()
        {
            RAGE.Game.Vehicle.SetVehicleTyreBurst(GetVehicle(5.0f), 0, false, 100.0f);
            RAGE.Game.Vehicle.SetVehicleTyreBurst(GetVehicle(5.0f), 1, false, 100.0f);
            RAGE.Game.Vehicle.SetVehicleTyreBurst(GetVehicle(5.0f), 4, false, 100.0f);
            RAGE.Game.Vehicle.SetVehicleTyreBurst(GetVehicle(5.0f), 5, false, 100.0f);
            RAGE.Game.Vehicle.SmashVehicleWindow(GetVehicle(5.0f), 0);
            RAGE.Game.Vehicle.SmashVehicleWindow(GetVehicle(5.0f), 1);
        }

        static int currentlyTowedVehicle = -1;

        private void SyncTow(object[] args)
        {
            int veh_obj = 0;
            int veh2 = 0;
            List<Vehicle> vehicles = Entities.Vehicles.All;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles != null)
                {
                    if (vehicles[i].GetSharedData("targetVehicle") != null)
                    {
                        veh_obj = vehicles[i].Handle;
                    }
                    if (vehicles[i].GetSharedData("vehicle") != null)
                    {

                        veh2 = vehicles[i].Handle;
                    }
                    RAGE.Game.Entity.AttachEntityToEntity(veh_obj, veh2, 20, -0.5f, -5.0f, 1.0f, 0.0f, 0.0f, 0.0f, false, false, false, false, 20, true);
                }
            }
        }


        private void SyncDetch(object[] args)
        {
            int veh_obj = 0;
            int veh2 = 0;
            List<Vehicle> vehicles = Entities.Vehicles.All;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles != null)
                {
                    if (vehicles[i].GetSharedData("currentlyTowedVehicle") != null)
                    {
                        veh_obj = vehicles[i].Handle;
                        //Chat.Output(veh_obj.ToString());
                    }
                    if (vehicles[i].GetSharedData("vehicle") != null)
                    {

                        veh2 = vehicles[i].Handle;
                        // Chat.Output(veh2.ToString());
                    }
                    //if(RAGE.Game.Entity.IsEntityAttached(veh_obj)){ 
                    RAGE.Game.Entity.AttachEntityToEntity(veh_obj, veh2, 20, -0.5f, -12.0f, 0.5f, 0.0f, 0.0f, 0.0f, false, false, false, false, 20, true);
                    RAGE.Game.Entity.DetachEntity(veh_obj, false, false);
                    RAGE.Game.Vehicle.SetVehicleOnGroundProperly(veh_obj, 0);
                    RAGE.Game.Entity.SetEntityInvincible(veh_obj, false);
                    RAGE.Game.Entity.SetEntityCollision(veh_obj, true, true);
                    //}
                }
            }
        }
        public static void Tow()
        {
            int ped = Player.LocalPlayer.Handle;
            int vehicle = RAGE.Game.Ped.GetVehiclePedIsIn(ped, true);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == vehicle);
          

            if (RAGE.Game.Vehicle.IsVehicleModel(vehicle, RAGE.Game.Misc.GetHashKey("flatbed")))
            {
                int targetVehicle = GetVehicle(11.0f);
                
                Vector3 towPos = RAGE.Game.Entity.GetEntityCoords(vehicle, false);
                Vector3 targetPos = RAGE.Game.Entity.GetEntityCoords(targetVehicle, false);
                float dist = RAGE.Game.Misc.GetDistanceBetweenCoords(towPos.X, towPos.Y, towPos.Z, targetPos.X, targetPos.Y, targetPos.Z, true);

                if (dist < 11.0f)
                {
                    if (currentlyTowedVehicle == -1)
                    {
                        if (targetVehicle != -1)
                        {
                            if (!RAGE.Game.Ped.IsPedInAnyVehicle(ped, true))
                            {
                                uint model = RAGE.Game.Entity.GetEntityModel(targetVehicle);
                                int clas = RAGE.Game.Vehicle.GetVehicleClass(targetVehicle);
                                if (vehicle != targetVehicle && clas != 10 && clas != 11 && clas != 15 && clas != 16 && clas != 17 && clas != 19 && clas != 20 && clas != 21 && clas != 14)
                                {
                                    Vehicle veh2 = vehicles.Find(pl => pl.Handle == targetVehicle);
                                    //RAGE.Game.Entity.AttachEntityToEntity(targetVehicle, vehicle, 20, -0.5f, -5.0f, 1.0f, 0.0f, 0.0f, 0.0f, false, false, false, false, 20, true);
                                    Events.CallRemote("Sync_Event_Tow", veh2, veh_obj);

                                    if (RAGE.Game.Entity.IsEntityAttached(targetVehicle))
                                    {
                                        currentlyTowedVehicle = targetVehicle;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                        // RAGE.Game.Entity.AttachEntityToEntity(currentlyTowedVehicle, vehicle, 20, -0.5f, -12.0f, 0.5f, 0.0f, 0.0f, 0.0f, false, false, false, false, 20, true);
                        // RAGE.Game.Entity.DetachEntity(currentlyTowedVehicle, false, false);
                        // RAGE.Game.Vehicle.SetVehicleOnGroundProperly(currentlyTowedVehicle, 0);
                        // RAGE.Game.Entity.SetEntityInvincible(currentlyTowedVehicle, false);
                        // RAGE.Game.Entity.SetEntityCollision(currentlyTowedVehicle, true, true);
                        Vehicle veh2 = vehicles.Find(pl => pl.Handle == targetVehicle);
                        Events.CallRemote("Sync_Event_Detach", veh2, veh_obj);
                        if (!RAGE.Game.Entity.IsEntityAttached(currentlyTowedVehicle))
                        {
                            currentlyTowedVehicle = -1;
                        }
                    }
                }

            }
        }

        public static void Text3d(float x, float y, float z, string text)
        {
            float x1 = 0;
            float y1 = 0;
            bool onscreen = RAGE.Game.Graphics.GetScreenCoordFromWorldCoord(x, y, z, ref x1, ref y1);
            Vector3 posCam = RAGE.Game.Cam.GetGameplayCamCoords();
            float dist = RAGE.Game.Misc.GetDistanceBetweenCoords(posCam.X, posCam.Y, posCam.Z, x, y, z, true);
            float scale = (1 / dist) * 2.0f;
            float fov = (1 / RAGE.Game.Cam.GetGameplayCamFov()) * 70;
            scale = scale * fov;
            float length = text.Length * 0.002645f;

            if (onscreen != false)
            {
                RAGE.Game.Ui.SetTextScale(0.0f * scale, 0.5f * scale);
                RAGE.Game.Ui.SetTextFont(4);
                RAGE.Game.Ui.SetTextProportional(true);
                RAGE.Game.Ui.SetTextColour(209, 211, 212, 160);
                RAGE.Game.Ui.SetTextDropshadow(0, 0, 0, 0, 120);
                RAGE.Game.Ui.SetTextDropShadow();
                RAGE.Game.Ui.SetTextOutline();
                RAGE.Game.Ui.BeginTextCommandDisplayText("STRING");
                RAGE.Game.Ui.SetTextCentre(true);
                RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);
                RAGE.Game.Ui.EndTextCommandDisplayText(x1, y1, 1);
                // RAGE.Game.Graphics.DrawRect(x1, y1 + 0.017f * scale, length * scale, 0.34f * scale, 30, 29, 45, 100, 0);
            }
        }

        // прочкача

        private void BuyButton(object[] args)
        {
            int veh = Player.LocalPlayer.GetVehicleIsIn(true);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            RAGE.Game.Vehicle.SetVehicleUndriveable(veh, false);
            if (args[0].ToString() == "cash")
            {
                Notify("~c~Вы заплатили наличными ~g~" + args[1].ToString() + " $");
                buy = true;
            }
            else
            {
                Notify("~c~Вы заплатили картой ~g~" + args[1].ToString() + " $");
                buy = true;
            }
            int color1 = 0;
            int color2 = 0;

            for (int i = 0; i < 76; i++)
            {
                if (i == 66)
                {

                    RAGE.Game.Vehicle.GetVehicleColours(Player.LocalPlayer.GetVehicleIsIn(true), ref color1, ref color2);
                    Currmod[i] = color1;
                }
                else if (i == 67)
                {
                    RAGE.Game.Vehicle.GetVehicleColours(Player.LocalPlayer.GetVehicleIsIn(true), ref color1, ref color2);
                    Currmod[i] = color2;
                }
                else if (i == 55)
                {
                    Currmod[i] = RAGE.Game.Vehicle.GetVehicleWindowTint(Player.LocalPlayer.GetVehicleIsIn(true));
                }
                else
                {
                    Currmod[i] = RAGE.Game.Vehicle.GetVehicleMod(Player.LocalPlayer.GetVehicleIsIn(true), i);
                }
            }
            CallRemote("SaveAllMods", Currmod, veh_obj);

        }

        private void ResetCamera(object[] args)
        {
            if (Open && !Cursor)
            {
                RAGE.Ui.Cursor.Visible = false;
                Cursor = true;
            }
            Notify("Нажмите ALT для редактирования");
        }

       // public void DrawMarkers(object[] args)
       // {
       //     foreach (var item in Ticks_Mechs.CustomsCords)
       //     {
       //
       //         //item.CustomsCoords.Z = item.CustomsCoords.Z - 1;
       //         new Marker(0, item.RepairCoords, 1f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80));
       //         // markMech1.Visble = true;
       //         // RAGE.Elements.SphereColshape cols = new RAGE.Elements.SphereColshape(item.RepairCoords, 1f, RAGE.Elements.Player.LocalPlayer.Dimension);
       //         // cols.SetData<string>("mechs", "mechs");
       //         // new Marker(0, item.RepairCoords, 0.5f, new Vector3(), new Vector3(), new RGBA(0, 255, 100), isVisible:true);
       //
       //     }
       //     foreach (var item in Ticks_Mechs.CustomsCords)
       //     {
       //         if (item.CustomCoords != null)
       //         {
       //             //item.Z = item.Z - 1;
       //             new Marker(0, item.CustomCoords, 1f, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new RGBA(255, 200, 0, 80));
       //
       //         }
       //     }
       //     foreach (var item in Ticks_Mechs.CustomsCords)
       //     {
       //         //item.Z = item.Z - 1;
       //         new Marker(1, item.BuyCustomsCoords, 1f, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new RGBA(255, 200, 0, 80));
       //
       //         new Blip(402, item.BuyCustomsCoords, name: "AutoService", color: 46, shortRange: true);
       //     }
       //
       // }


        private void PressCancel(object[] args)
        {
            int veh = Player.LocalPlayer.GetVehicleIsIn(true);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            if (Convert.ToInt32(args[0]) == 66)
            {
                Events.CallRemote("customColor1", args[0], Currmod[Convert.ToInt32(args[0])], veh_obj);
            }
            else if (Convert.ToInt32(args[0]) == 67)
            {
                Events.CallRemote("customColor2", args[0], Currmod[Convert.ToInt32(args[0])], veh_obj);
            }
            else
            {
                Events.CallRemote("custom", Convert.ToInt32(args[0]), Currmod[Convert.ToInt32(args[0])], veh_obj);
            }

        }

        private void PressArrow(object[] args)
        {
            int typeMods = (int)args[0];
            int veh = GetVehicle(5.0f);
            Chat.Output(veh.ToString());
            // Chat.Output(Entities.Vehicles.GetAtHandle(veh).ToString());

            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            Chat.Output(veh_obj.ToString());
            switch (typeMods)
            {

                case 66:
                    Events.CallRemote("customColor1", args[0], args[1], veh_obj);
                    break;
                case 67:
                    Events.CallRemote("customColor2", args[0], args[1], veh_obj);
                    break;
                default:
                    Events.CallRemote("custom", args[0], args[1], veh_obj);
                    break;
            }

            switch (typeMods)
            {
                case 36:
                case 37:
                case 38:
                    RAGE.Game.Vehicle.SetVehicleDoorOpen(Player.LocalPlayer.GetVehicleIsIn(true), 5, false, false);
                    break;
                case 31:
                case 32:
                case 44:
                    RAGE.Game.Vehicle.SetVehicleDoorOpen(Player.LocalPlayer.GetVehicleIsIn(true), 0, false, false);
                    RAGE.Game.Vehicle.SetVehicleDoorOpen(Player.LocalPlayer.GetVehicleIsIn(true), 1, false, false);
                    RAGE.Game.Vehicle.SetVehicleDoorOpen(Player.LocalPlayer.GetVehicleIsIn(true), 2, false, false);
                    RAGE.Game.Vehicle.SetVehicleDoorOpen(Player.LocalPlayer.GetVehicleIsIn(true), 3, false, false);
                    break;
                case 39:
                case 40:
                case 41:
                    RAGE.Game.Vehicle.SetVehicleDoorOpen(Player.LocalPlayer.GetVehicleIsIn(true), 4, false, false);
                    break;
                default:
                    for (int i = 0; i <= 5; i++)
                    {
                        if (RAGE.Game.Vehicle.GetVehicleDoorAngleRatio(Player.LocalPlayer.GetVehicleIsIn(true), i) > 0)
                        {
                            RAGE.Game.Vehicle.SetVehicleDoorShut(Player.LocalPlayer.GetVehicleIsIn(true), i, false);
                        }
                    }
                    break;
            }

        }
        private void PressExit(object[] args)
        {
            int veh = Player.LocalPlayer.GetVehicleIsIn(true);
            List<Vehicle> vehicles = Entities.Vehicles.All;
            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
            RAGE.Game.Vehicle.SetVehicleUndriveable(veh, false);
            Mechanic_Browser.Close();
            Mods.Clear();
            Open = false;
            RAGE.Game.Vehicle.SetVehicleUndriveable(Player.LocalPlayer.GetVehicleIsIn(true), false);
            RAGE.Game.Vehicle.SetVehicleDoorsLocked(Player.LocalPlayer.GetVehicleIsIn(true), 1);
            //show = false;

            if (!buy)
            {
                //Chat.Output("1");
                Events.CallRemote("CancelAllMods", Currmod, veh_obj);
            }
            for (int i = 0; i <= 5; i++)
            {
                if (RAGE.Game.Vehicle.GetVehicleDoorAngleRatio(Player.LocalPlayer.GetVehicleIsIn(true), i) > 0)
                {
                    RAGE.Game.Vehicle.SetVehicleDoorShut(Player.LocalPlayer.GetVehicleIsIn(true), i, false);
                }
            }
            Events.CallRemote("custom", 55, Currmod[55], veh_obj);
        }

        public static void StartCustomize()
        {
            int TypeMods;
            int veh = Player.LocalPlayer.GetVehicleIsIn(true);
            int[] price = new int[76];
            int color1 = 0;
            int color2 = 0;
            for (int i = 0; i < 76; i++)
            {
                price[i] = 100;
                TypeMods = RAGE.Game.Vehicle.GetNumVehicleMods(veh, i);

                if (i == 66)
                {
                    RAGE.Game.Vehicle.GetVehicleColours(veh, ref color1, ref color2);
                    Currmod[i] = color1;
                    TypeMods = 160;
                }
                else if (i == 67)
                {
                    RAGE.Game.Vehicle.GetVehicleColours(veh, ref color1, ref color2);
                    Currmod[i] = color2;
                    TypeMods = 160;
                }
                else
                {
                    Currmod[i] = RAGE.Game.Vehicle.GetVehicleMod(veh, i);
                }
                if (TypeMods != 0)
                {
                    Mods.Add(new Current_Mods(i, TypeMods + 1, price));
                }
            }
            var json = JsonConvert.SerializeObject(Mods);
            RAGE.Ui.Cursor.Visible = true;
            Mechanic_Browser.Call(json);
        }

    }
}
