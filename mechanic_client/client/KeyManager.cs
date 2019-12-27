
using cs_packages.browsers;
using cs_packages.player;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace cs_packages.client
{
    public static class KeyManager
    {

        /// <summary>
        /// ���� ������ ������ ��������� ����������
        /// -2  - �������
        /// -1  - � ����
        /// 0 - �����
        /// 1 - ����
        /// 2 - ���
        /// 3 - ��������
        /// 4 - ���������
        /// 5 - ���� �����
        /// 6 - ������
        /// 7 - ����/��������
        /// 8 - �����
        /// 9 - �������
        /// 10 - �������
        /// 11 - �����
        /// 12 - ��������
        /// 13 - ���� ����
        /// 14 - ���
        /// 15 - �������
        /// </summary>
        public static int block = 0;
        public static RAGE.Elements.Player player;
        private const int ResetTime = 1000;    // Time to reset the key
        private static bool _keyStatus = true; // The state of the key
        public static RAGE.Elements.TextLabel label;
        public static Task t1;
        /// <summary>
        /// ����� ���� , ���� 1
        /// </summary>
        public const int KeyTilda = 0xC0;
        public const int KeyF5 = 0x74;
        public const int KeyNum9 = 0x69;

        public const int KeyPhone = 0xBA;
        /// <summary>
        /// ����� ����, ���� 2
        /// </summary>
        public const int KeyT = 0x54;
        public const int KeyB = 0x42;
        public const int KeyN = 0x4E;
        public const int KeyX = 0x58;
        public const int KeyK = 0x4B;
        public const int KeyO = 0x4F;
        public const int KeyP = 0xDB;
        public const int KeyU = 0x55;
        //public const int KeyT = 0x54;
        /// <summary>
        /// �������� ���������
        /// </summary>
        public const int KeyEnter = 0x0D;
        /// <summary>
        /// ��������� ��������
        /// </summary>
        public const int KeyE = 0x45;
        //public const int KeyT = 0x54;
        //public const int KeyT = 0x54;
        //public const int KeyT = 0x54;
        /// <summary>
        /// ����� ������� ��������, ���� 3
        /// </summary>
        public const int KeyL = 0x4C;
        public const int KeyF7 = 0x76;
        /// <summary>
        /// ������ ��������
        /// </summary>
        public const int KeyF2 = 0x71;

        /// <summary>
        /// ����� ���������
        /// </summary>
        public const int KeyI = 0x49;
        public const int KeyY = 0x59;


        static KeyManager()
        {
            Events.Add("KeyIpress", KeyIPress);
            Events.Add("KeyOpress", KeyOPress);
            Events.Add("KeyLpress", KeyLPress);
            Events.Add("KeyKpress", KeyKPress);
          //  Events.Add("KeyJpress", KeyJPress);
            Events.Add("KeyEpress", KeyEPress);
            Events.Add("KeyPress", KeyMenuPress);
            Events.Add("KeyNpress", KeyNpress);
            Events.Add("KeyMpress", KeyMpress);
            Events.Add("KeyF2press", KeyF2Press);
            Events.Add("KeyF5press", KeyF5Press);
            Events.Add("KeyF7press", KeyF7Press);
            Events.Add("KeyXpress", KeyXPress);
            Events.Add("KeyYpress", KeyYPress);
            Events.Add("KeyGpress", KeyGpress);
            Events.Add("KeyXpressDown", KeyXpressDown);
            Events.Add("KeyPolice", KeyPolice);
               Events.Add("KeyCtrlpress", KeyCtrlpress);

        }


        public static void KeyGpress(object[] args)
        {
            Action act = () =>
            {



                if (block == 0)
                {
                    Events.CallRemote("animate.server", "random@mugging3", "handsup_standing_base");
                    block = 3;
                }
            };

            KeyBind(act);



            // ��������� ��� �������� �� ������


        }

        public static void KeyKPress(object[] args)
        {
            Action act = () =>
            {
                int veh = mechanic_client.Mechanic_Client.GetVehicle(10.0f);
                uint modelVeh = RAGE.Game.Entity.GetEntityModel(veh);
                Vector3 coords_b = new Vector3();
                List<Vehicle> vehicles = Entities.Vehicles.All;
                Vector3 pos = Player.LocalPlayer.Position;
                Vehicle veh_obj = vehicles.Find(pl => pl.Handle == veh);
                int index_d = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "door_dside_f");
                if (index_d == -1)
                {
                    index_d = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "wheel_lf");
                }

                Vector3 coords_d = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index_d);

                int index_b = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "boot");
                if (index_b == -1)
                {
                    index_b = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "bumper_r");
                }
                if (index_b == -1)
                {
                    index_b = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "indicator_lr");
                }
                if (index_b == -1)
                {
                    index_b = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "brakelight_l");
                }
                if (index_b == -1)
                {
                    index_b = RAGE.Game.Entity.GetEntityBoneIndexByName(veh, "exhaust");
                }
                coords_b = RAGE.Game.Entity.GetWorldPositionOfEntityBone(veh, index_b);

                if (RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords_d.X, coords_d.Y, coords_d.Z) <= 1.5f && RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords_d.X, coords_d.Y, coords_d.Z) < RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords_b.X, coords_b.Y, coords_b.Z)  /*||  RAGE.Game.Vehicle.IsThisModelABike(modelVeh) || RAGE.Game.Vehicle.IsThisModelAQuadbike(modelVeh) || RAGE.Game.Vehicle.IsThisModelABicycle(modelVeh)|| RAGE.Game.Vehicle.IsThisModelABoat(modelVeh)*/)
                {

                    if (block == 13 || block == 0 || block == 1)
                    {
                        DrawInfo.LoadScreen = true;
                        Events.CallRemote("OpenCarMenuServer", veh_obj);
                    }
                }


                if (RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords_b.X, coords_b.Y, coords_b.Z) <= 1.5f && RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords_d.X, coords_d.Y, coords_d.Z) > RAGE.Game.Utils.Vdist(pos.X, pos.Y, pos.Z, coords_b.X, coords_b.Y, coords_b.Z))
                {
                    if (block == 0)
                    {

                        DrawInfo.LoadScreen = true;
                        //browsers.Phone.OpenClose();
                        Events.CallRemote("CarInvOpenClient", veh_obj);

                    }
                }


            };

            KeyBind(act);

            // ��������� ��� �������� �� ������

        }

      //  public static int GetVehicle(float radius)
      //  {
      //      Vector3 pos = Player.LocalPlayer.Position;
      //      if (Player.LocalPlayer.IsSittingInAnyVehicle())
      //      {
      //          return Player.LocalPlayer.GetVehicleIsIn(true);
      //      }
      //      else
      //      {
      //          int veh = RAGE.Game.Vehicle.GetClosestVehicle(pos.X + 0.0001f, pos.Y + 0.0001f, pos.Z + 0.0001f, radius + 0.0001f, 0, 8192 + 4096 + 4 + 2 + 1);
      //          if (!RAGE.Game.Entity.IsEntityAVehicle(veh))
      //          {
      //              veh = RAGE.Game.Vehicle.GetClosestVehicle(pos.X + 0.0001f, pos.Y + 0.0001f, pos.Z + 0.0001f, radius + 0.0001f, 0, 4 + 2 + 1);
      //              return veh;
      //          }
      //          else
      //          {
      //              return veh;
      //          }
      //
      //
      //      }
      //
      //  }

        public static void KeyCtrlpress(object[] args)
        {
            Action act = () =>
            {
                DrawInfo.target = null;
                player = null;
                DrawInfo.playerLight = false;
             
                //if (block == 13 || block == 0 || block == 1)
                //{
                //  DrawInfo.LoadScreen = true;
                // PoliceCircle.OpenClose();
              


                    //Chat.Output("Cam " + RAGE.Game.Cam.GetGameplayCamRelativeHeading().ToString());
                    //Chat.Output("Pl " + RAGE.Elements.Player.LocalPlayer.GetHeading().ToString());
                    List<RAGE.Elements.Player> players = RAGE.Elements.Entities.Players.All;
                Chat.Output("Client find " + players.Count.ToString() + "players");
             

                for (int i = 0; i < players.Count; i++)
                    {
                        if (players[i] != RAGE.Elements.Player.LocalPlayer)
                            if (Math.Abs(players[i].Position.DistanceTo(RAGE.Elements.Player.LocalPlayer.Position)) < 3f)
                            {
                            Chat.Output("Client " + players[i].Name + " in range");




                            Vector3 napravl = RAGE.Elements.Player.LocalPlayer.Position;
                                Vector3 normalize = players[i].Position;
                                double rad = Math.Atan2(napravl.Y - normalize.Y, napravl.X - normalize.X);
                                double grad = ((rad * 180) / 3.14) + 90.0;
                                if (grad < 0)
                                    grad = 360 + grad;

                          //  Chat.Output(Math.Abs(grad - RAGE.Elements.Player.LocalPlayer.GetHeading() - RAGE.Game.Cam.GetGameplayCamRelativeHeading()).ToString());
                            double result = Math.Abs(grad - RAGE.Elements.Player.LocalPlayer.GetHeading() - RAGE.Game.Cam.GetGameplayCamRelativeHeading());
                            if(result >= 360)
                            {
                                result -= 360;
                            }
                            if (result < 25)
                                {
                                Chat.Output("Try to call" + players[i].Name);
                                DrawInfo.target = players[i];
                                    player = DrawInfo.target;
                                    DrawInfo.playerLight = true;
                                    DrawInfo.leter = "H";
                                  

                                    return;
                                }


                            }
                    }
               



            };

            KeyBind(act);



            // ��������� ��� �������� �� ������


        }
        public static void KeyYPress(object[] args)
        {

            Action act = () =>
            {
                if (block == 4 || block == 0)
                    if (Inventory.give)
                    {
                        Inventory.GiveTrue();
                    }
                else
                    {
                        Events.CallRemote("NotifyClient");
                    }

            };

            KeyBind(act);







        }

        public static void KeyXPress(object[] args)
        {

            //Action act = () =>
            //{
            RAGE.Game.Audio.PlaySoundFrontend(1, "Off_High", "MP_RADIO_SFX", true);
            Task.Factory.StartNew(() =>
            {

                RAGE.Voice.Muted = true;
                RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
                Events.CallRemote("PlayerStopTalk.server");


                Menu.hudCef.ExecuteJs("microphoneStatus(false);");
            });



            //};

            //KeyBind(act);






        }
        public static void KeyXpressDown(object[] args)
        {
            RAGE.Game.Audio.PlaySoundFrontend(1, "End_Squelch", "CB_RADIO_SFX", true);
            Task.Factory.StartNew(() =>
            {




                RAGE.Voice.Muted = false;
                RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mic_chatter", "mp_facial");
                Menu.hudCef.ExecuteJs("microphoneStatus(true);");
                Events.CallRemote("PlayerTalk.server");
            });

        }



        public static void KeyF2Press(object[] args)
        {


            Action act = () =>
            {
                Events.CallRemote("stopAnimation");
                if (block == 3)
                    block = 0;

            };

            KeyBind(act);



        }
        public static void KeyF7Press(object[] args)
        {
            Action act = () =>
            {
                Cursor.Visible = !Cursor.Visible;

            };

            KeyBind(act);

        }
        public static void KeyF5Press(object[] args)
        {
            Action act = () =>
            {
                Browser.CloseAllBrowsersAndDefreese();
                if (block == 0)
                {
                    block = 77;
                    Chat.Output("Key blocked");
                    return;
                }

                if (block == 77)

                {
                    block = 0;
                    Chat.Output("Key unblocked");
                    return;
                }

            };

            KeyBind(act);

        }


        public static void KeyPolice(object[] args)
        {
            if (block == 0 || block == 12)
            {
                Action act = () =>
            {
               if(Usability.UsabilityBrowser==null)
               DrawInfo.LoadScreen = true;
              
                if (player != null)
                {
                    Events.CallRemote("OpenUsabilityClient", player);
                    player = null;
                }
                else
                {
                    Events.CallRemote("OpenUsabilityClient", null);
                }

                //browsers.Usability.OpenUsability();

            };

                KeyBind(act);



            }
        }


        public static void KeyNpress(object[] args)
        {
            if (block == 15 || block == 0 || block == 1)
            {
                Action act = () =>
            {
                DrawInfo.LoadScreen = true;
                Tablet.InPDVehicle();

            };

                KeyBind(act);
            }

            //Vehicle.OpenCarMenu();

        }


        public static void KeyMpress(object[] args)
        {
            if (block == 0 || block == 1)
            {
                Action act = () =>
            {
                DrawInfo.LoadScreen = true;
                //browsers.Phone.OpenClose();
                Events.CallRemote("OpenClosePhone");

            };

                KeyBind(act);



            }
        }




        public static void Cars()
        {

            if (block == 13 || block == 0 || block == 1)
            {
                RAGE.Elements.Vehicle vehicle = null;
                if (!RAGE.Elements.Player.LocalPlayer.IsInAnyVehicle(false))
                {
                    try
                    {
                        List<RAGE.Elements.Vehicle> vehicles = RAGE.Elements.Entities.Vehicles.All;
                        //    RAGE.Elements.Vehicle vehicle = vehicles.Find(veh => veh.Position.DistanceTo2D(pos) <= 5f);
                        for (int i = 0; i < vehicles.Count; i++)
                        {
                            if (vehicles[i].Position.DistanceTo2D(RAGE.Elements.Player.LocalPlayer.Position) <= 4f)
                            {
                                vehicle = vehicles[i];

                            }
                        }
                    }
                    catch
                    {
                        vehicle = null;
                    }
                }
                else
                {
                    try
                    {
                        if (RAGE.Elements.Player.LocalPlayer.IsInAnyVehicle(false))
                            vehicle = RAGE.Elements.Player.LocalPlayer.Vehicle;
                    }
                    catch
                    {
                        vehicle = null;
                    }
                }
                //  Chat.Output("FindCar");
                if (vehicle != null)
                {
                    try
                    {
                        Events.CallRemote("OpenCloseMenuCar", vehicle.GetNumberPlateText());
                    }
                    catch
                    {

                    }
                }






            }
        }




        public static void KeyLPress(object[] args)
        {
            if (block == 3 || block == 0)
            {
                Action act = () =>
            {
                //browsers.Phone.OpenClose();
                Menu.OpenAnimList();

            };

                KeyBind(act);



                //Events.CallRemote("MoveToMarker");
            }
        }


        public static void KeyEPress(object[] args)
        {

            if (block == 1 || block == 0)
            {
                Action act = () =>
                {
                    Chat.Output("E");
                    //browsers.Phone.OpenClose();
                    Events.CallRemote("MoveToMarker");

                };

                KeyBind(act);

            }
        }




        public static void KeyOPress(object[] args)
        {

            if (block == 0)
            {
                Action act = () =>
                {
                    DrawInfo.LoadScreen = true;
                    //browsers.Phone.OpenClose();
                    Events.CallRemote("CarInvOpenClient");

                };

                KeyBind(act);






            }
        }


        public static void KeyIPress(object[] args)
        {


            if (block == 0)
            {
                Action act = () =>
                {
                    DrawInfo.LoadScreen = true;
                    Events.CallRemote("getinventary.server");


                };

                KeyBind(act);




                //Events.CallRemote("setweapon", RAGE.Elements.Player.LocalPlayer.GetAmmoInWeapon(Convert.ToUInt32(args[0])));






            }
        }

        public static void KeyMenuPress(object[] arg)
        {


            if (block == 1 || block == 0)
            {
                Action act = () =>
                {
                    //browsers.Phone.OpenClose();
                    Menu.OpenMenu();

                };

                KeyBind(act);
            }

        }



        public static void KeyBind(Action action)
        {

            // if (!Input.IsDown(key) || !_keyStatus) return;
            if (!_keyStatus) return;
            action.Invoke();
            _keyStatus = false;
            Task.Delay(ResetTime).ContinueWith((task) => { _keyStatus = true; });
        }

    }

}