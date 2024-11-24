namespace SampleCleanArchitecture.Shared.DTO.Passengers
{
    public record PassengerJourneyDTO(Ulid Id, PassengerDTO Passenger, JourneyDTO Journey, JourneyState IsCanceled)
    {
    }
}
