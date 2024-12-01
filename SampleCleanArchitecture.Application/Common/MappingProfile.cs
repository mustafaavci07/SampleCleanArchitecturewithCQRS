

namespace SampleCleanArchitecture.Application.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Journey,JourneyDTO>();
            CreateMap<Passenger,PassengerDTO>();
            CreateMap<PassengerJourney,PassengerJourneyDTO>();
            CreateMap<Payment,PaymentDTO>();
        }
    }
}
