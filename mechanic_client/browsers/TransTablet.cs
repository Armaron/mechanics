using cs_packages.client;
using Newtonsoft.Json;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace mechanic_client.browsers
{
    partial class Tablet : Events.Script
    {
        public Tablet()
        {
            Events.Add("openclose.tablet", OpenCloseTrans);
            Events.Add("exitMedicalNote", OpenCloseTrans);
            Events.Add("lockTablet", OpenCloseTrans);
            Events.Add("exitNote", OpenCloseTrans);

            Events.Add("hireTransportCompany", HireTransportCompany);                           
            Events.Add("dissmisalTransportCompany", DissmisalTransportCompany);                 
            Events.Add("dissmisalTransWorkerOnTablet", DissmisalTransWorkerOnTablet);       
            Events.Add("hireTransWorkerOnTablet", HireTransWorkerOnTablet);                 
            Events.Add("buyAutoTransportCompany", BuyAutoTransportCompany);                 
            Events.Add("LoadBuisnessPageTrans", LoadBuisnessPageTrans);                             
            Events.Add("changeNameTransportCompany", ChangeNameTransportCompany);               
        }

        public static string jsonBuis = ""; //++
        public static Trans_Buis trans_b = new Trans_Buis();

        private void ChangeNameTransportCompany(object[] args)
        {
            trans_b.Name = args[0].ToString();
            Events.CallRemote("Update_Name_Buis_Trans", args[0].ToString());
            jsonBuis = JsonConvert.SerializeObject(trans_b);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"pushTransportCompany('{jsonBuis}')");
        }

        private void LoadBuisnessPageTrans(object[] args)
        {
           
            SortedList<string, string> listWorkers = JsonConvert.DeserializeObject<SortedList<string, string>>(args[4].ToString());
            List<WorkersTrans> workers = new List<WorkersTrans>();
          
            foreach (var item in listWorkers)
            {
                
                workers.Add(new WorkersTrans(item.Key, item.Value, args[5].ToString()));
            }

            trans_b = new Trans_Buis(args[0].ToString(), args[1].ToString(), (int)args[2], (int)args[3], workers);
            jsonBuis = JsonConvert.SerializeObject(trans_b);
            
            cs_packages.browsers.Tablet.phone.ExecuteJs($"initTransportCompany('{jsonBuis}')");
           
        }

        private void BuyAutoTransportCompany(object[] args)
        {
            trans_b.TrucksCount = trans_b.TrucksCount + 1;
            jsonBuis = JsonConvert.SerializeObject(trans_b);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"pushTransportCompany('{jsonBuis}')");
            Events.CallRemote("Update_Cars_Count_Trans", args[0].ToString(), (int)args[1]);
        }

        private void HireTransWorkerOnTablet(object[] args)
        {
            trans_b.WorkersListTrans.Add(new WorkersTrans(args[0].ToString(), DateTime.Now.ToString(), null));
            jsonBuis = JsonConvert.SerializeObject(trans_b);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"pushTransportCompany('{jsonBuis}')");
        }

        private void DissmisalTransWorkerOnTablet(object[] args)
        {
            trans_b.WorkersListTrans.Remove(trans_b.WorkersListTrans.Find(x => x.FullName == args[0].ToString()));
            jsonBuis = JsonConvert.SerializeObject(trans_b);
            cs_packages.browsers.Tablet.phone.ExecuteJs($"pushTransportCompany('{jsonBuis}')");
        }

        private void DissmisalTransportCompany(object[] args)
        {
            Events.CallRemote("Find_Player_To_Remove_Trans", args[0].ToString());
        }

        private void HireTransportCompany(object[] args)
        {
            if (args[0] != null)
            {

                Events.CallRemote("Find_Player_To_Add_Trans", args[0].ToString(), Player.LocalPlayer.GetSharedData("typeTrans").ToString());

            }
            else
            {
                Mechanic_Client.Notify("Введите имя");
            }
        }

        public static void OpenCloseTrans(object[] args)
        {
            if (Player.LocalPlayer.GetSharedData("transBuisness") != null)
            {
                
                {
                    if (cs_packages.browsers.Tablet.phone == null)
                    {



                        cs_packages.browsers.Tablet.phone = new HtmlWindow("package://auth/assets/tablet.html");
                        KeyManager.block = 10;
                        Events.CallRemote("Load_Buisness_Trans");//++
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
                }
            }
        }
    }
}
