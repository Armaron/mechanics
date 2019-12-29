using cs_packages.client;
using cs_packages.player;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
    public class PoliceCircle : Events.Script
    {
        static public HtmlWindow Circle = null;

        public PoliceCircle()
        {
            Events.Add("takeHandcuff", HandCuff); //наручники
            Events.Add("toLead" , ToLead); //за собой
            Events.Add("seatCar", ToAuto); // в авто

            Events.Add("takeSearch", TakePrints); //отпечатки
            Events.Add("openItem", OpenItem); //вскрыть

            Events.Add("searchEvidence", SearchEvidence); // поиск улик
            Events.Add("barrier", Barrier); // ограждение
            Events.Add("search", EvidenceList);
            Events.Add("closeEvidenceMenu", CloseEvidenceMenu);
            Events.Add("openEvidenceMenu", OpenEvidenceMenu);

        }



        public static void OpenClose()
        {
            Usability.OpenUsability(null);
        }


        public void HandCuff(object[] args)
        {
           // Chat.Output("Cuff");
            Events.CallRemote("Cuff");
            OpenClose();
        }
        public void ToLead(object[] args)
        {
            Events.CallRemote("FOLOW");
            OpenClose();
        }
        public void ToAuto(object[] args)
        {
            Events.CallRemote("INCAR");
            OpenClose();
        }
        public void TakePrints(object[] args)
        {
            Events.CallRemote("TakePrints");
            OpenClose();
        }
        public void OpenItem(object[] args)
        {

        }
        public void SearchEvidence(object[] args)
        {

        }
        public void Barrier(object[] args)
        {

        }
        public void EvidenceList(object[] args)
        {
            Events.CallRemote("SearchInv");
            OpenClose();
        }
        public void OpenEvidenceMenu(object[] args)
        {
         
        }
        public void CloseEvidenceMenu(object[] args)
        {

        }

    }
}
