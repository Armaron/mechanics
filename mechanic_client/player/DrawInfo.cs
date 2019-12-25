using cs_packages.client;
using mechanic_client;
using RAGE;
using RAGE.Elements;
using RAGE.Game;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace cs_packages.player
{
    class DrawInfo : Events.Script
    {
        static List<Blip> blips = new List<Blip>();
     public   static List<uint> weapons = new List<uint>();
        public static List<uint> coldweapons = new List<uint>();
        static bool pause = false;
        static RAGE.Elements.Player PlayerLocal = RAGE.Elements.Player.LocalPlayer;
        static public bool Drive = false;
        public static bool CarBrake;
        public static bool CarForvard;
        public static bool LoadScreen;
        static public long p;
        static public long g;
        static public int x = 1920;
        static public int y = 1080;
        static public double degre;
        private static double degre1;
        static RAGE.Elements.Marker textLabel;
        static bool togle;
        static public RAGE.Elements.Player target;
        static public string leter;
        static public bool playerLight;


        public DrawInfo()
        {
            // Холодное оружие

            coldweapons.Add(0x99B507EA);
            coldweapons.Add(0xDFE37640);
            coldweapons.Add(0xDD5DF8D9);



            //-------------------

            //Огнестрельное оружие


            weapons.Add(0x1B06D571);
            weapons.Add(0xBFE256D4);
            weapons.Add(0x5EF9FEC4);
            weapons.Add(0x22D8FE39);
          
            weapons.Add(0x99AEEB3B);
            weapons.Add(0xD205520E);
            weapons.Add(0x13532244);
            weapons.Add(0x2BE6766B);
            weapons.Add(0xEFE7E2DF);
            weapons.Add(0x0A3D4D34);
            weapons.Add(0x555AF99A);

            weapons.Add(0x7846A318);
            weapons.Add(0xBFEFFF6D);

            weapons.Add(0x83BF0278);
            weapons.Add(0x05FC3C11);

            //--------------------












            //weapons.Add(0x99B507EA);
            //weapons.Add(0x99B507EA);

            //weapons.Add(0x99B507EA);
            //weapons.Add(0x99B507EA);
            //weapons.Add(0x99B507EA);

            RAGE.Game.Graphics.GetScreenResolution(ref x, ref y);

            Events.Add("LocalTick", Draw);
        }
        public static void CreateText()
        {
           // Chat.Output("Create");
            togle = true;
            textLabel = new Marker(1, new Vector3(target.Position.X, target.Position.Y, target.Position.Z - 1.2f), 0.5f,new Vector3(0f,0f,0f), new Vector3(0f, 0f, 0f), new RGBA(255,255,0,50),dimension: RAGE.Elements.Player.LocalPlayer.Dimension);     //TextLabel(target.Position, "Press H", new RGBA(255, 255, 0), dimension: target.Dimension);
        }
        public static void DeleteText()
        {
            try
            {if (textLabel != null)
                {
                    togle = false;
                    textLabel.Destroy();
                    textLabel = null;
                }
            }
            catch
            {

            }
        }
        public static void Draw(object[] arg)
        {
            if (Menu.ready)
            {
                RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.0f);

               // GameMaster.CheckPlayer();




                player.VoiceChat.VoiceProcess();
                if (playerLight)
                {
                    g++;
                    if(g % 10 == 0)
                    {
                        try
                        {
                            textLabel.Position = new Vector3(target.Position.X, target.Position.Y, target.Position.Z - 1.2f);
                            if(Math.Abs(textLabel.Position.DistanceTo(RAGE.Elements.Player.LocalPlayer.Position) )>  5f)
                            {
                                g = -1;
                                DeleteText();
                                playerLight = false;
                                KeyManager.player = null;
                            }
                        }
                        catch
                        {

                        }
                    }


                    //   nametags[j].Player.Coo
                    //      RAGE.Game.UIText.Draw("Press H", new System.Drawing.Point((int)(RAGE.NUI.Game.ScreenResolution.Width * nametags[j].ScreenX), (int)(RAGE.NUI.Game.ScreenResolution.Height * nametags[j].ScreenY)), 0.25f, System.Drawing.Color.Yellow, RAGE.Game.Font.ChaletLondon, true);
                    if (!togle)
                        CreateText();

               

                 //   textLabel.Position = target.Position;

                    if (g >= 900)
                    {
                        g = 0;
                        DeleteText();
                        playerLight = false;
                        KeyManager.player = null;
                    }
                }
                else if(g!=-1)
                {
                    g = -1;
                    DeleteText();
                    playerLight = false;
                    KeyManager.player = null;
                }

                if (LoadScreen)
                {
                    p++;
                    degre += 20;
                    if (degre >= 360)
                        degre = 0;
                    double rad = (degre * 3.14) / 180;
                    int x1 = x / 2 + (int)(Math.Sin(rad) * 50);
                    int y1 = y / 2 - (int)(Math.Cos(rad) * 50);



                    degre1 = degre - 20;
                    if (degre1 <= 0)
                        degre1 = 340;
                    double rad1 = (degre1 * 3.14) / 180;
                    int x2 = x / 2 + (int)(Math.Sin(rad) * 50);
                    int y2 = y / 2 - (int)(Math.Cos(rad) * 50);

                    //RAGE.Game.Graphics.DrawSprite("mpmissionend", "goldmedal", 0.5f, 0.5f, 0.1f, 0.1f, 0f, 255, 255, 255, 255, 1);
                    //RAGE.Game.UIText.Draw("о", new Point(x1, y1 / 2), 30f, Color.Yellow, Font.Monospace, true);
                    //RAGE.Game.UIContainer.Draw();
                    RAGE.Game.UIRectangle.Draw(new Point(x2, y2), new Size(1, 1), Color.Yellow);
                    RAGE.Game.UIRectangle.Draw(new Point(x1, y1), new Size(3, 3), Color.Yellow);
                    if (p >= 200)
                    {
                        p = 0;
                        LoadScreen = false;
                    }
                }

                //p++;
                ////   Chat.Show(false);
                //if (Drive && p%200==0)
                //{
                //    PlayerState.ped_Driver.TaskVehicleGotoNavmesh(PlayerState.vehicle.Handle, PlayerState.coord.X, PlayerState.coord.Y, PlayerState.coord.Z, 10f, 156, 5f);
                //}
                if (CarBrake)
                {
                    RAGE.Game.Pad.DisableControlAction(27, 72, true);
                }
                //Запрет движения вперед
                if (CarForvard)
                {
                    RAGE.Game.Pad.DisableControlAction(27, 71, true);
                }
                if (RAGE.Game.Ui.IsPauseMenuActive())
                {
                    if (!pause)
                    {
                        Browser.CloseAllBrowsersAndDefreese();
                        pause = true;
                    }
                    return;
                }
                pause = false;

                RAGE.Game.Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_cellgate"), 461.8065f, -994.4086f, 25.06443f, true, 0.0f, false);
                RAGE.Game.Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_cellgate"), 461.8065f, -997.6583f, 25.06443f, true, 0.0f, false);
                RAGE.Game.Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_cellgate"), 461.8065f, -1001.302f, 25.06443f, true, 0.0f, false);


                NoClip.RenderTick();
                Ticks_Mechs.TickMech();
                NoClip.KeyHandler();

                if (PlayerState.block)
                {
                    for (int i = 0; i < 33; i++)
                    {
                        RAGE.Game.Pad.DisableAllControlActions(i);
                        //  RAGE.Game.Pad.EnableAllControlActions(i);
                    }
                }







                if (PlayerLocal.HasBeenDamagedByAnyVehicle())
                {
                    int bone = 31086;
                    Chat.Output("Сбила машина " + bone.ToString());
                    PlayerLocal.GetLastDamageBone(ref bone);
                    if (bone != 0)
                    {
                        Chat.Output("Сбила машина " + bone.ToString());
                        Events.CallRemote("GetDamag", bone, "травма");
                        PlayerLocal.ClearLastDamageBone();
                        PlayerLocal.ClearLastWeaponDamage();
                    }
                    PlayerLocal.ClearLastDamageBone();
                    PlayerLocal.ClearLastWeaponDamage();
                }
                if (PlayerLocal.HasBeenDamagedByAnyObject())
                {
                    int bone = 31086;
                    Chat.Output("Дамаг объектом " + bone.ToString());
                    PlayerLocal.GetLastDamageBone(ref bone);
                    if (bone != 0)
                    {
                        Chat.Output("Дамаг объектом " + bone.ToString());
                        Events.CallRemote("GetDamag", bone, "травма");

                    }
                    PlayerLocal.ClearLastDamageBone();
                }

                if (PlayerLocal.HasBeenDamagedByAnyPed())
                {
                    int bone = 31086;
                    if (PlayerLocal.HasBeenDamagedByWeapon(0, 2))
                    {
                        if (PlayerLocal.HasBeenDamagedByWeapon(0, 1))
                        {

                          
                            PlayerLocal.GetLastDamageBone(ref bone);
                            if (bone != 0)
                            {

                                foreach (uint weap in coldweapons)
                                {

                                    if (PlayerLocal.HasBeenDamagedByWeapon(weap, 0))
                                    {
                                       
                                        Events.CallRemote("GetDamag", bone, "холодное");
                                        PlayerLocal.ClearLastDamageBone();
                                        PlayerLocal.ClearLastWeaponDamage();
                                        goto B;
                                       
                                    }
                                }

                                Events.CallRemote("GetDamag", bone, "травма");
                                PlayerLocal.ClearLastDamageBone();
                                PlayerLocal.ClearLastWeaponDamage();

                            B:;



                                
                           
                            }
                            PlayerLocal.ClearLastDamageBone();
                            PlayerLocal.ClearLastWeaponDamage();
                        }

                        else
                        {
                            bone = 31086;
                           
                            PlayerLocal.GetLastDamageBone(ref bone);
                            if (bone != 0)
                                //     PlayerLocal
                                //layerLocal.HasBeenDamagedByAnyPed();

                                foreach (uint weap in weapons)
                                {

                                    if (PlayerLocal.HasBeenDamagedByWeapon(weap, 0))
                                    {
                                       
                                        Events.CallRemote("GetDamag", bone, "огнестрел");
                                        PlayerLocal.ClearLastDamageBone();
                                        PlayerLocal.ClearLastWeaponDamage();
                                        break;
                                    }
                                }
                            PlayerLocal.ClearLastDamageBone();
                            PlayerLocal.ClearLastWeaponDamage();
                        }
                    }

                    //if (bone == 31086)
                    //{

                    //    PlayerLocal.ClearLastDamageBone();
                    //}
                    //if (bone == 18905)
                    //{
                    //    Chat.Output("Вам захуярили в левую руку");
                    //    PlayerLocal.ClearLastDamageBone();
                    //}
                    //if (bone == 65245)
                    //{
                    //    Chat.Output("Вам захуярили в левую ногу");
                    //    PlayerLocal.ClearLastDamageBone();
                    //}



                }






                foreach (Blip blip in blips)
                {
                    blip.Destroy();
                }
                blips.Clear();


                List<RAGE.Events.TickNametagData> nametags = (List<RAGE.Events.TickNametagData>)arg[0];


                //RAGE.Game.UIText.Draw("F5", new System.Drawing.Point(420, 5), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- закрыть интерфейс", new System.Drawing.Point(500, 5), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("F7", new System.Drawing.Point(420, 20), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- курсор", new System.Drawing.Point(500, 20), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("J", new System.Drawing.Point(420, 35), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- меню взаимодействия", new System.Drawing.Point(510, 35), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("K", new System.Drawing.Point(420, 50), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("   - меню взаимодействия с ТС", new System.Drawing.Point(550, 50), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("L", new System.Drawing.Point(420, 65), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- меню эмоций", new System.Drawing.Point(500, 65), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("Z", new System.Drawing.Point(420, 80), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- микрофон", new System.Drawing.Point(500, 80), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("M", new System.Drawing.Point(420, 95), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- телефон", new System.Drawing.Point(500, 95), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("N", new System.Drawing.Point(420, 110), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- планшет", new System.Drawing.Point(500, 110), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);

                //RAGE.Game.UIText.Draw("F2", new System.Drawing.Point(420, 125), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                //RAGE.Game.UIText.Draw("- остановить анимацию", new System.Drawing.Point(515, 125), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.HouseScript, true);



                //List<Player> players = Entities.Players.All;
                //for (int i = 0; i < players.Count; i++)
                //{
                //    try
                //    {



                //        if (players[i].GetData<bool>("patrul"))
                //        {
                //            Blip blip = new Blip(56, players[i].Position, color: 38, shortRange: false, dimension: 4294967295);
                //            blips.Add(blip);
                //        }
                //        if (players[i].GetSharedData("fullname") != null)
                //            RAGE.Game.UIText.Draw(players[i].GetSharedData("fullname").ToString(), new System.Drawing.Point(900, 6 * i), 0.3f, System.Drawing.Color.DarkViolet, RAGE.Game.Font.Pricedown, true);
                //    }
                //    catch
                //    {

                //    }
                //}



                RAGE.Game.Graphics.GetScreenResolution(ref x, ref y);

                string zone = RAGE.Game.Zone.GetNameOfZone(RAGE.Elements.Player.LocalPlayer.Position.X, RAGE.Elements.Player.LocalPlayer.Position.Y, RAGE.Elements.Player.LocalPlayer.Position.Z);

                for (int i = 0; i < PlayerState.Zones.Length; i++)
                {
                    if (zone == PlayerState.Zones[i])
                    {
                        RAGE.Game.UIText.Draw(PlayerState.ZonesRes[i], new System.Drawing.Point((x / 4), y - 50), 0.7f, System.Drawing.Color.White, RAGE.Game.Font.HouseScript, true);
                    }
                }





            }

        }

    }
}
