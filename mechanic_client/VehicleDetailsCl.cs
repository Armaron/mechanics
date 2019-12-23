

namespace mechanic_client
{
   public  class VehicleDetailsCl
    {
        public string carNumber { get; set; }

        public string sellDate { get; set; }
        public string carType { get; set; }
        public int carScore { get; set; }
        public string dateOf { get; set; }
        public string doneWorks { get; set; }

        public VehicleDetailsCl()
        {

        }


        public VehicleDetailsCl(string carNumber, string sellDate, string carType, int carScore, string dateOf, string doneWorks)
        {
            this.carNumber = carNumber;
            this.carType = carType;
            this.sellDate = sellDate;
            this.carScore = carScore;
            this.dateOf = dateOf;
            this.doneWorks = doneWorks;
        }

    }
}
