namespace PostalService.Api.Models
{
    public class ParcelCost
    {
        public ParcelCost(decimal cost, string parcelName)
        {
            CostOfDelivery = cost;
            ParcelName = parcelName;
        }
        public decimal CostOfDelivery { get; private set; }
        public string ParcelName { get; private set; }
    }
}
