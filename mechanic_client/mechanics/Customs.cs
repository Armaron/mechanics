using RAGE;

namespace mechanic_client
{
    public class Customs
    {
        public string NameStatic { get; set; }
        public string NameCustoms { get; set; }
       
       public Vector3 BuyCustomsCoords { get; set; }
       public Vector3 RepairCoords { get; set; }
       public Vector3 CustomCoords { get; set; }

        public Customs()
        {

        }
        public Customs(string NameCustoms, Vector3 RepairCoords)
        {
            this.NameCustoms = NameCustoms;
            //this.BuyCustomsCoords = BuyCustomsCoords;
            this.RepairCoords = RepairCoords;
        }

        public Customs(string NameCustoms, Vector3 BuyCustomsCoords, Vector3 RepairCoords)
        {
            this.NameCustoms = NameCustoms;
            this.BuyCustomsCoords = BuyCustomsCoords;
            this.RepairCoords = RepairCoords;
        }
        public Customs(string NameStatic, string NameCustoms, Vector3 BuyCustomsCoords,  Vector3 RepairCoords, Vector3 CustomCoords)
        {
            this.NameStatic = NameStatic;
           
            this.NameCustoms = NameCustoms;
            this.BuyCustomsCoords = BuyCustomsCoords;
            this.RepairCoords = RepairCoords;
            this.CustomCoords = CustomCoords;

        }
    }
}
