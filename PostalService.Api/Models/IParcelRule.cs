namespace PostalService.Api.Models
{
    public interface IParcelRule
    {
        int Priority { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Rate { get; set; }
        int WeightLimit { get; set; }
        int VolumeLimit { get; set; }
    }
}
