using RAGE;
using RAGE.Elements;
using System;
using System.Collections.Generic;

namespace cs_packages.player
{
    public class VoiceChat : Events.Script
    {
        public VoiceChat()
        {

            Events.OnPlayerQuit += RemoveFromVoice;
        }

        public static long LatestProcess = 0;
        public static bool loaded = false;
        public static float MaxDistance = 60.0f;
        public static int VoiceToggleControl = 0x73;
        public static bool Voice3d = true;

        public static List<Player> ActivePlayers = new List<Player>();

        static private void Add(Player player)
        {
            if (loaded)
            {
                player.SetData<bool>("IsListening", true);
                Events.CallRemote("add_voice_listener", player);

                ActivePlayers.Add(player);

                player.Voice3d = Voice3d;
            }
        }

        static private void Remove(Player player, bool notify)
        {
            try
            {


                ActivePlayers.Remove(player);
            }
            catch
            
            {

            }
            try
            {
                if ((int)player.GetSharedData("abonent") == (int)RAGE.Elements.Player.LocalPlayer.GetSharedData("phonenumber"))
                {
                    return;
                }
            }
            catch
            {

            }

            player.SetData<bool>("IsListening", false);


            Events.CallRemote("remove_voice_listener", player);

        }


        static public void RemoveAllVoiceNOW()
        {
            for (int i = 0; i < ActivePlayers.Count; i++)
            {
                Player player = ActivePlayers[i];
                player.SetData<bool>("IsListening", false);


                Events.CallRemote("remove_voice_listener", player);

            }

        }

        static public void RemoveFromVoice(Player player)
        {
            Remove(player, false);
        }

        internal static bool LatestKeyState = false;

        static public void VoiceProcess()
        {

            if (loaded == true)
            {


                Vector3 localPosition = Player.LocalPlayer.Position;

                List<Player> players = Entities.Players.All;
                for (int i = 0; i < players.Count; i++)
                {

                    if (players[i] != Player.LocalPlayer)
                    {
                        if (!players[i].GetData<bool>("IsListening"))
                        {
                            if (!browsers.RadioStation.PlayerInRadio.Contains(players[i]))
                            {


                                float dist = (localPosition.DistanceToSquared(players[i].Position));

                                if (Math.Abs(dist) < Math.Abs(MaxDistance))
                                {
                                    Add(players[i]);
                                }
                            }
                        }
                    }

                }
                for (int i = 0; i < ActivePlayers.Count; i++)
                {

                    
                        if (!browsers.RadioStation.PlayerInRadio.Contains(ActivePlayers[i]))
                        {


                            if (ActivePlayers[i].Handle != 0)
                            {
                                float dist = (localPosition.DistanceToSquared(ActivePlayers[i].Position));

                                if (dist > MaxDistance)
                                {
                                    Remove(ActivePlayers[i], true);
                                }
                                else
                                {
                                    try
                                    {
                                        if ((int)ActivePlayers[i].GetSharedData("abonent") == (int)RAGE.Elements.Player.LocalPlayer.GetSharedData("phonenumber"))
                                        {
                                            return;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    ActivePlayers[i].VoiceVolume = (1.0f - ((dist / MaxDistance) / 2));
                                }
                            }
                            else
                            {
                                Remove(ActivePlayers[i], true);
                            }
                        }

                  

                }
            }
            browsers.RadioStation.UpdateVoice();
        }

        public static void Notify(string text)
        {
           // RAGE.Game.Ui.SetNotificationTextEntry("STRING");
           // RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);
           // RAGE.Game.Ui.DrawNotification(false, false);
        }
    }

    public class Util
    {
        static public long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}