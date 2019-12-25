using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;
using Newtonsoft.Json;
using cs_packages.client;
using System.Globalization;
using System.Threading.Tasks;

namespace cs_packages.browsers
{
    class TatooSalon : Events.Script
        
    {
        int sex;
        int tattooZone;
        
        static public HtmlWindow salon = null;
        static RAGE.Elements.Ped ped_0;
        static RAGE.Elements.Ped ped_1;
        static RAGE.Elements.Ped ped_2;
        static RAGE.Elements.Ped ped_3;
        static RAGE.Elements.Ped ped_4;

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

        public TatooSalon()
        {
  
            Events.Add("open.tatoosalon", OpenTatooSalon);
            Events.Add("set.tatoo", SetTatoo);
            Events.Add("selectBodyPart", SelectBodyPart);
            Events.Add("selectTattoo", SelectTattoo);
            Events.Add("tattooExit", TattooExit);
            Events.Add("cameraTattoo", Control);
            Events.Add("resetCamera", ResetCamera);
            Events.Add("tattooBuyButton", BuyTattoo);
            Events.Add("deleteTattoo", DeleteTattoo);  //доработать
            Events.Add("setNewTattoo", SetNewTattoo);  
        }
        public void OpenTatooSalon(object[] args)
        {
            KeyManager.block = 5;
            salon = new HtmlWindow("package://auth/assets/tattoo.html");
            salon.Active = true;

            // "tatoo_sandyshores_1"
            ped_0 = new RAGE.Elements.Ped(0xBE20FA04, new Vector3(1865.297f, 3747.99f, 33.03188f), 85f);
            ped_0.ActivatePhysics();
            ped_0.SetVisible(true, true);
            ped_0.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"tattoo_LosSantos_1"
            ped_1 = new RAGE.Elements.Ped(0xBE20FA04, new Vector3(325.2873f, 181.175f, 103.5865f), 90f);
            ped_1.ActivatePhysics();
            ped_1.SetVisible(true, true);
            ped_1.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"tattoo_LosSantos_2"
            ped_2 = new RAGE.Elements.Ped(0xBE20FA04, new Vector3(1321.95f, -1654.686f, 52.27557f), 0f);
            ped_2.ActivatePhysics();
            ped_2.SetVisible(true, true);
            ped_2.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"tattoo_LosSantos_3"
            ped_3 = new RAGE.Elements.Ped(0xBE20FA04, new Vector3(-1155.115f, -1428.082f, 4.954462f), 0f );
            ped_3.ActivatePhysics();
            ped_3.SetVisible(true, true);
            ped_3.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //"tattoo_Сhumash"
            ped_4 = new RAGE.Elements.Ped(0xBE20FA04, new Vector3(-3170.599f, 1078.527f, 20.82918f), 191f );
            ped_4.ActivatePhysics();
            ped_4.SetVisible(true, true);
            ped_4.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
                                                  
            sex = (int)args[0];
            forcamX = RAGE.Elements.Player.LocalPlayer.Position.X + 1.5f;
            forcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + 1.5f;
            forcamZ = RAGE.Elements.Player.LocalPlayer.Position.Z + 0.5f;
            tempforcamX = forcamX;
            tempforcamY = forcamY;
            tempforcamZ = forcamZ;
            rotcamX = forcamX;
            rotcamY = forcamY;
            rotcamZ = forcamZ;

            camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"),forcamX , forcamY, forcamZ, -20.0f, 0.0f, 180.0f, 70.0f, true, 2);
            RAGE.Game.Cam.SetCamActive(camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);
            Cursor.Visible = true;
            ResetCamera(null);

        }

        public void TattooExit(object[] args)
        {
            ped_0.Destroy();
            ped_1.Destroy();
            ped_2.Destroy();
            ped_3.Destroy();
            ped_4.Destroy();
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            RAGE.Game.Cam.DestroyCam(camera, true);
        //    RAGE.Game.Cam.GetCamRot
            salon.Destroy();
            Cursor.Visible = false;
            Events.CallRemote("DeleteTempTattoo");
            Events.CallRemote("SetPlayerDecor");
            Events.CallRemote("ExitSalon");
            Events.CallRemote("SetClothesDefault");
            KeyManager.block = 0;
        }
        
        public void DeleteTattoo(object[] args)
        {
          
            //Events.CallRemote("DeleteTempTattoo");
            Events.CallRemote("DeleteTattoo", tattooZone);
      
        }

        public void BuyTattoo(object[] args)
        {

           
       //     int bodyPart = Convert.ToInt32(args[0]);
            string tattooName = args[1].ToString();
            int tattooPrice = Convert.ToInt32(args[2]);
            string cashService = args[3].ToString();
           
            switch (cashService)
            {
                case "card": //карточкой
                    {
                     
                        Events.CallRemote("PayTattooCard", tattooZone, tattooName, tattooPrice);
                     
                        break;
                    }
                case "cash": //наличкой
                    {
                    
                        Events.CallRemote("PayTattooCash", tattooZone, tattooName, tattooPrice);
                        break;
                    }
                    
            }

            Task.Delay(1000);

        }

        public void SetNewTattoo(object[] args)
        {
            Events.CallRemote("SetPlayerDecor");

        }

        public void Control(object[] args)
        {

            if(args[0].ToString().Contains("cameraHeight"))
            {
                RAGE.Game.Cam.SetCamCoord(camera, rotcamX, rotcamY, tempforcamZ + float.Parse(args[1].ToString(), formatter));
               rotcamZ = tempforcamZ + float.Parse(args[1].ToString(), formatter);

            }



            if (args[0].ToString().Contains("cameraRotate"))
            {
                float statX = tempforcamX -1.5f;
                float statY = tempforcamY - 1.5f;
                float angle = ((float.Parse(args[1].ToString(), formatter) * (-180)) + 180);
              
                RAGE.Game.Cam.SetCamCoord(camera, RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180))*mod, RAGE.Elements.Player.LocalPlayer.Position.Y +  (float)Math.Sin((angle * 3.14 / 180)) * mod, rotcamZ);
                rotcamX = RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod;
                rotcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod;

                RAGE.Game.Cam.SetCamRot(camera,0,0, angle +90,2);

            }

           
               
          


        }

        public void ResetCamera(object[] args)
        {
            

            RAGE.Game.Cam.SetCamCoord(camera, rotcamX, rotcamY, tempforcamZ );
            rotcamZ = tempforcamZ + 0;


            float statX = tempforcamX - 1.5f;
            float statY = tempforcamY - 1.5f;
            float angle = (0 * (-180)) + 180 ;

            RAGE.Game.Cam.SetCamCoord(camera, RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod, RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod, rotcamZ);
            rotcamX = RAGE.Elements.Player.LocalPlayer.Position.X + (float)Math.Cos((angle * 3.14 / 180)) * mod;
            rotcamY = RAGE.Elements.Player.LocalPlayer.Position.Y + (float)Math.Sin((angle * 3.14 / 180)) * mod;

            RAGE.Game.Cam.SetCamRot(camera, 0, 0, angle +90, 2);
        }

        public void SetTatoo(object[] args)
        {
         
           
            //foreach (string tat in JsonConvert.DeserializeObject<List<string>>(args[0].ToString()))
            //{
           
                salon.ExecuteJs("pushTattooList("+ JsonConvert.SerializeObject(args[0].ToString())+");");


       

            //}
            //string name = "huy";
            //int cash = 99;

            //salon.ExecuteJs("pushTattooList('" + name + "'," + cash + ");");





        }

        public void SelectBodyPart(object[] args)
        {
            Events.CallRemote("tattoo.newpart", args[0]);

            tattooZone = (int)args[0];
           

            switch ((int)args[0])
            {
                case 0:
                    {
                        ResetCamera(args);
                        RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY, forcamZ);
                        tempforcamX = forcamX;
                        tempforcamY = forcamY;
                        tempforcamZ = forcamZ;
                        mod = 1.5f;
                        RAGE.Game.Cam.SetCamRot(camera, 0, 0,  90, 2);

                        //          int   Camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT"), RAGE.Elements.Player.LocalPlayer.Position.X + 2,
                        //                RAGE.Elements.Player.LocalPlayer.Position.Y+2, RAGE.Elements.Player.LocalPlayer.Position.Z,
                        //-10.0f, 0.0f, 0, 90.0f, true, 2);

                        //             RAGE.Game.Cam.SetCamActive(Camera, true);
                        //              RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

                        ResetCamera(null);

                        break;
                    }
                case 1:
                    {

                        RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY-0.5f, forcamZ+0.3f);
                        //RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY, forcamZ+0.3f);
                        //tempforcamX = forcamX;
                        tempforcamY = forcamY-0.5f;
                        tempforcamY = forcamY;
                        tempforcamZ = forcamZ+0.3f;
                        mod = 1; RAGE.Game.Cam.SetCamRot(camera, 0, 0, 90, 2);
                        //          int   Camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT"), RAGE.Elements.Player.LocalPlayer.Position.X + 3f,
                        //             RAGE.Elements.Player.LocalPlayer.Position.Y + 2f, RAGE.Elements.Player.LocalPlayer.Position.Z,
                        //-10.0f, 0.0f, 0, 90.0f, true, 2);

                        //             RAGE.Game.Cam.SetCamActive(Camera, true);
                        //             RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

                        ResetCamera(null);

                        break;
                    }
                case 2:
                    {
                        RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY, forcamZ);
                        tempforcamX = forcamX;
                        tempforcamY = forcamY;
                        tempforcamZ = forcamZ;
                        mod = 1.5f; ResetCamera(null);
                        break;
                    }
                case 3:
                    {
                        RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY, forcamZ);
                        tempforcamX = forcamX;
                        tempforcamY = forcamY;
                        tempforcamZ = forcamZ;
                        mod = 1.5f; ResetCamera(null);
                        break;
                    }
                case 4:
                    {
                        RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY, forcamZ-0.5f);
                        tempforcamX = forcamX;
                        tempforcamY = forcamY;
                        tempforcamZ = forcamZ - 0.5f;
                        mod = 1.5f; ResetCamera(null);

                        break;
                    }
                case 5:
                    {
                        RAGE.Game.Cam.SetCamCoord(camera, forcamX, forcamY, forcamZ - 0.5f);
                        tempforcamX = forcamX;
                        tempforcamY = forcamY;
                        tempforcamZ = forcamZ - 0.5f;
                        mod = 1.5f; ResetCamera(null);
                        break;
                    }
            }
        }

        public void SelectTattoo(object[] args)
        {

            Events.CallRemote("CreateTattoo", args[0].ToString(),sex,tattooZone);
           


           


           
        }


    }
}
