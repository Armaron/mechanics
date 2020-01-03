using Newtonsoft.Json;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;


namespace cs_packages.vehicle
{
    public class AutoSchoolTheory : Events.Script // название класса должно описывать его сущность
    {
        public static HtmlWindow AutoSchoolBrowser = null;
        
        public AutoSchoolTheory()
        {


            Events.Add("autoSchool", AutoSchool);
            Events.Add("examSchool", ExamSchool);
            Events.Add("payExam", PayExam);
            Events.Add("startExam", StartExam);
         //   Chat.Output("Загрузка клиентской части!   Loading....");
        }
        public void ExamSchool(object[] args)
        {

            Chat.Output(args[1].ToString());
            Chat.Output(args[0].ToString());

            if(Convert.ToInt32(args[1])>=4)
            
            {
                if(args[0].ToString() == "A")
                {
                    Chat.Output("MOTO");
                    CloseAutoSchool(null);
                    Events.CallRemote("StartPract_A");
                    
                    return;
                }
                if (args[0].ToString() == "B")
                {
                    Chat.Output("AUTO");
                    CloseAutoSchool(null);
                    Events.CallRemote("StartPract_B");
                   
                    return;
                }
                if (args[0].ToString() == "C")
                {
                    Chat.Output("C-category");
                    CloseAutoSchool(null);
                    Events.CallRemote("StartPract_C");
                   
                    return;
                }
                if (args[0].ToString() == "D")
                {
                    Chat.Output("D-category");
                    CloseAutoSchool(null);
                    Events.CallRemote("StartPract_D");
                   
                    return;
                }
                
            }
            else
            {
                CloseAutoSchool(null);
            }

        }
        public void AutoSchool(object[] args)
        {
           
            AutoSchoolBrowser = new HtmlWindow("package://auth/assets/autoschool.html");
            AutoSchoolBrowser.Active = true;
            //   Chat.Output(args[0].ToString());
            //AutoSchoolBrowser.ExecuteJs("pushAutoSchool('" + args[0].ToString() + "','" + "names" + "');");
         //   string[] mass = new bool[] { "true", "true", "true" };
       //     string json = JsonConvert.SerializeObject(mass);
            AutoSchoolBrowser.ExecuteJs("initSchool('true','true');");
            Cursor.Visible = true;
            //  AutoSchool.AutoSchoolItems(null);

        }
        static public void AutoSchoolItems(object[] args)
        {
            RAGE.Events.CallRemote("remote_examSchool", args[0]);
        }
        public void PayExam(object[] args)
        {
            /*проверка на доступность суммы, если сумма есть, списать.
             args[0] = списал или нет */
            // RAGE.Events.CallRemote("remote_payExam", args[0]);
            //RAGE.Events.CallRemote("remote_payExam", args[1]);
            RAGE.Events.CallRemote("remote_payExam", args[1]);
            AutoSchoolBrowser.ExecuteJs("startExam('"+args[0]+"');");
         //   Chat.Output(args[1].ToString());
        }
        static public void CloseAutoSchool(object[] args)
        {
            AutoSchoolBrowser.Destroy();
            AutoSchoolBrowser.Active = false;
            AutoSchoolBrowser = null;
            Cursor.Visible = false;

        }
        public void StartExam(object[] args)
        {

            AutoSchoolBrowser.ExecuteJs("startExam(currentExam)");

        }
    }
}
