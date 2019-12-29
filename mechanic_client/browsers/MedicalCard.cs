using RAGE;
using RAGE.Ui;
using Newtonsoft.Json;
using cs_packages.client;

namespace cs_packages.browsers
{
    public class MedicalCard : Events.Script
    {
        static string MedCard = "";
        static int count = 0;
        static public HtmlWindow MedCardHtml = null;
        public MedicalCard()
        {
            Events.Add("PackSellMedCard", PackSellMedCard);
            Events.Add("PackSellMedCardNext", PackSellMedCardNext);
            Events.Add("OpenMedCard", OpenMedCard);
            Events.Add("closeMenu", CloseMenu);
            Events.Add("OpenMedPlace", OpenMedPlace);
            Events.Add("chooseOperation", ChooseOperation);
            Events.Add("closeOperationsMenu", CloseMenu);
            Events.Add("startTreatment", StartTreatment);
            Events.Add("sendDiagnosis", Diagnosis);
            Events.Add("sendHome", SendHome);


        }
        public void StartTreatment(object[] args)
        {

            Events.CallRemote("StartTreatmentServer");
            CloseMenu(null);
        }
        public void Diagnosis(object[] args)
        {
            string text = args[0].ToString();
            Events.CallRemote("DiagnosisServer",text);
            CloseMenu(null);
        }
        public void SendHome(object[] args)
        {
           
            Events.CallRemote("SendHomeServer");
            CloseMenu(null);
        }
        public void ChooseOperation(object[] args)
        {
            string title = args[0].ToString();
            string elem = args[1].ToString();
            Events.CallRemote("SendOperation",title,elem);
            CloseMenu(null);
        }

        public void PackSellMedCard(object[] args)
        {
            MedCard = "";
            count = 0;
            Events.CallRemote("GetNextMedPack", count);
        }
        public void CloseMenu(object[] args)
        {
            KeyManager.block = 2;
            Chat.Show(true);

            MedCardHtml.Active = false;
            MedCardHtml.Destroy();
            Cursor.Visible = false;

            MedCardHtml = null;
           
        }

        public void PackSellMedCardNext(object[] args)
        {
            MedCard += args[0].ToString();
            count ++;
            Events.CallRemote("GetNextMedPack", count);
        }
        public void OpenMedCard(object[] args)
        {
            MedCardHtml = new HtmlWindow("package://auth/assets/personalCard.html");
            string name = args[0].ToString();
            var elem = new { title = name, view = MedCard };
            Chat.Show(false);
            KeyManager.block = 2;
            MedCardHtml.ExecuteJs("pushPersonalCard('"+JsonConvert.SerializeObject(elem)+"')");
            MedCardHtml.Active = true;
            Cursor.Visible = true;
        }
        public void OpenMedPlace(object[] args)
        {
            MedCardHtml = new HtmlWindow("package://auth/assets/operationMenu.html");

            string room = args[0].ToString();
            KeyManager.block = 2;

            MedCardHtml.ExecuteJs("pushRoom('" + room + "')");
            MedCardHtml.Active = true;
            Cursor.Visible = true;
        }

    }
}
