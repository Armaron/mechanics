

namespace mech_server
{
   public  class VehicleDetails 
    {
        public string CarNumber { get; set; }

        public string SellDate { get; set; }
        public string CarType { get; set; }
        public int CarScore { get; set; }
        public string DateOf { get; set; }
        public string DoneWorks { get; set; }
        public int TotalHealth { get; set; }
        public int BodyHealth { get; set; }
        public string CurrentMods { get; set; }
        public string Damag { get; set; }

        public VehicleDetails()
        {

        }
        public VehicleDetails(int totalHealth, int bodyHealth)
        {
            TotalHealth = totalHealth;
            BodyHealth = bodyHealth;
        }
        public VehicleDetails( int carScore, string dateOf, string doneWorks)
        {
            this.CarScore = carScore;
            this.DateOf = dateOf;
            this.DoneWorks = doneWorks;
        }

        public VehicleDetails(string carNumber, string sellDate, string carType, int carScore, string dateOf, string doneWorks)
        {
            this.CarNumber = carNumber;
            this.CarType = carType;
            this.SellDate = sellDate;
            this.CarScore = carScore;
            this.DateOf = dateOf;
            this.DoneWorks = doneWorks;
            //TotalHealth = totalHealth;
        }

    }
}
