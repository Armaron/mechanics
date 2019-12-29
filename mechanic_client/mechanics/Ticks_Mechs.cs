using RAGE;
using RAGE.Elements;
using System;
using System.Collections.Generic;
using System.Threading;

namespace mechanic_client
{
    class Ticks_Mechs : Events.Script
    {
        public static bool openRepair = false;
        public static int CarScore = 0;
        public static float Mileage = 0;
        public static float MileageKM = 0;
        public static float eng;
        public static float body;
        public static string Text = "";
        public static string Data = "";
        public static int TotalHealth = -1000;
        public static int TotalMaxHealth = -1000;
        public static int TotalBodyHealth = -1000;
        public static int TotalMaxBodyHealth = -1000;
        public static bool exitCar = false;
        public static uint modelVeh = 0;
        public static int targetVeh = 0;
        public static DateTime dt1 = DateTime.Now;

        public static bool onCoods = false;
        public static bool keyfixEng = false;
        public static bool keyfixBody = false;
        public static bool keyfixWind = false;
        public static bool keyfixWheel = false;
        public static bool keyFixBodyShell = false;
        public static bool inCar = false;
        public static int veh;

        public static List<Customs> CustomsCords = new List<Customs>()
        {

           new Customs("buis1","custmos1", new Vector3(252.6783f, 2597.389f, 44.81868f), new Vector3(257.8216f, 2593.467f, 44.52076f),null),
           new Customs("buis2","custmos2", new Vector3(721.5389f, -1084.641f, 22.22401f), new Vector3(732.6118f, -1088.999f, 22.16901f), new Vector3(735.502f, -1079.062f, 22.16869f)),
           new Customs("buis3","custmos3", new Vector3(484.3903f, -1309.438f, 29.23346f), new Vector3(480.8116f, -1321.589f, 29.20394f), new Vector3(479.0926f, -1315.811f, 29.20343f)),
           new Customs("buis4","custmos4", new Vector3(1153.392f, -785.2482f, 57.59873f), new Vector3(1149.656f, -775.9724f, 57.59866f),null),
           new Customs("buis5","custmos5", new Vector3(-1141.372f, -1991.64f, 13.16398f), new Vector3(-1157.609f, -2021.418f, 13.13204f), new Vector3(-1159.185f, -2006.514f, 13.18026f)),
           new Customs("buis6","custmos6", new Vector3(112.9487f, 6619.373f, 31.83793f), new Vector3(110.8199f, 6627.323f, 31.78724f), new Vector3(107.2648f, 6619.29f, 31.78725f)),
           new Customs("buis7","custmos7", new Vector3(2005.347f, 3791.077f, 32.18083f), new Vector3(2007.565f, 3798.764f, 32.18078f),null),
           new Customs("buis8","custmos8", new Vector3(548.2732f, -173.0667f, 54.48134f), new Vector3(538.398f, -176.5102f, 54.48743f),null),
           new Customs("buis9","custmos9", new Vector3(-64.2979f, 77.18669f, 71.6162f), new Vector3(-66.90628f, 82.67137f, 71.54682f),new Vector3(-80.98f, 88.93f, 71.53f)),
           new Customs("buis10","custmos10", new Vector3(1178.3f, 2647.108f, 37.79081f), new Vector3(1174.348f, 2640.481f, 37.76043f),null),
           new Customs("buis11","custmos11", new Vector3(-202.6446f, -1308.662f, 31.29279f), new Vector3(-221.5106f, -1329.439f, 30.89038f), new Vector3(-207.1979f, -1324.141f, 30.89041f)),
          // new Customs("buis12","custmos12", new Vector3(-425.688f, 1123.677f, 325.4203f),new Vector3(-425.688f, 1123.677f, 325.4203f), null ),
        };

        public Ticks_Mechs()
        {
            Events.Add("Load_Vehicle_Health", LoadVehicleHealth);
            Events.Add("Load_Vehicle_Body_Health", LoadVehicleBodyHealth);
            Events.Add("Load_Vehicle_Max_Health", LoadVehicleMaxHealth);
            Events.Add("Load_Vehicle_Max_Body_Health", LoadVehicleMaxBodyHealth);
            Events.Add("flagInCar", flagInCar);
        }

        private void flagInCar(object[] args)
        {
            if (!inCar) { inCar = true; }
            else { inCar = false; }

        }

        private void LoadVehicleMaxBodyHealth(object[] args)
        {
            TotalMaxBodyHealth = Convert.ToInt32(args[0]);
        }

        private void LoadVehicleMaxHealth(object[] args)
        {
            TotalMaxHealth = Convert.ToInt32(args[0]);
        }

        private void LoadVehicleBodyHealth(object[] args)
        {
            TotalBodyHealth = Convert.ToInt32(args[0]);
        }

        private void LoadVehicleHealth(object[] args)
        {
            TotalHealth = Convert.ToInt32(args[0]);
        }


        public static void TickMech()
        {

            if (Mechanic_Client.ActiveRepairKitCitizen)
            {
                veh = Mechanic_Client.GetVehicle(5.0f);
                Vector3 pos = Player.LocalPlayer.Position;


                if (veh == 0)
                {
                    Mechanic_Client.HasRepaikit(null);
                }

                if (veh != 0)
                {
                    // if (RAGE.Game.Pad.IsControlJustPressed(0, 38) && !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle))
                    // {
                    if (keyfixWheel /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                    {
                        Mechanic_Client.FixWheel(veh);
                        keyfixWheel = false;
                    }
                    if (keyfixEng /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                    {
                        Mechanic_Client.FixEngine(veh, 500, (int)RAGE.Game.Vehicle.GetVehicleEngineHealth(veh), -1000, -1000);
                        keyfixEng = false;
                    }
                }
            }

            if (Mechanic_Client.ActiveDiag)
            {
                if (Player.LocalPlayer.GetSharedData("Fraction") != null)
                {
                    if (Player.LocalPlayer.GetSharedData("Fraction").ToString() == "mechs")
                    {
                        veh = Mechanic_Client.GetVehicle(5.0f);
                        Vector3 pos = Player.LocalPlayer.Position;

                        if (veh == 0)
                        {
                            Mechanic_Client.ActiveDiagMech(null);
                        }
                        if (veh != 0)
                        {

                            Mechanic_Client.GetStatusWheel(veh);
                            Mechanic_Client.GetEngineHealth(veh);
                            Mechanic_Client.GetBodyHealth(veh);

                            if (keyfixWheel /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                            {
                                Mechanic_Client.FixWheel(veh);
                                keyfixWheel = false;
                            }
                            if (keyfixEng /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                            {
                                Mechanic_Client.FixEngine(veh, 500, (int)RAGE.Game.Vehicle.GetVehicleEngineHealth(veh), -1000, -1000);
                                keyfixEng = false;
                            }
                            if (keyfixBody /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                            {
                                Mechanic_Client.FixBody(veh, 500, (int)RAGE.Game.Vehicle.GetVehicleBodyHealth(veh), -1000, -1000);
                                keyfixBody = false;
                            }
                        }
                    }
                }
            }
            // точка починки

            if (onCoods)
            {
                if (Player.LocalPlayer.GetSharedData("Fraction") != null)
                {
                    if (Player.LocalPlayer.GetSharedData("Fraction").ToString() == "mechs")
                    {
                        veh = Mechanic_Client.GetVehicle(5.0f);
                        Vector3 pos = Player.LocalPlayer.Position;
                        modelVeh = RAGE.Game.Entity.GetEntityModel(veh);
                        if (RAGE.Game.Entity.DoesEntityExist(veh) && (RAGE.Game.Vehicle.IsThisModelACar(modelVeh) || RAGE.Game.Vehicle.IsThisModelABike(modelVeh) || RAGE.Game.Vehicle.IsThisModelAQuadbike(modelVeh)))
                        {
                            foreach (var item in CustomsCords)
                            {
                                if (RAGE.Game.Utils.Vdist(item.RepairCoords.X, item.RepairCoords.Y, item.RepairCoords.Z, pos.X, pos.Y, pos.Z) <= 10.0f && Math.Abs(pos.Z - item.RepairCoords.Z) <= 5.0f && Player.LocalPlayer.GetSharedData("typeCustoms").ToString() == item.NameCustoms)
                                {
                                    //onCoods = true;
                                    if (!openRepair) { Mechanic_Client.Notif("~y~Начать починку " + RAGE.Game.Vehicle.GetDisplayNameFromVehicleModel(modelVeh) + " ~g~[ENTER]"); targetVeh = veh; }

                                    if (/*RAGE.Game.Pad.IsControlPressed(0, 18) &&*/ /*RAGE.Game.Vehicle.IsVehicleSeatFree(veh, -1, 0) ||*/ openRepair)
                                    {

                                        Mechanic_Client.ActiveDiag = false;
                                        Mechanic_Client.ActiveRepairKit = false;
                                        if (TotalHealth == -1000)
                                        {
                                            List<Vehicle> vehicles = Entities.Vehicles.All;
                                            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == targetVeh);
                                            // Chat.Output(RAGE.Game.Vehicle.GetDisplayNameFromVehicleModel(modelVeh));
                                            Events.CallRemote("Load_Car_Health", veh_obj);
                                            Events.CallRemote("Load_Car_Max_Health", veh_obj);

                                            // veh_obj.SetEngineHealth(TotalHealth);
                                        }
                                        if (TotalBodyHealth == -1000)
                                        {
                                            List<Vehicle> vehicles = Entities.Vehicles.All;
                                            Vehicle veh_obj = vehicles.Find(pl => pl.Handle == targetVeh);
                                            Events.CallRemote("Load_Car_Body_Health", veh_obj);
                                            Events.CallRemote("Load_Car_Max_Body_Health", veh_obj);
                                            
                                        }

                                        openRepair = true;

                                        if (targetVeh != veh)
                                        {
                                            openRepair = false;
                                            TotalHealth = -1000;
                                            TotalBodyHealth = -1000;

                                        }

                                        bool[] windows = Mechanic_Client.GetWindow(targetVeh);
                                        bool[] wheel = Mechanic_Client.GetStatusWheel(targetVeh);
                                        bool[] doors = Mechanic_Client.GetDoors(targetVeh);
                                        eng = Mechanic_Client.GetEngineHealth(targetVeh);
                                        body = Mechanic_Client.GetBodyHealth(targetVeh);

                                        bool fixBody = false;

                                        if (Array.IndexOf(windows, true) == -1 && Array.IndexOf(wheel, true) == -1 && eng > 800 && body > 800 || Array.IndexOf(wheel, true) == -1 && eng == TotalMaxHealth && body == TotalMaxBodyHealth)
                                        {
                                            Mechanic_Client.Notif("~y~Починить корпус ~g~[F3]");
                                            fixBody = true;
                                        }
                                        else if (Array.IndexOf(doors, true) != -1 && Array.IndexOf(wheel, true) == -1 && eng > 800 && body > 800 || Array.IndexOf(wheel, true) == -1 && eng == TotalMaxHealth && body == TotalMaxBodyHealth)
                                        {
                                            Mechanic_Client.Notif("~y~Починить корпус ~g~[F3]");
                                            fixBody = true;
                                        }
                                        if (keyfixWind /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                        {
                                            // Chat.Output("1");
                                            if (Array.IndexOf(windows, true) != -1)
                                            {
                                                Mechanic_Client.FixWindow(targetVeh);
                                                // Text = Text + " Починка стекол@";
                                            }
                                            keyfixWind = false;
                                            return;
                                        }
                                        if (keyfixWheel /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                        {
                                            if (Array.IndexOf(wheel, true) != -1)
                                            {
                                                Mechanic_Client.FixWheel(targetVeh);
                                                // Text = Text + "Починка колёс@";
                                            }
                                            keyfixWheel = false;
                                            return;

                                        }
                                        if (keyfixEng  /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                        {
                                            // if (eng != TotalHealth)
                                            // {
                                            if (RAGE.Game.Vehicle.GetVehicleEngineHealth(targetVeh) <= 500)
                                            {
                                                Text = Text + "@Капитальный ремонт двигателя@";
                                            }
                                            if (body <= 500 || eng <= 500)
                                            {
                                                Data = "@" + DateTime.Now.ToString() + "@";

                                            }

                                            Mechanic_Client.FixEngine(targetVeh, 1000, (int)RAGE.Game.Vehicle.GetVehicleEngineHealth(targetVeh), TotalHealth, TotalMaxHealth);

                                            TotalHealth = -1000;
                                            keyfixEng = false;
                                            //}
                                            return;
                                        }
                                        if (keyfixBody /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                        {
                                            //  if (/*Array.IndexOf(windows, true) == -1 &&*/  (Array.IndexOf(doors, true) != -1) || RAGE.Game.Vehicle.IsThisModelABike(modelVeh))
                                            //{
                                            if (RAGE.Game.Vehicle.GetVehicleBodyHealth(targetVeh) <= 500)
                                            {

                                                Text = Text + "@Капитальная ремонт кузова@";
                                            }
                                            Mechanic_Client.FixBody(targetVeh, 1000, (int)RAGE.Game.Vehicle.GetVehicleBodyHealth(targetVeh), TotalBodyHealth, TotalMaxBodyHealth);

                                            TotalBodyHealth = -1000;
                                            //}
                                            if (body <= 500 || eng <= 500)
                                            {
                                                Data = "@" + DateTime.Now.ToString() + "@";

                                            }
                                            keyfixBody = false;
                                            return;
                                        }
                                        if (keyFixBodyShell /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                        {
                                            if (fixBody  /*!fixB*/)
                                            {
                                                Mechanic_Client.FixBodyShell(veh);


                                                //Text = Text + "Общая починка@";
                                                //fixB = true;
                                            }
                                            keyFixBodyShell = false;
                                            return;
                                        }
                                    }


                                }
                            }
                        }
                        else
                        {
                            openRepair = false;
                            //fixB = false;
                        }
                    }
                }
            }
           
            //пробег
            if (inCar)
            {
                if (RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle))
                {
                    if (RAGE.Game.Vehicle.GetPedInVehicleSeat(Player.LocalPlayer.GetVehicleIsIn(true), -1, 0) == Player.LocalPlayer.Handle)
                    {
                        if (RAGE.Game.Entity.GetEntitySpeed(Player.LocalPlayer.GetVehicleIsIn(true)) == 0 || !exitCar)
                        {
                            exitCar = true;
                            dt1 = DateTime.Now;
                        }
                        else
                        {
                            float speed = RAGE.Game.Entity.GetEntitySpeed(Player.LocalPlayer.GetVehicleIsIn(false));
                            DateTime dt2 = DateTime.Now;
                            float speedkm = speed / 1000;
                            TimeSpan sec = dt2 - dt1;
                            Mileage = (Mileage + (speedkm * (float)sec.TotalSeconds));
                            MileageKM = Mileage / 1000;
                            CarScore = Convert.ToInt32(MileageKM);
                            //Chat.Output("Пробег: " + Mileage.ToString());
                            // exitCar = true;
                        }

                    }

                }
                else
                {
                    exitCar = false;
                }
            }
        }


    }
}
