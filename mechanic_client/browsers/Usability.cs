using cs_packages.client;
using cs_packages.player;
using RAGE;
using RAGE.Ui;
using System;

namespace cs_packages.browsers
{
    public class Usability : Events.Script
    {

        public static HtmlWindow UsabilityBrowser = null;
        public Usability()
        {


            Events.Add("giveMoney", GiveMoney);

            Events.Add("showPassport", ShowPassport);

            Events.Add("takePassport", TakePassport);
            Events.Add("takeSearch", TakeSearch);
           // Events.Add("takeHandcuff", TakeHandcuff);

            Events.Add("giveKeys", GiveKeys);

            Events.Add("meet", Meet);
            Events.Add("SeePassport", SeePassport);
            Events.Add("closePassport", ClosePassport);
            Events.Add("OpenUsabilityServer", OpenUsability);
            Events.Add("PlayerCircle", PlayerCircle);


            Events.Add("showMedicalCard", ShowMedicalCard);
            Events.Add("watchPatient", WatchPatient);

            Events.Add("reanimation", Reanimation);
            //   Events.Add("repair", Repair);

        }
        public void Reanimation(object[] args)
        {
            Events.CallRemote("ReanimationServer");
            OpenUsability(null);
        }
        public void ShowMedicalCard(object[] args)
        {
            Events.CallRemote("ShowMedicalCardServer");
            OpenUsability(null);
        }
        public void WatchPatient(object[] args)
        {
            Events.CallRemote("WatchPatientServer");
            OpenUsability(null);
        }
        public void PlayerCircle(object[] args)
        {
            string id = args[0].ToString();
            if(id == "meet")
            {
                Meet(args);
            }
        }
        public static void OpenUsability(object[] args)
        {

            if (UsabilityBrowser == null)
            {
                KeyManager.block = 6;


                //var Elem1 = new { id = "takeHandcuff", status = "hide" };
                //var Elem2 = new { id = "takeBag", status = "hide" };
                //var Elem3 = new { id = "repair", status = "hide" };
                //var Elem4 = new { id = "reanimation", status = "hide" };
                //var Elems = new { Elem1, Elem2, Elem3, Elem4 };

                UsabilityBrowser = new HtmlWindow("package://auth/assets/newCircle.html");

                UsabilityBrowser.ExecuteJs("initPlayerCircle('" + args[0].ToString().ToLower() + "','"+ args[1].ToString().ToLower() + "','"+ args[3].ToString().ToLower() + "','" + args[3].ToString().ToLower() + "');");
                Chat.Output("initPlayerCircle('" + args[0].ToString().ToLower() + "','" + args[1].ToString().ToLower() + "','" + args[3].ToString().ToLower() + "','" + args[3].ToString().ToLower() + "');");
                string fraction = args[4].ToString();
                if(fraction!="")
                {
                    UsabilityBrowser.ExecuteJs("addFractionCircle('"+fraction+"');");
                }

                UsabilityBrowser.Active = true;


                //Chat.Show(false);
                Cursor.Visible = true;
                DrawInfo.LoadScreen = false;
            }
            else
            {
                KeyManager.block = 0;

                UsabilityBrowser.Active = false;
                UsabilityBrowser.Destroy();
                UsabilityBrowser = null;


                Chat.Show(true);
                Cursor.Visible = false;
            }


        }



        public void SeePassport(object[] args)
        {
            //  Chat.Output("PASSPORT");
            //  Chat.Output("Пришли авгументы: name - " + args[0].ToString() +
            //"; age - " + args[1] +
            //"; number - " + args[2] +
            //"; id - " + args[3] +
            //"; signature - " + args[4] +
            //"; carlic - " + args[5] +
            //"; lic - " + args[6] +
            //"; carnumb - " + args[7] +
            //"; housenumb - " + args[8] +
            //"; rank - " + args[9] + ";");


            KeyManager.block = 6;
            UsabilityBrowser = new HtmlWindow("package://auth/assets/passport.html");

            UsabilityBrowser.ExecuteJs("menu.pass = {name:'" + args[0].ToString() +
              "',age:" + args[1] + ",number:" + args[2] + ",id:" + args[3] + ",signature:'" + args[4] + "',carlic:'" + args[5].ToString() + "',lic:'" +
              args[6].ToString() + "',carnumb:'" + args[7] + "',housenumb:'" + args[8] + "',rank:'" + args[9].ToString() + "'};");

            Events.CallRemote("DoNotWantMorePassport");

            UsabilityBrowser.Active = true;
            //Chat.Show(false);
            Cursor.Visible = true;

        }

        public void ClosePassport(object[] args)
        {
            //  Chat.Output("CLOSE PASSPORT");

            KeyManager.block = 0;

            UsabilityBrowser.Active = false;

            UsabilityBrowser = null;


            Chat.Show(true);
            Cursor.Visible = false;

            //UsabilityBrowser.ExecuteJs("menu.closePassport();");

        }
        public void GiveMoney(object[] args)
        {
            int count = Convert.ToInt32(args[0]);

            Events.CallRemote("GiveMoney_Server", count);
            if (UsabilityBrowser != null)
            {
                OpenUsability(null);
            }
        }
        public void ShowPassport(object[] args)
        {
            Events.CallRemote("ShowPassport_Server");

            OpenUsability(null);
        }
        public void TakePassport(object[] args)
        {

            Events.CallRemote("TakePassport_Server");

            OpenUsability(null);

        }
        public void TakeSearch(object[] args)
        {

            Events.CallRemote("TakeSearch_Server");

            OpenUsability(null);

        }
        public void TakeHandcuff(object[] args)
        {
            Events.CallRemote("TakeHandcuff_Server");

            OpenUsability(null);


        }
        public void GiveKeys(object[] args)
        {


            Events.CallRemote("GiveKeys_Server");

            OpenUsability(null);

        }
        public void Meet(object[] args)
        {

            Events.CallRemote("Meet_Server");

            OpenUsability(null);

        }




    }
}
