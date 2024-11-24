

namespace SampleCleanArchitecture.Core.Domain.Journeys
{
    public class Journey :AuditableBaseEntity
    {
        public DateTime DepartureTimeUTC { get; set; }
        public DateTime ArrivalTimeUTC { get; set; }
        public string DepartureFrom { get; set; }
        public string ArrivalTo { get; set; }
        public double Price { get; set; }
        public int PassengerCapacity { get; set; }
        public bool Canceled { get; set; } = false;
    }
}
