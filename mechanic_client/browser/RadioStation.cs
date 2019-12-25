using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;
using cs_packages.client;
using Newtonsoft.Json;

namespace cs_packages.browsers
{
    public class RadioStation : Events.Script
    {

        static float volume = 1;
        static public HtmlWindow radio = null;
        public  static List<RAGE.Elements.Player> PlayerInRadio = new List<RAGE.Elements.Player>();
        public  RadioStation()
        {


            Events.Add("RadioSet", RadioSet);
            Events.Add("RadioChange", RadioChange);
            Events.Add("ResetStation", ResetStation);
            Events.Add("RadioChangeOut", RadioChangeOut);
            Events.Add("raciya.station", ChangeStation);
          
            Events.Add("raciya.volume", ChangeVolume);
        }
        public static void Open()
        {
            if (radio == null)
            {
                KeyManager.block = 8;
                radio = new HtmlWindow("package://auth/assets/raciya.html");
                radio.Active = true;

                Cursor.Visible = true;
            }
            else
            {
                KeyManager.block = 0;
              
                radio.Active = false;
                radio.Destroy();
                radio = null;
                Cursor.Visible = false;
            }
            


        }

        public void ChangeStation(object[] args)
        {
            Events.CallRemote("changeStation", args[0]);
        }


        public void RadioSet(object[] args)
        {
            if (JsonConvert.DeserializeObject<List<RAGE.Elements.Player>>(args[0].ToString()) != null)
                PlayerInRadio =  JsonConvert.DeserializeObject<List<RAGE.Elements.Player>>(args[0].ToString());
            foreach(RAGE.Elements.Player target in PlayerInRadio)
            {
                target.VoiceVolume = volume;
            }
        }
        public void RadioChange(object[] args)
        {

            PlayerInRadio.Add((RAGE.Elements.Player)args[0]);
            foreach (RAGE.Elements.Player target in PlayerInRadio)
            {
                target.VoiceVolume = volume;
            }
        }
        public void RadioChangeOut(object[] args)
        {
            PlayerInRadio.Remove((RAGE.Elements.Player)args[0]);
            foreach (RAGE.Elements.Player target in PlayerInRadio)
            {
                target.VoiceVolume = volume;
            }
        }
        public void ChangeVolume(object[] args)
        {
            volume = Convert.ToInt32(args[0]) / 7;
           
        }
        public static void UpdateVoice()
        {
            
            foreach (RAGE.Elements.Player target in PlayerInRadio)
            {
                target.VoiceVolume = volume;
            }
        }
        public void ResetStation(object[] args)
        {
            PlayerInRadio.Clear();
        }
        
    }
}
