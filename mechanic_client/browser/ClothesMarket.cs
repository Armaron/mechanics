using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using cs_packages.client;
using Newtonsoft.Json;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
    public class ClothesMarket : Events.Script
    {





        

        static public HtmlWindow salon = null;
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
      
        float mod = 1.5f;
        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public ClothesMarket()
        {
            //server ----   list  = name + price
            Events.Add("open.clothesmarket", Open);
            Events.Add("clothesExit", Exit);
            Events.Add("cameraClothes", Control);
            Events.Add("resetCamera", ResetCamera);
            Events.Add("clothesSelectColor", ClothesColor);
            Events.Add("clothesBuyButton", ClothesBuy);
            Events.Add("wearClothes", WearClothes);
            Events.Add("removeClothes", RemoveClothes);

            //SetClothes

        }




        public void Open(object[] args)
        {

            KeyManager.block = 5;
            salon = new HtmlWindow("package://auth/assets/clothes.html");
            salon.Active = true;
            //   Chat.Output(args[0].ToString());
            salon.ExecuteJs("pushClothesShop('" + args[0].ToString() + "');");
            forcamX = RAGE.Elements.Player.LocalPlayer.Position.X + 1.5f;
            forcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + 1.5f;
            forcamZ = RAGE.Elements.Player.LocalPlayer.Position.Z + 0.5f;
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

            Events.CallRemote("RemoveAllWeapons");

        }
        public void Exit(object[] args)
        {

            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            RAGE.Game.Cam.DestroyCam(camera, true);
            salon.Destroy();
            Cursor.Visible = false;
            Chat.Show(true);


            Events.CallRemote("SetPlayerDecor");
            Events.CallRemote("ExitSalon");
            Events.CallRemote("SetClothesDefault");
            KeyManager.block = 0;



        }

        public void Control(object[] args)
        {

            if (args[0].ToString().Contains("cameraHeight"))
            {
                RAGE.Game.Cam.SetCamCoord(camera, rotcamX, rotcamY, tempforcamZ + float.Parse(args[1].ToString(), formatter));
                rotcamZ = tempforcamZ + float.Parse(args[1].ToString(), formatter);

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


        
        public void RemoveClothes(object[] args)
        {

          




            Events.CallRemote("RemoveClothes", args[0]);


        }


        public void ClothesColor(object[] args)
        {

          int  color = Convert.ToInt32(args[1]);




            Events.CallRemote("SetClothes", args[0], color);
           

        }



        public void WearClothes(object[] args)
        {
           
            Events.CallRemote("SetClothes", args[0], 0);
        }


        public void ClothesBuy(object[] args)
        {
            string cashService = args[0].ToString();
            int price = Convert.ToInt32(args[1]);
            Chat.Output(cashService + "; Price: " + price);
            //List clothes = args[3];
            switch (cashService)
            {
                case "card": //карточкой
                    {

                        Events.CallRemote("PayClothesCard", price, args[2]);
                        
                        break;
                    }
                case "cash": //наличкой
                    {

                        Events.CallRemote("PayClothesCash", price, args[2]);

                        break;
                    }

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


    }
}
