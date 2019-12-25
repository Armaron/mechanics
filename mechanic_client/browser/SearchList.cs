using System;
using System.Collections.Generic;
using System.Text;
using cs_packages.client;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
   public class SearchList : Events.Script
    {
        static public HtmlWindow search = null;
        static string text = "";
        static string name="";
        public SearchList()
        {
            Events.Add("OpenSearch", OpenSearch); //наручники
            Events.Add("OpenWeapon", OpenWeapon); //наручники
            Events.Add("OpenGarage", OpenGarage); //наручники
            Events.Add("OpenEvedence", OpenEvedence); //наручники
            Events.Add("closeProofsMenu", CloseProofsMenu);
            Events.Add("proofsThing", ProofsThing);
            Events.Add("closePoliceAmmunition", CloseProofsMenu);
            Events.Add("LspdUseWeapon", LspdUseWeapon);
            Events.Add("LspdUseGarage", LspdUseGarage);
            Events.Add("closePoliceGarage", CloseProofsMenu);
            Events.Add("closeEvidenceMenu", CloseProofsMenu);
            Events.Add("evidenceList", EvidenceList);
            Events.Add("OpenWardrobe", OpenWardrobe);
            Events.Add("LspdUseWardrobe", LspdUseWardrobe);
        }
        public void LspdUseWardrobe(object[] args)
        {
            Events.CallRemote("LspdUseWardrobeServer", args[0].ToString());
            CloseProofsMenu(null);

          

        }
        public void OpenWardrobe(object[] args)
        {
            search = new HtmlWindow("package://auth/assets/policeWardrobe.html");
            search.Active = true;
            search.ExecuteJs("pushWardrobe('" + args[0].ToString() + "');");

            KeyManager.block = 13;

            Cursor.Visible = true;
            Chat.Show(false);


        }
        public void LspdUseWeapon(object[] args)
        {
            Events.CallRemote("GetWeaponPolice", args[0].ToString(),args[1]);
            CloseProofsMenu(null);



        }
        public void EvidenceList(object[] args)
        {
            //Chat.Output(args[1].ToString());

            int id = Convert.ToInt32(args[0]);
            if (name.Contains("камеры"))
            {
                 text = "Обнаружено при просмотре " + name + ": ";
            }
            else
            {
                text = "Найдено при обыске " + name + ": ";
            }
            CloseProofsMenu(null);
            Events.CallRemote("ProofsAdd", id, text, args[1].ToString(), name);





        }
        public void LspdUseGarage(object[] args)
        {
            Events.CallRemote("GetCarPolice", args[0].ToString());
            CloseProofsMenu(null);



        }
        public void OpenSearch(object[] args)
        {
            search = new HtmlWindow("package://auth/assets/proofsMenu.html");
            search.Active = true;
            search.ExecuteJs("pushProofs('" + args[0].ToString() + "');");
            text = args[0].ToString();
            KeyManager.block = 13;

            Cursor.Visible = true;
            Chat.Show(false);


        }
        public void OpenEvedence(object[] args)
        {
            name = args[0].ToString();
            search = new HtmlWindow("package://auth/assets/evidenceMenu.html");
            search.Active = true;
            search.ExecuteJs("pushEvidence('" + args[1].ToString() + "');");
            text = args[1].ToString();
            KeyManager.block = 13;

            Cursor.Visible = true;
            Chat.Show(false);


        }
        public void OpenWeapon(object[] args)
        {
            search = new HtmlWindow("package://auth/assets/policeWeapon.html");
            search.Active = true;
            search.ExecuteJs("pushWeapons('"+ args[0].ToString()+"');");
           
            KeyManager.block = 13;

            Cursor.Visible = true;
            Chat.Show(false);


        }
        public void OpenGarage(object[] args)
        {
            search = new HtmlWindow("package://auth/assets/policeGarage.html");
            search.Active = true;
            search.ExecuteJs("pushGarage('" + args[0].ToString() + "');");

            KeyManager.block = 13;

            Cursor.Visible = true;
            Chat.Show(false);


        }
        public static void CloseProofsMenu(object[] args)
        {
            
            search.Active = false;
            search.Destroy();
            search = null;
            

            KeyManager.block = 0;

            Cursor.Visible = false;
            Chat.Show(true);


        }
        public void ProofsThing(object[] args)
        {

            Events.CallRemote("ProofsAdd",args[0],text);




            search.Active = false;
            search.Destroy();
            search = null;


            KeyManager.block = 0;

            Cursor.Visible = false;
            Chat.Show(true);


        }


    }
}
