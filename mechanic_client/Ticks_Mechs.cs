
//using Newtonsoft.Json;
using RAGE;
using RAGE.Elements;
using Serv_RP.player;
using System;
using System.Collections.Generic;

namespace mechanic_client
{
    class Ticks_Mechs : Events.Script
    {
        bool openRepair = false;
        bool openBuyBuis = false;
        public static int CarScore = 0;
        public static float Mileage = 0;
        public static string Text = "";
        public static string Data="";
        public static string TypeCustoms;
        public static string NameCustoms;
        public static int TotalHealth = -1000;
        public static int TotalMaxHealth = -1000;
        public static int TotalBodyHealth = -1000;
        public static int TotalMaxBodyHealth = -1000;
        // private bool fixB = false;
        private bool exitCar = false;
        private uint modelVeh;
        private DateTime dt1 = DateTime.Now;
       
       
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
            Events.Tick += TickHandler;
            Events.Add("Load_Vehicle_Health", LoadVehicleHealth);
            Events.Add("Load_Vehicle_Body_Health", LoadVehicleBodyHealth);
            Events.Add("Load_Vehicle_Max_Health", LoadVehicleMaxHealth);
            Events.Add("Load_Vehicle_Max_Body_Health", LoadVehicleMaxBodyHealth);
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


        private void TickHandler(List<Events.TickNametagData> nametags)
        {

            int veh = Mechanic_Client.GetVehicle(5.0f);
            Vector3 pos = Player.LocalPlayer.Position;
            modelVeh = RAGE.Game.Entity.GetEntityModel(veh);
            if (Player.LocalPlayer.GetSharedData(PlayerData.Fraction) != null)
            {
                if (Player.LocalPlayer.GetSharedData(PlayerData.Fraction).ToString() == "mechs")
                {
                    if (Mechanic_Client.ActiveDiag)
                    {
                        modelVeh = RAGE.Game.Entity.GetEntityModel(veh);

                    }

                    if (veh != -1 && Mechanic_Client.ActiveDiag)
                    {
                        Mechanic_Client.GetStatusWheel();
                        Mechanic_Client.GetEngineHealth();
                        Mechanic_Client.GetBodyHealth();
                    }
                    if (veh != -1 && Mechanic_Client.ActiveRepairKit)
                    {
                       // if (RAGE.Game.Pad.IsControlJustPressed(0, 38) && !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle))
                       // {
                            if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 38) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                            {
                                Mechanic_Client.FixWheel();
                            }
                            if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 20) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                            {
                                Mechanic_Client.FixEngine(500, (int)RAGE.Game.Vehicle.GetVehicleEngineHealth(veh), -1000, -1000);
                            }
                            if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 246) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                            {
                                Mechanic_Client.FixBody(500, (int)RAGE.Game.Vehicle.GetVehicleBodyHealth(veh), - 1000, -1000);
                            }
                            //mechanic_client.ActiveRepairKit = false;
                       // }
                    }

                    // точка починки

                    if (RAGE.Game.Entity.DoesEntityExist(veh) && (RAGE.Game.Vehicle.IsThisModelACar(modelVeh) || RAGE.Game.Vehicle.IsThisModelABike(modelVeh) || RAGE.Game.Vehicle.IsThisModelAQuadbike(modelVeh)))
                    {
                        foreach (var item in CustomsCords)
                        {
                            if (RAGE.Game.Utils.Vdist(item.RepairCoords.X, item.RepairCoords.Y, item.RepairCoords.Z, pos.X, pos.Y, pos.Z) <= 10.0f && Math.Abs(pos.Z - item.RepairCoords.Z) <= 5.0f && Player.LocalPlayer.GetSharedData("typeCustoms").ToString() == item.NameCustoms)
                            {
                                if (!openRepair) { Mechanic_Client.Notif("~y~Начать починку ~g~[ENTER]"); }

                                if (RAGE.Game.Pad.IsControlPressed(0, 18) || openRepair)
                                {
                                    Mechanic_Client.ActiveDiag = false;
                                    Mechanic_Client.ActiveRepairKit = false;
                                    if (TotalHealth == -1000)
                                    {
                                        List<Vehicle> vehicles = Entities.Vehicles.All;
                                        Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
                                        Events.CallRemote("Load_Car_Health", veh_obj);
                                        Events.CallRemote("Load_Car_Max_Health", veh_obj);
                                    }
                                    if (TotalBodyHealth == -1000)
                                    {
                                        List<Vehicle> vehicles = Entities.Vehicles.All;
                                        Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
                                        Events.CallRemote("Load_Car_Body_Health", veh_obj);
                                        Events.CallRemote("Load_Car_Max_Body_Health", veh_obj);
                                    }
                                    openRepair = true;

                                    bool[] windows = Mechanic_Client.GetWindow();
                                    bool[] wheel = Mechanic_Client.GetStatusWheel();
                                    bool[] doors = Mechanic_Client.GetDoors();
                                    float eng = Mechanic_Client.GetEngineHealth();
                                    float body = Mechanic_Client.GetBodyHealth();
                                   
                                    bool fixBody = false;
                                   // if (body <= 500 || eng <= 500)
                                   // {
                                   //     Data = DateTime.Now.ToString();
                                   //     
                                   // }
                                    //else
                                    //{
                                    //    Data = "Капитальный ремонт не проводился";
                                    //}
                                    

                                    if (Array.IndexOf(windows, true) == -1 && Array.IndexOf(wheel, true) == -1 && eng > 800 && body > 800 || Array.IndexOf(wheel, true) == -1 && eng == 1000 && body == 1000 )
                                    {
                                        Mechanic_Client.Notif("~y~Починить корпус ~g~[Ctrl+E]");
                                        fixBody = true;
                                    }
                                    else if (Array.IndexOf(doors, true) != -1 && Array.IndexOf(wheel, true) == -1 && eng > 800 && body > 800 || Array.IndexOf(wheel, true) == -1 && eng == 1000 && body == 1000)
                                    {
                                        Mechanic_Client.Notif("~y~Починить корпус ~g~[Ctrl+E]");
                                        fixBody = true;
                                    }
                                    if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 32) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                    {
                                        // Chat.Output("1");
                                        if (Array.IndexOf(windows, true) != -1)
                                        {
                                            Mechanic_Client.FixWindow();
                                           // Text = Text + " Починка стекол@";
                                        }
                                        return;
                                    }
                                    if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 38) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                    {
                                        if (Array.IndexOf(wheel, true) != -1)
                                        {
                                            Mechanic_Client.FixWheel();
                                           // Text = Text + "Починка колёс@";
                                        }
                                        return;
                                    }
                                    if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 20)  /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                    {
                                        // if (eng != TotalHealth)
                                        // {
                                        if (RAGE.Game.Vehicle.GetVehicleEngineHealth(veh) <= 500)
                                        {
                                            Text = Text + "@Капитальная починка двигателя@";
                                        }
                                        if (body <= 500 || eng <= 500)
                                            {
                                                Data = "@"+DateTime.Now.ToString()+"@";

                                            }

                                            Mechanic_Client.FixEngine(1000, (int)RAGE.Game.Vehicle.GetVehicleEngineHealth(veh), TotalHealth, TotalMaxHealth);
                                            
                                            TotalHealth = -1000;
                                        //}
                                        return;
                                    }
                                    if (RAGE.Game.Pad.IsControlPressed(0, 19) && RAGE.Game.Pad.IsControlJustPressed(0, 246) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                    {
                                      //  if (/*Array.IndexOf(windows, true) == -1 &&*/  (Array.IndexOf(doors, true) != -1) || RAGE.Game.Vehicle.IsThisModelABike(modelVeh))
                                        //{
                                            if (RAGE.Game.Vehicle.GetVehicleBodyHealth(veh) <= 500) {
                                                Text = Text + "@Капитальная починка корпуса@";
                                            }
                                            Mechanic_Client.FixBody(1000, (int)RAGE.Game.Vehicle.GetVehicleBodyHealth(veh),  TotalBodyHealth, TotalMaxBodyHealth);
                                           
                                            TotalBodyHealth = -1000;
                                        //}
                                        if (body <= 500 || eng <= 500)
                                        {
                                            Data = "@"+DateTime.Now.ToString()+"@";

                                        }
                                        return;
                                    }
                                    if (RAGE.Game.Pad.IsControlPressed(0, 36) && RAGE.Game.Pad.IsControlPressed(0, 38) /*&& !RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle)*/)
                                    {
                                        if (fixBody  /*!fixB*/)
                                        {
                                            Mechanic_Client.FixBodyShell();
                                            //Text = Text + "Общая починка@";
                                            //fixB = true;
                                        }
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

                    //прокачка 
                    if (!Mechanic_Client.Open)
                    {
                        if (RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle))
                        {
                            veh = RAGE.Game.Ped.GetVehiclePedIsUsing(Player.LocalPlayer.Handle);
                            modelVeh = RAGE.Game.Entity.GetEntityModel(veh);
                            pos = Player.LocalPlayer.Position;
                            if (RAGE.Game.Entity.DoesEntityExist(veh) && (RAGE.Game.Vehicle.IsThisModelACar(modelVeh) || RAGE.Game.Vehicle.IsThisModelABike(modelVeh) || RAGE.Game.Vehicle.IsThisModelAQuadbike(modelVeh)))
                            {
                                if (RAGE.Game.Vehicle.GetPedInVehicleSeat(veh, -1, 0) == Player.LocalPlayer.Handle)
                                {
                                    foreach (var item in CustomsCords)
                                    {
                                        if (item.CustomCoords != null) { 
                                        if (RAGE.Game.Utils.Vdist(item.CustomCoords.X, item.CustomCoords.Y, item.CustomCoords.Z, pos.X, pos.Y, pos.Z) <= 5.0f && Math.Abs(pos.Z - item.CustomCoords.Z) <= 3.0f && Player.LocalPlayer.GetSharedData("typeCustoms").ToString() == item.NameCustoms)
                                        {

                                            if (RAGE.Game.Pad.IsControlPressed(0, 166) && !Mechanic_Client.Open)
                                            {

                                                    //RAGE.Game.Vehicle.SetVehicleDoorsLocked(veh, 4);
                                                RAGE.Game.Vehicle.SetVehicleUndriveable(veh, true);
                                                Mechanic_Browser.Create();
                                                Mechanic_Client.StartCustomize();
                                                Mechanic_Client.Open = true;
                                                    Mechanic_Client.Cursor = true;
                                                    Mechanic_Browser.Open("");
                                            }

                                            //if (!Mechanic_Customs.show)
                                            //{
                                            //Mechanic_Customs.Notify("~c~Для открытия меню нажмите ~g~F5");
                                            //   Mechanic_Customs.show = false;
                                            RAGE.Game.Ui.BeginTextCommandDisplayHelp("STRING");
                                            RAGE.Game.Ui.AddTextComponentSubstringPlayerName("~c~Для открытия меню нажмите ~g~F5");
                                            RAGE.Game.Ui.EndTextCommandDisplayHelp(0, false, false, -1);
                                            //}
                                        }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (RAGE.Game.Pad.IsControlPressed(0, 19) && Mechanic_Client.Open && Mechanic_Client.Cursor)
                    {
                       
                        RAGE.Ui.Cursor.Visible = true;
                        Mechanic_Client.Cursor = false;
                    }
                }

            }
            foreach (var item in CustomsCords)
            {
                if (RAGE.Game.Utils.Vdist(item.BuyCustomsCoords.X, item.BuyCustomsCoords.Y, item.BuyCustomsCoords.Z, pos.X, pos.Y, pos.Z) <= 1.0f && Math.Abs(pos.Z - item.BuyCustomsCoords.Z) <= 1.0f)
                {
                    if (!openBuyBuis) { Mechanic_Client.Notif("~y~Купить автосервис ~g~[E]"); }
                    if (RAGE.Game.Pad.IsControlPressed(0, 38) && !openBuyBuis)
                    {
                        NameCustoms = item.NameStatic;
                        TypeCustoms = item.NameCustoms;
                        Mechanic_Client.OpenBuyBuis(new object[0]);
                        openBuyBuis = true;
                    }
                }
                else
                {
                    openBuyBuis = false;
                }
            }

           if (RAGE.Game.Pad.IsControlPressed(0, 249))
           {

               Tablet.OpenClose(null);
          
           }
                   
            //пробег
            if (RAGE.Game.Ped.IsPedSittingInAnyVehicle(Player.LocalPlayer.Handle))
            {
                if (RAGE.Game.Vehicle.GetPedInVehicleSeat(Player.LocalPlayer.GetVehicleIsIn(true), -1, 0) == Player.LocalPlayer.Handle)
                {
                     if (RAGE.Game.Entity.GetEntitySpeed(Player.LocalPlayer.GetVehicleIsIn(true)) == 0 ||  !exitCar)
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
                        Mileage = Mileage + (speedkm * (float)sec.TotalSeconds);
                        CarScore = Convert.ToInt32(Mileage);
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
