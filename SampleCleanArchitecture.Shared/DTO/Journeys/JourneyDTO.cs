

namespace SampleCleanArchitecture.Shared.DTO.Journeys
{
    public record JourneyDTO(Ulid Id,
        DateTime DepartureTimeUTC,
        DateTime ArrivalTimeUTC,
        string DepartureFrom,
        string ArrivalTo,
        double Price,
        int PassengerCapacity,
        bool Canceled
        )
    { }
}

