using System;
using System.Collections.Generic;
using System.Text;
using cs_packages.client;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
    public class BankTerminal : Events.Script, IPlaces
    {
        static public HtmlWindow terminal = null;
        public BankTerminal()
        {
            //From server
            Events.Add("open.terminal", Open);



            //From CEF
            Events.Add("cashpoint.exit", Close);
            Events.Add("cashpoint.withdrawal", CashPoint);
            Events.Add("cashpoint.putback", CashPointCard);
            Events.Add("cashpoint.topup", CashPointTel);

            //cashpointInit(card, cash, terminal, maxwithdrawal, phoneNumber)
            //maxwithdrawal - макс на снятие
            //номер телефона для инфы
            //mp.trigger("cashpoint.topup", currentVal);
            //уже был триггер


        }

        public void Open(object[] args)
        {
            int cardCash = Convert.ToInt32(args[0]);
            int handCash = Convert.ToInt32(args[1]);
            int terminalNumber = Convert.ToInt32(args[2]);
            int maxwithdrawal = Convert.ToInt32(args[3]);
            int phoneNumber = Convert.ToInt32(args[4]);
            //Chat.Output("cardCash " + args[0]);
            //Chat.Output("handCash" + args[1]);
            //Chat.Output("terminalNumber " + args[2]);
            //Chat.Output("maxwithdrawal " + args[3]);
            //Chat.Output("phoneNumber " + args[4]);
            

            KeyManager.block = 2;
            terminal = new HtmlWindow("package://auth/assets/cashpoint.html");
            terminal.Active = true;
            //cashpointInit(card,cash,terminal,maxwithdrawal,phoneNumber)
            terminal.ExecuteJs("cashpointInit('"+cardCash +"','" +handCash +"','" + terminalNumber + "','" + maxwithdrawal + "','" + phoneNumber + "');");
            Cursor.Visible = true;
            Chat.Show(false);
           
        }
        public void Close(object[] args)
        {
            KeyManager.block = 0;
            terminal.Active = false;
            //terminal.Destroy();
            terminal = null;
            Cursor.Visible = false;
            Chat.Show(true);
        }
        /// <summary>
        /// Снятие наличных
        /// </summary>
        /// <param name="args"></param>
        public void CashPoint(object[] args)
        {
            int CurentVal = Convert.ToInt32(args[0]);
            Events.CallRemote("ChangeCash", -CurentVal, CurentVal);
        }
        /// <summary>
        /// Положить на карту
        /// </summary>
        /// <param name="args"></param>
        public void CashPointCard(object[] args)
        {
            int CurentVal = Convert.ToInt32(args[0]);
            Events.CallRemote("ChangeCash", CurentVal, -CurentVal);


        }

        /// <summary>
        /// Пополнить счет телефона
        /// </summary>
        /// <param name="args"></param>
        public void CashPointTel(object[] args)
        {
            int CurentVal = Convert.ToInt32(args[0]);
            Events.CallRemote("PayPhone", CurentVal);

        }

    }
}
