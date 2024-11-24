
using SampleCleanArchitecture.Core.Domain.Journeys;

namespace SampleCleanArchitecture.Core.Domain.Passengers
{
    public class PassengerJourney:AuditableBaseEntity
    {
        public Passenger Passenger { get; set; }
        public Ulid PassengerId { get; set; }
        public Journey Journey { get; set; }
        public Ulid JourneyId { get; set; }
        public JourneyState IsCanceled { get; set; }
    }
}
