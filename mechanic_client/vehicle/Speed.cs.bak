using cs_packages.browsers;
using RAGE;
using RAGE.Ui;
using System;
using System.Threading.Tasks;

namespace cs_packages.vehicle
{
    public class Speed
    {
      
        public HtmlWindow speedometrCef = null;
        bool speedActive = false;
        public int maxfuelincar = 0;
        int speed = 0;
        string gear = "0";
        int arrowSpeed = 0;
        public float fuelLevel = 0;
        public float engHealth = 0;
        public float bodyHealth = 0;
        public bool fuelcheck;
        public int rashod;
        public int maxlevel;


        static Vector3 LastPosCar = new Vector3(0, 0, 0);


        public Speed()
        {
            Phone.EnterVehicle(null);
            RAGE.Elements.Player localPlayer = RAGE.Elements.Player.LocalPlayer;
            //  Events.Add("speedometr.client", Speedometr);
            Events.Add("FuelUp", FuelUp);
          

            Speedometr();
            //   UpdateSpeed();
        }
        public void Speedometr()
        {
            rashod = (int)RAGE.Elements.Player.LocalPlayer.Vehicle.GetSharedData("FuelOn100");
          
                engHealth = (float)RAGE.Elements.Player.LocalPlayer.Vehicle.GetSharedData("EngHealth");
                RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineHealth(engHealth);
        
                fuelLevel = (float)RAGE.Elements.Player.LocalPlayer.Vehicle.GetSharedData("FuelLevel");

             maxlevel = (int)RAGE.Elements.Player.LocalPlayer.Vehicle.GetSharedData("MaxLevel");

            bodyHealth = (float)RAGE.Elements.Player.LocalPlayer.Vehicle.GetSharedData("BodyHealth");
                RAGE.Elements.Player.LocalPlayer.Vehicle.SetBodyHealth(bodyHealth);
                   LastPosCar = RAGE.Elements.Player.LocalPlayer.Position;

            if (speedometrCef == null)
            {
                speedometrCef = new HtmlWindow("package://auth/assets/speed.html");
            }

            //engHealth =
            //RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineHealth();



            speedActive = true;
            speedometrCef.ExecuteJs("speedFadeIn();");
            speedometrCef.ExecuteJs("pushSpeed(0,0,'D',0);");


        }


        public void FuelUp(object[] args)
        {
            fuelLevel += (int)args[0];
        }
        public void UpdateDistation()
        {


         
                //bodyHealth = RAGE.Elements.Player.LocalPlayer.Vehicle.GetBodyHealth();
                //engHealth = RAGE.Elements.Player.LocalPlayer.Vehicle.GetEngineHealth();



                //if (fuelLevel <= 0.1f)
                //{
                //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineOn(false, false, false);
                //    return;
                //}


                //if (engHealth < 200f)
                //{
                //    if (engHealth < 100f)
                //        engHealth = 199f;
                //    speedometrCef.ExecuteJs("engineError('true');");
                //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineOn(false, false, false);
                //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEnginePowerMultiplier(0.1f);
                //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineTorqueMultiplier(0.1f);

                //}
                //else
                //{
                //    speedometrCef.ExecuteJs("engineError('false');");
                //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEnginePowerMultiplier(1f);
                //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineTorqueMultiplier(1f);



                //}

                //if (bodyHealth < 200f)
                //{
                //    if (bodyHealth < 100f)
                //        bodyHealth = 199f;
                //    speedometrCef.ExecuteJs("carbodyError('true');");
                //}
                //else
                //{
                //    speedometrCef.ExecuteJs("carbodyError('false');");
                //}












                //float dist = (RAGE.Elements.Player.LocalPlayer.Vehicle.Position.DistanceTo(LastPosCar));
              
                //LastPosCar = RAGE.Elements.Player.LocalPlayer.Vehicle.Position;
                //float level = 0;
                //level = fuelLevel; 

                float currents = RAGE.Elements.Player.LocalPlayer.Vehicle.GetSpeed();

                speed = Convert.ToInt32(currents * 3.6);


                arrowSpeed = (speed - 155);

               
                speedometrCef.ExecuteJs("pushCurrentGear('D');");
                speedometrCef.ExecuteJs("pushCurrentArrow('" + arrowSpeed + "');");
                speedometrCef.ExecuteJs("pushCurrentSpeed(" + speed + ",10);");
                






                


               
         
                //float downlevel = (100f / (float)rashod) * (Math.Abs(dist) / 950000f);


                //fuelLevel = level -= downlevel;
                ////if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetIsEngineRunning())
                ////{
                ////    fuelLevel = level -= 0.001f;
                ////}
                //double percent = 100.0 / (double)maxlevel;
                //int count = (int)(percent * fuelLevel);

                //speedometrCef.ExecuteJs("gasLines(" + count + ");");
                ////   Chat.Output((dist/100f).ToString());
                ////      Chat.Output(maxlevel.ToString() + "      " + level.ToString() + "    " + count.ToString());







                // RAGE.Elements.Player.LocalPlayer.Vehicle.GetBodyHealth2();







           


                //  await Task.Run(() => update());


            

        }

        public void SaveCar()
        {

            Events.CallRemote("ExitVehicle", fuelLevel, bodyHealth, engHealth, false);




        }
        public void DestroyCef()
        {
            Phone.ExitVehicle(null);
            Events.CallRemote("ExitVehicle", fuelLevel, bodyHealth, engHealth, true);
            speedometrCef.Destroy();

        }
        //public void UpdateSpeed()
        //{


        //    Task.Factory.StartNew(() =>
        //    {

        //        if (RAGE.Elements.Player.LocalPlayer.Vehicle != null)
        //        {

        //            if (speedometrCef != null)
        //            {


        //                    //if (fuelLevel <= 0.1f)
        //                    //{
        //                    //    RAGE.Elements.Player.LocalPlayer.Vehicle.SetEngineOn(false, false, false);

        //                    //}


        //                    float currents = RAGE.Elements.Player.LocalPlayer.Vehicle.GetSpeed();

        //                speed = Convert.ToInt32(currents * 3.6);


        //                arrowSpeed = (speed - 155);


        //                    // RAGE.Elements.Player.LocalPlayer.Vehicle.GetBodyHealth2();







        //                    speedometrCef.ExecuteJs("pushCurrentArrow(" + arrowSpeed + ");");


        //                speedometrCef.ExecuteJs("pushCurrentSpeed(" + speed + ",10);");
        //            }
        //            else
        //            {
        //                Speedometr(null);
        //            }
        //        }

        //    });



        //}


        public void update()
        {
        }
    }
}
