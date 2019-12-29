using cs_packages.client;
using RAGE;
using RAGE.Ui;
using System;
using System.Globalization;

namespace cs_packages.browsers
{
    class MakeUpSalon : Events.Script
    {
        #region Переменные
        float
              facialHair = 0, facialHairOpacity = 1,
              makeup = 0, makeupOpacity = 1,
              blush = 0, blushOpacity = 0,
              lipstick = 0, lipstickOpacity = 1;



        RAGE.Elements.Player localPlayer = RAGE.Elements.Player.LocalPlayer;
        static public HtmlWindow salonM = null;
        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
        static RAGE.Elements.Ped ped_0;
        static RAGE.Elements.Ped ped_1;
        static RAGE.Elements.Ped ped_2;
        static RAGE.Elements.Ped ped_3;
        static RAGE.Elements.Ped ped_4;
        static RAGE.Elements.Ped ped_5;
        static RAGE.Elements.Ped ped_6;
        public float forcamX;
        public float forcamY;
        public float forcamZ;
        int camera;
        public float tempforcamX;
        public float tempforcamY;
        public float tempforcamZ;
        float rotcamZ;
        float rotcamX;
        float rotcamY;
        float mod = 0.5f;
        int sex;


        int hairItem = 0,
            hairColors = 0;
        int beardColor = 0,
            blushColor = 0,
            lipstickColor = 0;

        #endregion


        public MakeUpSalon()
        {
            Events.Add("open.makeupsalon", OpenMakeUpSalon);
            Events.Add("cameraMakeup", Control);
            Events.Add("resetCameraMakeup", ResetCamera);
            Events.Add("makeupRange", InputsRange);
            Events.Add("selectColor", Colors);
            Events.Add("makeupBuyButton", MakeupBuyButton);    //допилить серверную часть            
            Events.Add("makeupExit", MakeupExit);
            //Events.Add("Refresh_HTML_MakeUp", Reload_HTML_MakeUp);
            Events.Add("payOk", PayOk);

        }


        public void OpenMakeUpSalon(object[] args)
        {
            KeyManager.block = 2;
            salonM = new HtmlWindow("package://auth/assets/makeup.html");
            salonM.Active = true;
            Chat.Show(false);

  			Events.CallRemote("RemoveAllWeapons");

            facialHair = (float)args[1];
            facialHairOpacity = (float)args[2];
            makeup = (float)args[3];
            makeupOpacity = (float)args[4];
            blush = (float)args[5];
            blushOpacity = (float)args[6];
            lipstick = (float)args[7];
            lipstickOpacity = (float)args[8];

            hairItem = (int)args[9];
            hairColors = (int)args[10];
            beardColor = (int)args[11];
            blushColor = (int)args[12];
            lipstickColor = (int)args[13];

            //"makeup_rockfordhills"
            ped_0 = new RAGE.Elements.Ped(0xA5720781, new Vector3(-815.9022f, -183.4249f, 37.5689f), 25f - 10f);
            ped_0.ActivatePhysics();
            ped_0.SetVisible(true, true);
            ped_0.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"makeup_devis"   
            ped_1 = new RAGE.Elements.Ped(0xA5720781, new Vector3(137.9725f, -1708.532f, 29.30162f), 225f - 10f);
            ped_1.ActivatePhysics();
            ped_1.SetVisible(true, true);
            ped_1.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"makeup_vespuchhi"
            ped_2 = new RAGE.Elements.Ped(0xA5720781, new Vector3(-1282.498f, -1118.221f, 7.000125f), 176.7643f - 10f);
            ped_2.ActivatePhysics();
            ped_2.SetVisible(true, true);
            ped_2.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"makeup_sandyshores"
            ped_3 = new RAGE.Elements.Ped(0x9D3DCB7A, new Vector3(1932.486f, 3731.173f, 32.85444f), 295f - 10f);
            ped_3.ActivatePhysics();
            ped_3.SetVisible(true, true);
            ped_3.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"makeup_mirrorpark"
            ped_4 = new RAGE.Elements.Ped(0xA5720781, new Vector3(-1212.493f, -473.8974f, 66.21803f), 168f - 10f);
            ped_4.ActivatePhysics();
            ped_4.SetVisible(true, true);
            ped_4.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"makeup_havik"
            ped_5 = new RAGE.Elements.Ped(0xA5720781, new Vector3(-34.13052f, -152.3357f, 57.08652f), 71.83329f - 10f);
            ped_5.ActivatePhysics();
            ped_5.SetVisible(true, true);
            ped_5.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"makeup_paletobey"
            ped_6 = new RAGE.Elements.Ped(0x9D3DCB7A, new Vector3(-278.6416f, 6226.972f, 31.70554f), 130f - 10f);
            ped_6.ActivatePhysics();
            ped_6.SetVisible(true, true);
            ped_6.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;


            forcamX = RAGE.Elements.Player.LocalPlayer.Position.X + 1.5f;
            forcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + 1.5f;
            forcamZ = RAGE.Elements.Player.LocalPlayer.Position.Z + 0.8f;
            tempforcamX = forcamX;
            tempforcamY = forcamY;
            tempforcamZ = forcamZ;
            rotcamX = forcamX;
            rotcamY = forcamY;
            rotcamZ = forcamZ;

            camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), forcamX, forcamY, forcamZ, -20.0f, 0.0f, 180.0f, 70.0f, true, 2);
            RAGE.Game.Cam.SetCamActive(camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

            Cursor.Visible = true;
            ResetCamera(null);
            sex = (int)args[0];
            if (sex == 0)
            {
                salonM.ExecuteJs("initializeMakeup('man');");
              //  Chat.Output(sex.ToString());
            }
            if (sex == 1)

            {
                salonM.ExecuteJs("initializeMakeup('girl');");
              //  Chat.Output(sex.ToString());
            }





        }

        public void Control(object[] args)
        {
            if (args[0].ToString().Contains("cameraHeight"))
            {
                RAGE.Game.Cam.SetCamCoord(camera, rotcamX, rotcamY, tempforcamZ + 0.3f * float.Parse(args[1].ToString(), formatter));
                rotcamZ = tempforcamZ + 0.3f * float.Parse(args[1].ToString(), formatter);
            }



            if (args[0].ToString().Contains("cameraRotate"))
            {
                float statX = tempforcamX - 1.5f;
                float statY = tempforcamY - 1.5f;
                float angle = ((float.Parse(args[1].ToString(), formatter) * (-180)) + 180);

                RAGE.Game.Cam.SetCamCoord(camera, RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod, RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod, rotcamZ);
                rotcamX = RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod;
                rotcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod;

                RAGE.Game.Cam.SetCamRot(camera, 0, 0, angle + 90, 2);




            }






        }

        public void ResetCamera(object[] args)
        {


            RAGE.Game.Cam.SetCamCoord(camera, rotcamX, rotcamY, tempforcamZ);
            rotcamZ = tempforcamZ + 0;


            float statX = tempforcamX - 1.5f;
            float statY = tempforcamY - 1.5f;
            float angle = (0 * (-180)) + 180;

            RAGE.Game.Cam.SetCamCoord(camera, RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod, RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod, rotcamZ);
            rotcamX = RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod;
            rotcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod;

            RAGE.Game.Cam.SetCamRot(camera, 0, 0, angle + 90, 2);
        }

        public void InputsRange(object[] args)
        {

            byte overlayID = 0;
            if (Convert.ToByte(args[2]) == 0)
            {
                overlayID = 255;
            }
            else
            {
                overlayID = Convert.ToByte(args[2]);

            }
            // //  Chat.Output(args[0].ToString() + "   " + args[1].ToString() + "   " + args[2].ToString() + "   ");
            string ForSearch = args[0].ToString();




            switch (ForSearch)
            {


                case "beard":
                    {
                        facialHair = overlayID;
                        facialHairOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                        //    localPlayer.setHeadOverlay(1, overlayID, facialHairOpacity, beardColor, 0);
                        localPlayer.SetHeadOverlay(1, overlayID, facialHairOpacity);
                        localPlayer.SetHeadOverlayColor(1, 1, beardColor, 0);

                        break;
                    }


                case "makeup":
                    {
                        makeup = overlayID;
                        makeupOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                        //  localPlayer.setHeadOverlay(4, overlayID, makeupOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(4, overlayID, makeupOpacity);
                        localPlayer.SetHeadOverlayColor(4, 0, 0, 0);
                        break;
                    }
                case "blush":
                    {
                        blush = overlayID;
                        blushOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                        // localPlayer.setHeadOverlay(5, overlayID, blushOpacity, blushColor, 0);
                        localPlayer.SetHeadOverlay(5, overlayID, blushOpacity);
                        localPlayer.SetHeadOverlayColor(5, 2, blushColor, 0);
                        break;
                    }


                case "lipstick":
                    {
                        lipstick = overlayID;
                        lipstickOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                        //   localPlayer.setHeadOverlay(8, overlayID, lipstickOpacity, lipstickColor, 0);
                        localPlayer.SetHeadOverlay(8, overlayID, lipstickOpacity);
                        localPlayer.SetHeadOverlayColor(8, 2, lipstickColor, 0);

                        break;
                    }
                case "haircut":
                    {

                        //Chat.Output(args[0].ToString());
                        localPlayer.SetComponentVariation(2, (int)args[2], 0, 2);
                        hairItem = (int)args[2];

                        break;
                    }


            }

        }

        public void Colors(object[] args)
        {



            switch (args[0].ToString())
            {


                case "beard":
                    {
                        beardColor = (int)args[1];

                        localPlayer.SetHeadOverlayColor(1, 1, beardColor, 0);


                        break;
                    }


                case "makeup":
                    {

                        localPlayer.SetHeadOverlayColor(4, 0, (int)args[1], 0);

                        break;
                    }
                case "blush":
                    {
                        blushColor = (int)args[1];
                        localPlayer.SetHeadOverlayColor(5, 2, blushColor, 0);
                        break;
                    }


                case "lipstick":
                    {
                        lipstickColor = (int)args[1];
                        localPlayer.SetHeadOverlayColor(8, 2, lipstickColor, 0);

                        break;
                    }
                case "haircut":
                    {

                        //     localPlayer.SetComponentVariation(2, (int)args[0], 0, 2);
                        hairColors = (int)args[1];
                        localPlayer.SetHairColor(hairColors, 0);





                        break;
                    }









            }



        }

        public void MakeupBuyButton(object[] args)
        {

            int makeupPrice = Convert.ToInt32(args[1]);
            string cashService = args[0].ToString();


            switch (cashService)
            {
                case "card": //карточкой
                    {

                        Events.CallRemote("PayMakeupCard", makeupPrice,
                                          hairColors, hairItem, facialHair, facialHairOpacity,
                                          makeup, makeupOpacity, blush, blushOpacity,
                                          lipstick, lipstickOpacity, beardColor,
                                          blushColor, lipstickColor);
                        //Chat.Output("makeupPrice " + makeupPrice +
                        //                 " hairColors: " + hairColors + " hairItem: " + hairItem + " facialHair: " + facialHair + " facialHairOpacity: " + facialHairOpacity +
                        //                 " makeup: " + makeup + " makeupOpacity: " + makeupOpacity + " blush: " + blush + " blushOpacity: " + blushOpacity +
                        //                 " lipstick: " + lipstick + " lipstickOpacity: " + lipstickOpacity + " beardColor: " + beardColor +
                        //                 " blushColor: " + blushColor + " lipstickColor: " + lipstickColor);

                        break;
                    }
                case "cash": //наличкой
                    {

                        Events.CallRemote("PayMakeupCash", makeupPrice,
                                          hairColors, hairItem, facialHair, facialHairOpacity,
                                          makeup, makeupOpacity, blush, blushOpacity,
                                          lipstick, lipstickOpacity, beardColor,
                                          blushColor, lipstickColor);
                        //Chat.Output("makeupPrice " + makeupPrice +
                        //                 " hairColors: " + hairColors + " hairItem: " + hairItem + " facialHair: " + facialHair + " facialHairOpacity: " + facialHairOpacity +
                        //                 " makeup: " + makeup + " makeupOpacity: " + makeupOpacity + " blush: " + blush + " blushOpacity: " + blushOpacity +
                        //                 " lipstick: " + lipstick + " lipstickOpacity: " + lipstickOpacity + " beardColor: " + beardColor +
                        //                 " blushColor: " + blushColor + " lipstickColor: " + lipstickColor);

                        break;
                    }

            }
        }

        public void MakeupExit(object[] args)
        {
            ped_0.Destroy();
            ped_1.Destroy();
            ped_2.Destroy();
            ped_3.Destroy();
            ped_4.Destroy();
            ped_5.Destroy();
            ped_6.Destroy();
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            RAGE.Game.Cam.DestroyCam(camera, true);
            salonM.Destroy();
            Cursor.Visible = false;
            Chat.Show(true);


            Events.CallRemote("ExitSalon");
           // Events.CallRemote("setPlayerSkin");

            KeyManager.block = 0;
        }
        //public void Reload_HTML_MakeUp(object[] args)
        //{

        //    salonM.Reload(true);

        //}

        public void PayOk(object[] args)
        {
            salonM.ExecuteJs("makeupRefresh();");


        }



    }
}
