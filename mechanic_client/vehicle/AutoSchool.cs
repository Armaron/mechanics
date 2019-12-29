using cs_packages.player;
using RAGE;
using System;
using System.Threading;

namespace cs_packages.vehicle
{
    partial class AutoSchool : Events.Script
    {

        public RAGE.Elements.Marker mark;
        //public RAGE.Elements.TubeColshape col;
        public RAGE.Elements.SphereColshape cols;
        public static RAGE.Elements.TextLabel text;
        public static bool player_on_exam = false;
        static bool exam_a = false;
        static bool exam_b = false;
        static bool exam_c = false;
        static bool exam_d = false;



        //TimerCallback tm = new TimerCallback(TimerCount);
        Timer timer;
        static int i = 0;
        static uint count_timer;

        public AutoSchool()
        {

            Events.Add("StartAutoSchoolExamPractic_A", StartAutoSchoolExamPractic_A);
            Events.Add("StartAutoSchoolExamPractic_B", StartAutoSchoolExamPractic_B);
            Events.Add("StartAutoSchoolExamPractic_C", StartAutoSchoolExamPractic_C);
            Events.Add("StartAutoSchoolExamPractic_D", StartAutoSchoolExamPractic_D);
            Events.Add("Stop_Exam", StopExam);

            Events.Add("ContinueExam_D", ContinueExam_D);


            Events.OnPlayerEnterColshape += OnPlayerEnterColshape;
            Events.OnPlayerExitColshape += OnPlayerExitColshape;


        }



        public static void SendNotification(string text, string color = "~y~")
        {
            //RAGE.Game.Ui.RemoveNotification(RAGE.Game.Ui.GetCurrentNotification());
            RAGE.Game.Ui.SetNotificationTextEntry("STRING");
            RAGE.Game.Ui.DrawNotificationWithIcon(2, 0, color + text);
            //RAGE.Game.Ui.DrawNotificationWithIcon(2, 0, text);

            ////Events.CallLocal("SendNotification", color + text);
            //Chat.Output(color + text);
        }
        public void TimerOn(uint limit_sec)
        {
            count_timer = limit_sec;
            if (timer == null) timer = new Timer(TimerCount, null, 0, 1000);
        }
        public void TimerOff()
        {
            timer = null;
            count_timer = 0;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void TimerCount(object obj)
        {
            if (count_timer > 0)
            {
                Vector3 text_pos = text.Position;
                //Chat.Output("   " + tickcount_autoschool);
                text.Destroy();
                text = new RAGE.Elements.TextLabel(text_pos, count_timer.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                count_timer--;
            }
            if (count_timer == 0)
            {
                //SendNotification("Время вышло", "~o~");
                //Chat.Output("Время вышло");
                //RAGE.Game.Ui.SetNotificationTextEntry("STRING");
                //RAGE.Game.Ui.DrawNotificationWithIcon(2, 0, "~o~Экзамен не сдан. Время вышло.");
                Events.CallLocal("Stop_Exam");
            }
        }
        //static AutoSchool destroyPoint = new AutoSchool();


        public void StopExam(object[] args)
        {
            //destroyPoint.DestroyPoint();
            DestroyPoint();
            DrawInfo.CarBrake = false;
            DrawInfo.CarForvard = false;
            exam_a = false;
            exam_b = false;
            exam_c = false;
            exam_d = false;
            //if (args[0] != null)
            //{
            //    SendNotification(args[0].ToString(), "~o~");
            //}
            player_on_exam = false;
            Events.CallRemote("ExitVehicle_autoschool");



        }

        public void DestroyPoint()
        {

            TimerOff();
            if (mark != null) mark.Destroy();
            if (cols != null)
            {
                if (cols.GetData<string>("AutoSchool") == "attach_trailer_1")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", null);
                }
                if (cols.GetData<string>("AutoSchool") == "uncouple_trailer_1")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("wants_to_unhook_a_trailer", null);
                }
                if (cols.GetData<string>("AutoSchool") == "attach_trailer_2")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", null);
                }
                cols.Destroy();
            }
            if (text != null) text.Destroy();
        }

        public void StartAutoSchoolExamPractic_A(object[] args)
        {
            Exam_A(new Vector3(1149.934f, 39.90543f, 80.89104f), 30, "colshape-A-11", false, true, 0.8f, "Начало экзамена (категория А)");
            player_on_exam = true;
            exam_a = true;

        }
        public void StartAutoSchoolExamPractic_B(object[] args)
        {
            Exam_B(new Vector3(1110.798f, 17.63148f, 79.75609f), 50, "colshape-B-1", false, true, in_chat: "Начало экзамена (категория B). Первый элемент - змейка.");
            player_on_exam = true;
            exam_b = true;
        }
        public void StartAutoSchoolExamPractic_C(object[] args)
        {
            Exam_C(new Vector3(1107.462f, -65.14445f, 80.84157f), 20, "colshape-C-1", false, true, in_chat: "Начало экзамена (категория C)");
            player_on_exam = true;
            exam_c = true;

        }
        public void StartAutoSchoolExamPractic_D(object[] args)
        {
            Exam_D(new Vector3(1194.264f, 324.1146f, 80.99086f), 20, "colshape-D-1", false, true, in_chat: "Начало экзамена (категория D).");
            player_on_exam = true;
            exam_d = true;
        }

        public void OnPlayerExitColshape(RAGE.Elements.Colshape colshape, RAGE.Events.CancelEventArgs cancel)
        {
            if (exam_d)
            {
                if (colshape.GetData<string>("AutoSchool") == "attach_trailer_1")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", null);
                }
                if (colshape.GetData<string>("AutoSchool") == "uncouple_trailer_1")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("wants_to_unhook_a_trailer", null);
                }
                if (colshape.GetData<string>("AutoSchool") == "attach_trailer_2")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", null);
                }
            }
        }
        public void OnPlayerEnterColshape(RAGE.Elements.Colshape colshape, RAGE.Events.CancelEventArgs cancel)
        {

            if (exam_a)
            {

                if (colshape.GetData<string>("AutoSchool") == "colshape-A-11")
                {
                    Exam_A(new Vector3(1155.137f, 48.50722f, 80.89104f), 15, "colshape-A-12", car_brake: true, radius: 0.8f);
                    RAGE.Game.Ui.DrawNotificationWithIcon(1, 1, "NOTIFICAION");

                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-12")
                {
                    Exam_A(new Vector3(1159.989f, 61.30799f, 80.89104f), 15, "colshape-A-13", car_brake: true, radius: 0.8f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-13")
                {
                    Exam_A(new Vector3(1168.709f, 71.32829f, 80.89114f), 15, "colshape-A-14", car_brake: true, radius: 0.8f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-14")
                {
                    Exam_A(new Vector3(1178.743f, 78.94286f, 80.89114f), 15, "colshape-A-15", car_brake: true, radius: 0.8f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-15")
                {
                    Exam_A(new Vector3(1181.618f, 83.839f, 80.89105f), 15, "colshape-A-16", car_brake: true, radius: 0.8f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-16")
                {
                    Exam_A(new Vector3(1183.935f, 100.3474f, 80.89107f), 15, "colshape-A-17", car_brake: true, radius: 0.8f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-17")
                {
                    Exam_A(new Vector3(1194.964f, 111.5125f, 80.89109f), 15, "colshape-A-18", car_brake: true, radius: 0.8f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-18")
                {
                    Exam_A(new Vector3(1197.6165f, 115.8511895f, 80.89107f), 15, "colshape-A-19", car_brake: true, radius: 1f);
                    return;
                }

                if (colshape.GetData<string>("AutoSchool") == "colshape-A-19")
                {
                    Exam_A(new Vector3(1210.73f, 136.5087f, 80.89037f), 17, "colshape-A-21", car_brake: true, radius: 5f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-21")
                {
                    Exam_A(new Vector3(1248.898f, 198.3508f, 80.89029f), 17, "colshape-A-22", car_brake: true, radius: 5f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-22")
                {
                    Exam_A(new Vector3(1236.986f, 263.5428f, 80.89029f), 17, "colshape-A-23", car_brake: true, radius: 5f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-23")
                {
                    Exam_A(new Vector3(1172.924f, 245.8384f, 80.89073f), 17, "colshape-A-24", car_brake: true, radius: 5f);
                    return;
                }

                if (colshape.GetData<string>("AutoSchool") == "colshape-A-24")
                {
                    Exam_A(new Vector3(1148.504f - 1f, 208.1557f + 0.66f, 80.89073f), 14, "colshape-A-31", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-31")
                {
                    Exam_A(new Vector3(1148.504f + 1f - 1.806666f * 1f, 208.1557f - 0.66f - 3.030266f * 1f, 80.89073f), 14, "colshape-A-32", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-32")
                {
                    Exam_A(new Vector3(1148.504f - 1f - 1.806666f * 2f, 208.1557f + 0.66f - 3.030266f * 2f, 80.89073f), 14, "colshape-A-33", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-33")
                {
                    Exam_A(new Vector3(1148.504f + 1f - 1.806666f * 3f, 208.1557f - 0.66f - 3.030266f * 3f, 80.89073f), 14, "colshape-A-34", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-34")
                {
                    Exam_A(new Vector3(1148.504f - 1f - 1.806666f * 4f, 208.1557f + 0.66f - 3.030266f * 4f, 80.89073f), 14, "colshape-A-35", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-35")
                {
                    Exam_A(new Vector3(1148.504f + 1f - 1.806666f * 5f, 208.1557f - 0.66f - 3.030266f * 5f, 80.89073f), 14, "colshape-A-36", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-36")
                {
                    Exam_A(new Vector3(1148.504f - 1f - 1.806666f * 6f, 208.1557f + 0.66f - 3.030266f * 6f, 80.89073f), 14, "colshape-A-37", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-37")
                {
                    Exam_A(new Vector3(1148.504f + 1f - 1.806666f * 7f, 208.1557f - 0.66f - 3.030266f * 7f, 80.89073f), 14, "colshape-A-38", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-38")
                {
                    Exam_A(new Vector3(1148.504f - 1f - 1.806666f * 8f, 208.1557f + 0.66f - 3.030266f * 8f, 80.89073f), 14, "colshape-A-39", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-39")
                {
                    Exam_A(new Vector3(1148.504f + 1f - 1.806666f * 9f, 208.1557f - 0.66f - 3.030266f * 9f, 80.89073f), 14, "colshape-A-310", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-310")
                {
                    Exam_A(new Vector3(1148.504f - 1f - 1.806666f * 10f, 208.1557f + 0.66f - 3.030266f * 10f, 80.89073f), 14, "colshape-A-311", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-311")
                {
                    Exam_A(new Vector3(1148.504f + 1f - 1.806666f * 11f, 208.1557f - 0.66f - 3.030266f * 11f, 80.89073f), 14, "colshape-A-312", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-312")
                {
                    Exam_A(new Vector3(1126.824f - 1f, 171.7925f + 0.66f, 80.89073f), 15, "colshape-A-313", car_brake: true, radius: 1.1f);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-313")
                {
                    Exam_A(new Vector3(1148.504f - 1.806666f * 16f, 208.1557f - 3.030266f * 16f, 80.89073f), 30, "colshape-A-41", car_brake: true, radius: 5f);
                    mark.Model = 4;
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-A-41")
                {
                    DestroyPoint();
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //RAGE.Chat.Output();
                        SendNotification("Экзамен не сдан! Вы повредили мотоцикл!");

                        Events.CallLocal("Stop_Exam", "~o~");
                    }
                    else
                    {
                        //RAGE.Chat.Output();
                        SendNotification("Поздравляем! Вы сдали экзамен на право управленя ТС категории 'A'!", "~g~");

                        Events.CallLocal("Stop_Exam");
                    }
                    return;
                }
            }

            if (exam_b)
            {
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-1")
                {
                    Exam_B(new Vector3(1138.622f, 63.34218f, 79.75611f), 20, "colshape-B-2", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-2")
                {
                    Exam_B(new Vector3(1147.373f, 68.25878f, 79.75611f), 20, "colshape-B-3", car_brake: true, in_chat: " Элемент - парковка задним ходом.");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-3")
                {
                    Exam_B(new Vector3(1147.633f, 58.18312f, 79.75611f), 20, "colshape-B-4", car_forvard: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-4")
                {
                    Exam_B(new Vector3(1147.633f, 58.18312f, 79.75611f), 20, "colshape-B-5", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-5")
                {
                    Exam_B(new Vector3(1137.299f, 75.51768f, 79.75611f), 20, "colshape-B-6", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-6")
                {
                    Exam_B(new Vector3(1131.8f, 86.38958f, 79.75611f), 20, "colshape-B-7", car_brake: true, in_chat: " Элемент - параллельная парковка.");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-7")
                {
                    Exam_B(new Vector3(1139.421f, 80.04048f, 79.75611f), 20, "colshape-B-8", car_forvard: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-B-8")
                {
                    DestroyPoint();
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //RAGE.Chat.Output();
                        SendNotification("Экзамен не сдан! Вы повредили мотоцикл!", "~o~");

                        Events.CallLocal("Stop_Exam");
                    }
                    else
                    {
                        //RAGE.Chat.Output();
                        SendNotification("Поздравляем! Вы сдали экзамен на право управленя ТС категории 'B'!", "~g~");
                        Events.CallLocal("Stop_Exam");
                    }
                    return;
                }



            }

            if (exam_c)
            {
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-1")
                {
                    Exam_C(new Vector3(1114.469f, -56.06f, 80.86434f), 15, "colshape-C-2", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-2")
                {
                    Exam_C(new Vector3(1121.003f, -27.23303f, 81.19032f), 25, "colshape-C-3", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-3")
                {
                    Exam_C(new Vector3(1142.027f, 2.151298f, 81.08032f), 35, "colshape-C-4", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-4")
                {
                    Exam_C(new Vector3(1154.316f, 13.468f, 80.88954f), 15, "colshape-C-5", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-5")
                {
                    Exam_C(new Vector3(1164.19f, 28.81169f, 80.88395f), 15, "colshape-C-6", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-6")
                {
                    Exam_C(new Vector3(1182.719f, 72.09799f, 81.1911f), 20, "colshape-C-7", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-7")
                {
                    Exam_C(new Vector3(1197.318f, 64.17087f, 81.14597f), 35, "colshape-C-8", car_brake: false, car_forvard: true, colshape: new Vector3(1194.724f, 59.91496f, 81.14926f));
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-8")
                {
                    Exam_C(new Vector3(1164.19f, 28.81169f, 80.88395f), 20, "colshape-C-9", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-9")
                {
                    Exam_C(new Vector3(1154.316f, 13.468f, 80.88954f), 15, "colshape-C-10", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-10")
                {
                    Exam_C(new Vector3(1142.027f, 2.151298f, 81.08032f), 25, "colshape-C-11", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-11")
                {
                    Exam_C(new Vector3(1121.003f, -27.23303f, 81.19032f), 35, "colshape-C-12", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-12")
                {
                    Exam_C(new Vector3(1114.469f, -56.06f, 80.86434f), 35, "colshape-C-13", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-13")
                {
                    Exam_C(new Vector3(1107.462f, -65.14445f, 80.84157f), 20, "colshape-C-14", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-14")
                {
                    Exam_C(new Vector3(1082.51f, -105.7036f, 80.99016f), 20, "colshape-C-15", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-15")
                {
                    Exam_C(new Vector3(1058.692f, -139.7284f, 73.18932f), 15, "colshape-C-16", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-16")
                {
                    Exam_C(new Vector3(1033.544f, -125.8349f, 73.18932f), 15, "colshape-C-17", car_brake: true);
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-17")
                {
                    Exam_C(new Vector3(1032.879f, -144.6567f, 73.18932f), 35, "colshape-C-18", car_brake: false, car_forvard: true, colshape: new Vector3(1037.758f, -139.4019f, 73.18941f));
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-C-18")
                {
                    DestroyPoint();
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //RAGE.Chat.Output("Экзамен не сдан! Вы повредили грузовик!");
                        SendNotification("Экзамен не сдан! Вы повредили грузовик!", "~o~");

                        Events.CallLocal("Stop_Exam");
                    }
                    else
                    {
                        //RAGE.Chat.Output();
                        SendNotification("Поздравляем! Вы сдали экзамен на право управленя ТС категории 'С'!", "~g~");
                        Events.CallLocal("Stop_Exam");
                    }
                    return;
                }
                return;

                //else RAGE.Chat.Output("Поздравляем! Вы сдали экзамен на право упавления транспортыми средствами категории С!");




            }

            if (exam_d)
            {
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-1")
                {
                    DestroyPoint();

                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //Chat.Output("Экзамен не сдан! Вы повредили грузовик!");
                        SendNotification("Экзамен не сдан! Вы повредили грузовик!", "~o~");

                        Events.CallLocal("Stop_Exam");
                    }
                    else
                    {
                        DrawInfo.CarBrake = false;
                        DrawInfo.CarForvard = true;
                        mark = new RAGE.Elements.Marker(1, new Vector3(1224.997f, 325.1884f, 80.99089f), 15, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                        cols = new RAGE.Elements.SphereColshape(mark.Position, 21f, RAGE.Elements.Player.LocalPlayer.Dimension);
                        cols.SetData<string>("AutoSchool", "attach_trailer_1");

                        TimerOn(40);
                        text = new RAGE.Elements.TextLabel(new Vector3(mark.Position.X, mark.Position.Y, mark.Position.Z + 6.5f), 40.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                        //Chat.Output("Подцепите прицеп.");
                        SendNotification("Подцепите прицеп.");
                    }
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "attach_trailer_1")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", "first_trailer");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-3")
                {
                    Exam_D(new Vector3(1158.596f, 258.912f, 80.85136f), 30, "colshape-D-4");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-4")
                {
                    Exam_D(new Vector3(1152.754f, 236.8226f, 81.12369f), 30, "colshape-D-5");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-5")
                {
                    Exam_D(new Vector3(1126.896f, 198.4175f, 81.02847f), 30, "colshape-D-6");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-6")
                {
                    Exam_D(new Vector3(1103.925f, 171.7585f, 80.84709f), 30, "colshape-D-7");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-7")
                {
                    Exam_D(new Vector3(1081.729f, 136.6476f, 80.84183f), 30, "colshape-D-8");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-8")
                {
                    Exam_D(new Vector3(1040.235f, 83.196415f, 81.191f), 30, "colshape-D-9");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-9")
                {
                    Exam_D(new Vector3(999.5571f, -14.15198f, 81.17246f), 30, "colshape-D-10");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-10")
                {
                    Exam_D(new Vector3(1033.554f, -88.49009f, 80.89136f), 30, "colshape-D-11");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-11")
                {
                    DestroyPoint();

                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //Chat.Output();
                        SendNotification("Экзамен не сдан! Вы повредили грузовик!", "~o~");
                        Events.CallLocal("Stop_Exam");
                    }
                    else
                    {
                        DrawInfo.CarBrake = false;
                        DrawInfo.CarForvard = false;
                        mark = new RAGE.Elements.Marker(1, new Vector3(1106.485f, -58.48954f, 80.96043f), 7, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                        cols = new RAGE.Elements.SphereColshape(mark.Position, 21f, RAGE.Elements.Player.LocalPlayer.Dimension);
                        cols.SetData<string>("AutoSchool", "uncouple_trailer_1");

                        TimerOn(60);
                        text = new RAGE.Elements.TextLabel(new Vector3(mark.Position.X, mark.Position.Y, mark.Position.Z + 6.5f), 60.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                        //Chat.Output("Припаркуйте и отцепите прицеп в указанной точке.");
                        SendNotification("Припаркуйте и отцепите прицеп в указанной точке.");
                    }
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "uncouple_trailer_1")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("wants_to_unhook_a_trailer", "first_trailer");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-13")
                {
                    Exam_D(new Vector3(1063.449f, -143.2029f, 73.18932f), 30, "colshape-D-14");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-14")
                {
                    DestroyPoint();

                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //Chat.Output("Экзамен не сдан! Вы повредили грузовик!");
                        SendNotification("Экзамен не сдан! Вы повредили грузовик!", "~o~");
                        Events.CallLocal("Stop_Exam");
                    }
                    else
                    {
                        DrawInfo.CarBrake = false;
                        DrawInfo.CarForvard = false;
                        mark = new RAGE.Elements.Marker(1, new Vector3(1031.456f, -123.9029f, 73.18932f), 15, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                        cols = new RAGE.Elements.SphereColshape(mark.Position, 21f, RAGE.Elements.Player.LocalPlayer.Dimension);
                        cols.SetData<string>("AutoSchool", "attach_trailer_2");

                        TimerOn(40);
                        text = new RAGE.Elements.TextLabel(new Vector3(mark.Position.X, mark.Position.Y, mark.Position.Z + 6.5f), 40.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                        //Chat.Output("Подцепите прицеп второй.");
                        SendNotification("Подцепите второй прицеп.");
                    }
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "attach_trailer_2")
                {
                    RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", "second_trailer");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-15")
                {
                    Exam_D(new Vector3(1089.27f, -109.5756f, 80.99023f), 30, "colshape-D-16");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-16")
                {
                    Exam_D(new Vector3(1033.554f, -88.49009f, 80.89136f), 30, "colshape-D-17");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-17")
                {
                    Exam_D(new Vector3(999.5571f, -14.15198f, 81.17246f), 30, "colshape-D-18");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-18")
                {
                    Exam_D(new Vector3(1040.235f, 83.196415f, 81.191f), 30, "colshape-D-19");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-19")
                {
                    Exam_D(new Vector3(1081.729f, 136.6476f, 80.84183f), 30, "colshape-D-20");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-20")
                {
                    Exam_D(new Vector3(1103.925f, 171.7585f, 80.84709f), 30, "colshape-D-21");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-21")
                {
                    Exam_D(new Vector3(1126.896f, 198.4175f, 81.02847f), 30, "colshape-D-22");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-22")
                {
                    Exam_D(new Vector3(1152.754f, 236.8226f, 81.12369f), 30, "colshape-D-23");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-23")
                {
                    Exam_D(new Vector3(1183.885f, 309.3438f, 80.99086f), 30, "colshape-D-24");
                    return;
                }
                if (colshape.GetData<string>("AutoSchool") == "colshape-D-24")
                {
                    DestroyPoint();
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                    {
                        //RAGE.Chat.Output("Экзамен не сдан! Вы повредили грузовик!");
                        SendNotification("Экзамен не сдан! Вы повредили грузовик!");

                        Events.CallLocal("Stop_Exam");
                    }
                    else
                    {
                        //RAGE.Chat.Output("Поздравляем! Вы сдали экзамен на право управленя ТС категории 'D'!");
                        SendNotification("Поздравляем! Вы сдали экзамен на право управленя ТС категории 'D'!", "~g~");
                        Events.CallLocal("Stop_Exam");
                    }
                    return;
                }
            }




        }





        public void Exam_A(Vector3 marker_a, uint time_sec, string cols_data, bool car_forvard = false, bool car_brake = false, float radius = 1, string in_chat = "Отлично! Направляйтесь к следующей точке!")
        {
            if (RAGE.Elements.Player.LocalPlayer.Vehicle != null)
            {
                DestroyPoint();
                if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                {
                    //Chat.Output("Экзамен не сдан! Вы повредили мотоцикл!");
                    SendNotification("Экзамен не сдан! Вы повредили мотоцикл!", "~o~");
                    Events.CallLocal("Stop_Exam");
                }
                else
                {
                    DrawInfo.CarBrake = car_brake;
                    DrawInfo.CarForvard = car_forvard;

                    //RAGE.Chat.Output(in_chat);
                    SendNotification(in_chat);

                    mark = new RAGE.Elements.Marker(1, marker_a, radius, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                    cols = new RAGE.Elements.SphereColshape(marker_a, radius * 1.25f, RAGE.Elements.Player.LocalPlayer.Dimension);
                    cols.SetData<string>("AutoSchool", cols_data);
                    text = new RAGE.Elements.TextLabel(new Vector3(marker_a.X, marker_a.Y, marker_a.Z + 1f), time_sec.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                    TimerOn(time_sec);
                }


            }

        }





        public void Exam_B(Vector3 marker, uint time_sec, string cols_data, bool car_forvard = false, bool car_brake = false, float radius = 2f, Vector3 colshape = null, string in_chat = "Отлично! Направляйтесь к следующей точке!")
        {
            if (RAGE.Elements.Player.LocalPlayer.Vehicle != null)
            {
                DestroyPoint();
                if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                {
                    //Chat.Output("Экзамен не сдан! Вы повредили автомобиль!");
                    SendNotification("Экзамен не сдан! Вы повредили автомобиль!", "~o~");
                    Events.CallLocal("Stop_Exam");
                }
                else
                {
                    DrawInfo.CarBrake = car_brake;
                    DrawInfo.CarForvard = car_forvard;

                    //RAGE.Chat.Output(in_chat);
                    SendNotification(in_chat);

                    mark = new RAGE.Elements.Marker(1, marker, radius, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                    if (colshape == null) cols = new RAGE.Elements.SphereColshape(marker, radius * 1.3f, RAGE.Elements.Player.LocalPlayer.Dimension);
                    else cols = new RAGE.Elements.SphereColshape(colshape, radius * 1.3f, RAGE.Elements.Player.LocalPlayer.Dimension);

                    TimerOn(time_sec);

                    text = new RAGE.Elements.TextLabel(new Vector3(marker.X, marker.Y, marker.Z + 1f), time_sec.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                    cols.SetData<string>("AutoSchool", cols_data);
                }
            }
        }


        public void Exam_C(Vector3 marker, uint time_sec, string cols_data, bool car_forvard = false, bool car_brake = false, float radius = 2.5f, Vector3 colshape = null, string in_chat = "Отлично! Направляйтесь к следующей точке!")
        {
            if (RAGE.Elements.Player.LocalPlayer.Vehicle != null)
            {
                DestroyPoint();
                if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
                {
                    //Chat.Output("Экзамен не сдан! Вы повредили грузовик!");
                    SendNotification("Экзамен не сдан! Вы повредили грузовик!", "~o~");
                    Events.CallLocal("Stop_Exam");
                }
                else
                {
                    DrawInfo.CarBrake = car_brake;
                    DrawInfo.CarForvard = car_forvard;

                    //RAGE.Chat.Output(in_chat);
                    SendNotification(in_chat);

                    mark = new RAGE.Elements.Marker(1, marker, radius, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                    if (colshape == null) cols = new RAGE.Elements.SphereColshape(marker, radius * 1.3f, RAGE.Elements.Player.LocalPlayer.Dimension);
                    else cols = new RAGE.Elements.SphereColshape(colshape, radius * 1.3f, RAGE.Elements.Player.LocalPlayer.Dimension);

                    TimerOn(time_sec);

                    text = new RAGE.Elements.TextLabel(new Vector3(marker.X, marker.Y, marker.Z + 1f), time_sec.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                    cols.SetData<string>("AutoSchool", cols_data);
                }


            }

        }

        public void Exam_D(Vector3 marker, uint time_sec, string cols_data, bool car_forvard = false, bool car_brake = false, float radius = 3.5f, Vector3 colshape = null, string in_chat = "Отлично! Направляйтесь к следующей точке!")
        {
            //if (RAGE.Elements.Player.LocalPlayer.Vehicle != null )
            //{

            DestroyPoint();

            if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetHealth() < 1000)
            {

                //Chat.Output("Экзамен не сдан! Вы повредили грузовик!");
                SendNotification("Экзамен не сдан! Вы повредили грузовик!", "~o~");

                Events.CallLocal("Stop_Exam");
            }
            else
            {
                DrawInfo.CarBrake = car_brake;
                DrawInfo.CarForvard = car_forvard;

                //RAGE.Chat.Output(in_chat);
                SendNotification(in_chat);


                mark = new RAGE.Elements.Marker(1, marker, radius, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new RGBA(255, 200, 0, 80), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);
                if (colshape == null) cols = new RAGE.Elements.SphereColshape(marker, radius * 1.3f, RAGE.Elements.Player.LocalPlayer.Dimension);
                else cols = new RAGE.Elements.SphereColshape(colshape, radius * 1.3f, RAGE.Elements.Player.LocalPlayer.Dimension);

                TimerOn(time_sec);


                text = new RAGE.Elements.TextLabel(new Vector3(marker.X, marker.Y, marker.Z + 1f), time_sec.ToString(), new RGBA(240, 211, 0), dimension: RAGE.Elements.Player.LocalPlayer.Dimension);

                cols.SetData<string>("AutoSchool", null);
                cols.SetData<string>("AutoSchool", cols_data);


            }
            //if (RAGE.Elements.Player.LocalPlayer. < 1000) ;

            //}

        }


        //RAGE.Elements.Player.LocalPlayer.Vehicle.SetData<string>("want_trailer", "first_trailer");

        public void ContinueExam_D(object[] args)
        {
            if (exam_d)
            {
                if ((bool)args[0])
                {
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetData<string>("want_trailer") == "first_trailer")
                    {
                        Exam_D(new Vector3(1183.885f, 309.3438f, 80.99086f), 30, "colshape-D-3");
                        return;
                    }
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetData<string>("want_trailer") == "second_trailer")
                    {
                        Exam_D(new Vector3(1063.449f, -143.2029f, 73.18932f), 30, "colshape-D-15");
                        //Exam_D(new Vector3(1058.692f, -139.7284f, 73.18932f), 30, "colshape-D-15");
                        return;
                    }
                    //Chat.Output("Экзамен не сдан! Вы подцепили не тот прицеп!");
                    SendNotification("Экзамен не сдан! Вы подцепили не тот прицеп!", "~o~");
                    Events.CallLocal("Stop_Exam");

                }

                else
                {
                    if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetData<string>("wants_to_unhook_a_trailer") == "first_trailer")
                    {
                        Exam_D(new Vector3(1082.51f, -105.7036f, 80.99016f), 20, "colshape-D-13");
                        return;
                    }
                    //if (RAGE.Elements.Player.LocalPlayer.Vehicle.GetData<string>("want_trailer") == "first_trailer")
                    //{


                    //    Exam_D(new Vector3(1183.885f, 309.3438f, 80.99086f), 30, "colshape-D-3");
                    //return;
                    //}

                    //Chat.Output("Экзамен не сдан! Вы потеряли прицеп!");
                    SendNotification("Экзамен не сдан! Вы потеряли прицеп!", "~o~");
                    Events.CallLocal("Stop_Exam");

                    return;

                }
            }
        }
    }
}
