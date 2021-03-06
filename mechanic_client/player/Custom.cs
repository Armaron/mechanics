﻿using RAGE;
using RAGE.Ui;
using System.Linq;
using cs_packages.client;
using System;
using System.Globalization;

namespace cs_packages.player
{
    class Custom : Events.Script
    {
        RAGE.Elements.Player localPlayer = RAGE.Elements.Player.LocalPlayer;
        int Camera;
        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        float noseWidth = -1, noseHeight = -1, noseLength = -1, noseBridge = -1, noseTip = -1, noseBridgeShift = -1,
    browHeight = -1, browWidth = -1, cboneHeight = -1, cboneWidth = -1, cheekWidth = -1, eyes = -1, lips = -1,
    jawWidth = -1, chinLength = -1, chinPos = -1, chinWidth = -1, chinShape = -1, neckWidth = -1, blemishes = 255, blemishesOpacity = 1, facialHair = 255, facialHairOpacity = 1, eyebrows =255, eyebrowsOpacity = 1,
    ageing = 255, ageingOpacity = 1, makeup = 255, makeupOpacity = 1, blush = 255, blushOpacity = 1, complexion = 255, complexionOpacity = 1,
    sundamage = 255, sundamageOpacity = 1, lipstick = 255, lipstickOpacity = 1, freckles = 255, frecklesOpacity = 1, chestHair = 255, chestHairOpacity = 1;

        int father = 0,
    mother = 0;
        float resemblance = 0,
           skinTone = 0;
        string gender = "false";
        int hairItem = 0,
         eyeColor = 0,
         hairColors = 0;
        int eyebrowColor = 0, beardColor = 0, chestHairColor = 0, blushColor = 0, lipstickColor = 0;
	
        public  Custom()
        {

            Events.Add("custom.create", CustomCreate);
            Events.Add("custom.destroy", CustomDestroy);
            Events.Add("inputsRange.client", InputsRange);

            Events.Add("custom.clientCreate", ClientCreate);

            Events.Add("creator_GenderChange.client", GenderChange);


            Events.Add("hairsList.client", HairsList);
            Events.Add("hairsColor.client", HairsColor);
            Events.Add("eyesColor.client", EyesColor);

            Events.Add("colors.client", Colors);
            Events.Add("custom.client", CustomClient);
            Events.Add("custom.db", CustomDB);
            Events.Add("register.complete", RegisterComplite);

        }
        public void CustomCreate(object[] args)
        {
            localPlayer.FreezePosition(true);
            Browser.CreateBrowserEvent(new object[] { "package://auth/assets/custom.html" });
            Camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), Constants.Camera_Creator.X, Constants.Camera_Creator.Y + 0.5f, Constants.Camera_Creator.Z,
            -10.0f, 0.0f, 0, 90.0f, true, 2);
          
            RAGE.Game.Cam.SetCamActive(Camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);


            RAGE.Game.Ui.DisplayHud(false);
            RAGE.Game.Ui.DisplayRadar(false);


            Events.CallRemote("positionCamera.server");
            DrawServer();


            //bBind(); // обработчик F3

            //blockKeys = true;
            //bindKeys(blockKeys);



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
                overlayID =  Convert.ToByte(args[2]);

            }
         //   Chat.Output(args[0].ToString() + "   " + args[1].ToString() + "   " + args[2].ToString() + "   ");
            string ForSearch = args[0].ToString();
          

         
            
            switch (ForSearch)
            {
                case "skinTone":
                    {
                        skinTone = float.Parse(args[1].ToString(), formatter) * 0.01f;
                        updateParents();
                        break;
                    }
                case "resemblance":

                    {
                        resemblance = float.Parse(args[1].ToString(), formatter) * 0.01f;
                        updateParents();
                        break;
                    }
                case "blemishesOpacity":
                    {
                        blemishes = overlayID;
                        blemishesOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                    //    localPlayer.setHeadOverlay(0, overlayID, blemishesOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(0, overlayID, blemishesOpacity);
                        localPlayer.SetHeadOverlayColor(0, 0, 0, 0);

                        break;
                    }
                case "facialHairOpacity":
                    {
                        facialHair = overlayID;
                        facialHairOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                     //   localPlayer.setHeadOverlay(1, overlayID, facialHairOpacity, beardColor, 0);
                        localPlayer.SetHeadOverlay(1, overlayID, facialHairOpacity);
                        localPlayer.SetHeadOverlayColor(1, 1, beardColor, 0);

                        break;
                    }
                case "eyebrowsOpacity":
                    {
                        eyebrows = overlayID;
                        eyebrowsOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                      //  localPlayer.setHeadOverlay(2, overlayID, eyebrowsOpacity, eyebrowColor, 0);
                        localPlayer.SetHeadOverlay(2, overlayID, eyebrowsOpacity);
                        localPlayer.SetHeadOverlayColor(2, 1, eyebrowColor, 0);
                        break;
                    }
                case "ageingOpacity":
                    {
                        ageing = overlayID;
                        ageingOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                    //    localPlayer.setHeadOverlay(3, overlayID, ageingOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(3, overlayID, ageingOpacity);
                        localPlayer.SetHeadOverlayColor(3, 0, 0, 0);
                        break;
                    }
                case "makeupOpacity":
                    {
                        makeup = overlayID;
                        makeupOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                     //   localPlayer.setHeadOverlay(4, overlayID, makeupOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(4, overlayID, makeupOpacity);
                        localPlayer.SetHeadOverlayColor(4, 0, 0, 0);
                        break;
                    }
                case "blushOpacity":
                    {
                        blush = overlayID;
                        blushOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                     //   localPlayer.setHeadOverlay(5, overlayID, blushOpacity, blushColor, 0);
                        localPlayer.SetHeadOverlay(5, overlayID, blushOpacity);
                        localPlayer.SetHeadOverlayColor(5, 2, blushColor, 0);
                        break;
                    }
                case "complexionOpacity":
                    {
                        complexion = overlayID;
                        complexionOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                     //   localPlayer.setHeadOverlay(6, overlayID, complexionOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(6, overlayID, complexionOpacity);
                        localPlayer.SetHeadOverlayColor(6, 0, 0, 0);
                        break;
                    }
                case "sundamageOpacity":
                    {
                        sundamage = overlayID;
                        sundamageOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                     //   localPlayer.setHeadOverlay(7, overlayID, sundamageOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(7, overlayID, sundamageOpacity);
                        localPlayer.SetHeadOverlayColor(7, 0, 0, 0);
                        break;
                    }
                case "lipstickOpacity":
                    {
                        lipstick = overlayID;
                        lipstickOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                    //    localPlayer.setHeadOverlay(8, overlayID, lipstickOpacity, lipstickColor, 0);
                        localPlayer.SetHeadOverlay(8, overlayID, lipstickOpacity);
                        localPlayer.SetHeadOverlayColor(8, 2, lipstickColor, 0);

                        break;
                    }
                case "frecklesOpacity":
                    {
                        freckles = overlayID;
                        frecklesOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                      //  localPlayer.setHeadOverlay(9, overlayID, frecklesOpacity, 0, 0);
                        localPlayer.SetHeadOverlay(9, overlayID, frecklesOpacity);
                        localPlayer.SetHeadOverlayColor(9, 0, 0, 0);
                        break;
                    }
                case "chestHairOpacity":
                    {
                        chestHair = overlayID;
                        chestHairOpacity = float.Parse(args[1].ToString(), formatter) * 0.01f;
                    //    localPlayer.setHeadOverlay(10, overlayID, chestHairOpacity, chestHairColor, 0);
                        localPlayer.SetHeadOverlay(10, overlayID, chestHairOpacity);
                        localPlayer.SetHeadOverlayColor(10, 1, chestHairColor, 0);
                        break;
                    }
                case "noseWidth":
                    {


                        noseWidth = float.Parse(args[1].ToString(), formatter);
                      //  noseWidth = (float)Convert.ToDouble(args[1]);


                        RAGE.Elements.Player.LocalPlayer.SetFaceFeature(0, noseWidth);

                        break;
                    }
                case "noseHeight":
                    {
                        noseHeight = float.Parse(args[1].ToString(), formatter);
                 localPlayer.SetFaceFeature(1, noseHeight);
                        break;
                    }
                case "noseLength":
                    {
                    noseLength = float.Parse(args[1].ToString(), formatter);
                        localPlayer.SetFaceFeature(2, noseLength);
                        break;
                    }
                case "noseBridge":
                    {
                        noseBridge = float.Parse(args[1].ToString(), formatter);
                    localPlayer.SetFaceFeature(3, noseBridge);
                        break;
                    }
                case "noseTip":
                    {
                        noseTip = float.Parse(args[1].ToString(), formatter);
                      localPlayer.SetFaceFeature(4, noseTip);
                        break;
                    }
                case "noseBridgeShift":
                    {
                        noseBridgeShift = float.Parse(args[1].ToString(), formatter);
                   localPlayer.SetFaceFeature(5, noseBridgeShift);
                        break;
                    }
                case "browHeight":
                    {
                        browHeight = float.Parse(args[1].ToString(), formatter);
                     localPlayer.SetFaceFeature(6, browHeight);
                        break;
                    }
                case "browWidth":
                    {
                        browWidth = float.Parse(args[1].ToString(), formatter);
                    localPlayer.SetFaceFeature(7, browWidth);
                        break;
                    }
                case "cboneHeight":
                    {
                        cboneHeight = float.Parse(args[1].ToString(), formatter);
                      localPlayer.SetFaceFeature(8, cboneHeight);
                        break;
                    }
                case "cboneWidth":
                    {
                        cboneWidth = float.Parse(args[1].ToString(), formatter);
                      localPlayer.SetFaceFeature(9, cboneWidth);
                        break;
                    }
                case "cheekWidth":
                    {
                        cheekWidth = float.Parse(args[1].ToString(), formatter);
                  localPlayer.SetFaceFeature(10, cheekWidth);
                        break;
                    }
                case "eyes":
                    {
                        eyes = float.Parse(args[1].ToString(), formatter);
                    localPlayer.SetFaceFeature(11, eyes);
                        break;
                    }
                case "lips":
                    {
                        lips = float.Parse(args[1].ToString(), formatter);
                     localPlayer.SetFaceFeature(12, lips);
                        break;
                    }
                case "jawWidth":
                    {
                        jawWidth = float.Parse(args[1].ToString(), formatter);
                     localPlayer.SetFaceFeature(13, jawWidth);
                        break;
                    }
                case "chinLength":
                    {
                        chinLength = float.Parse(args[1].ToString(), formatter);
                    localPlayer.SetFaceFeature(15, chinLength);
                        break;
                    }
                case "chinPos":
                    {
                        chinPos = float.Parse(args[1].ToString(), formatter);
                     localPlayer.SetFaceFeature(16, chinPos);
                        break;
                    }
                case "chinWidth":
                    {
                        chinWidth = float.Parse(args[1].ToString(), formatter);
                    localPlayer.SetFaceFeature(17, chinWidth);
                        break;
                    }
                case "chinShape":
                    {
                        chinShape = float.Parse(args[1].ToString(), formatter);
                     localPlayer.SetFaceFeature(18, chinShape);
                        break;
                    }
                case "neckWidth":
                    {
                        neckWidth = float.Parse(args[1].ToString(), formatter);
                      localPlayer.SetFaceFeature(19, neckWidth);
                        break;
                    }
            }
           
        }

        public void CustomDestroy(object[] args)
        {
            Browser.DestroyBrowserEvent(null);


            RAGE.Game.Ui.DisplayHud(true);
            RAGE.Game.Ui.DisplayRadar(true);

            localPlayer.FreezePosition(false);

               Menu.CreateAllMenu();
            //CreateCefForGame();
            //BindKeys();

            //blockKeys = false;
            //bindKeys(blockKeys);
            //   Events.CallRemote("CEFCreatedSuccess");
            //blockKeys = false;
            //bindKeys(blockKeys);
            //bUnbind();
            KeyManager.block = 0;
            Events.CallRemote("CEFCreatedSuccess");
        }


        public void ClientCreate(object[] args)
        {
            father = Constants.father[(int)args[1]];
            mother = Constants.mother[(int)args[0]];
            updateParents();
         //   DrawServer();
        }
        public void GenderChange(object[] args)
        {
            Events.CallRemote("creator_GenderChange", args[0]);
            switch ((int)args[0])
            {
                case 0:
                    gender = "false";
                    break;
                case 1:
                    gender = "true";
                    break;
                default:
                    gender = "true";
                    break;
            }
          //  DrawServer();
        }
        public void HairsList(object[] args)
        {

            localPlayer.SetComponentVariation(2, (int)args[0], 0, 2);
            hairItem = (int)args[0];
         //   DrawServer();
        }
        public void HairsColor(object[] args)
        {
         localPlayer.SetHairColor((int)args[0], 0);
            hairColors = (int)args[0];
         //   DrawServer();
        }
        public void EyesColor(object[] args)
        {
          localPlayer.SetEyeColor((int)args[0]);
            eyeColor = (int)args[0];
         //   DrawServer();
        }

        public void Colors(object[] args)
        {
            localPlayer.SetHeadOverlayColor(1, 1, (int)args[0], 0);
            localPlayer.SetHeadOverlayColor(2, 1, (int)args[1], 0);
            localPlayer.SetHeadOverlayColor(10, 1, (int)args[4], 0);
            localPlayer.SetHeadOverlayColor(5, 2, (int)args[2], 0);
            localPlayer.SetHeadOverlayColor(8, 2, (int)args[3], 0);

            eyebrowColor = (int)args[1];
            beardColor = (int)args[0];
            chestHairColor = (int)args[4];
            blushColor = (int)args[2];
            lipstickColor = (int)args[3];
          
        }
        public void CustomClient(object[] args)
        {
            if (args[0].ToString().Length >= 2 && args[1].ToString().Length >= 2 && args[2].ToString().Length >= 1)
            {
                Events.CallRemote("personReg.server", args[0], args[1], args[2]);
            }
            else
            {
                //   mp.game.graphics.notify("Данные должны содержать больше символов!");
            }
        }


        public void CustomDB(object[] args)
        {
            Menu.CreateAllMenu();
            Events.CallRemote("CEFCreatedSuccess");
        }

        public void DrawServer()
        {
            Events.CallRemote("custom.up", mother, father, resemblance,
  skinTone, gender, eyeColor, hairColors, hairItem, noseWidth,
  noseHeight, noseLength, noseBridge, noseTip, noseBridgeShift,
          browHeight, browWidth, cboneHeight, cboneWidth, cheekWidth, eyes, lips,
          jawWidth, chinLength, chinPos, chinWidth, chinShape, neckWidth,
          blemishes, blemishesOpacity, facialHair, facialHairOpacity, eyebrows,
          eyebrowsOpacity,
          ageing, ageingOpacity, makeup, makeupOpacity, blush, blushOpacity,
          complexion, complexionOpacity,
          sundamage, sundamageOpacity, lipstick, lipstickOpacity, freckles,
          frecklesOpacity, chestHair, chestHairOpacity, eyebrowColor, beardColor,
          chestHairColor, blushColor, lipstickColor);
        }
    

        public void RegisterComplite(object[] args)
        {
            RAGE.Game.Cam.DestroyCam(Camera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);
            Events.CallRemote("custom.save", mother, father, resemblance,
  skinTone, gender, eyeColor, hairColors, hairItem, noseWidth,
  noseHeight, noseLength, noseBridge, noseTip, noseBridgeShift,
          browHeight, browWidth, cboneHeight, cboneWidth, cheekWidth, eyes, lips,
          jawWidth, chinLength, chinPos, chinWidth, chinShape, neckWidth,
          blemishes, blemishesOpacity, facialHair, facialHairOpacity, eyebrows,
          eyebrowsOpacity,
          ageing, ageingOpacity, makeup, makeupOpacity, blush, blushOpacity,
          complexion, complexionOpacity,
          sundamage, sundamageOpacity, lipstick, lipstickOpacity, freckles,
          frecklesOpacity, chestHair, chestHairOpacity, eyebrowColor, beardColor,
          chestHairColor, blushColor, lipstickColor);
        }


        public void updateParents()
        {

            localPlayer.SetHeadBlendData(
       // shape
       mother,
       father,
       0,

       // skin
       mother,
       father,
       0,

       // mixes
       resemblance,
       skinTone,
       0.0f,

       false
   );
        }
    }
}
