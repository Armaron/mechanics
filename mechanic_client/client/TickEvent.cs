using cs_packages.player;
using cs_packages.vehicle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cs_packages.client
{
    public class TickEvent : RAGE.Events.Script
    {

        public static Int64 tickcount = 0;
        public static bool DrugTimer;

        public static Speed SpeedClass;
        public static bool carcheck = false;
        public TickEvent()
        {
            //RAGE.Events.Tick += Tick;

        }




        private void Tick(List<RAGE.Events.TickNametagData> nametags)
        {
            Task.Factory.StartNew(() =>
            {
                if (Menu.ready)
                {


                    if (Menu.ready)
                    {

                      

                        if (nametags != null)

                            for (int j = 0; j < nametags.Count; j++)

                            {
                                if (DrawInfo.playerLight)
                                {
                                    if (nametags[j].Player == DrawInfo.target)
                                    {
                                     //   nametags[j].Player.Coo
                                        RAGE.Game.UIText.Draw("Press H", new System.Drawing.Point((int)(RAGE.NUI.Game.ScreenResolution.Width * nametags[j].ScreenX), (int)(RAGE.NUI.Game.ScreenResolution.Height * nametags[j].ScreenY)), 0.25f, System.Drawing.Color.Yellow, RAGE.Game.Font.ChaletLondon, true);
                                    }
                                }
                                // nametagData.Player.SetData<bool>("Draw", false);
                                //    RAGE.Game.Graphics.DrawSprite("cross", "circle_checkpoints_cross", nametagData.ScreenX, nametagData.ScreenY, 0.1f, 0.1f, 0f, 255, 255, 255, 255, 0);
                                //RAGE.Elements.Player.LocalPlayer
                                // RAGE.Game.Natives.NetworkIsGamerTalking(nametagData.Player);





                                //if ((bool)nametagData.Player.GetSharedData("ClientUsePhone"))
                                //{



                                //    //RAGE.Game.Mobile.CreateMobilePhone(0);
                                //    //RAGE.Game.Mobile.ScriptIsMovingMobilePhoneOffscreen(false);
                                //    //RAGE.Game.Mobile.SetMobilePhonePosition(0, 0, 0);
                                //}
                                //if ((bool)nametagData.Player.GetSharedData("ClientCallPhone"))
                                //{



                                //    //RAGE.Game.Mobile.CreateMobilePhone(0);
                                //    //RAGE.Game.Mobile.ScriptIsMovingMobilePhoneOffscreen(false);
                                //    //RAGE.Game.Mobile.SetMobilePhonePosition(0, 0, 0);
                                //}


                                if (PlayerState.IDEnabled)
                                {

                                    for (int i = 0; i < PlayerState.Friends.Count; i++)

                                    {
                                        try
                                        {
                                            if (((int)nametags[j].Player.GetSharedData("ClientID")) == PlayerState.Friends[i].ID)
                                            {

                                                string output = PlayerState.Friends[i].Name + " " + PlayerState.Friends[i].ID.ToString();

                                                RAGE.Game.UIText.Draw(output, new System.Drawing.Point((int)(RAGE.NUI.Game.ScreenResolution.Width * nametags[j].ScreenX), (int)(RAGE.NUI.Game.ScreenResolution.Height * nametags[j].ScreenY)), 0.25f, System.Drawing.Color.Yellow, RAGE.Game.Font.ChaletLondon, true);

                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    // Chat.Output("ID");
                                    // RAGE.Game.UIText.Draw(nametagData.Player.GetData<string>("ID"), new System.Drawing.Point(nametagData.ScreenX, nametagData.ScreenY), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);
                                    //     RAGE.Game.Graphics.DrawDebugText("’”…", nametagData.ScreenX, nametagData.ScreenY, 1f, 255, 255, 255, 255);

                                    // RAGE.Game.Graphics.DrawSprite("cross", "circle_checkpoints_cross", nametagData.ScreenX, nametagData.ScreenY, 0.1f, 0.1f, 0f, 255, 255, 255, 255, 0);

                                    //var formattedName = nametagData.Player.Name.Replace("_", " ");

                                    string outp = nametags[j].Player.GetSharedData("ClientID").ToString();
                                    //var output = $"{formattedName}({nametagData.Player.RemoteId})";
                                    RAGE.Game.UIText.Draw(outp, new System.Drawing.Point((int)(RAGE.NUI.Game.ScreenResolution.Width * nametags[j].ScreenX), (int)(RAGE.NUI.Game.ScreenResolution.Height * nametags[j].ScreenY)), 1.0f, System.Drawing.Color.White, RAGE.Game.Font.ChaletLondon, true);

                                    //  RAGE.NUI.UIResText.Draw("’”…", 0, 0, RAGE.Game.Font.ChaletComprimeCologne, 0.3f, System.Drawing.Color.Aqua, RAGE.NUI.UIResText.Alignment.Centered, false, false, 0);


                                    //    RAGE.Game.Graphics.
                                    // RAGE.Game.Graphics.DrawDebugText2d(nametagData.Player.GetData<int>("ID").ToString(), nametagData.ScreenX, nametagData.ScreenY,1f, 255, 255, 255, 255);
                                    //nametagData.Player.name
                                    //   Chat.Output(nametagData.Player.GetData<int>("ID").ToString());
                                }

                                try
                                {
                                    if ((bool)nametags[j].Player.GetSharedData("Talk"))
                                    {
                                        //  RAGE.Game.Graphics.DrawSprite("cross", "circle_checkpoints_cross", nametagData.ScreenX, nametagData.ScreenY, 1f, 1f, 0f, 255, 255, 255, 255, 0);

                                        RAGE.Game.UIText.Draw("~y~∞", new System.Drawing.Point((int)(RAGE.NUI.Game.ScreenResolution.Width * nametags[j].ScreenX), (int)(RAGE.NUI.Game.ScreenResolution.Height * nametags[j].ScreenY)), 0.5f, System.Drawing.Color.White, RAGE.Game.Font.ChaletLondon, true);



                                        //    RAGE.Game.Graphics.DrawRect(nametagData.ScreenX, nametagData.ScreenY, 0.1f, 0.1f, 255, 255, 255, 255, 0);
                                        if (!nametags[j].Player.GetData<bool>("Draw"))
                                        {




                                            nametags[j].Player.SetData<bool>("Draw", true);
                                            nametags[j].Player.PlayFacialAnim("mic_chatter", "mp_facial");

                                        }

                                    }
                                    else
                                    {
                                        nametags[j].Player.SetData<bool>("Draw", false);
                                        nametags[j].Player.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
                                    }
                                }
                                catch
                                {

                                }







                                //   if (     RAGE.Game.Invoker.Invoke<bool>(RAGE.Game.Natives.NetworkIsGamerTalking,nametagData.Player))
                                //    if (nametagData.Player.GetData<bool>("Voice"))
                                //  RAGE.Game.Graphics.DrawDebugText2d(nametagData.Player.RemoteId.ToString(), nametagData.ScreenX, nametagData.ScreenY,  1, 255, 255, 255, 100);
                                //  RAGE.Game.Graphics.DrawRect(nametagData.ScreenX, nametagData.ScreenY,0.1f,0.1f, 255, 255, 255, 255,0);

                            }




                    } //  player.DrawInfo.Draw(nametags);




                    //player.Skills.StaminaTimeSet();


                    //     RAGE.Game.UIText.Draw(browsers.ClothesMarket.color.ToString(), new System.Drawing.Point(500, 500), 0.5f, System.Drawing.Color.GreenYellow, RAGE.Game.Font.Monospace, true);




                    //







                    //if (tickcount % 500 == 0)
                    //{

                    //    if (Menu.ready)
                    //    {
                    //     //   Events.CallRemote("updateParametrs.server");

                    //        try
                    //        {
                    //            List<RAGE.Elements.Vehicle> vehicles = RAGE.Elements.Entities.Vehicles.All;
                    //            for (int i = 0; i < vehicles.Count; i++)
                    //            {
                    //                if ((bool)vehicles[i].GetSharedData("Laggage"))
                    //                {


                    //                    vehicles[i].SetDoorOpen(5, false, false);



                    //                }
                    //                else
                    //                {
                    //                    vehicles[i].SetDoorShut(5, false);
                    //                }
                    //            }
                    //        }
                    //        catch
                    //        {

                    //        }
                    //    }







                    //    //      mp.game.graphics.notify("¿ÍÍ‡ÛÌÚ ÛÒÔÂ¯ÌÓ ÒÓı‡ÌÂÌ");
                    //}



                    //if (DrugTimer)
                    //{

                    //    Task.Factory.StartNew(() =>
                    //    {

                    //        if (tickcount - cs_packages.player.Skills.DrugTimeNow >= 10000)
                    //        {
                    //            DrugTimer = false;
                    //            cs_packages.player.Skills.DrugTimeNow = 0;
                    //            RAGE.Events.CallRemote("drugtimer.stop");



                    //        }
                    //    });


                    //}






                    //if (tickcount % 1000 == 0)
                    //{

                    //    Task.Factory.StartNew(() =>
                    //    {

                    //    });

                    //}






                    //RAGE.Game.Pad.DisableControlAction(0, 20, true);
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(0, 20))
                    //{

                    //    RAGE.Voice.Muted = false;
                    //    RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mic_chatter", "mp_facial");
                    //    Menu.hudCef.ExecuteJs("microphoneStatus(true);");
                    //    Events.CallRemote("PlayerTalk.server");
                    //    //    Chat.Output(RAGE.Elements.Player.LocalPlayer.GetSharedData("Talk").ToString());

                    //    ///   RAGE.Game.Graphics.StartScreenEffect("DrugsMichaelAliensFightIn",10000,false); - ˝ÙÙÂÍÚ ˝Í‡Ì‡

                    //}
                    //if (RAGE.Game.Pad.IsDisabledControlJustReleased(0, 20))
                    //{

                    //    RAGE.Voice.Muted = true;
                    //    RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
                    //    Events.CallRemote("PlayerStopTalk.server");
                    //    //    Chat.Output(RAGE.Elements.Player.LocalPlayer.GetSharedData("Talk").ToString());

                    //    Menu.hudCef.ExecuteJs("microphoneStatus(false);");

                    //}





                    //if (tickcount > Int64.MaxValue - 100)
                    //{
                    //    tickcount = 0;
                    //}

                    //if (tickcount % 60 == 0)
                    //{

                    //    // Chat.Output(Win32.IsFocusWindow().ToString());

                    //        if (RAGE.Elements.Player.LocalPlayer.Vehicle != null && RAGE.Elements.Player.LocalPlayer.Vehicle.GetPedInSeat(-1, 1) == RAGE.Elements.Player.LocalPlayer.Handle)
                    //        {
                    //            if (SpeedClass != null)
                    //            {
                    //                SpeedClass.UpdateDistation();
                    //            }
                    //            else
                    //            {
                    //                SpeedClass = new Speed();
                    //            }
                    //        }
                    //        else if (SpeedClass != null)
                    //        {
                    //            SpeedClass.DestroyCef();
                    //            SpeedClass = null;
                    //            //   Speed.fuelLevel = 999f;
                    //        }


                    //}
                    //else
                    //if (tickcount % 1001 == 0)
                    //{
                    //   // Chat.Output(Win32.IsFocusWindow().ToString());
                    //        if (RAGE.Elements.Player.LocalPlayer.IsInAnyVehicle(false))
                    //        {
                    //            if (RAGE.Elements.Player.LocalPlayer.Vehicle != null && RAGE.Elements.Player.LocalPlayer.Vehicle.GetPedInSeat(-1, 1) == RAGE.Elements.Player.LocalPlayer.Handle)
                    //            {
                    //                if (SpeedClass != null)
                    //                {
                    //                    SpeedClass.SaveCar();
                    //                }

                    //            }
                    //            else
                    //            {
                    //                if (SpeedClass != null)
                    //                {
                    //                    SpeedClass.DestroyCef();
                    //                    SpeedClass = null;
                    //                }
                    //            }
                    //        }
                    //        //else if (Speed.speedometrCef != null)
                    //        //{
                    //        //    Speed.speedometrCef.Destroy();
                    //        //    Speed.speedometrCef = null;
                    //        //}


                    //}








                    //KeyManager.KeyBind(KeyManager.KeyPhone, () =>
                    //{

                    //    Chat.Output("button press");



                    //});


                    //RAGE.Game.Pad.DisableControlAction(1, 26, true); // C

                    //RAGE.Game.Pad.DisableControlAction(2, 79, true); // C
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(1, 26))
                    //{
                    //    if (!carcheck)
                    //    {
                    //        if (Vehicle.CarMenu != null)
                    //        {
                    //            Vehicle.OpenCarMenu(null);

                    //        }
                    //        else
                    //        {
                    //            CaraMenu();
                    //        }
                    //    }

                    //}
                    //else
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(2, 79))
                    //{
                    //    if (!carcheck)
                    //    {
                    //        if (Vehicle.CarMenu != null)
                    //        {
                    //            Vehicle.OpenCarMenu(null);

                    //        }
                    //        else
                    //        {
                    //            CaraMenu();
                    //        }
                    //    }

                    //}

                    //KeyManager.KeyBind(KeyManager.KeyP, () =>
                    //{


                    //});


                    //RAGE.Game.Pad.DisableControlAction(0, 249, true); // N
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(0, 249))
                    //{




                    //}


                    ////KeyManager.KeyBind(KeyManager.KeyU, () =>
                    ////{


                    ////});
                    //RAGE.Game.Pad.DisableControlAction(0, 86, true);



                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(0, 86))
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {
                    //        if (KeyManager.block == 0)
                    //            Events.CallRemote("MoveToMarker");

                    //    });
                    //}

                    //   if(RAGE.Game.Ui.GetPauseMenuState())

                    //KeyManager.KeyBind(KeyManager.KeyE, () =>
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {
                    //        if (KeyManager.block == 0)
                    //            Events.CallRemote("MoveToMarker");

                    //    });

                    //});
                    //    KeyManager.KeyBind(KeyManager.KeyT, () =>
                    //    {

                    //            if (KeyManager.block == 0)
                    //                KeyManager.block = 2;


                    //    // Events.CallRemote("MoveToMarker");
                    //});


                    //–¿ƒ»Œ

                    //KeyManager.KeyBind(KeyManager.KeyK, () =>
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {

                    //        if (KeyManager.block == 0 || KeyManager.block == 8)
                    //        {
                    //            Events.CallRemote("OpenCloseRadio");
                    //            // browsers.RadioStation.Open();
                    //        }
                    //    });


                    //    // Events.CallRemote("MoveToMarker");
                    //});


                    //RAGE.Game.Pad.DisableControlAction(0, 19, true); // L alt
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(0, 19))
                    //{

                    //        if (KeyManager.block == 0 || KeyManager.block == 12)
                    //        {
                    //            browsers.Usability.OpenUsability();
                    //        }

                    //}


                    //KeyManager.KeyBind(KeyManager.KeyO, () =>
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {


                    //    });


                    //    // Events.CallRemote("MoveToMarker");
                    //});




                    //KeyManager.KeyBind(KeyManager.KeyX, () =>
                    //{

                    //    //Task.Factory.StartNew(() =>
                    //    //{
                    //    //    if (KeyManager.block == 0 || KeyManager.block == 1)
                    //    //        Events.CallRemote("RadioMuted");

                    //    //});

                    //    // Events.CallRemote("MoveToMarker");
                    //});


                    //NUM 9 
                    //KeyManager.KeyBind(KeyManager.KeyNum9, () =>
                    //{


                    //    if (KeyManager.block == 0 || KeyManager.block == 1)
                    //        player.PlayerState.IDEnabled = !player.PlayerState.IDEnabled;


                    //// Events.CallRemote("MoveToMarker");
                    //});




                    //RAGE.Game.Pad.DisableControlAction(0, 244, true); //M
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(0, 244))
                    //{



                    //}




                    //    KeyManager.KeyBind(KeyManager.KeyY, () =>
                    //    {

                    //        Task.Factory.StartNew(() =>
                    //        {

                    //            if (Inventory.give)
                    //                Inventory.GiveTrue();
                    //        });

                    //    // Events.CallRemote("MoveToMarker");
                    //});







                    //RAGE.Game.Pad.DisableControlAction(0, 27, true); //Middle mouse button
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(0, 27))
                    //{

                    //    if (KeyManager.block == 3 || KeyManager.block == 0)
                    //    {
                    //        Menu.OpenAnimList();
                    //        //Events.CallRemote("MoveToMarker");
                    //    }

                    //}




                    //    KeyManager.KeyBind(KeyManager.KeyL, () =>
                    //    {


                    //    //Task.Factory.StartNew(() =>
                    //    //{

                    //    //    if (KeyManager.block == 3 || KeyManager.block == 0)
                    //    //    {
                    //    //        Menu.OpenAnimList();
                    //    //        //Events.CallRemote("MoveToMarker");
                    //    //    }
                    //    //});

                    //});


                    //    KeyManager.KeyBind(KeyManager.KeyEnter, () =>
                    //    {

                    //            if (KeyManager.block == 2)
                    //                KeyManager.block = 0;



                    //    // Events.CallRemote("MoveToMarker");
                    //});


                    //KeyManager.KeyBind(KeyManager.KeyN, () =>
                    //{


                    //    // Events.CallRemote("MoveToMarker");
                    //});

                    //KeyManager.KeyBind(KeyManager.KeyF2, () =>
                    //{
                    ////   RAGE.Elements.MapObject mapObject = new RAGE.Elements.MapObject(113622690, ped.Position, new Vector3());





                    //});
                    //KeyManager.KeyBind(KeyManager.KeyF7, () =>
                    //{
                    ////   RAGE.Elements.MapObject mapObject = new RAGE.Elements.MapObject(113622690, ped.Position, new Vector3());

                    //Cursor.Visible = !Cursor.Visible;

                    //});



                    //KeyManager.KeyBind(KeyManager.KeyF5, delegate
                    //{


                    //    



                    //});




                    //RAGE.Game.Pad.DisableControlAction(30, 39, true); //ı
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(30, 39))
                    //{
                    //    if (KeyManager.block == 0)
                    //    {
                    //           Events.CallRemote("CarInvOpenClient");





                    //    }
                    //}


                    //RAGE.Game.Pad.DisableControlAction(30, 40, true); //ı
                    //if (RAGE.Game.Pad.IsDisabledControlJustPressed(30, 40))
                    //{

                    //        if (KeyManager.block == 4 || KeyManager.block == 0)
                    //        {



                    //            //Events.CallRemote("setweapon", RAGE.Elements.Player.LocalPlayer.GetAmmoInWeapon(Convert.ToUInt32(args[0])));

                    //            if (Inventory.inventoryCef != null)
                    //            {
                    //                Inventory.inventoryCef.Destroy();
                    //                Inventory.inventoryCef = null;
                    //                KeyManager.block = 0;
                    //                //     Chat.Show(true);

                    //                Cursor.Visible = false;

                    //            }
                    //            else
                    //            {
                    //                Events.CallRemote("getinventary.server");
                    //            }


                    //        }


                    //}


                    //KeyManager.KeyBind(KeyManager.KeyI, () =>
                    //{


                    //    // Inventory.OpenInventory();
                    //});




                }
            });
        }
    }







}