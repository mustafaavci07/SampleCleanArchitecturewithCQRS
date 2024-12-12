

using SampleCleanArchitecture.Application.Journeys.Commands.CreateJourney;
using SampleCleanArchitecture.Application.Journeys.Commands.UpdateJourney;
using SampleCleanArchitecture.Application.PassengerJourneys.Commands.CreatePassengerJourney;
using SampleCleanArchitecture.Application.PassengerJourneys.Commands.UpdatePassengerJourney;
using SampleCleanArchitecture.Application.Passengers.Commands.CreatePassenger;
using SampleCleanArchitecture.Application.Passengers.Commands.UpdatePassenger;
using SampleCleanArchitecture.Application.Payments.Commands.CreatePayment;

namespace SampleCleanArchitecture.Application.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Journey,JourneyDTO>().ReverseMap();
            CreateMap<Passenger,PassengerDTO>().ReverseMap();
            CreateMap<PassengerJourney,PassengerJourneyDTO>().ReverseMap();
            CreateMap<Payment,PaymentDTO>().ReverseMap();
            CreateMap<CreateJourneyCommand, Journey>();
            CreateMap<UpdateJourneyCommand, Journey>();
            CreateMap<CreatePassengerCommand, Passenger>();
            CreateMap<UpdatePassengerCommand, Passenger>();
            CreateMap<CreatePassengerJourneyCommand, PassengerJourney>();
            CreateMap<UpdatePassengerJourneyCommand, PassengerJourney>();
            CreateMap<CreatePaymentCommand,Payment>();
        }
    }
}
