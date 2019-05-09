using PostalService.Api.Domain;
using System.Linq;

namespace PostalService.Api.Infra
{
    public interface IParcelInventory
    {
        ParcelCollection ParcelCollection { get; }
        ParcelHandler FirstParcelHandler { get; }
    }
    public class ParcelInventory : IParcelInventory
    {
        private readonly ParcelCollection _parcelCollection;
        public ParcelCollection ParcelCollection { get { return _parcelCollection; } }
        public ParcelHandler FirstParcelHandler { get { return _parcelCollection.Parcels[0]; } }

        public ParcelInventory(ParcelCollection parcels)
        {
            _parcelCollection = parcels;
            _parcelCollection.Parcels = _parcelCollection.Parcels.OrderBy(x => x.Priority).ToList();
            SetChain();
        }

        private void SetChain()
        {
            for (int i = 0; i < _parcelCollection.Parcels.Count - 1; i++)
            {
                _parcelCollection.Parcels[i].SetNextParcel(_parcelCollection.Parcels[i + 1]);
            }
        }
    }
}
