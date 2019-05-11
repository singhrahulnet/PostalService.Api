using PostalService.Api.Domain;

namespace PostalService.Api.Infra
{
    public interface IParcelInventory
    {
        ParcelHandler FirstParcelHandler { get; }
    }
    public class ParcelInventory : IParcelInventory
    {
        private ParcelCollection _parcelCollection;

        public ParcelInventory(ParcelCollection parcels)
        {
            _parcelCollection = parcels;
            InitInventory();
        }
        public ParcelHandler FirstParcelHandler { get { return _parcelCollection.Parcels[0]; } }

        private void InitInventory()
        {
            _parcelCollection.SortByPriority();
            _parcelCollection.SetChain();
        }
    }
}
