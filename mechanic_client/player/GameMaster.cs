using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;

namespace cs_packages.player
{
   public static class GameMaster
    {
        public static Vector3 LastPos = new Vector3(0f,0f,0f);
        public static void CheckPlayer()
        {
            //int weap = 0;
            //RAGE.Elements.Player.LocalPlayer.GetCurrentWeapon(ref weap, true);
            //if (weap != 0 || weap !=-1569615261)
            //{
            //    uint current =    DrawInfo.weapons.Find(wea => wea == weap);
            //    current = DrawInfo.coldweapons.Find(wea => wea == weap);
            //    if(current == 0)
            //    {
            //        Chat.Output(weap.ToString());
            //    }
            //}
         
            //if(LastPos!= new Vector3(0f,0f,0f))
            //{
            //    if(Math.Abs(RAGE.Elements.Player.LocalPlayer.Position.DistanceTo(LastPos))>500)
            //    {
            //        Chat.Output("Иди на хуй ебучий читер");
            //    }
            //    LastPos = RAGE.Elements.Player.LocalPlayer.Position;

            //}
           
        }


    }
}
