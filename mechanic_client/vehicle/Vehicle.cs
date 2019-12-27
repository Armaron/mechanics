using cs_packages.client;
using cs_packages.player;
using mechanic_client;
using RAGE;
using RAGE.Ui;
using System.Collections.Generic;

namespace cs_packages.vehicle
{
    class Vehicle : Events.Script
    {
        static public HtmlWindow CarMenu = null;
        static string vehicle = "";
        static public RAGE.Elements.Marker mark;
        static public RAGE.Elements.TubeColshape shape;

        public Vehicle()
        {
            Events.OnPlayerLeaveVehicle += LeaveVehicle;
            Events.Add("luggageAction", LuggageAction);
            Events.Add("cowlAction", CowlAction);
            Events.Add("carAction", CarAction);
            Events.Add("takeCarKeys", TakeCarKeys);
            Events.Add("OpenCloseCarMenu", OpenCarMenu);
            Events.Add("changeLocker", СhangeLocker);
            Events.Add("parking", Parking);
            Events.Add("CreateGarageMarker", CreateGarageMarker);
            Events.Add("DestroyGarageMarker", DestroyGarageMarker);
            Events.Add("kickAway", KickAway);
            Events.Add("doDiagnostic", doDiagnostic);
            Events.Add("evacuation", evacuation);
            Events.Add("serviceBook", serviceBook);

        }

        private void evacuation(object[] args)
        {
            Mechanic_Client.Tow();
            
        }

        private void serviceBook(object[] args)
        {
            Mechanic_Client.OpenServiceBook();
           
        }
        private void doDiagnostic(object[] args)
        {
            Mechanic_Client.ActiveDiagMech(null);
           
        }
        public void LeaveVehicle()
        {

            //        Speed.speedometrCef.Destroy();
            //   Speed.speedometrCef = null;


            if (TickEvent.SpeedClass != null)
            {
                TickEvent.SpeedClass.SaveCar();
            }
            //Speed.fuelLevel = 999;


        }



        public void CreateGarageMarker(object[] args)
        {

            Vector3 pos = (Vector3)args[0];

            mark = new RAGE.Elements.Marker(0, pos, 2f, new Vector3(), new Vector3(), new RGBA(0, 0, 255, 100), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
            shape = new RAGE.Elements.TubeColshape(pos, 3f, 3f, RAGE.Elements.Player.LocalPlayer.Dimension);

            shape.SetData<int>("garage",999);




        }

        public void DestroyGarageMarker(object[] args)
        {

            Vector3 pos = (Vector3)args[0];
            if (mark != null)
                mark.Destroy();
            if (shape != null)
                shape.Destroy();






        }

        public void СhangeLocker(object[] args)
        {



            Events.CallRemote("СhangeLocker.Client", vehicle);


            OpenCarMenu(null);






        }

        public void KickAway(object[] args)
        {



            Events.CallRemote("KickAway.Server",vehicle);


            OpenCarMenu(null);






        }


        public void Parking(object[] args)
        {



            Events.CallRemote("Parking.Client", vehicle);


            OpenCarMenu(null);






        }




        public static void OpenCarMenu(object[] args)
        {
            //Chat.Output("FindCar1");

            if (args == null)
            {
                CarMenu.Active = false;
                CarMenu.Destroy();
                CarMenu = null;
                KeyManager.block = 0;
                vehicle = "";




                Cursor.Visible = false;
                Chat.Show(true);
                DrawInfo.LoadScreen = false;
                return;
            }
            vehicle = args[0].ToString();


            if (CarMenu == null)
            {
                CarMenu = new HtmlWindow("package://auth/assets/carCircle.html");

                KeyManager.block = 13;

                CarMenu.Active = true;
                //   Chat.Output(args[0].ToString());



                Cursor.Visible = true;
                Chat.Show(false);
            }

            else
            {
                CarMenu.Active = false;
                CarMenu.Destroy();
                CarMenu = null;
                KeyManager.block = 0;
                vehicle = "";




                Cursor.Visible = false;
                Chat.Show(true);
            }


        }

        public void LuggageAction(object[] args)
        {
            RAGE.Elements.Vehicle veh = null;

            List<RAGE.Elements.Vehicle> vehicles = RAGE.Elements.Entities.Vehicles.All;

            if (vehicle != null)
            {
                for (int i = 0; i < vehicles.Count; i++)
                {
                    string h = vehicles[i].GetNumberPlateText();

                    string t = h.Replace(" ", string.Empty);
                    if (t == vehicle)
                    {
                        veh = vehicles[i];
                        break;
                    }
                }
            }

            if (veh != null)
            {
                if (veh.GetDoorAngleRatio(5) > 0)
                {
                    veh.SetDoorShut(5, false);
                    Events.CallRemote("OpenCloseLagagge", vehicle, false);
                }
                else
                {
                    veh.SetDoorOpen(5, false, false);
                    Events.CallRemote("OpenCloseLagagge", vehicle, true);
                }
                OpenCarMenu(null);
            }





        }
        public void CowlAction(object[] args)
        {


            //if (vehicle.GetDoorAngleRatio(4) > 0)
            //    vehicle.SetDoorShut(4, false);
            //else

            //    vehicle.SetDoorOpen(4, false, false);
            //OpenCarMenu(null);




        }

        public void CarAction(object[] args)
        {


            Events.CallRemote("OpenCloseCar", vehicle);
            //Chat.Output("open/close");
            OpenCarMenu(null);




        }
        public void TakeCarKeys(object[] args)
        {


            Events.CallRemote("TakeCarKeys.Client", vehicle);
            //Chat.Output("open/close");
            OpenCarMenu(null);




        }
    }
}
