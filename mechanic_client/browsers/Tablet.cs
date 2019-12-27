using cs_packages.client;
using cs_packages.player;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;

namespace cs_packages.browsers
{
    public class Tablet : Events.Script
    {
        static public HtmlWindow phone = null;
        static uint globalDimension = 4294967295;
        public Tablet()
        {
            Events.Add("openclose.tablet", OpenClose);
            Events.Add("carBuyButton", CarBuyButton);
            Events.Add("lockTablet", OpenClose);
            Events.Add("OpenNote", OpenCloseNote);
            Events.Add("OpenNoteMed", OpenCloseNoteMed);
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
            Events.Add("AddPoliceMark", AddPoliceMark);
            Events.Add("AddMedicMark", AddMedicMark);
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
            Events.CallRemote("HireNewbieMed", args[0]);
        }


        public void HaveUpdate(object[] args)
        {
            
            if(phone!=null)
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
            Events.CallRemote("ViewClews", args[0],args[1]);
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
            Events.CallRemote("PunishmentStatus", args[0], args[1],args[2]);
        }
        public void AddViolationThing(object[] args)
        {
            Events.CallRemote("AddViolationThing", args[0],args[1]);
        }
        public void SellContactAuthData(object[] args)
        {
            Events.CallRemote("SellContactAuthData");
        }
        public void SellContactAuthDataServer(object[] args)
        {
            string data = args[0].ToString();
            phone.ExecuteJs("adsSendData('"+data+"');");
        }
        public void MoveTo(object[] args)
        {
            Vector3 vector3 = new Vector3(Convert.ToSingle(args[0]), Convert.ToSingle(args[1]), Convert.ToSingle(args[2]));
            RAGE.Elements.Player.LocalPlayer.TaskGoToCoordAnyMeans(vector3.X,vector3.Y,vector3.Z,1f,0,false, 786603, 0xbf800000);
            //Chat.Output("Trigger come");
           // Events.CallRemote("HireNewbie", args[0]);
        }
        //public void ReviveThing(object[] args)
        //{
        //    //Chat.Output("Trigger come");
        //    Events.CallRemote("ReviveThing", args[0]);
        //}

        public void AddMedicMark(object[] args)
        {
            Vector3 vector3 = new Vector3(Convert.ToSingle(args[0]), Convert.ToSingle(args[1]), Convert.ToSingle(args[2]));
            Blip blip = new Blip(51, vector3, color: 1, shortRange: false, dimension: globalDimension);
            blip.SetData("SenderPhoneNumber", 00);

            Colshape colshape = new SphereColshape(vector3, 1.0f, globalDimension);
            colshape.SetData("SenderPhoneNumber", 00);

            Phone.blips.Add(blip);
            Phone.colshapes.Add(colshape);

        }


        public void AddPoliceMark(object[] args)
        {
            Vector3 vector3 = new Vector3(Convert.ToSingle(args[0]), Convert.ToSingle(args[1]), Convert.ToSingle(args[2]));
            Blip blip = new Blip(60, vector3, color: 3, shortRange: false, dimension: globalDimension);
            blip.SetData("SenderPhoneNumber", 00);

            Colshape colshape = new SphereColshape(vector3, 1.0f, globalDimension);
            colshape.SetData("SenderPhoneNumber", 00);

             Phone.blips.Add(blip);
             Phone.colshapes.Add(colshape);

        }


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
        public static void OpenCloseNote(object[] args)
        {
            if (phone == null)
            {




                phone = new HtmlWindow("package://auth/assets/policeNotebook.html");
                phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "','" + args[1].ToString() + "','" + args[2].ToString() + "','" + args[3].ToString() + "','"+args[4].ToString() + "','admin','" + args[5].ToString() + "');");
                // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "',filer, archive," +  "'"+args[3].ToString() + "'," + true + ");");
                KeyManager.block = 15;
                Chat.Show(false);

                phone.Active = true;


                Cursor.Visible = true;




                DrawInfo.LoadScreen = false;

            }
            else
            {
                phone.Reload(true);

              
                phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "','" + args[1].ToString() + "','" + args[2].ToString() + "','" + args[3].ToString() + "','" + args[4].ToString() + "','admin','" + args[5].ToString() + "');");
                // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "',filer, archive," +  "'"+args[3].ToString() + "'," + true + ");");
               


               // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "','" + args[1].ToString() + "','" + args[2].ToString() + "','" + args[3].ToString() + "','admin');");
            }
        }
        public static void OpenCloseNoteMed(object[] args)
        {
            if (phone == null)
            {




                phone = new HtmlWindow("package://auth/assets/medicalNotebook.html");
                phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "','admin');");
                // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "',filer, archive," +  "'"+args[3].ToString() + "'," + true + ");");
                KeyManager.block = 15;
                Chat.Show(false);

                phone.Active = true;


                Cursor.Visible = true;
                KeyManager.block = 15;



                DrawInfo.LoadScreen = false;

            }
            else
            {
                phone.Reload(true);
                KeyManager.block = 15;

                phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "','admin');");
                // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "',filer, archive," +  "'"+args[3].ToString() + "'," + true + ");");



                // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "','" + args[1].ToString() + "','" + args[2].ToString() + "','" + args[3].ToString() + "','admin');");
            }
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
            }else
            {
                OpenClose(null);
            }
        }



          public static void OpenClose(object[] args)
        {
            if (RAGE.Elements.Player.LocalPlayer.GetSharedData("mechBuisness") == null)
            {

            if (phone == null)
            {

            

                //  RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100);


                phone = new HtmlWindow("package://auth/assets/tablet.html");
                //    RAGE.Game.Mobile.DisablePhoneThisFrame(true);
                //     RAGE.Game.Mobile.SetPhoneLean(true);//поворрот экрана
                ///  RAGE.Elements.Player.LocalPlayer.PlayAnim("static", "amb@code_human_wander_mobile@male@base", 1f, true, true, true, 1f, 8);
                // RAGE.Elements.Player.LocalPlayer.TaskPlayPhoneGestureAnimation("amb@code_human_wander_mobile@male@base", "static", "BONEMASK_HEAD_NECK_AND_R_ARM",
                //   0.5f,0.25f,true,true);

                KeyManager.block = 10;
                Chat.Show(false);
                phone.Active = true;

                Cursor.Visible = true;

                //// phone.ExecuteJs("settingsInitialize(" + 7 + ");");
                //Chat.Output(args[0].ToString());
                //Chat.Output(args[1].ToString());

                DrawInfo.LoadScreen = false;


            }
            else
            {

                KeyManager.block = 0;
                Chat.Show(true);

                phone.Active = false;
                Cursor.Visible = false;

                phone = null;
                if(RAGE.Elements.Player.LocalPlayer.Vehicle==null)
                {
                    Events.CallLocal("freezestop");
                    Events.CallRemote("stopAnimationPD");//  client.TriggerEvent("freeze");
                }

            }  
            }
        }

    }
}
