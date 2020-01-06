
using cs_packages.client;
using Newtonsoft.Json;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;
using System.Collections.Generic;

namespace mechanic_client
{
    partial class Tablet : Events.Script
    {


        public Tablet()
        {

            // Events.Add("openclose.tablet", OpenClose);
            // Events.Add("carBuyButton", CarBuyButton);
            //// Events.Add("lockTablet", OpenClose);
            //
            // Events.Add("exitNote", OpenClose);
            // Events.Add("changeThing", EditState);
            // Events.Add("addThing", AddThing);
            // Events.Add("searchArchive", SearchArchive);
            // Events.Add("searchThing", SearchThing);
            // Events.Add("FindArchive", FindArchive);
            // Events.Add("FindThing", FindThing);
            // Events.Add("moreThings", MoreThings);
            // Events.Add("moreArchive", MoreArchive);
            // Events.Add("MoreThingsServer", MoreThingsServer);
            // Events.Add("MoreArchiveServer", MoreArchiveServer);
            //
            // Events.Add("searchPersonal", SearchPerson);
            // Events.Add("policeOnlineChange", PoliceOnlineChange);
            // Events.Add("giveLicence", GiveLicence);
            // Events.Add("entryToThing", EntryToThing);
            // Events.Add("addViolator", AddViolator);
            // //   Events.Add("addViolationThing", AddViolationThing);
            // Events.Add("person", Persons);
            // Events.Add("sellContactAuthData", SellContactAuthData);
            // Events.Add("MoveTo", MoveTo);
            // // Events.Add("reviveThing", ReviveThing);
            // Events.Add("SellContactAuthDataServer", SellContactAuthDataServer);
            // Events.Add("addViolationThing", AddViolationThing);
            // Events.Add("punishmentStatus", PunishmentStatus);
            // Events.Add("hireNewbie", HireNewbie);
            // Events.Add("reviveThing", ReviveThing);
            // Events.Add("viewClews", ViewClews);
            // Events.Add("removeWanted", RemoveWanted);
            // Events.Add("HaveUpdate", HaveUpdate);
            // Events.Add("GetUpdate", GetUpdate);
            // Events.Add("exitMedicalNote", OpenClose);
            // Events.Add("medicalOnlineChange", MedicalOnlineChange);
            // Events.Add("hireNewbieMedical", HireNewbieMedical);
            // Events.Add("lockTablet", OpenClose);

            Events.Add("openclose.tablet", OpenCloseMech);
            Events.Add("exitMedicalNote", OpenCloseMech);
            Events.Add("lockTablet", OpenCloseMech);
            Events.Add("exitNote", OpenCloseMech);

            Events.Add("hireMechanical", HireMechanical);                                 //+
            Events.Add("dissmisalMechanical", DissmisalMechanical);                       //+
            Events.Add("dissmisalMechanicalOnTablet", DissmisalMechanicalOnTablet);       //+
            Events.Add("hireMechanicalOnTablet", HireMechanicalOnTablet);                 //+
            Events.Add("buyTransportMechanics", BuyTransportMechanics);                   //+
            Events.Add("LoadBuisnessPage", LoadBuisnessPage);                             //+
            Events.Add("changeNameMechanical", ChangeNameMechanical);                     //+
            //client.TriggerEvent("OpenNote", MembersJSON, PersonsJSON, ViolatorsJSON, StatementJSON);
        }


        public static string jsonBuis = ""; //++
        public static Mech_Org mechBuis = new Mech_Org();    //++
                                                             //static public HtmlWindow cs_packages.browsers.Tablet.phone = null;


        // private static HtmlWindow phone = nu;

        public static void LoadBuisnessPage(object[] args)//++
        {

            //if (phone == null)
            //{
            // phone1 = new HtmlWindow("package://auth/assets/tablet.html");
            // phone = new HtmlWindow("package://auth/assets/tablet.html");
            SortedList<string, string> listWorkers = JsonConvert.DeserializeObject<SortedList<string, string>>(args[4].ToString());
            List<Workers> workers = new List<Workers>();
            // Chat.Output(args[1].ToString() + " " + args[0].ToString());
            foreach (var item in listWorkers)
            {
                //Player.LocalPlayer.GetSharedData("Nickname");
                //Chat.Output(Player.LocalPlayer.GetSharedData("Nickname") + "1");
                workers.Add(new Workers(item.Key, item.Value, args[5].ToString()));
            }
            // List<Player> pl = Entities.Players.All;
            //   foreach (var item in pl)
            //   {
            //       // RAGE.Chat.Output(item.GetSharedData(PlayerData.Nickname).ToString());
            //       if (item.GetSharedData("Nickname") != null)
            //       {
            //           RAGE.Chat.Output(item.GetSharedData("Nickname").ToString() + "1");
            //           if (item.GetSharedData("Nickname").ToString() == fullName)
            //           {
            //               Online = true;
            //           }
            //           else
            //           {
            //               Online = false;
            //           }
            //       }
            //   }
            mechBuis = new Mech_Org(args[0].ToString(), args[1].ToString(), (int)args[2], (int)args[3], workers);
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            //Chat.Output(jsonBuis1);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"initCustoms('{jsonBuis}')");
            //phone.Active = true;

            // }
            // else
            // {
            //
            //     phone.Active = false;
            //     phone = null;
            // }

        }

        private void HireMechanical(object[] args)//++
        {
           // Chat.Output(args[0].ToString());
            if (args[0] != null)
            {

                Events.CallRemote("Find_Player_To_Add", args[0].ToString(), Player.LocalPlayer.GetSharedData("typeCustoms").ToString());

            }
            else
            {
                Mechanic_Client.Notify("Введите имя");
            }
        }

        private void DissmisalMechanical(object[] args)//++
        {
            Events.CallRemote("Find_Player_To_Remove", args[0].ToString());
        }

        private void DissmisalMechanicalOnTablet(object[] args)//++
        {
            mechBuis.WorkersList.Remove(mechBuis.WorkersList.Find(x => x.FullName == args[0].ToString()));
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"updateCustoms('{jsonBuis}')");
        }

        public static void HireMechanicalOnTablet(object[] args)//++
        {
            mechBuis.WorkersList.Add(new Workers(args[0].ToString(), DateTime.Now.ToString(), null));
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"updateCustoms('{jsonBuis}')");
        }


        private void ChangeNameMechanical(object[] args)//++
        {
            // Chat.Output(args[0].ToString());
            mechBuis.Name = args[0].ToString();
            Events.CallRemote("Update_Name_Buis", args[0].ToString());
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"updateCustoms('{jsonBuis}')");
        }

        public static void BuyTransportMechanics(object[] args)//++
        {

            mechBuis.TrucksCount = mechBuis.TrucksCount + 1;
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"pushCustoms('{jsonBuis}')");
            Events.CallRemote("Update_Cars_Count", args[0].ToString(), (int)args[1]);
        }

        public static void OpenCloseMech(object[] args)
        {
            if (Player.LocalPlayer.GetSharedData("mechBuisness") != null)
            {
                // if (Player.LocalPlayer.GetSharedData("Fraction").ToString() == "mechs")
                // {
                {
                    if (cs_packages.browsers.Tablet.phone == null)
                    {


                        //Chat.Output(Player.LocalPlayer.GetSharedData("Fraction").ToString());
                        //  RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100);
                        //Tablet
                        cs_packages.browsers.Tablet.phone = new HtmlWindow("package://auth/assets/tablet.html");
                        KeyManager.block = 10;
                        Events.CallRemote("Load_Buisness");//++
                        cs_packages.browsers.Tablet.phone.Active = true;
                        Cursor.Visible = true;

                    }
                    else
                    {
                        KeyManager.block = 0;
                        Chat.Show(true);

                        cs_packages.browsers.Tablet.phone.Active = false;
                        Cursor.Visible = false;

                        cs_packages.browsers.Tablet.phone = null;
                        if (RAGE.Elements.Player.LocalPlayer.Vehicle == null)
                        {
                            Events.CallLocal("freezestop");
                            Events.CallRemote("stopAnimationPD");//  client.TriggerEvent("freeze");
                        }

                    }
                    //}
                }
            }
        }
    }
}
