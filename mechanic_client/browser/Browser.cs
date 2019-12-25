using RAGE;
using RAGE.Ui;
using System.Linq;
using cs_packages.client;
using cs_packages.browsers;

namespace cs_packages
{
    public class Browser : Events.Script
    {
        private static object[] parameters = null;
        public static HtmlWindow customBrowser = null;
  
        public Browser()
        {
            Events.Add("createBrowser", CreateBrowserEvent);
            Events.Add("executeFunction", ExecuteFunctionEvent);
            Events.Add("destroyBrowser", DestroyBrowserEvent);
            Events.Add("closeAll", CloseAllBrowsersAndDefreese);
            //   Events.OnBrowserCreated += OnBrowserCreatedEvent;
        }

        public static void ExecuteFunctionEvent(object[] args)
        {
            // Check for the parameters
            string input = string.Empty;
          //  Chat.Output("1651651651");
            // Split the function and arguments
            string function = args[0].ToString();
            object[] arguments = args.Skip(1).ToArray();

            foreach (object arg in arguments)
            {
                // Append all the arguments
                input += input.Length > 0 ? (", '" + arg.ToString() + "'") : ("'" + arg.ToString() + "'");
            }

            // Call the function with the parameters
            customBrowser.ExecuteJs(function + "(" + input + ");");
          //  Chat.Output("1651651651");
        }
        public static void CreateBrowserEvent(object[] args)
        {
            if (customBrowser == null)
            {
                // Get the URL from the parameters
                string url = args[0].ToString();
                parameters = args.Skip(1).ToArray();

                // Create the browser
                customBrowser = new HtmlWindow(url);
                Cursor.Visible = true;
                Chat.Show(true);
                Chat.Activate(false);
                KeyManager.block = -2;

            }
        }

        public static void OnBrowserCreatedEvent(HtmlWindow window)
        {
            if (window.Id == customBrowser.Id)
            {
                // Enable the cursor
                Cursor.Visible = true;

                if (parameters.Length > 0)
                {
                    // Call the function passed as parameter
                    ExecuteFunctionEvent(parameters);
                }
            }
        }

        public static void DestroyBrowserEvent(object[] args)
        {
            // Disable the cursor
            Cursor.Visible = false;

            // Destroy the browser

            if (customBrowser != null)
            {
                customBrowser.Destroy();
                customBrowser = null;
            }
          
           
            Chat.Activate(true);
            KeyManager.block = 0;
            // Menu.CreateAllMenu();
        }

        public static void CloseAllBrowsersAndDefreese(object[] args = null)
        {
            //Chat.Output("Нажал F7"); //DEBUG
            //Chat.Output("Текущий дименшн: " + RAGE.Elements.Player.LocalPlayer.Dimension.ToString());//DEBUG

            if (MakeUpSalon.salonM != null) Events.CallLocal("makeupExit");
            if (TatooSalon.salon != null) Events.CallLocal("tattooExit");
            if (Shop.shop != null) Events.CallLocal("shopExit");
            if (ClothesMarket.salon != null) Events.CallLocal("clothesExit");
          //  if (GasStation.Station != null) Events.CallLocal("exitAzs");
            if (Busines.BusinesBrowser != null) Events.CallLocal("closeBusiness");
            if (BankTerminal.terminal != null) Events.CallLocal("cashpoint.exit");
            if (SellPoint.SellPointBrowser != null) Events.CallLocal("closeSellPoint");

            if (Phone.phone.Active != false) Phone.ClosePhone();
            if (RadioStation.radio != null) RadioStation.Open();
            if (Usability.UsabilityBrowser != null) Usability.OpenUsability(null);
            if (Craft.craft != null) Craft.CreateBrowserEvent(null);

            ////////////////////////////

            if (Menu.menuCef.Active) Menu.OpenMenu();
            if (Menu.animCircleCef.Active) Menu.OpenAnimList();
            if (Tablet.phone != null) Tablet.OpenClose(null);

            TickEvent.carcheck = false;
            Cursor.Visible = false;
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            Events.CallRemote("stopAnimation");











         //   KeyManager.block = 0;
        }


    }
}
