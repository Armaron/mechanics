using System;
using System.Collections.Generic;
using RAGE;
using RAGE.Elements;

namespace cs_packages.player
{
   public  class NoClip :Events.Script

    {
        public static int camera;
        public static bool isNoclip = false;
        public NoClip()
        {
           
        }

        public static void RenderTick()
        {
            if (isNoclip)
            {
                Vector3 pos = Player.LocalPlayer.Position;
                Vector3 cam = getCamDirection();
                Player.LocalPlayer.SetVelocity(0.0001f, 0.0001f, 0.0001f);

                if (RAGE.Game.Pad.IsControlPressed(0, 32) && RAGE.Game.Pad.IsControlPressed(0, 21))
                {
                 
                    //Player.LocalPlayer.FreezePosition(false);
                    //pos.X = pos.X + 1.0f * 5 * cam.X;
                    //pos.Y = pos.Y + 1.0f * 5 * cam.Y;
                    //pos.Z = pos.Z + 1.0f * 5 * cam.Z;
                }
                else if (RAGE.Game.Pad.IsControlPressed(0, 36) && RAGE.Game.Pad.IsControlPressed(0, 32))
                {
                    Player.LocalPlayer.FreezePosition(false);
                    pos.X = pos.X + 0.4f * cam.X;
                    pos.Y = pos.Y + 0.4f * cam.Y;
                    pos.Z = pos.Z + 0.4f * cam.Z;
                }
                else if (RAGE.Game.Pad.IsControlPressed(0, 32))
                {
                    Player.LocalPlayer.FreezePosition(false);
                    pos.X = pos.X + 1.0f * cam.X;
                    pos.Y = pos.Y + 1.0f * cam.Y;
                    pos.Z = pos.Z + 1.0f * cam.Z;
                }
                else if (RAGE.Game.Pad.IsControlPressed(0, 269) && RAGE.Game.Pad.IsControlPressed(0, 21))
                {
                    Player.LocalPlayer.FreezePosition(false);
                    pos.X = pos.X - 1.0f * 5 * cam.X;
                    pos.Y = pos.Y - 1.0f * 5 * cam.Y;
                    pos.Z = pos.Z - 1.0f * 5 * cam.Z;
                }
                else if (RAGE.Game.Pad.IsControlPressed(0, 269))
                {
                    Player.LocalPlayer.FreezePosition(false);
                    pos.X = pos.X - 1.0f * cam.X;
                    pos.Y = pos.Y - 1.0f * cam.Y;
                    pos.Z = pos.Z - 1.0f * cam.Z;
                }
                else if (RAGE.Game.Pad.IsControlPressed(0, 205))
                {
                    Player.LocalPlayer.FreezePosition(false);
                    pos.Z = pos.Z - 0.2f;
                }
                else if (RAGE.Game.Pad.IsControlPressed(0, 206))
                {
                    Player.LocalPlayer.FreezePosition(false);
                    pos.Z = pos.Z + 0.2f;
                }
                else
                {
                    Player.LocalPlayer.FreezePosition(true);
                }
                Player.LocalPlayer.Position = pos;
            }

        }

        public static void Notify(string text)
        {
            RAGE.Game.Ui.SetNotificationTextEntry("STRING");
            RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);
            RAGE.Game.Ui.DrawNotification(true, false);
        }

        public static void KeyHandler()
        {

            //if (RAGE.Game.Pad.IsControlJustPressed(1, 289))
            //{
            //    isNoclip = !isNoclip;
            //    if (isNoclip)
            //    {
            //        startNoClip();
            //        Notify("~c~Noclip ~g~ активирован");
            //    }
            //    else
            //    {
            //        stopNoClip();
            //        Notify("~c~Noclip ~r~ отключен");
            //    }

            //}
        }

        public static void startNoClip()
        {
            Player.LocalPlayer.SetInvincible(true);
            Player.LocalPlayer.SetVisible(false, false);
            Player.LocalPlayer.SetCollision(false, false);
        }
        public static void stopNoClip()
        {
            Player.LocalPlayer.FreezePosition(false);
            Player.LocalPlayer.SetInvincible(false);
            Player.LocalPlayer.SetVisible(true, false);
            Player.LocalPlayer.SetCollision(true, false);
        }
        public static Vector3 getCamDirection()
        {
            float heading = RAGE.Game.Cam.GetGameplayCamRelativeHeading() + Player.LocalPlayer.GetHeading();
            float pitch = RAGE.Game.Cam.GetGameplayCamRelativePitch();

            Vector3 dir = new Vector3(
               (float)-Math.Sin(heading * Math.PI / 180),
               (float)Math.Cos(heading * Math.PI / 180),
               (float)Math.Sin(pitch * Math.PI / 180.0));

            double len = Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y + dir.Z * dir.Z);
            if (len != 0)
            {
                dir.X = dir.X / (float)len;
                dir.Y = dir.Y / (float)len;
                dir.Z = dir.Z / (float)len;
            }
            return dir;
        }
    }









}
