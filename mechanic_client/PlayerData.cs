using System;
using System.Collections.Generic;
using System.Text;

namespace Serv_RP.player
{
    public class PlayerData 
    {

        #region основные параметры персонажа

        public const string PLAYER_MAX_WEIGHT_INVENTORY = "PLAYER_MAX_WEIGHT_INVENTORY";

        public const string PLAYER_MONEY = "PLAYER_MONEY";
        public const string PLAYER_MONEY_CARD = "PLAYER_MONEY_CARD";

        public const string PLAYER_ID = "PLAYER_ID";
        public const string PLAYER_HUNGER = "PLAYER_HUNGER";
        public const string PLAYER_DRINK = "PLAYER_DRINK";
        public const string PLAYER_TATTOO = "PLAYER_TATTOO";
		
		 public const string PHONE_BALANCE = "PHONE_BALANCE";
	  
	  
        public const string PHONE_CONTACTS = "PHONE_CONTACTS";
        public const string PHONE_NUMBER = "PHONE_NUMBER";

        public const string FORCE = "FORCE";
        public const string FORCE_LVL = "FORCE_LVL";

        public const string STAMINA = "STAMINA";
        public const string STAMINA_LVL = "STAMINA_LVL";

        public const string LUCKY = "LUCKY";
        public const string LUCKY_LVL = "LUCKY_LVL";

        public const string INSTRUMENT = "INSTRUMENT";
        public const string INSTRUMENT_LVL = "INSTRUMENT_LVL";

        public const string PRICEWEIGHT = "PRICEWEIGHT";
        public const string PRICEWEIGHT_LVL = "PRICEWEIGHT_LVL";


        public const string SPEED = "SPEED";
        public const string SPEED_LVL = "SPEED_LVL";

      //  public const string FRIENDS = "FRIENDS";
        public const string FRIEND_CLASS = "FRIEND_CLASS";
        public const string HOUSE_ID = "HOUSE_ID";
        public const string ROOM_ID = "ROOM_ID";
        public const string HOUSE_STREET = "HOUSE_STREET";

        public const string GarageCount = "GarageCount";
        public const string GaragePosition = "GaragePosition";
        public const string CarInGarage = "CarInGarage";

        public const string Fraction = "Fraction";
        public const string FractionCar = "FractionCar";
        public const string FractionState = "FractionState";


        public const string Arrested = "Arrested";
        public const string JailPos = "JailPos";
        public const string JailTime = "JailTime";

        #endregion


        #region данные пользователя
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public const string Login = "Login";
        /// <summary>
        /// Автологин
        /// </summary>
        public const string AutoLogin = "AutoLogin";
        /// <summary>
        /// Уровень администратора
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// Никнэйм
        /// </summary>
        public const string Nickname = "Nickname";
        /// <summary>
        /// Возраст
        /// </summary>
        public const string Age = "Age";
        /// <summary>
        /// Имя
        /// </summary>
        public const string Name = "Name";
        public const string Family = "Family";

        public const string slotAnim1 = "slotAnim1";
        public const string slotAnim2 = "slotAnim2";
        public const string slotAnim3 = "slotAnim3";
        public const string slotAnim4 = "slotAnim4";
        public const string slotAnim5 = "slotAnim5";
        public const string slotAnim6 = "slotAnim6";
        public const string slotAnim7 = "slotAnim7";
        public const string slotAnim8 = "slotAnim8";

        public const string playerPos = "playerPos";
        /// <summary>
        /// Класс транзакций объектов
        /// </summary>
        public const string TransactionData = "TransactionData";

        #endregion

        #region Внешность персонажа
        public const string mother = "mother";
        public const string father = "father";
        public const string resemblance = "resemblance";
        public const string skinTone = "skinTone";
        public const string gender = "gender";
        public const string eyeColor = "eyeColor";
        public const string hairColors = "hairColors";
        public const string hairItem = "hairItem";
        public const string noseWidth = "noseWidth";
        public const string noseHeight = "noseHeight";
        public const string noseLength = "noseLength";
        public const string noseBridge = "noseBridge";
        public const string noseTip = "noseTip";
        public const string noseBridgeShift = "noseBridgeShift";
        public const string hair = "hair";

        public const string browHeight = "browHeight";
        public const string browWidth = "browWidth";
        public const string cboneHeight = "cboneHeight";
        public const string cboneWidth = "cboneWidth";
        public const string cheekWidth = "cheekWidth";
        public const string eyes = "eyes";
        public const string lips = "lips";
        public const string jawWidth = "jawWidth";
        public const string chinLength = "chinLength";
        public const string chinPos = "chinPos";
        public const string chinWidth = "chinWidth";
        public const string chinShape = "chinShape";
        public const string neckWidth = "neckWidth";
        public const string blemishes = "blemishes";
        public const string blemishesOpacity = "blemishesOpacity";
        public const string facialHair = "facialHair";
        public const string facialHairOpacity = "facialHairOpacity";
        public const string eyebrows = "eyebrows";
        public const string eyebrowsOpacity = "eyebrowsOpacity";

        public const string ageing = "ageing";
        public const string ageingOpacity = "ageingOpacity";
        public const string makeup = "makeup";
        public const string makeupOpacity = "makeupOpacity";
        public const string blush = "blush";
        public const string blushOpacity = "blushOpacity";
        public const string complexion = "complexion";
        public const string complexionOpacity = "complexionOpacity";

        public const string sundamage = "sundamage";
        public const string sundamageOpacity = "sundamageOpacity";
        public const string lipstick = "lipstick";
        public const string lipstickOpacity = "lipstickOpacity";
        public const string freckles = "freckles";
        public const string frecklesOpacity = "frecklesOpacity";
        public const string chestHair = "chestHair";
        public const string chestHairOpacity = "chestHairOpacity";
        public const string eyebrowColor = "eyebrowColor";
        public const string beardColor = "beardColor";
        public const string chestHairColor = "chestHairColor";
        public const string blushColor = "blushColor";
        public const string lipstickColor = "lipstickColor";

        #endregion






        #region Документы

        public const string PLAYER_CAR_CARD = "PLAYER_CAR_CARD";
        public const string PLAYER_WEAPON_LICENSE = "PLAYER_WEAPON_LICENSE";
        #endregion





        #region Хранимые динамические ссылки
        public const string PLAYER_ON_SHAPE = "PLAYER_ON_SHAPE";
        #endregion


        #region Временные флаги
        public const string PLAYER_INVENTORY = "PLAYER_INVENTORY";
        public const string PLAYER_IN_VEHICLE = "PLAYER_IN_VEHICLE";
        public const string AnimSet = "AnimSet";
        public const string Death = "Death";
        public const string Death_Place = "Death_Place";
        public const string Enter_Place = "Enter_Place";
        public const string Radio_Station = "Radio_Station";
        public const string Want_Passport = "Want_Passport";
        public const string Want_Meet = "Want_Meet";
        #endregion

        #region Временные данные
        /// <summary>
        /// Принят вызов
        /// </summary>
        public const string SetCall = "SetCall";
        public const string GetCall = "GetCall";
        public const string GetCallID = "GetCallID";
        public const string GetCallClient = "GetCallClient";

        public const string PLAYER_EVENT_ON_SHAPE = "PLAYER_EVENT_ON_SHAPE";
        
		public const string PLAYER_COMMAND_ON_SHAPE = "PLAYER_COMMAND_ON_SHAPE";
        
		public const string password = "password";
        
		public const string email = "email";
        
		public const string TempDecoration = "TempDecoration";
        
		public const string ClothesOnPlayer = "ClothesOnPlayer";
        
		public const string ReadyToVoice = "ReadyToVoice";
        
		public const string RadioMuted = "RadioMuted";
        public const string PlayerAtHouse = "PlayerAtHouse";
        public const string HouseDimension = "HouseDimension";
        public const string HouseNum = "HouseNum";
        public const string RoomNum = "RoomNum";
        public const string SetGarage = "SetGarage";
        public const string HousePosition = "HousePosition";
        public const string HouseNumberForGarage = "HouseNumberForGarage";

        ///Под действием наркотиков
        public const string UseDrug = "UseDrug";
		public const string DrugType = "DrugType";
        public const string Vehicle_Class = "Vehicle_Class";
 public const string HasRadio = "HasRadio";
        public const string OnTheRadio = "OnTheRadio";
        public const string BuyCar = "BuyCar";
        public const string BuyCarNumber = "BuyCarNumber";
        public const string CarList = "CarList";
        public const string BuyType = "BuyType";



        public const string CuffTime = "CuffTime";
     

        #endregion
        //new
        #region Данные для телефона
        public const string PLAYER_PHONE_WALLPAPER = "PLAYER_PHONE_WALLPAPER";
        internal static string VehicleInAutoschool = "VehicleInAutoschool";
        internal static string TrailerInAutoschool = "TrailerInAutoschool";
        internal static string TrailerInAutoschool_3 = "TrailerInAutoschool_3";

        public static string TrailerInAutoschool_2 = "TrailerInAutoschool_2";



        #endregion
    }
}
