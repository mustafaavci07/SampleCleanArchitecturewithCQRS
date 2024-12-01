


namespace SampleCleanArchitecture.Application.Journeys.Commands.CreateJourney
{
    public class CreateJourneyValidation:AbstractValidator<CreateJourneyCommand>
    {
        public CreateJourneyValidation()
        {
            RuleFor(r=>r.DepartureFrom).NotNull().NotEmpty();
            RuleFor(r=>r.ArrivalTo).NotNull().NotEmpty();   
            RuleFor(r=>r.PassengerCapacity).GreaterThan(0).LessThan(50);
            RuleFor(r => r.Price).GreaterThan(0);
            RuleFor(r => r.DepartureTimeUTC).LessThan(r => r.ArrivalTimeUTC);
            
        }
    }
}
