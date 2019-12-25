
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

            Events.Add("openclose.tablet", OpenClose);
            Events.Add("carBuyButton", CarBuyButton);
           // Events.Add("lockTablet", OpenClose);

            Events.Add("exitNote", OpenClose);
            Events.Add("changeThing", EditState);
            Events.Add("addThing", AddThing);
            Events.Add("searchArchive", SearchArchive);
            Events.Add("searchThing", SearchThing);
            Events.Add("FindArchive", FindArchive);
            Events.Add("FindThing", FindThing);
            Events.Add("moreThings", MoreThings);
            Events.Add("moreArchive", MoreArchive);
            Events.Add("MoreThingsServer", MoreThingsServer);
            Events.Add("MoreArchiveServer", MoreArchiveServer);

            Events.Add("searchPersonal", SearchPerson);
            Events.Add("policeOnlineChange", PoliceOnlineChange);
            Events.Add("giveLicence", GiveLicence);
            Events.Add("entryToThing", EntryToThing);
            Events.Add("addViolator", AddViolator);
            //   Events.Add("addViolationThing", AddViolationThing);
            Events.Add("person", Persons);
            Events.Add("sellContactAuthData", SellContactAuthData);
            Events.Add("MoveTo", MoveTo);
            // Events.Add("reviveThing", ReviveThing);
            Events.Add("SellContactAuthDataServer", SellContactAuthDataServer);
            Events.Add("addViolationThing", AddViolationThing);
            Events.Add("punishmentStatus", PunishmentStatus);
            Events.Add("hireNewbie", HireNewbie);
            Events.Add("reviveThing", ReviveThing);
            Events.Add("viewClews", ViewClews);
            Events.Add("removeWanted", RemoveWanted);
            Events.Add("HaveUpdate", HaveUpdate);
            Events.Add("GetUpdate", GetUpdate);
            Events.Add("exitMedicalNote", OpenClose);
            Events.Add("medicalOnlineChange", MedicalOnlineChange);
            Events.Add("hireNewbieMedical", HireNewbieMedical);
            Events.Add("lockTablet", OpenClose);
            Events.Add("hireMechanical", HireMechanical);                                 //+
            Events.Add("dissmisalMechanical", DissmisalMechanical);                       //+
            Events.Add("dissmisalMechanicalOnTablet", DissmisalMechanicalOnTablet);       //+
            Events.Add("hireMechanicalOnTablet", HireMechanicalOnTablet);                 //+
            Events.Add("buyTransportMechanics", BuyTransportMechanics);                   //+
            Events.Add("LoadBuisnessPage", LoadBuisnessPage);                             //+
            Events.Add("changeNameMechanical", ChangeNameMechanical);                     //+
            //client.TriggerEvent("OpenNote", MembersJSON, PersonsJSON, ViolatorsJSON, StatementJSON);
        }


        public void MedicalOnlineChange(object[] args)
        {
            string current = args[0].ToString();
            string name = args[1].ToString();
            if (current == "upRank")
            {
                Events.CallRemote("UPrankMed", name);
            }
            if (current == "downRank")
            {
                Events.CallRemote("DOWNrankMed", name);
            }
            if (current == "removeRank")
            {
                Events.CallRemote("REMOVEMed", name);
            }

        }
        public void HireNewbieMedical(object[] args)
        {
            Events.CallRemote("HireNewbieMedical", args[0]);
        }


        public void HaveUpdate(object[] args)
        {

            if (phone != null)
                phone.ExecuteJs("reloadNotebook();");
        }
        public void GetUpdate(object[] args)
        {

            Events.CallRemote("GetUpdate", args[0]);
        }
        public void RemoveWanted(object[] args)
        {
            Events.CallRemote("RemoveWantedServer", args[0]);


        }
        public void GiveLicence(object[] args)
        {
            Events.CallRemote("GiveLicenceServer", args[0]);


        }
        public void AddThing(object[] args)
        {
            Events.CallRemote("AddThingServer", args[0]);


        }
        public void ViewClews(object[] args)
        {
           
            Events.CallRemote("ViewClews", args[0], args[1]);
            OpenClose(null);

        }
        public void ReviveThing(object[] args)
        {
            Events.CallRemote("ReviveThing", args[0]);
        }
        public void HireNewbie(object[] args)
        {
            Events.CallRemote("HireNewbie", args[0]);
        }
        public void PunishmentStatus(object[] args)
        {
            Events.CallRemote("PunishmentStatus", args[0], args[1], args[2]);
        }
        public void AddViolationThing(object[] args)
        {
            Events.CallRemote("AddViolationThing", args[0], args[1]);
        }
        public void SellContactAuthData(object[] args)
        {
            Events.CallRemote("SellContactAuthData");
        }
        public void SellContactAuthDataServer(object[] args)
        {
            string data = args[0].ToString();
            phone.ExecuteJs("adsSendData('" + data + "');");
        }
        public void MoveTo(object[] args)
        {
            Vector3 vector3 = new Vector3(Convert.ToSingle(args[0]), Convert.ToSingle(args[1]), Convert.ToSingle(args[2]));
            RAGE.Elements.Player.LocalPlayer.TaskGoToCoordAnyMeans(vector3.X, vector3.Y, vector3.Z, 1f, 0, false, 786603, 0xbf800000);
            //Chat.Output("Trigger come");
            // Events.CallRemote("HireNewbie", args[0]);
        }
        //public void ReviveThing(object[] args)
        //{
        //    //Chat.Output("Trigger come");
        //    Events.CallRemote("ReviveThing", args[0]);
        //}



        public void SearchPerson(object[] args)
        {
            //Chat.Output("Trigger come");
            Events.CallRemote("SearchPerson", args[0]);
        }
        public void Persons(object[] args)
        {
            // Chat.Output("Trigger come");

            phone.ExecuteJs("personInit('" + args[0].ToString() + "');");


        }
        //public void AddViolationThing(object[] args)
        //{
        //    // Chat.Output("Trigger come");
        //    Events.CallRemote("AddViolationThing", Convert.ToInt32(args[0]), Convert.ToInt32(args[0]));
        //}
        public void EntryToThing(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("EntryToThing", Convert.ToInt32(args[0]), args[1].ToString());
        }
        public void AddViolator(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("AddViolator", Convert.ToInt32(args[0]), args[1].ToString());

        }

        public void PoliceOnlineChange(object[] args)
        {
            string current = args[0].ToString();
            string name = args[1].ToString();
            if (current == "upRank")
            {
                Events.CallRemote("UPrank", name);
            }
            if (current == "downRank")
            {
                Events.CallRemote("DOWNrank", name);
            }
            if (current == "removeRank")
            {
                Events.CallRemote("REMOVE", name);
            }

        }

        public void MoreThingsServer(object[] args)
        {
            // Chat.Output("Trigger come");
            phone.ExecuteJs("appendThings('" + args[0].ToString() + "');");
        }

        public void MoreArchiveServer(object[] args)
        {
            // Chat.Output("Trigger come");
            phone.ExecuteJs("appendArchive('" + args[0].ToString() + "');");
        }


        public void MoreThings(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("MoreThings", args[0]);
        }
        public void MoreArchive(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("MoreArchive", args[0]);
        }
        public void FindArchive(object[] args)
        {
            // Chat.Output("Trigger come");
            phone.ExecuteJs("searchArchive('" + args[0].ToString() + "');");
        }

        public void FindThing(object[] args)
        {
            // Chat.Output("Trigger come");
            phone.ExecuteJs("searchThing('" + args[0].ToString() + "');");
        }





        public void SearchArchive(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("SearchArchive", args[0]);
        }

        public void SearchThing(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("SearchThing", args[0]);
        }


        public void EditState(object[] args)
        {
            // Chat.Output("Trigger come");
            Events.CallRemote("NoteBookEdit", args[0]);
        }
        public void CarBuyButton(object[] args)
        {
            Events.CallRemote("BuyCar", args[0], args[1], args[2], args[3], args[4]);
        }
       
       

        public static void InPDVehicle()
        {
            if (RAGE.Elements.Player.LocalPlayer.Vehicle != null)
            {
                if (RAGE.Elements.Player.LocalPlayer.GetSharedData("PoliceMember") != null)
                {
                    if ((bool)RAGE.Elements.Player.LocalPlayer.GetSharedData("PoliceMember"))
                    {
                        Events.CallRemote("GetCarPoliceNote");
                    }
                }
            }
            else
            {
                OpenClose(null);
            }
        }



        public static string jsonBuis = ""; //++
        public static Mech_Org mechBuis = new Mech_Org();    //++
        static public HtmlWindow phone = null;
        

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
                phone.ExecuteJs($"initCustoms('{jsonBuis}')");
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
            Chat.Output(args[0].ToString());
            if (args[0] != null) {

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
            phone.ExecuteJs($"updateCustoms('{jsonBuis}')");
        }

        public static void HireMechanicalOnTablet(object[] args)//++
        {
            mechBuis.WorkersList.Add(new Workers(args[0].ToString(), DateTime.Now.ToString(), null));
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            phone.ExecuteJs($"updateCustoms('{jsonBuis}')");
        }


        private void ChangeNameMechanical(object[] args)//++
        {
            // Chat.Output(args[0].ToString());
            mechBuis.Name = args[0].ToString();
            Events.CallRemote("Update_Name_Buis", args[0].ToString());
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            phone.ExecuteJs($"updateCustoms('{jsonBuis}')");
        }

        public static void BuyTransportMechanics(object[] args)//++
        {
           
            mechBuis.TrucksCount = mechBuis.TrucksCount + 1;
            jsonBuis = JsonConvert.SerializeObject(mechBuis);
            phone.ExecuteJs($"pushCustoms('{jsonBuis}')");
            Events.CallRemote("Update_Cars_Count", args[0].ToString(), (int)args[1]);
        }

        public static void OpenClose(object[] args)
        {
            if (Player.LocalPlayer.GetSharedData("mechBuisness") != null)
            {
               // if (Player.LocalPlayer.GetSharedData("Fraction").ToString() == "mechs")
               // {
                    {
                        if (phone == null)
                        {


                            //Chat.Output(Player.LocalPlayer.GetSharedData("Fraction").ToString());
                            //  RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100);
                            //Tablet
                            phone = new HtmlWindow("package://auth/assets/tablet.html");
                            KeyManager.block = 10;
                        Events.CallRemote("Load_Buisness");//++
                            phone.Active = true;
                            Cursor.Visible = true;

                        }
                        else
                        {
                            KeyManager.block = 0;
                            Chat.Show(true);

                            phone.Active = false;
                            Cursor.Visible = false;

                            phone = null;
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
