namespace PostalService.Api.Models
{
    public class ParcelResult
    {
        public ParcelResult(decimal cost, string parcelName)
        {
            CostOfDelivery = cost;
            ParcelName = parcelName;
        }
        public decimal CostOfDelivery { get; private set; }
        public string ParcelName { get; private set; }
    }
}
