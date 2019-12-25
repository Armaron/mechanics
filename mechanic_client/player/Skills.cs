using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;

namespace cs_packages.player
{
   public class Skills : Events.Script
    {
       
	


	   public static int Stamina { get; set; }
        public static int StaminaLvl { get; set; }
		
        public static int Stamina_Time { get; set; }
		public static long DrugTimeNow {get; set;}
        public static bool Stop { get; set; }

		public Skills()
		{
		  Events.Add("usedrug", UseDrug);
            Events.Add("stamina.start", StaminaStart);

        }
		public  void UseDrug(object[] args)
        {
           // RAGE.Elements.Player.LocalPlayer.SetIsDrunk(true);
         
          
           DrugTimeNow = cs_packages.client.TickEvent.tickcount;
		  cs_packages.client.TickEvent.DrugTimer = true;
        }
        public void StaminaStart(object[] args)
        {
            try
            {
                Stamina = (int)args[0];
                StaminaLvl = (int)args[1];
                if (StaminaLvl == 0)
                {

                }
                if (StaminaLvl == 1)
                {
                    RAGE.Elements.Player.LocalPlayer.SetMaxHealth(120);
                    RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.2f);
                }
                if (StaminaLvl == 2)
                {
                    RAGE.Elements.Player.LocalPlayer.SetMaxHealth(150);
                    RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.4f);
                }
                if (StaminaLvl == 3)
                {
                    RAGE.Elements.Player.LocalPlayer.SetMaxHealth(170);
                    RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.6f);
                }
                if (StaminaLvl == 4)
                {
                    RAGE.Elements.Player.LocalPlayer.SetMaxHealth(180);
                    RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.8f);


                }
                if (StaminaLvl == 5)
                {
                    RAGE.Elements.Player.LocalPlayer.SetMaxHealth(200);
                    RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(1.0f);
                }
            }
            catch
            {

            }

        }

        public static void StaminaSet()
        {
            Stamina++;
            StaminaLvlSet();
        }

        public static void StaminaTimeSet(bool reset = false)
        {
            if(reset)
            {
                Stamina_Time = 0;
                Stop = true;
                return;
            }

            if (!Stop)
            {
                Stamina_Time++;
                if (Stamina_Time >= 50)
                {
                   
                    Events.CallRemote("StaminaSetExp",Stamina);
                    StaminaSet();
                    Stamina_Time = 0;
                }
            }
        }


        public static void StaminaLvlSet()
        {
            if(Stamina == 20)
            {
              //  Chat.Output("LVL 1");
                StaminaLvl = 1;
                Events.CallRemote("StaminaSet",1);
                RAGE.Elements.Player.LocalPlayer.SetMaxHealth(120);
                RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.2f);
            //    RAGE.Game.Player.SetRunSprintMultiplierForPlayer(1.49f);
            //    RAGE.Game.Player.SetPlayerSneakingNoiseMultiplier
            }
            if (Stamina == 40)
            {
                StaminaLvl = 2;
                Events.CallRemote("StaminaSet", 2);
                RAGE.Elements.Player.LocalPlayer.SetMaxHealth(150);
                RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.4f);
            }
            if (Stamina == 120)
            {
                StaminaLvl = 3;
                Events.CallRemote("StaminaSet", 3);
                RAGE.Elements.Player.LocalPlayer.SetMaxHealth(170);
                RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.6f);
            }
            if (Stamina == 480)
            {
                StaminaLvl = 4;
                Events.CallRemote("StaminaSet", 4);
                RAGE.Elements.Player.LocalPlayer.SetMaxHealth(180);
                RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.8f);
            }
            if (Stamina == 2400)
            {
                StaminaLvl = 5;
                Events.CallRemote("StaminaSet", 5);
                RAGE.Elements.Player.LocalPlayer.SetMaxHealth(200);
                RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(1.0f);
            }

        }




    }
}
